using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveHealthPots : InteractionObject
{
    public Item item;
    public override void Interaction()
    {
        base.Interaction(); // se apeleaza metoda parinte
        InventoryManager.instance.Add(item);
        InventoryManager.instance.Add(item);
        bool wasPickedUp = InventoryManager.instance.Add(item);

        //distrugem obiectul
        if (wasPickedUp)
        {
            this.GetComponent<NPC>().sentences[0] = "That's all I can give you!";
            Destroy(this);
        }
    }
}
