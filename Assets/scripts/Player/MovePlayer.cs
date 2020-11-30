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
    private float verticalMove;
    public float gravityScale;
    private bool _canPlane;
    private bool jumpBtnDown = false;
    public bool isClimbing;

    private AudioSource jumpSound;
    public ParticleSystem particleSystem;
    public SpriteRenderer sp;
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask collisionLayer;
    public Animator animator;
    public UnityEvent OnLandEvent;

    public Rigidbody2D rb;
    private Vector3 _velocity = Vector3.zero;

    // // Double click
    // private const float DOUBLE_CLICK_TIME = 0.2f;
    // private float lastClickTime;
    private void Awake()
    {
        jumpSound = GameObject.Find("Jump").GetComponent<AudioSource>();
        if (OnLandEvent == null)
        {
            OnLandEvent = new UnityEvent();
        }
    }


    private void Update()
    {
        //     if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        //     {
        //         
        //         float timeLastClick = Time.time - lastClickTime;
        //         lastClickTime = Time.time;
        //         if (timeLastClick <= DOUBLE_CLICK_TIME)
        //         {
        //             if (Math.Sign(rb.velocity.x) == 1)
        //             {
        //                 dash(1000);
        //             }
        //             else
        //             {
        //                 dash(-1000);
        //             }
        //         }
        //     }
        jumpEntries();
        if (isClimbing)
        {
            MoveDirection(rb.velocity.y);
            sp.flipY = _alignPosition;
        }
        else
        {
            MoveDirection(rb.velocity.x);
            sp.flipX = _alignPosition;
        }
    }

    // Plus pour la gestion de la physique et pas d'entrée d'input
    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, collisionLayer);
        horizontalMove = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        verticalMove = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        PlayerMove(horizontalMove, verticalMove);
    }

    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
        animator.SetBool("IsFlying", false);
        animator.SetBool("IsGrounded", true);
    }

    void jumpEntries()
    {
        if (Input.GetButtonDown("Jump"))
        {
            jumpBtnDown = true;
        }
        else if (Input.GetButtonUp("Jump"))
        {
            jumpBtnDown = false;
        }

        if (Mathf.Sign(rb.velocity.y) == -1 && (animator.GetBool("IsJumping") || jumpBtnDown))
        {
            _canPlane = true;
        }
        else
        {
            _canPlane = false;
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            isJumping = true;
        }

        if (isGrounded && rb.velocity.y < 0)
        {
            OnLandEvent.Invoke();
        }


        if (_canPlane && Input.GetButtonDown("Jump"))
        {
            Plane();
        }
        else if (Input.GetButtonUp("Jump"))
        {
            animator.SetBool("IsFlying", false);
            rb.gravityScale = 1f;
            rb.mass = 1f;
        }
    }

    void PlayerMove(float _horizontaleMove, float _verticalMove)
    {
        if (!isClimbing)
        {
            Vector3 targetVelocity = new Vector2(_horizontaleMove, rb.velocity.y);
            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref _velocity, .05f);

            if (isJumping)
            {
                jumpSound.Play();
                animator.SetBool("IsGrounded", false);
                animator.SetBool("IsJumping", true);
                rb.AddForce(new Vector2(0f, jumpForce));
                isJumping = false;
            }
        }
        else
        {
            animator.SetBool("IsJumping", false);
            Vector3 targetVelocity = new Vector2(0, _verticalMove);
            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref _velocity, .05f);
        }
    }

    void Plane()
    {
        animator.SetBool("IsFlying", true);
        rb.gravityScale = gravityScale;
        rb.mass = 0.7f;
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

    void dash(float force)
    {
        rb.AddForce(new Vector2(force, 0));
    }



    // private void OnDrawGizmos()
    // {
    //     Gizmos.color = Color.red;
    //     Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    // }
}