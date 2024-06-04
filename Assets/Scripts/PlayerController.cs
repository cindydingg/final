using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 2;
    [SerializeField] private float sprintSpeed = 5;
    [SerializeField] private float jumpHeight = 5;
    [SerializeField] private float doubleJumpHeight = 5;
    [SerializeField] private float superJumpHeight = 10;
    [SerializeField] private float superSpeed = 10;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckDistance = 1.0f;
    [SerializeField] private TextMeshProUGUI powerUpNotification;
    
    private float horizontalDir;
    private bool isGrounded = false;
    private bool canDoubleJump = false;
    private bool hasDoubleJumpPowerUp = false;
    private bool hasSuperJumpPowerUp = false;
    private bool hasSuperSpeedPowerUp = false;
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
        Time.timeScale = 1f;
        powerUpNotification.text = "";
    }

    void Update()
    {
        float currentSpeed = speed;
        bool isRunning = Keyboard.current.leftShiftKey.isPressed;

        if (hasSuperSpeedPowerUp)
        {
            if (isRunning)
            {
                sprintSpeed = superSpeed;
            }
        }
        if (isRunning) {
            currentSpeed = sprintSpeed;
        }

        rb.velocity = new Vector2(horizontalDir * currentSpeed, rb.velocity.y);

        animator.SetFloat("Speed", Mathf.Abs(horizontalDir));
        animator.SetBool("isRunning", isRunning);

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
        isGrounded = CheckIfGrounded();

        if (isGrounded)
        {
            animator.SetBool("isJumping", false);
        }
    }

    bool CheckIfGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayer);
        return hit.collider != null;
    }

    void OnJump()
    {
        isGrounded = CheckIfGrounded();
        Debug.Log("OnJump called. isGrounded: " + isGrounded + ", canDoubleJump: " + canDoubleJump);
        if (isGrounded)
        {
            if (hasSuperJumpPowerUp)
            {
                Jump(superJumpHeight);
                hasSuperJumpPowerUp = false; 
                Debug.Log("Super jump executed.");
                powerUpNotification.text = "Super Jump Activated!";
                StartCoroutine(ClearNotificationAfterDelay(2));
            }
            else
            {
                Jump(jumpHeight);
                if (hasDoubleJumpPowerUp)
                {
                    canDoubleJump = true;
                }
            }
        }
        else if (!isGrounded && canDoubleJump)
        {
            Jump(doubleJumpHeight);
            canDoubleJump = false;
            Debug.Log("Double jump executed.");
        }
    }

    void OnMove(InputValue value)
    {
        Vector2 inputDir = value.Get<Vector2>();
        horizontalDir = Mathf.Clamp(inputDir.x, -1.0f, 1.0f);
    }

    public void CollectItem(GameObject collectible)
    {
        Debug.Log("CollectItem called with: " + collectible.name);
        CollectibleItems collectibleComponent = collectible.GetComponent<CollectibleItems>();
        if (collectibleComponent != null)
        {
            string itemType = collectibleComponent.itemType;
            if (inventory.ContainsKey(itemType))
            {
                inventory[itemType]++;
            }
            else
            {
                inventory.Add(itemType, 1);
            }
            totalCollectibles++;
            UIManager.Instance.UpdateCollectibleCount(totalCollectibles);
            Destroy(collectible);
            Debug.Log("Collected: " + itemType + ". Total: " + inventory[itemType]);
        }
        else
        {
            Debug.LogWarning("Collected item does not have a CollectibleItems component.");
        }
    }

    public void CollectPotion(GameObject potion)
    {
        hasDoubleJumpPowerUp = true;
        Destroy(potion);
        Debug.Log("Collected Potion: Double Jump Activated! hasDoubleJumpPowerUp: " + hasDoubleJumpPowerUp);
        powerUpNotification.text = "Double Jump Activated!";
        StartCoroutine(ClearNotificationAfterDelay(2));
    }

    public void CollectSuperJump(GameObject superJump)
    {
        hasSuperJumpPowerUp = true;
        Destroy(superJump);
        Debug.Log("Collected Super Jump: Super Jump Activated! hasSuperJumpPowerUp: " + hasSuperJumpPowerUp);
        powerUpNotification.text = "Super Jump Activated!";
        StartCoroutine(ClearNotificationAfterDelay(2));
    }

    public void CollectSuperSpeed(GameObject superSpeed)
    {
        hasSuperSpeedPowerUp = true;
        Destroy(superSpeed);
        Debug.Log("Collected Super Speed: Super Speed Activated! hasSuperSpeedPowerUp: " + hasSuperSpeedPowerUp);
        powerUpNotification.text = "Super Speed Activated!";
        StartCoroutine(ClearNotificationAfterDelay(2));
    }

    public void TakePortal(GameObject portal)
    {
        if (totalCollectibles == 3)
        {
            CompleteLevel();
        }
    }

    private void Jump(float height)
    {
        rb.velocity = new Vector2(rb.velocity.x, height);
        animator.SetBool("isJumping", true);
    }

    private void CompleteLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    private IEnumerator ClearNotificationAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        powerUpNotification.text = "";
    }
}