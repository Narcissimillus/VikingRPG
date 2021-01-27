using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item", order = 1)]
public class Item : ScriptableObject
{
    new public string name = "New MyScriptableObject"; //suprascrie atributul name
    public bool colorIsRandom = false;
    public bool isStackable = false;
    public int count = 1;
    public Color thisColor = Color.white;
    public Sprite icon;
    public GameObject itemObject;
    public enum Type { Equipable, Consumable, Inspectable};
    public Type itemType;
    public Vector3[] spawnPoints;
    public virtual void Equip(Transform player)
    {
        // Use item
        PlayerController playerController = player.GetComponent<PlayerController>();
        Animator playerAnimator = player.GetComponent<Animator>();
        if (playerController.rightHandEquipped == false && name == "Sword" && playerController.hasBowEquipped == false)
        {
            Debug.Log("Equiping " + name);
            FindObjectOfType<AudioManager>().Play("EquipSword");
            playerAnimator.SetTrigger("equipRightHand");
            playerController.StartCoroutine(WaitEquipSword(0.5f, player));
            playerController.rightHandEquipped = true;
            playerAnimator.SetInteger("damage", 20);
            InventoryManager.instance.Remove(this);
        }
        else if(playerController.rightHandEquipped == true && name == "Sword")
        {
            Debug.Log("Right hand is already equipped, press 'U' to unequip");
        }
        else if (playerController.hasBowEquipped == true && name == "Sword")
        {
            Debug.Log("You can't equip " + name + " while Bow is equipped");
        }
        if (playerController.leftHandEquipped == false)
        {
            if(name == "Shield")
            {
                Debug.Log("Equiping " + name);
                FindObjectOfType<AudioManager>().Play("Equip");
                playerAnimator.SetTrigger("equipLeftHand");
                playerController.StartCoroutine(WaitEquipShield(0.25f, player));
                playerController.leftHandEquipped = true;
                playerController.protectionArmour = 5;
                playerAnimator.SetBool("hasShield", true);
                InventoryManager.instance.Remove(this);
            }
            else if (name == "Bow" && playerController.rightHandEquipped == false)
            {
                Debug.Log("Equiping " + name);
                FindObjectOfType<AudioManager>().Play("Equip");
                playerAnimator.SetTrigger("equipLeftHand");
                playerController.StartCoroutine(WaitEquipBow(0.25f, player));
                playerController.leftHandEquipped = true;
                playerController.hasBowEquipped = true;
                playerAnimator.SetBool("hasBow", true);
                InventoryManager.instance.Remove(this);
            }
            else if (name == "Bow")
            {
                Debug.Log("You need to unequip your right hand in order to equip " + name);
            }
        }
        else if(playerController.leftHandEquipped == true && name != "Sword")
        {
            Debug.Log("Left hand is already equipped, press 'U' to unequip");
        }
    }

    public virtual void Consume(Transform player)
    {
        Animator playerAnimator = player.GetComponent<Animator>();
        if (playerAnimator.GetInteger("HP") == 100)
        {
            Debug.Log("Your health is 100 (maxed out).");
        }
        else if (playerAnimator.GetInteger("HP") > 80)
        {
            playerAnimator.SetInteger("HP", 100);
            player.GetComponent<PlayerController>().StartCoroutine(player.GetComponent<PlayerController>().ShowHealth(5f));
            Debug.Log("You increased your health to the maximum.");
            InventoryManager.instance.Remove(this);
        }
        else
        {
            playerAnimator.SetInteger("HP", playerAnimator.GetInteger("HP") + 20);
            player.GetComponent<PlayerController>().StartCoroutine(player.GetComponent<PlayerController>().ShowHealth(5f));
            Debug.Log("You increased your health by 20.");
            InventoryManager.instance.Remove(this);
        }
    }

    IEnumerator WaitEquipShield(float t, Transform player)
    {
        yield return new WaitForSeconds(t);
        GameObject newItem = Instantiate(itemObject, Vector3.zero, Quaternion.identity);
        newItem.transform.parent = player.GetChild(1).GetChild(0).GetChild(0).GetChild(4).GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetChild(0).transform;
        newItem.transform.localPosition = new Vector3(-0.1133335f, 0.07939018f, 0.02578373f);
        newItem.transform.localRotation = Quaternion.Euler(-161.363f, -13.67499f, 32.25999f);
        newItem.GetComponent<BoxCollider>().isTrigger = true;
        newItem.GetComponent<BoxCollider>().enabled = false;
    }

    IEnumerator WaitEquipBow(float t, Transform player)
    {
        yield return new WaitForSeconds(t);
        GameObject newItem = Instantiate(itemObject, Vector3.zero, Quaternion.identity);
        newItem.transform.parent = player.GetChild(1).GetChild(0).GetChild(0).GetChild(4).GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetChild(0).transform;
        newItem.transform.localPosition = new Vector3(-0.1f, 0.05f, 0.015f);
        newItem.transform.localRotation = Quaternion.Euler(-92, -116, 41.25f);
    }

    IEnumerator WaitEquipSword(float t, Transform player)
    {
        yield return new WaitForSeconds(t);
        GameObject newItem = Instantiate(itemObject, Vector3.zero, Quaternion.identity);
        newItem.transform.parent = player.GetChild(1).GetChild(0).GetChild(0).GetChild(4).GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetChild(0).transform;
        newItem.transform.localPosition = new Vector3(0.1076981f, -0.04643028f, 0.00212967f);
        newItem.transform.rotation = player.GetChild(1).GetChild(0).GetChild(0).GetChild(4).GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetChild(0).transform.rotation;
        newItem.transform.localRotation = Quaternion.Euler(-13.044f, 31.579f, 34.462f);
        newItem.GetComponent<BoxCollider>().isTrigger = true;
        newItem.GetComponent<BoxCollider>().enabled = false;
        newItem.GetComponent<AttackTrigger>().enabled = true;
    }
}
