using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoverCanvasScript : MonoBehaviour
{
    public GameObject image;
    private void Start()
    {
        image.SetActive(false);
    }
    private void Update()
    {
        if (LevelPicker.adState == LevelPicker.AdState.NotShowingAd)
        {
            image.SetActive(false);
        }
        else
        {
            image.SetActive(true);
        }
    }

}
