using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    // singleton
    public static InventoryManager instance;

    public delegate void OnInventoryChanged();
    public OnInventoryChanged onInventoryChangedCallback;

    public int space = 20;

    void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("There's more than one instance of InventoryManager!");
            return;
        }
        instance = this;
    }

    //lista de obiecte
    public List<Item> items = new List<Item>();

    //metode pentru gestionare
    public bool Add(Item item)
    {
        if(items.Count >= space)
        {
            Debug.Log("Invetory is full");
            return false;
        }
        items.Add(item);

        if(onInventoryChangedCallback != null)
        {
           onInventoryChangedCallback.Invoke(); //notifica despre  modificare
        }
        return true;
    }

    public void Remove(Item item)
    {
        items.Remove(item);
        if (onInventoryChangedCallback != null)
        {
            onInventoryChangedCallback.Invoke(); //notifica despre  modificare
        }
    }
}
