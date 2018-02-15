using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class AdsManager : MonoBehaviour {
   
	// Use this for initialization
	void Start () {
        Advertisement.Initialize("1702417");
        

	}
    public void ShowAdsM()
    {
        StartCoroutine(ShowAds());
    }
    IEnumerator ShowAds()
    {
        if (!Advertisement.IsReady())
            yield return null;
            Advertisement.Show();
        Debug.Log(Advertisement.version);
    }
	
	
}
