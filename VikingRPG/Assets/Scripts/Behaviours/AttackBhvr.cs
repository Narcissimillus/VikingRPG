using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBhvr : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stateInfo.normalizedTime > 0.35f && stateInfo.normalizedTime < 0.45f)
        {
            // animator.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(4).GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetComponent<SphereCollider>().enabled = true;
            animator.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(4).GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetComponentInChildren<BoxCollider>().enabled = true;
        }
        else
        {
            // animator.GetBoneTransform(leftHand).GetComponent<SphereCollider>().enabled = false;
            animator.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(4).GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetComponentInChildren<BoxCollider>().enabled = false;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // animator.GetBoneTransform(leftHand).GetComponent<SphereCollider>().enabled = false;
        animator.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(4).GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetComponentInChildren<BoxCollider>().enabled = false;
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
