using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Berri_Controller : MonoBehaviour
{
    [SerializeField] private float speed = 5;
    [SerializeField] private float jumpHeight = 6;
    private float horizontalDir;
    private bool isGrounded = false;
    private int JumpCount = 0;
    private SpriteRenderer berriboy;
    private int totalCollectibles = 0;

    Animator anim;
    private Rigidbody2D rb;
    private Dictionary<string, int> inventory = new Dictionary<string, int>();


    // Start is called before the first from update
    void Start()
    {        
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        berriboy = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(horizontalDir * speed, rb.velocity.y);
        if(rb.velocity == new Vector2(0, 0))
        {
            anim.SetBool("is_walking", false);
            anim.SetBool("is_jumping", false);
            anim.SetBool("is_running", false);
        }

    }

    void OnJump()
    {
        if (JumpCount < 2)
        {
            JumpCount++;
            anim.SetBool("is_jumping", true);
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight); 
        }
    }

    void OnMove(InputValue value)
    {
        //Debug.Log(value.ToString());
        anim.SetBool("is_walking", true);
        Vector2 inputDir = value.Get<Vector2>();
        horizontalDir = Mathf.Clamp(inputDir.x, -1.5f, 1.5f);
        if (horizontalDir < 0)
        {
            berriboy.flipX = false;
        }
        else if (horizontalDir > 0)
        {
            berriboy.flipX = true;
        }
    }

    void OnRun(InputValue value)
    {
        if (value.isPressed)    //Run code that happens when berri is running
        {
            Debug.Log("Running True");
            anim.SetBool("is_running", true);
            anim.SetBool("is_walking", false);
        }
        else    //below code happens when he stops running
        {
            Debug.Log("running false");
            anim.SetBool("is_running", false);
        }
    }

   
    private void OnCollisionEnter2D(Collision2D col) 
    {
        if (col.gameObject.CompareTag("Floor")) {
            isGrounded = true;
            JumpCount = 0;
            anim.SetBool("is_jumping", false);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Floor")) {
            isGrounded = false;
            anim.SetBool("is_jumping", true);
        }
    }
    public void CollectItem(GameObject collectible)
    {
        string itemType = collectible.GetComponent<Berri_Collectibles>().itemType;
        if (inventory.ContainsKey(itemType))
        {
            inventory[itemType]++;
        }
        else
        {
            inventory.Add(itemType, 1);
        }
        totalCollectibles++;
      //  UIManager.Instance.UpdateCollectibleCount(totalCollectibles);
        Destroy(collectible);
        Debug.Log("Collected: " + itemType + ". Total: " + inventory[itemType]);
        if (totalCollectibles == 3)
        {
            CompleteLevel();
        }
    }
    private void CompleteLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}
