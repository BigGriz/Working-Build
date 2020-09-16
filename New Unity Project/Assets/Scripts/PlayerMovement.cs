using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Required Fields")]
    public float moveSpeed = 2.5f;
    public bool hidden;
    public float sprintTimer = 0.0f;
    public float sprintSpeed = 4.0f;
    public float normalSpeed = 2.0f;
    public bool sprinting;
    public float sprintCooldown = 3.0f;

    // Input
    float horizontalInput;
    float verticalInput;
    [HideInInspector] public Vector2 inputVector = Vector2.zero;
    public Vector3 xOffset = new Vector3(0.32f, 0.0f, 0.0f);
    // Rubbish
    public List<Rubbish> rubbishList;
    public Rubbish rubbish;
    public bool inRange;
    public bool atBurrow;
    [HideInInspector] public Rubbish carriedItem;

    #region Setup
    private Rigidbody2D rb;
    private Animator animator;
    private AudioSource audio;
    private void Awake()
    {
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

        // Get Current Input
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        inputVector = new Vector2(horizontalInput, verticalInput);
        inputVector = inputVector.normalized;

        if (inputVector != Vector2.zero)
        {
            GetComponent<SpriteRenderer>().flipX = (inputVector.x < 0);
        }

        // Set Idle or Moving
        if (inputVector == Vector2.zero)
        {
            ChangeAnimState(false);
        }
        else
        {
            ChangeAnimState(true);
        }

        // Check for Input
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!carriedItem && rubbishList.Count > 0)
            {
                carriedItem = GetClosest();
                carriedItem.CarryMe(this.transform);
                audio.Play();
            }
            else if (!carriedItem && inRange)
            {
                CallbackHandler.instance.TakeTrash();
                if (carriedItem)
                {
                    audio.Play();
                }
            }
            else if (carriedItem)
            {
                if (atBurrow)
                {
                    CallbackHandler.instance.StoreTrash(carriedItem);
                    audio.Play();
                    AudioController.instance.FadeToBGM();
                }
                else
                {
                    carriedItem.DropMe();
                    audio.Play();
                    AudioController.instance.FadeToBGM();
                }
                carriedItem = null;
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
            sprinting = false;
        }

        if (sprintTimer <= 0)
        {
            sprintCooldown = 2.0f;
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
            sprintTimer -= Time.deltaTime;
        }
        else
        {
            moveSpeed = normalSpeed;
            sprintCooldown -= Time.deltaTime;
        }
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

    // Check for Overlap
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Rubbish"))
        {
            rubbishList.Add(collision.GetComponent<Rubbish>());
        }
    }

    // Check for Exit
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Rubbish"))
        {
            rubbishList.Remove(collision.GetComponent<Rubbish>());
        }
    }

    public void ChangeAnimState(bool _moving)
    {
        animator.SetBool("Moving", _moving);
    }
}
