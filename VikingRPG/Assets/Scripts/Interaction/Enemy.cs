using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : InteractionObject
{
    PlayerController playerController;
    Animator playerAnimator;
    UnityEngine.AI.NavMeshAgent playerAgent;
    public override void Interaction()
    {
        base.Interaction();
        gameObject.GetComponentInChildren<Canvas>().enabled = true;
        player.GetComponentInChildren<Canvas>().enabled = true;
        playerController = player.GetComponent<PlayerController>();
        playerAnimator = player.GetComponent<Animator>();
        playerAgent = player.GetComponent<UnityEngine.AI.NavMeshAgent>();
        if (playerController.hasBowEquipped == false)
        {
            StartCoroutine(AutoAttack());
        }
    }

    IEnumerator AutoAttack()
    {
        yield return new WaitUntil(() => playerAgent.destination == this.transform.position);
        while ((playerAgent.destination == this.transform.position || 
               Vector3.Distance(this.transform.position, player.position) <= 1.5f) &&
               playerController.hasBowEquipped == false && playerController.rightHandEquipped == true &&
               this.transform.GetComponent<Animator>().GetBool("Died") == false)
        {
            player.rotation = Quaternion.RotateTowards(player.rotation, Quaternion.LookRotation(this.transform.position - player.position), 360 * Time.deltaTime);
            if (!playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            {
                playerAnimator.SetTrigger("attack");
                FindObjectOfType<AudioManager>().Play("AttackMiss");
            }
            yield return null;
        }
        if (this.transform.GetComponent<Animator>().GetBool("Died"))
        {
            playerAnimator.SetTrigger("victory");
            FindObjectOfType<AudioManager>().Play("Victory");
        }
        yield return 0;
    }
}
