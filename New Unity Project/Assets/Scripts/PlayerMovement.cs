using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Required Fields")]
    public float sprintSpeed = 4.0f;
    public float normalSpeed = 2.0f;

    // Movement
    float moveSpeed = 2.5f;
    bool sprinting;
    [HideInInspector] public float sprintCooldown = 3.0f;
    [HideInInspector] public float sprintTimer = 0.0f;
    [HideInInspector] public bool hidden;

    // Input
    float horizontalInput;
    float verticalInput;
    [HideInInspector] public Vector2 inputVector = Vector2.zero;
    [HideInInspector] public Vector3 xOffset = new Vector3(0.32f, 0.0f, 0.0f);

    // Rubbish
    [HideInInspector] public List<Rubbish> rubbishList;
    [HideInInspector] public Rubbish rubbish;
    [HideInInspector] public bool inRange;
    [HideInInspector] public bool atBurrow;
    [HideInInspector] public Rubbish carriedItem;

    // Prevents Spacebar Spam
    [HideInInspector] public float haxBlocker;

    bool lastUpDir;
    public bool lastRightDir;

    #region Setup
    public static PlayerMovement instance;

    private Rigidbody2D rb;
    private Animator animator;
    private AudioSource audio;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Multiple Characters! ABORT ABORT!");
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
    }
    #endregion Setup

    // Get Input
    private void Update()
    {
        if (CallbackHandler.instance.globalInfo.gamePaused)
            return;

        haxBlocker -= Time.deltaTime;

        // Get Current Input
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        inputVector = new Vector2(horizontalInput, verticalInput);
        inputVector = inputVector.normalized;

        if (horizontalInput != 0)
        {
            GetComponent<SpriteRenderer>().flipX = (inputVector.x < 0);
            lastRightDir = (inputVector.x < 0);
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = lastRightDir;
        }

        // Idle
        if (inputVector == Vector2.zero)
        {
            ChangeAnimState(false, lastUpDir);
        }
        // Moving
        else
        {
            // Moving Down
            if (verticalInput == -1)
            {
                ChangeAnimState(true, true);
                lastUpDir = true;
            }
            // Moving Up
            else if (verticalInput == 1)
            {
                ChangeAnimState(true, false);
                lastUpDir = false;
            }
        }

        // Check for Input
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Space))
        {
            if (!carriedItem && rubbishList.Count > 0)
            {
                carriedItem = GetClosest();
                if (carriedItem.carried)
                {
                    AIMovement ai = carriedItem.character.GetComponent<AIMovement>();

                    ai.carriedItem = null;
                    carriedItem.DropMe();
                    carriedItem.CarryMe(this.transform);

                    AudioController.instance.PlaySFX(3);

                    ai.allowMovement = false;
                    ai.paused = true;
                    ai.pauseTimer = 0.5f;
                }
                else
                {
                    carriedItem.CarryMe(this.transform);
                }
                audio.Play();
                haxBlocker = 1.0f;
            }
            else if (!carriedItem && inRange)
            {
                CallbackHandler.instance.TakeTrash();
                if (carriedItem)
                {
                    audio.Play();
                }
                haxBlocker = 1.0f;
            }
            else if (carriedItem)
            {
                if (atBurrow)
                {
                    CallbackHandler.instance.StoreTrash(carriedItem);
                    AudioController.instance.PlaySFX(2);
                    audio.Play();
                    AudioController.instance.FadeToBGM();
                    carriedItem = null;
                }
                else
                {
                    if (haxBlocker <= 0.0f)
                    {
                        carriedItem.DropMe();
                        audio.Play();
                        AudioController.instance.FadeToBGM();
                        carriedItem = null;
                    }
                }            
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (sprintCooldown <= 0 && sprintTimer > 0)
            {
                sprinting = true;
            }
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            if (sprintCooldown <= 0.0f)
            {
                sprintCooldown = 1.0f;
            }
            sprinting = false;
        }

        if (sprintTimer <= 0)
        {
            sprintCooldown = 1.0f;
            sprinting = false;
        }

        if (!sprinting && sprintTimer < 1.0f)
        {
            moveSpeed = normalSpeed;
            sprintTimer += Time.deltaTime;
            sprintCooldown -= Time.deltaTime;
        }
        else if (sprinting)
        {
            moveSpeed = sprintSpeed;
            sprintTimer -= Time.deltaTime * 2;
        }
        else
        {
            moveSpeed = normalSpeed;
            sprintCooldown -= Time.deltaTime;
        }
    }

    public void ChangeAnimState(bool _moving, bool _down)
    {
        animator.SetBool("Moving", _moving);
        animator.SetBool("Down", _down);
    }

    // Probably Over-Engineered but Feels Better.
    public Rubbish GetClosest()
    {
        float dist = Mathf.Infinity;
        Rubbish temp = null;

        foreach (Rubbish n in rubbishList)
        {
            if (Vector2.Distance(n.transform.position, transform.position + Mathf.Sign(inputVector.x) * xOffset) < dist)
            {
                temp = n;
                dist = Vector2.Distance(n.transform.position, transform.position);
            }
        }

        return (temp);
    }

    // Move Character
    private void FixedUpdate()
    {
        if (CallbackHandler.instance.globalInfo.gamePaused)
            return;

        Vector2 currentPos = rb.position;
        Vector2 newPos = currentPos + inputVector * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(newPos);
        inputVector = Vector2.zero;
    }

    #region Triggers
    // Check for Overlap
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Rubbish rubbish = collision.GetComponent<Rubbish>();

        if (rubbish && !rubbish.inBin)
        {
            rubbishList.Add(collision.GetComponent<Rubbish>());
        }
    }
    // Check for Exit
    private void OnTriggerExit2D(Collider2D collision)
    {
        Rubbish rubbish = collision.GetComponent<Rubbish>();

        if (rubbish)
        {
            if (rubbishList.Contains(rubbish))
            {
                rubbishList.Remove(collision.GetComponent<Rubbish>());
            }
        }
    }
    #endregion Triggers
}
