using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Required Fields")]
    public float moveSpeed = 2.5f;
    public bool hidden;

    // Input
    float horizontalInput;
    float verticalInput;
    Vector2 inputVector = Vector2.zero;
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
        // Get Current Input
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        inputVector = new Vector2(horizontalInput, verticalInput);
        inputVector = inputVector.normalized;

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
                audio.Play();
            }
            else if (carriedItem)
            {
                if (atBurrow)
                {
                    CallbackHandler.instance.StoreTrash(carriedItem);
                    audio.Play();
                }
                else
                {
                    carriedItem.DropMe();
                    audio.Play();
                }
                carriedItem = null;
            }
        }
    }

    // Probably Over-Engineered but Feels Better.
    public Rubbish GetClosest()
    {
        float dist = Mathf.Infinity;
        Rubbish temp = null;

        foreach (Rubbish n in rubbishList)
        {
            if (Vector2.Distance(n.transform.position, transform.position + xOffset) < dist)
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
