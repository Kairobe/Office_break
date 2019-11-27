using System.Collections;
using UnityEngine;

public class BossController : MonoBehaviour
{
    private bool playAnimation = true;
    private Animator animator;

    [SerializeField]
    private Floor actualFloor;

    private enum Floor { Office = 0, Garden = 1, Cafe = 2 }

    // Start is called before the first frame update
    void Start()
    {
        /// animator = this.GetComponent<Animator>();
        actualFloor = Floor.Cafe;
    }

    // Update is called once per frame
    void Update()
    {
        if (playAnimation)
        {
            StartCoroutine(this.PlayAnimation());
        }
    }

    IEnumerator PlayAnimation()
    {
        playAnimation = false;

        int newFloor = Random.Range(0, 3);

        // Simulate the floor changing time.
        yield return new WaitForSeconds(3);

        actualFloor = (Floor)newFloor;

        // The random time to wait for the animation.
        int randomWait = Random.Range(0, 2);
        yield return new WaitForSeconds(randomWait);
        MoveBossToElevator(actualFloor);

        ///animator.Play("--ANIMATION NAME--");
        playAnimation = true;
    }

    private void MoveBossToElevator(Floor currentFloor)
    {
        Debug.Log(currentFloor);

        switch (currentFloor)
        {
            case Floor.Cafe:
                this.transform.position = new Vector3(15.62385f, 0.6f, -15.95f);
                break;
            case Floor.Garden:
                this.transform.position = new Vector3(10.96f, 0.6f, -15.95f);
                break;
            case Floor.Office:
                this.transform.position = new Vector3(14.37f, 0.6f, -15.95f);
                break;
            default:
                break;
        }
    }
}