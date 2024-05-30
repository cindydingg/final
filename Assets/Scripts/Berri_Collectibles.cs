using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Berri_Collectibles : MonoBehaviour
{
    public string itemType;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Berri_Controller playerController = other.gameObject.GetComponent<Berri_Controller>();
            playerController.CollectItem(this.gameObject);
            //Destroy(this.gameObject);
        }
    }
}