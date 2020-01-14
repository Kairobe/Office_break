using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation_Movement : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

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

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, animatorStateInfo, layerIndex);

        if (Input.GetAxis("Vertical") > 0) animator.SetBool("isMoving", true);
        if (Input.GetAxis("Vertical") == 0) animator.SetBool("isMoving", false);

        if (Input.GetAxis("Horizontal") > 0) animator.SetBool("isTurningRight", true);
        if (Input.GetAxis("Horizontal") == 0) animator.SetBool("isTurningRight", false);

        if (Input.GetAxis("Horizontal") < 0) animator.SetBool("isTurningLeft", true);
        if (Input.GetAxis("Horizontal") == 0) animator.SetBool("isTurningLeft", false);
    }
}
