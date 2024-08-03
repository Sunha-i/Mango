using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

[System.Serializable]
public class InventorySystem
{
    [SerializeField] private List<InventorySlot> inventorySlots;

    public List<InventorySlot> InventorySlots => inventorySlots;
    public int InventorySize => InventorySlots.Count;

    public UnityAction<InventorySlot> OnInventorySlotChanged;

    public InventorySystem(int size)
    {
        inventorySlots = new List<InventorySlot>(size); //size를 먼저 넘겨줌

        for (int i = 0; i < size; i++)
        {
            inventorySlots.Add(new InventorySlot()); //null, stacksize -1
        }
    }

    public bool AddToInventory(InventoryItemData itemToAdd, int amountToAdd)
    {
        inventorySlots[0] = new InventorySlot(itemToAdd, amountToAdd);
        return true;
    }
}
