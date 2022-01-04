using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowMediumRectangle : MonoBehaviour
{
    
  //  Destroy MediumRectangle Ads whan player go to Next Scene

    private void OnEnable()
    {
        // Show  Admob Interstitial ads.............................
        if (AdsManager.Instance)
            AdsManager.Instance.ShowMediumRectangle();
    }
    private void OnDisable()
    {
        if (AdsManager.Instance)
        {
            AdsManager.Instance.HideMediumRectangleAd();
        }

    }
}
