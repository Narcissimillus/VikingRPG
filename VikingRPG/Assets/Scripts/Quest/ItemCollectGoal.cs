using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollectGoal : Goal
{
    public string itemName;       

    public ItemCollectGoal(string itemName, bool completed, int currentAmount, int amount)
    {
        this.itemName = itemName;
        this.completed = completed;
        this.currentAmount = currentAmount;
        this.amount = amount;
    }

    public override void Init()
    {
        base.Init();
        InventoryManager.instance.onInventoryChangedCallback += ItemPicked;
    }

    void ItemPicked()
    {
        this.currentAmount = 0;
        foreach(Item it in InventoryManager.instance.items)
        {
            if(it.name == itemName)
            {
                this.currentAmount++;
            }
        }
        if (this.currentAmount >= amount)
        {
            this.completed = true;
        }
        else
        {
            this.completed = false;
        }
    }
}
