using UnityEngine;

public class PickUpObject : InteractionObject
{
    public Item item;
    public override void Interaction()
    {
        base.Interaction(); // se apeleaza metoda parinte, in caz ca avem ceva generic

        if (Mathf.Approximately(player.GetComponent<UnityEngine.AI.NavMeshAgent>().destination.x, transform.position.x) && 
            Mathf.Approximately(player.GetComponent<UnityEngine.AI.NavMeshAgent>().destination.z, transform.position.z))
        {
            //mecanica
            Debug.Log("Pickin up " + item.name);
            bool wasPickedUp = InventoryManager.instance.Add(item);
            FindObjectOfType<AudioManager>().Play("DropItem");

            //distrugem obiectul
            if (wasPickedUp)
            {
                Destroy(gameObject);
            }
        }

    }
}
