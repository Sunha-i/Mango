using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public abstract class InventoryDisplay : MonoBehaviour
{
    [SerializeField] MouseItemData mouseInventoryItem;
    protected InventorySystem inventorySystem;
    protected Dictionary<InventorySlot_UI, InventorySlot> slotDictionary;
    public InventorySystem InventorySystem => inventorySystem;
    public Dictionary<InventorySlot_UI, InventorySlot> SlotDictionary => slotDictionary;

    protected virtual void Start()
    {

    }

    public abstract void AssignSlot(InventorySystem invToDisplay);

    protected virtual void UpdateSlot(InventorySlot updatedSlot)
    {
        foreach (var slot in SlotDictionary)
        {
            if (slot.Value == updatedSlot) //Slot Value - the "under the hood" inventory slot.
            {
                slot.Key.UpdateUISlot(updatedSlot); //Slot Key - the UI representation of the value.
            }
        }
    }

    public void SlotClicked(InventorySlot_UI clickedUISlot)
    {
        
        // Clicked slot has an item - mouse doesn't have an item - pick up that item.

        if (clickedUISlot.AssignedInventorySlot.ItemData != null && mouseInventoryItem.AssignedInventorySlot.ItemData == null)
        {
            // If player is holding shift key? Split the stack.

            mouseInventoryItem.UpdateMouseSlot(clickedUISlot.AssignedInventorySlot);
            clickedUISlot.ClearSlot();
            return;
        }

        //Clicked slot doesn't have an item - Mouse does have an item - place the mouse item into the empty slot.
        if (clickedUISlot.AssignedInventorySlot.ItemData == null && mouseInventoryItem.AssignedInventorySlot.ItemData != null)
        {
            clickedUISlot.AssignedInventorySlot.AssignItem(mouseInventoryItem.AssignedInventorySlot);
            clickedUISlot.UpdateUISlot();

            mouseInventoryItem.ClearSlot();
        }
        //Both slots have an item - decide what to do...
            //Are both items the same? If so combine them.
                // If the slot stack size + mouse stack size > the slot Max Stack Size? If so, take from mouse.
            //If different items, then swap the items.
    }

}
