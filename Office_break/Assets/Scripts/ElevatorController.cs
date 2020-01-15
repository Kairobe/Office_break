using UnityEngine;

public class ElevatorController : MonoBehaviour
{
    private Animator elevatorAnimator;

    /// <summary> Called before the first frame update. </summary>
    private void Start()
    {
        this.elevatorAnimator = this.GetComponent<Animator>();
    }

    /// <summary> Called once per frame. </summary>
    private void Update()
    {
    }

    /// <summary>
    /// Open or closes the doors depending on if <paramref name="openDoors"/> is true or false.
    /// </summary>
    /// <param name="openDoors"> Indicates if the doors must be open, false in other case. </param>
    public void ManageDoors(bool openDoors)
    {
        this.elevatorAnimator.SetBool("Open", openDoors);
    }
}