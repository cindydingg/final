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
                Destroy(this.gameObject);
            }
            else if (itemType == "SuperJump")
            {
                playerController.CollectSuperJump(this.gameObject);
                Destroy(this.gameObject);
            }
            else if (itemType == "SuperSpeed")
            {
                playerController.CollectSuperSpeed(this.gameObject);
                Destroy(this.gameObject);
            }
            else if (itemType == "Portal")
            {
                playerController.TakePortal(this.gameObject);
            }
            else
            {
                playerController.CollectItem(this.gameObject);
                Destroy(this.gameObject);
            }
        }
    }
}
