using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : InteractionObject
{
    new public string name;
    [TextArea(3, 10)]
    public string[] sentences; //se pot configura liniile de dialog in editor

    public override void Interaction()
    {
        base.Interaction(); // se apeleaza metoda parinte
        DialogueManager.instance.AddNewDialogue(sentences, name, this);
        DialogueManager.instance.ShowDialogue();
    }

    public void HasQuest()
    {
        if(GetComponent<QuestNPC>() != null)
        {
            this.enabled = false;
            GetComponent<QuestNPC>().GiveQuest();
            GetComponent<QuestNPC>().enabled = true;
        }
    }
}
