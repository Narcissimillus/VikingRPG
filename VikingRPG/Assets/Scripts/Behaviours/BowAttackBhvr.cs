using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowAttackBhvr : StateMachineBehaviour
{
    bool justOnce;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        justOnce = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stateInfo.normalizedTime > 0.57f && justOnce == false && animator.GetBool("hasBow"))
        {
            justOnce = true;
            GameObject arrow = GameObject.Instantiate(animator.GetComponent<PlayerController>().arrow.gameObject);
            arrow.SetActive(true);
            Transform bowTransform = animator.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(4).GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetChild(5).transform;
            arrow.transform.rotation = bowTransform.rotation;
            arrow.transform.position = bowTransform.position;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("attack");
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
