using UnityEngine;
using TMPro;

public class UIBehaviorScript : MonoBehaviour
{
    public LevelPicker levelPicker;
    public GameObject ActivegameplayUi;
    public GameObject endGameUI;
    public GameObject offerRewardUI;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI endGameHighScoreText;
    public TextMeshProUGUI endGameScoreText;
    public TextMeshProUGUI rewardGameHighScoreText;
    public TextMeshProUGUI rewardGameScoreText;

    private static bool canBeRewardedRestart = true;



    bool isAlreadySet; // isAlreadySet keeps the game from running UIEndGameBehaviors() every frame.

    // Start is called before the first frame update
    void Awake()
    {
        isAlreadySet = false;
        ActivegameplayUi.SetActive(true);
        endGameUI.SetActive(false);
        offerRewardUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (ShipController.score > -1)
        {
            scoreText.text = ShipController.score.ToString();
        }
        if (ShipController.isPlayerDead)
        {
            if (!isAlreadySet)
            {
                //Debug.Log("High Score " + PlayerPrefs.GetInt("highScore") + "    Score: " + ShipController.score);
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
        int highScore = PlayerPrefs.GetInt("highScore", 0);
        bool showRewardUI = false;

        if (ShipController.score > -1 && canBeRewardedRestart)
        {
            showRewardUI = true;
            canBeRewardedRestart = false;
        }else
        {
            canBeRewardedRestart = true;
        }
        levelPicker.ShowAd(LevelPicker.afterDeathAd);

        if (ShipController.score > highScore && showRewardUI)
        {
            levelPicker.isAskingAboutRewardAd = true;
            offerRewardUI.SetActive(true);
            PlayerPrefs.SetInt("highScore", ShipController.score);
            rewardGameHighScoreText.text = "NEW HIGH SCORE:  " + ShipController.score;
            rewardGameScoreText.text = "";
        }
        else if(showRewardUI && ShipController.score <= highScore)
        {
            levelPicker.isAskingAboutRewardAd = true;
            offerRewardUI.SetActive(true);
            rewardGameHighScoreText.text = "Current high score:  " + PlayerPrefs.GetInt("highScore", 0);
            rewardGameScoreText.text = "Score: " + ShipController.score;
        }
        else if (ShipController.score > highScore)
        {
            endGameUI.SetActive(true);
            PlayerPrefs.SetInt("highScore", ShipController.score);
            endGameHighScoreText.text = "NEW HIGH SCORE:  " + ShipController.score;
            endGameScoreText.text = "";
        }
        else
        {
            endGameUI.SetActive(true);
            endGameHighScoreText.text = "Current high score:  " + PlayerPrefs.GetInt("highScore", 0);
            endGameScoreText.text = "Score: " + ShipController.score;
        }

            isAlreadySet = true;

        }
    public void ExtraLifeButtonPressed()
    {
        levelPicker.ShowAd(LevelPicker.newLifeRewardedAd);
    }
}


