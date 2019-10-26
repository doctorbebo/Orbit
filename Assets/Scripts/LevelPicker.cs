using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;

public class LevelPicker : MonoBehaviour, IUnityAdsListener 
{
    public static bool isRewardedANewLife;

    Scene scene;
    string gameId = "3337357";
    bool testMode = false;
    static int attemptCounter;

    private static int scoreTemp;

    string newLifeRewardedAd = "New_Life_Ad";
    string basicAd = "AfterDeathAd";

    bool isAdSHowing;

    private void Start()
    {
        scene = SceneManager.GetActiveScene();

        Advertisement.AddListener(this);
        Advertisement.Initialize(gameId, testMode);

        attemptCounter = PlayerPrefs.GetInt("Attempt Counter", 0);
        attemptCounter++;
        PlayerPrefs.SetInt("Attempt Counter", attemptCounter);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReloadLevel();
        }
        if (Input.GetMouseButtonDown(0) && ShipController.isPlayerDead)
        { 
            ReloadLevel();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (MainMenuUI.showMainMenu == true)
            {
                Application.Quit();
            }
            else
            {
                MainMenuUI.showMainMenu = true;
                ReloadLevel();
            }
        }

    }

    public void OnUnityAdsReady(string placementId)
    {
        if(placementId == basicAd || placementId == newLifeRewardedAd)
        {
          // Debug.Log(placementId + "is pulled from the Internet and is ready.");
        }else
        {
           Debug.LogError("Ad placementId Strings do not match.");
        }
    }

    public void OnUnityAdsDidError(string message)
    {
        Debug.LogError(message);
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        isAdSHowing = true;
       // Debug.Log(placementId + " started.");

    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        Time.timeScale = 1f;

        if (showResult == ShowResult.Finished && placementId == newLifeRewardedAd)
        {
            RewardPlayer();
        }
        else if (showResult == ShowResult.Failed)
        {
           Debug.LogError(placementId + " Failed to Play.");
        }
    }

     public void ReloadLevel()
    {
        if (isAdSHowing)
        {
            return;
        }
        SaveTempScore();

        SceneManager.LoadScene(scene.buildIndex);
        PlanetBehavior.farthestXPosition = 0;

       if(attemptCounter %  4 == 0)
        {
            Time.timeScale = 0f;
            ShowAd(basicAd);
            isAdSHowing = true;
        }
    }

    void ShowAd(string placementId)
    {
        Advertisement.Show(placementId);
    }

    private static void SaveTempScore()
    {
        scoreTemp = ShipController.score;
    }
    private static void RewardPlayer()
    {
        ShipController.score = scoreTemp;
    }
}
