using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float moveSpeed;
    private float moveSpeedStore;
    public float speedMultiplier;

    public float speedMultiplierPoint;
    private float speedMultiplierPointStore;

    private float speedPointCount;
    private float speedPointCountStore;

    public float jumpForce;

    public float jumpTime;
    private float jumpTimeCounter;

    private bool stoppedJumping;

    private bool canDoubleJump;

    public bool grounded;
    public LayerMask whatIsGround;
    public Transform groundCheck;
    public float groundCheckRadius;

    private Collider2D thisCollider;
    private Rigidbody2D thisRigidBody;

    private Animator thisAnimator;

    public GameManager theGameManager;

    public AudioSource jumpSound;
    public AudioSource deathSound;

    // Start is called before the first frame update
    void Start()
    {
        thisRigidBody = GetComponent<Rigidbody2D>();
        thisCollider = GetComponent<Collider2D>();
        thisAnimator = GetComponent<Animator>();

        jumpTimeCounter = jumpTime;

        speedPointCount = speedMultiplierPoint;

        moveSpeedStore = moveSpeed;
        speedPointCountStore = speedPointCount;
        speedMultiplierPointStore = speedMultiplierPoint;

        stoppedJumping = true;
    }

    // Update is called once per frame
    void Update()
    {
        //grounded = Physics2D.IsTouchingLayers(thisCollider, whatIsGround);
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);


        if(transform.position.x > speedPointCount)
        {
            speedPointCount += speedMultiplierPoint;

            speedMultiplierPoint = speedMultiplierPoint * speedMultiplier;

            moveSpeed = moveSpeed * speedMultiplier;
        }

        thisRigidBody.velocity = new Vector2(moveSpeed, thisRigidBody.velocity.y);

        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(grounded)
            {
                thisRigidBody.velocity = new Vector2(thisRigidBody.velocity.x, jumpForce);
                stoppedJumping = false;
                jumpSound.Play();
            }

            if(!grounded && canDoubleJump)
            {
                thisRigidBody.velocity = new Vector2(thisRigidBody.velocity.x, jumpForce);

                stoppedJumping = false;
                canDoubleJump = false;
                jumpSound.Play();
            }
        }

        if(Input.GetKey (KeyCode.Space) && !stoppedJumping)
        {
            if(jumpTimeCounter > 0)
            {
                thisRigidBody.velocity = new Vector2(thisRigidBody.velocity.x, jumpForce);
                jumpTimeCounter -= Time.deltaTime;
            }
        }

        if(Input.GetKeyUp (KeyCode.Space))
        {
            jumpTimeCounter = 0;
            stoppedJumping = true;
        }

        if(grounded)
        {
            jumpTimeCounter = jumpTime;
            canDoubleJump = true;
        }

        thisAnimator.SetFloat("speed", thisRigidBody.velocity.x);
        thisAnimator.SetBool("grounded", grounded);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "killbox")
        {
            theGameManager.RestartGame();
            moveSpeed = moveSpeedStore;
            speedPointCount = speedPointCountStore;
            speedMultiplierPoint = speedMultiplierPointStore;
            deathSound.Play();
        }
    }

}
