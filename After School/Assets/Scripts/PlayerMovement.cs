using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float playerSpeed = 5f;
    public Rigidbody2D rb;
    public Rigidbody2D mrb;
    public Animator animator;
    public Animator manimator;
    public Collider2D hkdoor;
    public Collider2D khdoor;
    public Collider2D hbdoor;
    public Collider2D bhdoor;
    Vector2 movement;
    float facing = -1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
        manimator.SetFloat("Horizontal", movement.x);
        manimator.SetFloat("Vertical", movement.y);
        manimator.SetFloat("Speed", movement.sqrMagnitude);
        if (Input.GetAxisRaw("Vertical") > 0)
        {
            facing = 1f;
        }
        if (Input.GetAxisRaw("Vertical") < 0)
        {
            facing = -1f;
        }
        animator.SetFloat("Facing", facing);
        manimator.SetFloat("Facing", facing);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * playerSpeed * Time.fixedDeltaTime);
    }

    public void Teleport(float x, float y)
    {
        Vector2 transport;
        transport.x = rb.position.x - x;
        transport.y = rb.position.y - y;
        rb.MovePosition(rb.position + transport);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision == hkdoor){
            transform.position = new Vector3(-46.5f, 11.54f, -3f);
        }
        if(collision == khdoor)
        {
            transform.position = new Vector3(-0.96f, 18.11f, -3f);
        }
        if (collision == hbdoor)
        {
            transform.position = new Vector3(47.54f, 14.6f, -3f);
        }
        if (collision == bhdoor)
        {
            transform.position = new Vector3(8.65f, 20.42f, -3f);
        }
    }
}
