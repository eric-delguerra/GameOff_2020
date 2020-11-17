using System;
using UnityEngine;
using UnityEngine.Events;

public class MovePlayer : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    public bool isJumping;
    public bool isGrounded;
    private bool _alignPosition = true;
    private float horizontalMove;
    public float gravityScale;
    private bool _canPlane;

    public ParticleSystem particleSystem;
    public SpriteRenderer sp;
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask collisionLayer;
    public Animator animator;
    public UnityEvent OnLandEvent;
    
    public Rigidbody2D rb;
    
    private Vector3 _velocity = Vector3.zero;

    private void Awake()
    {
        if (OnLandEvent == null)
        {
            OnLandEvent = new UnityEvent();
        }
    }

    private void Update()
    {   
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
                isJumping = true;
                _canPlane = true;
        }

        if (isGrounded && rb.velocity.y < 0)
        {
            OnLandEvent.Invoke();
        }
        
        MoveDirection(rb.velocity.x);
        sp.flipX = _alignPosition;
        
        
        if (_canPlane && Input.GetKeyDown(KeyCode.Space))
        {
            Plane();
        }
        else if (_canPlane && Input.GetKeyUp(KeyCode.Space))
        {
            animator.SetBool("IsFlying", false);
            rb.gravityScale = 1f;
        }
    }

    // Plus pour la gestion de la physique et pas d'entrée d'input
    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, collisionLayer);
        horizontalMove = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        PlayerMove(horizontalMove);
        
    }

    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
        animator.SetBool("IsFlying", false);
    }
    
    void PlayerMove(float horizontaleMove)
    {
        Vector3 targetVelocity = new Vector2(horizontaleMove, rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref _velocity, .05f);

        if (isJumping)
        {
            animator.SetBool("IsJumping", true);
            rb.AddForce(new Vector2(0f, jumpForce));
            isJumping = false;
        }
        
    }

    void Plane()
    {
        animator.SetBool("IsFlying", true);
        rb.gravityScale = gravityScale;
    }
    
    void MoveDirection(float num)
    {
        animator.SetFloat("Speed", Mathf.Abs(num));

        if (num > 0.1f)
        {
            _alignPosition = false;
        }
        else if (num < -0.1f)
        {
            _alignPosition = true;
        }
    }

    // private void OnDrawGizmos()
    // {
    //     Gizmos.color = Color.red;
    //     Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    // }
}
