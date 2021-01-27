using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestNPC : InteractionObject
{
    public bool assigned;
    public Quest quest;
    public GameObject questPanel;
    public Text questTitle, questDescription, questRewards;
    public Canvas questSymbolUI;
    new public string name;
    [TextArea(3, 10)]
    public string[] sentences; //se pot configura liniile de dialog in editor

    public void GiveQuest()
    {
        questPanel.SetActive(true);
        questTitle.text = quest.questName;
        questDescription.text = quest.description;
        foreach (Reward questReward in quest.Rewards)
        {
            questRewards.text += questReward.rewardName + ", ";
        }
        questRewards.text = questRewards.text.Remove(questRewards.text.Length - 2) + ".";
    }

    public void AcceptQuest()
    {
        questSymbolUI.enabled = false;
        assigned = true;
        questPanel.SetActive(false);
        if(GetComponent<NPC>() != null)
        {
            GetComponent<NPC>().enabled = false;
        }
    }

    public void CancelQuest()
    {
        questPanel.SetActive(false);
        GetComponent<NPC>().enabled = true;
        this.enabled = false;
    }
    public override void Interaction()
    {
        base.Interaction(); // se apeleaza metoda parinte
        if (!assigned)
        {
            //dialog
            StartCoroutine(WaitQuestAccepted());
        }
        else
        {
            quest.CheckGoals();
            if (quest.completed)
            {
                GetComponent<NPC>().enabled = true;
                GetComponent<NPC>().sentences = new string[] { "Thank you for your help! Here's your reward!" };
                DialogueManager.instance.AddNewDialogue(GetComponent<NPC>().sentences, name);
                DialogueManager.instance.ShowDialogue();
                GetComponent<NPC>().sentences = new string[] { "Hello again, mighty warrior!", "Thank you again for your help!"};
                Destroy(this);
            }
        }
    }

    IEnumerator WaitQuestAccepted()
    {
        while (!assigned)
        {
            yield return null;
        }
        DialogueManager.instance.AddNewDialogue(sentences, name);
        DialogueManager.instance.ShowDialogue();
        yield return 0;
    }
}
