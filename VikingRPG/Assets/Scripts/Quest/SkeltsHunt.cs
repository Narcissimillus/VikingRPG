using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeltsHunt : Quest
{
    public string[] sentences;
    public EnemyController enemy;

    // Start is called before the first frame update
    void Start()
    {
        Goals.Add(new KillGoal(enemy, false, 0, 1));

        Goals.ForEach(g => g.Init());

        Rewards.Add(new ItemReward("500 coins", rewardItem[0]));
    }

    public override void Init()
    {
        base.Init();
        enemy.gameObject.SetActive(true);
        if(!completed)
        {
            if (Goals[0].completed == true)
            {
                completed = true;
                GiveReward();
            }
            else
            {
                int amount = Goals[0].amount - Goals[0].currentAmount;
                if(amount > 1)
                    sentences[0] = "You still have to slay " + Goals[0].amount + " skelst!";
                else
                    sentences[0] = "Almost there! One more skelt to slay!";
                DialogueManager.instance.AddNewDialogue(sentences, "Bjorn");
                DialogueManager.instance.ShowDialogue();
            }
        }
    }
}
