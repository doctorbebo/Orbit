using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HowToPlayImageIntializer : MonoBehaviour
{
    public RectTransform rect;
    // Start is called before the first frame update
    void Start()
    {
        float screenEdgeBuffer = Screen.width / 10;
        rect.sizeDelta = new Vector2 (Screen.width - screenEdgeBuffer, Screen.height - screenEdgeBuffer);
    }

}
