using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class playerControl : MonoBehaviour
{

    public float speed;
    public float jumpForce;
    private float moveInput;

    private Rigidbody2D rb;
    private Vector2 mp;
    private Transform playerPos;
    private Vector3 mousePos;
    private bool isGrounded;
    private bool facingRight = true;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;
    private int extraJumps;
    public int extraJumpsValue;
    private Animator anim;

    

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        extraJumps = extraJumpsValue;
        rb = GetComponent<Rigidbody2D>();



    }

    private void FixedUpdate()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        playerPos = transform;
        moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        if (facingRight == false && mousePos.x > playerPos.position.x)
        {
            Flip();
        }
        else if (facingRight == true && mousePos.x < playerPos.position.x)
        {
            Flip();
        }

    }
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
        {
            anim.SetTrigger("takeOff");
        }
        if (rb.velocity.y < 0)
        {

            anim.SetBool("isJumping", false);
        }
        else if(isGrounded == false)
        {
            anim.SetBool("isJumping", true);
        }
        if (isGrounded == true)
        {
            extraJumps = extraJumpsValue;
        }
        if (Input.GetKeyDown(KeyCode.Space) && extraJumps > 0)
        {
            
            rb.velocity = Vector2.up * jumpForce;
            extraJumps--;
        } else if (Input.GetKeyDown(KeyCode.Space) && extraJumps == 0 && isGrounded == true)
        {
            rb.velocity = Vector2.up * jumpForce;
            
        }
      
        
       
        if (moveInput == 0) 
        {
            anim.SetBool("isRunning", false);
        } else
        {
            anim.SetBool("isRunning", true);
        }

    }
}
