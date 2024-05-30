using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleItems : MonoBehaviour
{
    public string itemType;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerController playerController = other.gameObject.GetComponent<PlayerController>();

            if (itemType == "Potion")
            {
                playerController.CollectPotion(this.gameObject);
            }
            else if (itemType == "SuperJump")
            {
                playerController.CollectSuperJump(this.gameObject);
            }
            else
            {
                playerController.CollectItem(this.gameObject);
            }
            Destroy(this.gameObject);
        }
    }
}
