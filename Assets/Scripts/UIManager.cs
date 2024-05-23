using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance; 
    [SerializeField] private TextMeshProUGUI collectibleCountText;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void UpdateCollectibleCount(int count)
    {
        collectibleCountText.text = "ingredients: " + count;
    }
}

