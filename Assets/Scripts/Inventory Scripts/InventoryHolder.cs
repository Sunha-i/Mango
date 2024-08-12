using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class InventoryHolder : MonoBehaviour
{
    [SerializeField] private int inventorySize;
    [SerializeField] protected InventorySystem inventorysystem; //inventoryholder는 하나의 inventorysystem을 가짐

    public InventorySystem InventorySystem => inventorysystem;
    public static UnityAction<InventorySystem> OnDynamicInventoryDisplayRequested;

    protected virtual void Awake()
    {
        inventorysystem = new InventorySystem(inventorySize);
    }
}
