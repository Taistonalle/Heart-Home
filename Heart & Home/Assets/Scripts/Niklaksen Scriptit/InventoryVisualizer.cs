using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryVisualizer : MonoBehaviour
{
    // Start is called before the first frame update
    InventoryManager invMng;
    void Awake()
    {
        invMng = FindObjectOfType<InventoryManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateUI() {

    }
}
