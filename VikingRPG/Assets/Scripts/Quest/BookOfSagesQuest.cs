using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookOfSagesQuest : Quest
{
    public string[] sentences;
    public EnemyController enemy;
    public GameObject doctor;
    public Item rewardItem, itemRequired;
    public bool completedFirstPart, completedSecondPart;

    // Start is called before the first frame update
    void Start()
    {
        Goals.Add(new ItemCollectGoal("Sword", false, 0, 1));
        Goals.Add(new ItemCollectGoal("Shield", false, 0, 1));
        Goals.Add(new ItemCollectGoal("Bow", false, 0, 1));
        // Goals.Add(new KillGoal(enemy, false, 0, 1));
        Goals.Add(new ItemCollectGoal("BookOfSages", false, 0, 1));

        Goals.ForEach(g => g.Init());

        Rewards.Add(new ItemReward("sword"));
        Rewards.Add(new ItemReward("shield"));
        Rewards.Add(new ItemReward("bow"));
        Rewards.Add(new ItemReward("500 coins", rewardItem));
        //Rewards.Add(new ExperienceReward(...));

    }

    public override void Init()
    {
        base.Init();
        int i = 0;
        if (!completedFirstPart)
        {
            foreach (ItemCollectGoal g in Goals)
            {
                i++;
                if (i < Goals.Count)
                {
                    if (g.completed == false)
                    {
                        completedFirstPart = false;
                        break;
                    }
                    else
                    {
                        completedFirstPart = true;
                        enemy.gameObject.SetActive(true);
                    }
                }
            }
            if (!completedFirstPart)
            {
                sentences[0] = "You still need to collect ";
                i = 0;
                foreach (ItemCollectGoal g in Goals)
                {
                    i++;
                    if (i < Goals.Count)
                    {
                        if (g.completed == false)
                        {
                            sentences[0] += g.itemName.ToLower() + ", ";
                        }
                    }
                }
                sentences[0] = sentences[0].Remove(sentences[0].Length - 2) + ".";
                sentences[1] = "If you have items equipped, please unequip them in order to check your inventory.";
                DialogueManager.instance.AddNewDialogue(sentences, "Jorgen");
                DialogueManager.instance.ShowDialogue();
            }
            else if (!completedSecondPart)
            {
                if (doctor.GetComponent<GiveHealthPots>() != null)
                {
                    doctor.GetComponent<NPC>().sentences[0] = "Hey! I heard from Jorgen you can use a little help! Here, take some 3 health potions.";
                    doctor.GetComponent<GiveHealthPots>().enabled = true;
                }
                sentences[0] = "Excellent work! Now all you need to do is to recover the Book of Sages from that skeleton!";
                sentences[1] = "You can take some health potions from my friend, who's a doctor. He's nearby.";
                DialogueManager.instance.AddNewDialogue(sentences, "Jorgen");
                DialogueManager.instance.ShowDialogue();
            }
        }
        else if (!completedSecondPart)
        {
            if(doctor.GetComponent<GiveHealthPots>() != null)
            {
                doctor.GetComponent<NPC>().sentences[0] = "Hey! I heard from Jorgen you can use a little help! Here, take some 3 health potions.";
                doctor.GetComponent<GiveHealthPots>().enabled = true;
            }
            if(Goals[Goals.Count - 1].completed == true)
            {
                completedSecondPart = true;
                completed = true;
                GiveReward();
                InventoryManager.instance.Remove(itemRequired);
            }
            else
            {
                sentences[0] = "Excellent work! Now all you need to do is to recover the Book of Sages from that skeleton!";
                sentences[1] = "You can take some health potions from my friend, who's a doctor. He's nearby.";
                DialogueManager.instance.AddNewDialogue(sentences, "Jorgen");
                DialogueManager.instance.ShowDialogue();
            }
        }
    }
}
