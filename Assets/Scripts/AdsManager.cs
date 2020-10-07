using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour, IUnityAdsListener
{
    string gameID = "3852717";
    string myPlacementId = "rewardedVideo";
    bool testMode = true;

    void Start ()
    {
        Advertisement.AddListener(this);
        Advertisement.Initialize(gameID, testMode);
    }
    
    public void ShowRewardedAd()
    {
        Debug.Log("Showing Rewarded Ad");

        if (Advertisement.IsReady("rewardedVideo"))
        {
            Advertisement.Show("rewardedVideo");
        }
        else
        {
            Debug.Log("Rewarded video is not ready at the moment. Try again later.");
        }
    }

    // Implement IUnityAdsListener interface methods:
    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        // Define conditional logic for each ad completion status:
        if (showResult == ShowResult.Finished)
        {
            Debug.Log("You finished the Ad, heres 100 Gems!");
            GameManager.Instance.Player.AddGems(100);
            UIManager.Instance.OpenShop(GameManager.Instance.Player.diamonds);
        }
        else if (showResult == ShowResult.Skipped)
        {
            Debug.Log("You skipped the ad. No Gems for you");
        }
        else if (showResult == ShowResult.Failed)
        {
            Debug.LogWarning("The ad did not finish due to an error.");
        }
    }

    public void OnUnityAdsReady(string placementId)
    {
        // If the ready Placement is rewarded, show the ad:
        if (placementId == myPlacementId)
        {
            // Optional actions to take when the placement becomes ready(For example, enable the rewarded ads button)
        }
    }

    public void OnUnityAdsDidError(string message)
    {   
        // Log the error.
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        // Optional actions to take when the end-users triggers an ad.
    }

    // When the object that subscribes to ad events is destroyed, remove the listener:
    public void OnDestroy()
    {
        Advertisement.RemoveListener(this);
    }

}
