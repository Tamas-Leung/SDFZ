using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public SpriteRenderer sr;
    public Animator anim;

    public float moveSpeed = 2f;

    Rigidbody2D rb;
    float horizontal;
    float vertical;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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



        rb.velocity = direction;

    }
}
