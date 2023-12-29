using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb2d;
    private Vector2 moveInput;
    private Animator anim;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        moveInput.Normalize();
        if (moveInput != Vector2.zero) {
            anim.SetFloat("X", moveInput.x);
            anim.SetFloat("Y", moveInput.y);
        }

        if (moveInput.x < 0) {
            spriteRenderer.flipX = true;
        } else if (moveInput.x > 0) {
            spriteRenderer.flipX = false;
        }
    }

    private void FixedUpdate() {
        rb2d.velocity = moveInput * moveSpeed;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Movable")) {
            Debug.Log("Collided with Movable");
        }
    }
}
