using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemReward : Reward
{
    public ItemReward(string rewardName, Item rewardItem = null)
    {
        this.rewardName = rewardName;
        this.rewardItem = rewardItem;
    }

    public override void Init()
    {
        // ...
    }
}
