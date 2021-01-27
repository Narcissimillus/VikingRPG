using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{

    Item item;
    public Image icon;
    public Button dropButton;
    public Text stackNo;
    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddItem(Item newItem, int count = 1)
    {
        item = newItem;
        icon.sprite = item.icon;
        icon.enabled = true;
        dropButton.interactable = true;
        if (newItem.isStackable)
        {
            stackNo.enabled = true;
            stackNo.text = count.ToString();
        }
    }

    public void RemoveItem()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        dropButton.interactable = false;
        stackNo.enabled = false;
    }

    public void OnDropButton()
    {
        if(item.name == "Bow")
        {
            GameObject itemDropped = Instantiate(item.itemObject, player.transform.position, Quaternion.Euler(-90, 90, 0));
        }
        else
        {
            GameObject itemDropped = Instantiate(item.itemObject, player.transform.position, Quaternion.identity);
        }
        InventoryManager.instance.Remove(item);
        FindObjectOfType<AudioManager>().Play("DropItem");
    }

    public void UseItem()
    {
        if(item != null)
        {
            if(item.itemType.ToString() == "Equipable")
            {
                item.Equip(player);
            }
            else if(item.itemType.ToString() == "Consumable")
            {
                item.Consume(player);
            }
            else if(item.itemType.ToString() == "Inspectable")
            {
                if (item.name == "BookOfSages")
                    Debug.Log("This is the " + item.name + ". Be careful with it!");
                else
                    Debug.Log("Just some" + item.name + ". Nothing interesting!");
            }
        }
    }
}
