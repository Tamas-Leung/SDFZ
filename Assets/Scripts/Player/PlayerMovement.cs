using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D rb;
    private float horizontal;
    private float vertical;
    private Player player;
    private Vector2 screenBounds;
    private float objectWidth;
    private float objectHeight;
    private Animator anim;
    private float dashingCooldownTimer;
    private bool dashing;
    private bool pressedSpace;
    [SerializeField] private AnimationCurve dashCurve;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = transform.Find("Sprite").GetComponentInChildren<Animator>();
        player = GetComponent<Player>();
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
        pressedSpace = Input.GetKeyDown(KeyCode.Space);

        if (dashingCooldownTimer <= 0 && pressedSpace)
        {
            dashing = true;
            anim.SetTrigger("Dash");
            dashingCooldownTimer += player.dashCooldown;
            StartCoroutine(Dash());
        }

        if (dashingCooldownTimer > 0)
        {
            dashingCooldownTimer -= Time.deltaTime;
        }


    }

    private void FixedUpdate()
    {


        if (!dashing)
        {
            //Move character 
            Vector2 direction = new Vector2(horizontal, vertical);
            direction.Normalize();

            Vector2 movement = direction * player.movementSpeed;

            //Sets speed for animator, changing from idle to movement
            anim.SetFloat("Speed", movement.magnitude);

            // Set Velocity
            rb.velocity = movement;
        }


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

    private IEnumerator Dash()
    {
        Vector2 startingPosition = transform.position;
        Vector2 direction = new Vector2(horizontal, vertical);
        Vector2 endPosition = startingPosition + direction * player.dashRange;
        float elapsed = 0f;
        //Reset Velocity
        rb.velocity = Vector2.zero;

        while (elapsed < player.dashDuration)
        {
            elapsed += Time.deltaTime;
            float percentageComplete = Mathf.Clamp01(elapsed / player.dashDuration);
            Vector2 nextPosition = Vector2.Lerp(startingPosition, endPosition, dashCurve.Evaluate(percentageComplete));
            rb.MovePosition(nextPosition);
            yield return null;
        }

        dashing = false;
    }
}
