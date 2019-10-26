using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour, IUnityAdsListener {

    string gameId = "3337357";
    bool testMode = true;

    private static int attemptCounter = 0;
    // Start is called before the first frame update
    void Start()
    {
        Advertisement.AddListener(this);
        Advertisement.Initialize(gameId, testMode);
        Debug.Log("Advertisement Initialize");
        
    }
    public void ShowAd()
    {
        attemptCounter++;
        if ((attemptCounter % 10) == 0)
        {
            Advertisement.Show("afterdeathad");
        }

        if (ShipController.score > 1)
        {
            OnUnityAdsReady("New_Life_Ad");

        }


        Debug.Log("Attempt Counter: " + attemptCounter);
    }

    public void OnUnityAdsReady(string placementId)
    {
        Advertisement.Show(placementId);
    }

    public void OnUnityAdsDidError(string message)
    {
        Debug.Log("Reward Ad had an error");
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        Debug.Log("Reward Ad Started.");
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        // Define conditional logic for each ad completion status:
        if (showResult == ShowResult.Finished)
        {
            // Reward the user for watching the ad to completion.
        }
        else if (showResult == ShowResult.Skipped)
        {
            // Do not reward the user for skipping the ad.
        }
        else if (showResult == ShowResult.Failed)
        {
            Debug.LogWarning("The ad did not finish due to an error.");
        }
    }
}
