using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Berri_Controller : MonoBehaviour
{
    [SerializeField] private float speed = 5;
    [SerializeField] private float jumpHeight = 5;
    //float horizontalDir;

    private float movement = 0f;
    private bool isGrounded = false;
    private int JumpCount = 0;

    Animator anim;
    private Rigidbody2D rb;
    // private float inputSensitivity = 1.0f;

    // Start is called before the first from update
    void Start()
    {        
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        // AdjustInputSensitivity();
    }

    // Update is called once per frame
    void Update()
    {
        Move(movement);
        //rb.velocity = new Vector2(horizontalDir * speed, rb.velocity.y);       
    }

    private void Move(float x)
    {
        rb.velocity = new Vector2(x * speed, rb.velocity.y);
        anim.SetFloat("walking_speed", Math.Abs(movement));
    }

    private void Jump()
    {
        Vector2 jumpAdder = new Vector2(rb.velocity.x, jumpHeight);
        rb.AddForce(jumpAdder, ForceMode2D.Impulse);
        JumpCount++;
    }

    void OnJump()
    {
        if (JumpCount < 2)
        {
            Jump();
            anim.SetBool("is_jumping", true);
            //  rb.velocity = new Vector2(rb.velocity.x, jumpHeight); 
        }
    }

    void OnMove(InputValue value)
    {

        movement = value.Get<float>();
        Debug.Log(movement);
        //Vector2 inputDir = value.Get<Vector2>();
        //float inputX = inputDir.x;

        //inputX = Mathf.Clamp(inputX, -1.5f, 1.5f);
        //horizontalDir = inputX;
        //// horizontalDir = inputX * inputSensitivity;
    }

    private void OnCollisionEnter2D(Collision2D col) 
    {
        if (col.gameObject.CompareTag("Floor")) {
            isGrounded = true;
            JumpCount = 0;
        }
        // } else if (col.gameObject.CompareTag("Collectible")) {
        //     isCollectible = true;
        // }

    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Floor")) {
            isGrounded = false;
        }
        // } else if (other.gameObject.CompareTag("Collectible")) {
        //     isCollectible = false;
        // }
    }

    // private void AdjustInputSensitivity()
    // {
    //     if (Application.isMobilePlatform) {
    //         inputSensitivity = 0.85f;
    //     }
    // }
}
