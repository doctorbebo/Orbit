using UnityEngine;
using TMPro;
public class MainMenuUI : MonoBehaviour
{

    public GameObject mainMenuUI;
    public GameObject gamePlayUI;
    public TextMeshProUGUI highScoreText;
    public static bool showMainMenu = true; 

    private void Start()
    {
        if(showMainMenu)
        {
            mainMenuUI.SetActive(true);
            gamePlayUI.SetActive(false);
            Time.timeScale = 0;
            highScoreText.text = "High Score: " + PlayerPrefs.GetInt("highScore", 0);
            showMainMenu = false;
        }
        else
        {
            mainMenuUI.SetActive(false);
            gamePlayUI.SetActive(true);
        }

    }

    public void Play ()
    {
        if(PlayerPrefs.GetInt("highScore", 0) == 0)
        {
            ShipController.gameMode = ShipController.GameMode.Learning;
        }else
        {
            ShipController.gameMode = ShipController.GameMode.Playing;
        }
        Time.timeScale = 1.0f;
        mainMenuUI.SetActive(false);
        gamePlayUI.SetActive(true);
    }
    public void HowtoPlay()
    {
        ShipController.gameMode = ShipController.GameMode.Learning;
        Time.timeScale = 1.0f;
        LevelPicker.ReloadScene();
    }




}
