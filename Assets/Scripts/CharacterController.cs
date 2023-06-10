using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Vector2 movement;
    public Animator animator;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
            animator.SetFloat("Speed", movement.sqrMagnitude);
        
        if (movement.x < 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        if(movement.x > 0)
            {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }


        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

    }
}