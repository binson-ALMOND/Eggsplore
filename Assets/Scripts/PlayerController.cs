using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Flight Settings")] 
    [SerializeField] private float flySpeed = 5f;

    [SerializeField] private float maxSpeed = 8f;
    [SerializeField] private float linearDrag = 2f;
    
    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 moveInput;
    private bool isFacingRight = false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        rb.linearDamping = linearDrag;
    }

    
    void Update()
    {
        Vector2 input = Vector2.zero;

        if (Keyboard.current != null)
        {
            if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed) input.y = 1f;
            if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed) input.y = -1f;
            if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) input.x = -1f;
            if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) input.x = 1f;
        }
        
        moveInput = input.normalized;

        bool moving = input.magnitude > 0;
        animator.SetBool("isMoving", moving);
        
        if (input.x < 0 && !isFacingRight)
        {
            Flip();
        }
        else if (input.x > 0 && isFacingRight)
        {
            Flip();
        }
        
    }
    

    void FixedUpdate()
    {
        if (moveInput.magnitude > 0)
        {
            rb.AddForce(moveInput*flySpeed, ForceMode2D.Force);
            
        }

        if (rb.linearVelocity.magnitude > maxSpeed)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    
}
