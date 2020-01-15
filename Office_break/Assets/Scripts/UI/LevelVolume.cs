using System;
using UnityEngine;
using UnityEngine.UI;

public class LevelVolume : MonoBehaviour
{
    private Text text;
    private float volume;

    /// <summary> Called before the first frame update. </summary>
    private void Start()
    {
        text = GetComponent<Text>();
    }

    /// <summary> Called once per frame. </summary>
    private void Update()
    {
        volume = AudioListener.volume * 100;

        if (volume < 0)
        {
            volume = 0;
        }
        text.text = Math.Floor(volume).ToString();
    }
}