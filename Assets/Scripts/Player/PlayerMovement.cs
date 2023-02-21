using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed = 2f;

    Rigidbody2D rb;
    float horizontal;
    float vertical;
    private Vector2 screenBounds;
    private float objectWidth;
    private float objectHeight;
    private Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        Camera mainCamera = FindObjectOfType<Camera>();
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
        objectWidth = transform.GetComponent<Collider2D>().bounds.extents.x; //extents = size of width / 2
        objectHeight = transform.GetComponent<Collider2D>().bounds.extents.y; //extents = size of height / 2
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {

        //Move character 
        Vector2 direction = new Vector2(horizontal, vertical);
        direction.Normalize();
        direction *= moveSpeed;

        //Sets speed for animator, changing from idle to movement
        anim.SetFloat("Speed", direction.magnitude);

        // Set Velocity
        rb.velocity = direction;

        // Clamp position to be within screen
        Vector3 viewPos = rb.position;
        viewPos.x = Mathf.Clamp(viewPos.x, -9 + objectWidth, 9 - objectWidth);
        viewPos.y = Mathf.Clamp(viewPos.y, -5 + objectHeight, 5 - objectHeight);
        transform.position = viewPos;

    }


    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
        {
            // Ignore Enemies in collision
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }
}
