using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    public static DialogueManager instance;
    public GameObject panel;
    public NPC npc;
    public string NPCName;
    public List<string> sentences = new List<string>();

    Button continueButton;
    Text dialogeTextContainer, NPCNameContainer;

    int lineIndex;

    void Awake()
    {
        //get child components
        dialogeTextContainer = panel.transform.GetChild(1).GetComponent<Text>();
        NPCNameContainer = panel.transform.GetChild(2).GetComponentInChildren<Text>();
        continueButton = panel.transform.GetChild(1).GetComponent<Button>();
        // continueButton.onClick.AddListener(delegate { ContinueDialog(); });

        instance = this;
    }

    public void AddNewDialogue(string[] lines, string name, NPC npcGameObject = null)
    {
        //initializeaza dialogul curent din NPC
        lineIndex = 0;
        sentences.Clear();
        foreach (string line in lines)
            sentences.Add(line);
        NPCName = name;
        npc = npcGameObject;
    }

    public void ShowDialogue()
    {
        dialogeTextContainer.text = sentences[lineIndex]; //afiseaza linia de dialog curenta
        NPCNameContainer.text = NPCName;
        panel.SetActive(true);
    }

    public void ContinueDialogue()
    {
        if(lineIndex < sentences.Count - 1)
        {
            lineIndex++;
            ShowDialogue();
        }
        else
        {
            EndDialogue();
        }
    }

    public void EndDialogue()
    {
        lineIndex = 0;
        panel.SetActive(false);
        sentences.Clear();
        if(npc != null)
        {
            npc.HasQuest();
        }
    }

}
