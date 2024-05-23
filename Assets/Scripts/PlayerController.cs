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

    private Rigidbody2D rb;
    private Dictionary<string, int> inventory = new Dictionary<string, int>();

    void Start()
    {        
        rb = GetComponent<Rigidbody2D>();
    }

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
        horizontalDir = Mathf.Clamp(inputDir.x, -1.5f, 1.5f);
    }

    private void OnCollisionEnter2D(Collision2D col) 
    {
        if (col.gameObject.CompareTag("Floor"))
        {
            isGrounded = true;
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
        Destroy(collectible);
        Debug.Log("Collected: " + itemType + ". Total: " + inventory[itemType]);
    }
}
