using UnityEngine;
using TMPro;

public class UIBehaviorScript : MonoBehaviour
{

    public GameObject ActivegameplayUi;
    public GameObject endGameUI;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI endGameHighScoreText;
    public TextMeshProUGUI endGameScoreText;


    bool isAlreadySet; // isAlreadySet keeps the game from running UIEndGameBehaviors() every frame.

    // Start is called before the first frame update
    void Awake()
    {
        isAlreadySet = false;
        ActivegameplayUi.SetActive(true);
        endGameUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (ShipController.score > -1)
        {
            scoreText.text = ShipController.score.ToString();
        }
        if(ShipController.isPlayerDead)
        {
            if(!isAlreadySet)
            {
                Debug.Log("High Score " + PlayerPrefs.GetInt("highScore") + "    Score: " + ShipController.score);
                UIEndGameBehaviors();
            }


        }

        if (Input.GetKey(KeyCode.H))
        {
            PlayerPrefs.SetInt("highScore", 0);
            Debug.Log("High Score Reset");
        }


    }
    void UIEndGameBehaviors()
    {
        ActivegameplayUi.SetActive(false);
        endGameUI.SetActive(true);

        if (ShipController.score > PlayerPrefs.GetInt("highScore", 0))
        {
            PlayerPrefs.SetInt("highScore", ShipController.score);
            endGameHighScoreText.text = "NEW HIGH SCORE:  " + ShipController.score;
            endGameScoreText.text = "";
        }
        else
        {
            endGameHighScoreText.text = "Current high score:  " + PlayerPrefs.GetInt("highScore", 0);
            endGameScoreText.text = "Score: " + ShipController.score;

        }

        isAlreadySet = true;

    }
}
