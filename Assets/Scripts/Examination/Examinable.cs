using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Examinable : MonoBehaviour
{
    //Outline outline;
    public string message;

    public bool onToggle = false;

    public UnityEvent onInteraction;
    public Item Item;

    void Start()
    {
        //outline = GetComponent<Outline>();
        DisableOutline();
    }

    public void Pickup()
    {
        ToggleExamination();
    }

    public void RealPickup()
    {
        InventoryManager.Instance.Add(Item);
        InventoryManager.Instance.ListItems();
        onInteraction.Invoke();
    }

    public void DisableOutline()
    {
        //outline.enabled = false;
    }

    public void EnableOutline()
    {
        //outline.enabled = true;
    }

    public void ToggleExamination()
    {
        onToggle = !onToggle;
    }
}
