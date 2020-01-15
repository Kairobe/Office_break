using UnityEngine;

public class PlayerAnimationMovement : StateMachineBehaviour
{
    /// <summary> Called on each Update frame between OnStateEnter and OnStateExit callbacks </summary>
    /// <param name="animator"> The <see cref="Animator"/> associated with the current <see cref="Animation"/>. </param>
    /// <param name="animatorStateInfo">
    /// The information about the <see cref="Animator"/> current state.
    /// </param>
    /// <param name="layerIndex"> The index of the layer. </param>
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, animatorStateInfo, layerIndex);

        if (Input.GetAxis("Vertical") > 0)
        {
            animator.SetBool("isMoving", true);
        }

        if (Input.GetAxis("Vertical") == 0)
        {
            animator.SetBool("isMoving", false);
        }

        if (Input.GetAxis("Horizontal") > 0)
        {
            animator.SetBool("isTurningRight", true);
        }

        if (Input.GetAxis("Horizontal") == 0)
        {
            animator.SetBool("isTurningRight", false);
        }

        if (Input.GetAxis("Horizontal") < 0)
        {
            animator.SetBool("isTurningLeft", true);
        }

        if (Input.GetAxis("Horizontal") == 0)
        {
            animator.SetBool("isTurningLeft", false);
        }
    }
}