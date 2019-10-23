using UnityEngine;
using TMPro;
public class TurorialButton : MonoBehaviour
{
    public GameObject howToPlayImage;
    public TextMeshProUGUI text;
    Vector3  originalPosition;
    string orginalText;
    bool isShowingHowToPlay;

    public RectTransform rect;

    private void Start()
    {
        originalPosition = rect.transform.localPosition;
        orginalText = text.text;
    }

    public void ShowHowToPlay()
    {
        if(!isShowingHowToPlay)
        {
            rect.transform.localPosition = new Vector3(0.0f, -200f);
            howToPlayImage.SetActive(true);
            text.text = "Return";
            isShowingHowToPlay = true;
        }
        else
        {
            rect.transform.localPosition = originalPosition;
            howToPlayImage.SetActive(false);
            text.text = orginalText;
            isShowingHowToPlay = false;
        }
    }
}
