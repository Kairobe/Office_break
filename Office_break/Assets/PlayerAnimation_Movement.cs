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
        if (Input.GetKeyDown(KeyCode.UpArrow)) animator.SetBool("isMoving", true);
        if (Input.GetKeyUp(KeyCode.UpArrow)) animator.SetBool("isMoving", false);

        if (Input.GetKeyDown(KeyCode.RightArrow)) animator.SetBool("isTurningRight", true);
        if (Input.GetKeyUp(KeyCode.RightArrow)) animator.SetBool("isTurningRight", false);

        if (Input.GetKeyDown(KeyCode.LeftArrow)) animator.SetBool("isTurningLeft", true);
        if (Input.GetKeyUp(KeyCode.LeftArrow)) animator.SetBool("isTurningLeft", false);
    }
}
