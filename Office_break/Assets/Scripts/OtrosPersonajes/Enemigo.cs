using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{

    [SerializeField]
    private float _Speed = 1.5f;
    [SerializeField]
    private bool _speedDecreased = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_speedDecreased)
        {
            _Speed = _Speed - 2f;
        }
    }

    public void hitwithArrow()
    {
        var coroutine = DecreaseSpeed(3);

        StartCoroutine(coroutine);
    }

    public IEnumerator DecreaseSpeed(int seconds)
    {
        _speedDecreased = true;

        for (int i = 0; i < seconds; i++)
        {
            yield return new WaitForSeconds(1);
        }

        _speedDecreased = false;
    }



    
}
