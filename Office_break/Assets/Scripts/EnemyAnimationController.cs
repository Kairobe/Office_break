using UnityEngine;

public class EnemyAnimationController : StateMachineBehaviour
{
    /// <summary> Called on each Update frame between OnStateEnter and OnStateExit callbacks </summary>
    /// <param name="animator"> The <see cref="Animator"/> associated with the current <see cref="Animation"/>. </param>
    /// <param name="stateInfo"> The information about the <see cref="Animator"/> current state. </param>
    /// <param name="layerIndex"> The index of the layer. </param>
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);

        animator.SetBool("isMoving", true);
    }
}