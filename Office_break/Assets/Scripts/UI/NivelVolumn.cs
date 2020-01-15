using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NivelVolumn : MonoBehaviour
{
    private Text texto;
    private float vol;
    // Start is called before the first frame update
    void Start()
    {
        texto = GetComponent<Text>();        
    }

    // Update is called once per frame
    void Update()
    {
        vol = AudioListener.volume * 100;
        if(vol < 0) { vol = 0; }
        texto.text = Math.Floor(vol).ToString() ;
    }
}
