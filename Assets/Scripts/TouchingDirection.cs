using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchingDirection : MonoBehaviour
{
    public ContactFilter2D castFilter;
    public float groundDistance = 0.05f;
    public float wallDistance = 0.2f;
    CapsuleCollider2D touchColli;
    Animator animator;

    RaycastHit2D[] groundHits = new RaycastHit2D[5];
    RaycastHit2D[] wallHits = new RaycastHit2D[5];

    [SerializeField]
    private bool _isGrounded = true;

    public bool isGrounded
    {
        get
        {
            return _isGrounded;
        }
        set
        {
            _isGrounded = value;
            animator.SetBool(AnimationStrings.isGrounded, value);

        }
    }
    [SerializeField]
    private bool _isOnWall = false;
    internal static bool IsOnWall;

    private Vector2 wallCheckDirection => gameObject.transform.localScale.x > 0 ? Vector2.right : Vector2.left;
    public bool isOnWall
    {
        get { return _isOnWall; }
        set
        {
            _isOnWall = value;
            animator.SetBool(AnimationStrings.isOnWall, value);
        }
    }

    private void Awake()
    {
        touchColli = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
        isOnWall = false;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        isGrounded = touchColli.Cast(Vector2.down, castFilter, groundHits, groundDistance) > 0;
        //check for wall collisions
        isOnWall = touchColli.Cast(wallCheckDirection, castFilter, wallHits, wallDistance) > 0 ;
        
    }
}
