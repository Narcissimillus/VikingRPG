using UnityEngine;

public class InvetoryUI : MonoBehaviour
{
    public GameObject inventoryUI;

    InventoryManager inventory;
    ItemSlot[] slots;
    // Start is called before the first frame update
    void Start()
    {
        inventory = InventoryManager.instance;
        inventory.onInventoryChangedCallback += UpdateUI; //definesc o metoda ca se apeleaza la aparitia unui eveniment delegat

        slots = GetComponentsInChildren<ItemSlot>(); //fiecare slot din inventar
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Invetory"))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
        }
    }

    void UpdateUI()
    {
        Debug.Log("Updating UI");

        //actualizare fiecare slot
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i], inventory.items[i].count);
            }
            else
                slots[i].RemoveItem();
        }

    }
}
