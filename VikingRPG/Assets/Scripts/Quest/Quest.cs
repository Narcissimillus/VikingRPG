using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    public string questName;
    public string description;
    public List<Goal> Goals = new List<Goal>();
    public List<Reward> Rewards = new List<Reward>();
    public Item itemRequired;
    public Item[] rewardItem;
    public bool completed;

    public virtual void Init()
    {

    }
    public void CheckGoals()
    {
        completed = Goals.TrueForAll(g => g.completed); //questul este gata cand toate obiectivele sunt complete
        if (completed)
        {
            InventoryManager.instance.Remove(itemRequired);
            GiveReward();
        }
        else
        {
            Init();
        }
    }

    public void GiveReward()
    {
        Debug.Log("You have been rewarded!");
        foreach (Reward r in Rewards)
        {
            if(r.rewardItem != null)
            {
                bool wasPickedUp = InventoryManager.instance.Add(r.rewardItem);
                if (r.rewardItem.isStackable)
                {
                    string[] words = r.rewardName.Split(' ');
                    r.rewardItem.count = int.Parse(words[0]);
                }
            }
        }
        //in functie de tipul recompensei se adauga obiecte in inventar, sau se adauga experienta, skill points etc.
    }
}
