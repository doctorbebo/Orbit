using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;


public class LevelPicker : MonoBehaviour, IUnityAdsListener
{
    public ShipController shipController;

    public enum AdState
    {
        ShwowingBasicAd,
        ShowingNewLifeRewardAd,
        NotShowingAd
    }

    public static AdState adState = AdState.NotShowingAd;
    public static bool isRewardedANewLife;
    public static string newLifeRewardedAd = "New_Life_Ad";
    public static string afterDeathAd = "AfterDeathAd";

    private static int scoreTemp;
    private static int attemptCounter;

    [HideInInspector]
    public bool isAskingAboutRewardAd = false;

    private Scene scene;
    private string gameId = "3337357";
    private bool testMode = true;
    private bool canReloadLevel;

    private void Start()
    {
        canReloadLevel = true;
        scene = SceneManager.GetActiveScene();

        Advertisement.AddListener(this);
        Advertisement.Initialize(gameId, testMode);

        attemptCounter = PlayerPrefs.GetInt("Attempt Counter", 0);
        attemptCounter++;
        PlayerPrefs.SetInt("Attempt Counter", attemptCounter);

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            ReloadLevel();

        if (Input.GetMouseButtonDown(0) && ShipController.isPlayerDead && adState == AdState.NotShowingAd && !isAskingAboutRewardAd)
            ReloadLevel();

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
        if (placementId == afterDeathAd || placementId == newLifeRewardedAd)
        {
            // Debug.Log(placementId + "is pulled from the Internet and is ready.");
        } else
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
    }
    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {

        Time.timeScale = 1f;
        if (placementId == afterDeathAd && adState != AdState.ShowingNewLifeRewardAd)
        {
            adState = AdState.NotShowingAd;
        } 

        if (showResult == ShowResult.Finished && placementId == newLifeRewardedAd)
        {
            adState = AdState.NotShowingAd;
            isAskingAboutRewardAd = false;
            shipController.SetIsRewardedNewLife(true);
            ReloadLevel();
        }
        else if (showResult == ShowResult.Skipped && placementId == newLifeRewardedAd)
        {
            adState = AdState.NotShowingAd;
            isAskingAboutRewardAd = false;
            ReloadLevel();
        }
        else if (showResult == ShowResult.Failed)
        {
            Debug.LogError(placementId + " Failed to Play.");
            adState = AdState.NotShowingAd;
            isAskingAboutRewardAd = false;
        }
    }


    public void ShowAd(string placmentID)
    {
        int attemptsBetweenAds = 8;

        if (attemptCounter % attemptsBetweenAds == 0 && placmentID == afterDeathAd)
        {
            Advertisement.Show(placmentID);
            adState = AdState.ShwowingBasicAd;
            Time.timeScale = 0f;
        }
        else if (placmentID == newLifeRewardedAd)
        {
            Advertisement.Show(placmentID);
            adState = AdState.ShowingNewLifeRewardAd;
        }
        else if (attemptCounter % attemptsBetweenAds == 0)
        {
            Debug.LogError(placmentID + " is incorrect. Double check for typo.");
        }
    }
    public void ReloadLevel()
    {
        if(canReloadLevel)
        {
            SceneManager.LoadScene(scene.buildIndex);
            PlanetBehavior.farthestXPosition = 0;
        }
    }
    public void RestartButtonAd()
    {
        ShowAd(afterDeathAd);
    }
}
