using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5;
    [SerializeField] private float jumpHeight = 5;
    private float horizontalDir;
    private bool isGrounded = false;
    private int totalCollectibles = 0;

    private Rigidbody2D rb;
    private Animator animator; 
    private SpriteRenderer spriteRenderer;
    private Dictionary<string, int> inventory = new Dictionary<string, int>();

    void Start()
    {        
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        rb.velocity = new Vector2(horizontalDir * speed, rb.velocity.y);       

        animator.SetFloat("Speed", Mathf.Abs(horizontalDir));

        if (horizontalDir > 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (horizontalDir < 0)
        {
            spriteRenderer.flipX = false;
        }
    }

    void FixedUpdate()
    {
        if (isGrounded)
        {
            animator.SetBool("isJumping", false);
        }
    }

    void OnJump()
    {
        if (isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
            animator.SetBool("isJumping", true);
            isGrounded = false;
        }
    }

    void OnMove(InputValue value)
    {
        Vector2 inputDir = value.Get<Vector2>();
        horizontalDir = Mathf.Clamp(inputDir.x, -1.0f, 1.0f);
    }

    private void OnCollisionEnter2D(Collision2D col) 
    {
        if (col.gameObject.CompareTag("Floor"))
        {
            isGrounded = true;
            animator.SetBool("isJumping", false);
        }
        else if (col.gameObject.CompareTag("Collectible"))
        {
            CollectItem(col.gameObject);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {
            isGrounded = false;
        }
    }

    public void CollectItem(GameObject collectible)
    {
        string itemType = collectible.GetComponent<CollectibleItems>().itemType;
        if (inventory.ContainsKey(itemType))
        {
            inventory[itemType]++;
        }
        else
        {
            inventory.Add(itemType, 1);
        }
        totalCollectibles++;
        // UIManager.Instance.UpdateCollectibleCount(inventory[itemType]);
        UIManager.Instance.UpdateCollectibleCount(totalCollectibles);
        Destroy(collectible);
        Debug.Log("Collected: " + itemType + ". Total: " + inventory[itemType]);
    }
}
