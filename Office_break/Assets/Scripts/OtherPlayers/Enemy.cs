using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float speed = 1.5f;

    [SerializeField]
    private bool speedIsDecreased = false;

    /// <summary> Called before the first frame update. </summary>
    private void Start()
    {
    }

    /// <summary> Called once per frame. </summary>
    private void Update()
    {
        if (speedIsDecreased)
        {
            speed -= 2f;
        }
    }

    /// <summary> Hits the enemy with an arrow. </summary>
    public void HitWithArrow()
    {
        var coroutine = DecreaseSpeed(3);

        StartCoroutine(coroutine);
    }

    /// <summary> Decreases the speed during the given seconds. </summary>
    /// <param name="seconds"> The seconds to decrease the speed. </param>
    /// <returns>
    /// An empty <see cref="IEnumerator"/> that enables this method to be called as a coroutine.
    /// </returns>
    public IEnumerator DecreaseSpeed(int seconds)
    {
        speedIsDecreased = true;

        for (int i = 0; i < seconds; i++)
        {
            yield return new WaitForSeconds(1);
        }

        speedIsDecreased = false;
    }
}