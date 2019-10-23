using UnityEngine;

public class UIBackgroundIntialize : MonoBehaviour
{
    public RectTransform rect;
    // Start is called before the first frame update
    void Start()
    {
        float borderSize = Screen.width / 10;
        rect.sizeDelta = new Vector2(Screen.width - borderSize, Screen.height - borderSize);
    }


}
