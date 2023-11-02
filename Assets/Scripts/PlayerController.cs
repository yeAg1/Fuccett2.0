using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirection))]

public class PlayerController : MonoBehaviour
{
    public float walkspeed = 5f;
    public float jumpImpluse = 10f;
    public float attackDuration = 0.5f;
    public float rollSpeed = 10f;
    public float rollDuration = 0.5f;
    public Transform attackPoint;
    public LayerMask attackLayers;
    Vector2 moveInput;
    TouchingDirection td;
    private Damageable damageable;
    Rigidbody2D rb;
    Animator animator;

    public bool IsHit { get 
        { 
            return animator.GetBool(AnimationStrings.isHit); 
        } 
        set 
        { 
            animator.SetBool(AnimationStrings.isHit, value); 
        } 
    }


    public bool CanMove { get 
        { 
            return animator.GetBool(AnimationStrings.canMove); 
        } }
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        td = GetComponent<TouchingDirection>();
        damageable = GetComponent <Damageable>();
    }

    private bool isMoving
    {
        get
        {
            return _isMoving;
        }
        set
        {
            _isMoving = value;
            animator.SetBool(AnimationStrings.isMoving, value);

        }
    }

    [SerializeField]
    public bool _isMoving = false;

    public bool _isFacingRight = true;
    private float runSpeed;
    private float airWalkSpeed;
    private object touchingDirection;

    public bool isFacingRight

    
    {
        get { return _isFacingRight; }
        private set
        {
            if (_isFacingRight != value)
            {
                transform.localScale *= new Vector2(-1, 1);
            }

            _isFacingRight = value;
        }
    }

    public bool IsRunning { get; private set; }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        //if (damageable.IsHit)
        rb.velocity = new Vector2(moveInput.x * walkspeed, rb.velocity.y);

        animator.SetFloat(AnimationStrings.yVelocity, rb.velocity.y);
    }

    public void OnMove(InputAction.CallbackContext context)
    {

        moveInput = context.ReadValue<Vector2>();
        isMoving = moveInput != Vector2.zero;

        SetFacingDirection(moveInput);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if(context.started && td.isGrounded)
        {
            animator.SetTrigger(AnimationStrings.jump);
            rb.velocity = new Vector2(rb.velocity.y, jumpImpluse);
        }
    }

    private void SetFacingDirection(Vector2 moveInput)
    {
        if (moveInput.x > 0 && !isFacingRight)
        {
            //right
            isFacingRight = true;
        }
        else if (moveInput.x < 0 && isFacingRight)
        {
            //left
            isFacingRight = false;
        }
    }
    public void OnRoll(InputAction.CallbackContext context)
    {
        if (context.started && td.isGrounded)
        {
            StartCoroutine(Roll());
        }
    }

    IEnumerator Roll()
    {
        float rollTime = 0f;
        animator.SetBool("Rolling", true);
        Vector2 rollDirection = isFacingRight ? Vector2.right : Vector2.left; // Get the direction player is facing

        // Apply an instant burst of speed
        rb.velocity = new Vector2(rollDirection.x * rollSpeed, rb.velocity.y);

        while (rollTime < rollDuration)
        {
            rollTime += Time.deltaTime;
            yield return null;
        }

        rb.velocity = Vector2.zero; // Stop the player after rollDuration
        animator.SetBool("Rolling", false);
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            StartCoroutine(Attack());
        }
    }

    IEnumerator Attack()
    {
        animator.SetTrigger(AnimationStrings.attack);

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, 0.5f, attackLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            // damage enemy
        }

        yield return new WaitForSeconds(attackDuration);
    }

    public void OnHit(int damage, Vector2 knockback)
    {
        IsHit = true;
        rb.velocity = new Vector2(knockback.x, knockback.y + rb.velocity.y);
    }
}
