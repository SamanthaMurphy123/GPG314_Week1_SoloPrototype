using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;

    public KeyCode left;
    public KeyCode right;
    public KeyCode jump;
    public KeyCode throwBall;

    private Rigidbody2D rb;

    public Transform groundCheckPoint;
    public float groundCheckRadius;
    public LayerMask whatIsGround;

    public bool isGrounded;

    private Animator anim;

    public GameObject bullet;
    public Transform throwPoint;

    private Bullet bulletScript;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        anim = GetComponent<Animator>();

        bulletScript = bullet.GetComponent<Bullet>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, whatIsGround);

        if (Input.GetKey(left))
        {
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
        }
        else if ( Input.GetKey(right))
        {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);

        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);

        }

        if (Input.GetKeyDown(jump) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);

        }

        if (Input.GetKeyDown(throwBall))
        {
          // Instantiate(bullet, throwPoint.position, throwPoint.rotation);

            // Determine the direction of the player.
            float direction = (transform.localScale.x < 0) ? -1f : 1f;

            // Create the bullet with an appropriate velocity.
            GameObject newBullet = Instantiate(bullet, throwPoint.position, throwPoint.rotation);
            Rigidbody2D bulletRb = newBullet.GetComponent<Rigidbody2D>();

            // Set the bullet's initial velocity based on the player's direction.
            bulletRb.velocity = new Vector2(direction * bulletScript.ballSpeed, 0f);
        }


        if (rb.velocity.x < 0)
        {
            transform.localScale = new Vector3(-4, 4, 4);
        }
        else if(rb.velocity.x > 0)
        {
            transform.localScale = new Vector3(4, 4, 4);
        }

        anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        anim.SetBool("Grounded", isGrounded);

    }
}
