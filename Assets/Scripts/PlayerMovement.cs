using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    private float horizontal;

    public float playerSpeed = 2;
    public float jumpForce = 2;
    public float raycastLength = 2;


    public bool isGrounded;
    public LayerMask groundLayerMask;
    public Transform respawnPoint;

    private SpriteRenderer spriteRenderer;
    private Animator anim;
    


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        respawnPoint.position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(x: horizontal * playerSpeed, y: rb.velocity.y);
        horizontal = Input.GetAxis("Horizontal");


        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
                {
            rb.velocity = new Vector2(rb.velocity.x, y:jumpForce);
        }

        if (rb.velocity.x != 0)
        {
            anim.SetBool(name: "isMoving", value: true);
        }
        else
        {
            anim.SetBool(name: "isMoving", value: false);
        }

        if (horizontal < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (horizontal > 0)
        {
            spriteRenderer.flipX = false;
        }

        //player is on ground or not
        isGrounded = (bool)Physics2D.Raycast(origin:(Vector2)transform.position, direction: Vector2.down, distance: raycastLength, (int)groundLayerMask);
        Debug.DrawRay(start: transform.position, dir: Vector3.down * raycastLength, Color.green);

        //jumping
        anim.SetBool(name: "isGrounded", isGrounded);



    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Coin")
        {
            Destroy(other.gameObject);
        }

        if(other.tag == "Respawn")
        {
            Respawn();
        }
    }

    void Respawn()
    {
        transform.position = respawnPoint.position;
    }

}
