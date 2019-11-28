using UnityEngine;

public class ElevatorController : MonoBehaviour
{
    private Animator elevatorAnimator;

    // Start is called before the first frame update
    void Start()
    {
        this.elevatorAnimator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ManageDoors(bool openDoors)
    {
        this.elevatorAnimator.SetBool("Open", openDoors);
    }

    private void OnTriggerEnter(Collider other)
    {
        this.ManageDoors(true);
    }

    private void OnTriggerExit(Collider other)
    {
        this.ManageDoors(false);
    }
}