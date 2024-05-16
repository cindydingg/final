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
    float horizontalDir;
    private bool isGrounded = false;
    private Animator anim;
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
        rb.velocity = new Vector2(horizontalDir * speed, rb.velocity.y);       
    }

    void OnJump()
    {
        if (isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight); 
        }
    }

    void OnMove(InputValue value)
    {
        Vector2 inputDir = value.Get<Vector2>();
        float inputX = inputDir.x;
        
        inputX = Mathf.Clamp(inputX, -1.5f, 1.5f);
        horizontalDir = inputX;
        // horizontalDir = inputX * inputSensitivity;
    }

    private void OnCollisionEnter2D(Collision2D col) 
    {
        if (col.gameObject.CompareTag("Floor")) {
            isGrounded = true;
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
