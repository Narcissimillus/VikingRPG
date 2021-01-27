using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillGoal : Goal
{
    public EnemyController enemy;

    public KillGoal(EnemyController enemy, bool completed, int currentAmount, int amount)
    {
        this.enemy = enemy;
        this.completed = completed;
        this.currentAmount = currentAmount;
        this.amount = amount;
    }

    public override void Init()
    {
        base.Init();
        enemy.onDie += EnemyKilled;
    }

    void EnemyKilled(string enemy)
    {
        if (enemy == this.enemy.name)
        {
            this.currentAmount++;
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
