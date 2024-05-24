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
            other.gameObject.GetComponent<PlayerController>().CollectItem(this.gameObject);
            Destroy(this.gameObject);
        }
    }
}
