using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    private Animator anim;
    private Rigidbody2D rb;
    private Transform player;
    private Vector2 directionToPlayer;
    private float movementSpeed;
    private SpriteRenderer sr;
    private Enemy enemy;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<Player>().transform;
        movementSpeed = GetComponent<Enemy>().movementSpeed;
        enemy = GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy.isDead) return;
        directionToPlayer = (player.position - transform.position).normalized;

    }

    private void FixedUpdate()
    {
        if (enemy.isDead)
        {
            rb.velocity = Vector2.zero;
            return;
        }
        Vector2 direction = (directionToPlayer * movementSpeed);
        //Sets speed for animator, changing from idle to movement
        anim.SetFloat("Speed", direction.magnitude);
        if (direction.x > 0)
            sr.flipX = false;
        else
            sr.flipX = true;
        rb.velocity = direction;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Ignore Enemies in collision
        if (collision.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }
}
