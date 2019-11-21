using UnityEngine;
using UnityEngine.UI;

public class ChooseRandomColor : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(ChangeImageColor);
    }

    private void ChangeImageColor()
    {
        GetComponent<Image>().color = RandomColor();
    }

    private Color RandomColor()
    {
        return new Color(
            UnityEngine.Random.Range(0f, 1f),
            UnityEngine.Random.Range(0f, 1f),
            UnityEngine.Random.Range(0f, 1f)
            );
    }
}