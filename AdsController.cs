using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;
using ChartboostSDK;

public class AdsController : MonoBehaviour {

	public static AdsController instance;

	private const string app_id = "1533311";

	public bool canShowChartboostInterstitial;
	public bool canShowChartboostVideo;

	void Awake() {
		MakeSingleton();

		if(!canShowChartboostInterstitial) {
			LoadChartboostInterstitialAds();
		}

		if(!canShowChartboostVideo) {
			LoadChartboostVideoAds();
		}

		LoadUnityAds();
	}

	void OnLevelWasLoaded() {
		if(SceneManager.GetActiveScene().name == "Gameplay" || SceneManager.GetActiveScene().name == "Challenges Gameplay") {
			MasterControl.instance.numberOfAds++;
			int x = MasterControl.instance.numberOfAds % 4;
			//Debug.Log("Modulus operator is: " + x);
			if(MasterControl.instance.numberOfAds == 1 || x == 0) {
				if(MasterControl.instance.canShowAds > 0) {
					//Show ad
					//Debug.Log("You will see an ad here.");
					if(canShowChartboostInterstitial) {
						ShowChartboostInterstitial();
					} else {
						LoadChartboostInterstitialAds();
					}
				} else {
					//Debug.Log("No ad shown. Make sure the no-ad option is bought. if so, THANK YOU FOR YOUR PURCHASE! :D");
				}

			}
		}
	}

	void MakeSingleton() {
		if(instance != null) {
			Destroy(gameObject);
		} else {
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
	}


	void OnEnable() {
		Chartboost.didCompleteRewardedVideo += VideoCompleted;
		Chartboost.didCacheInterstitial += DidCacheInterstital;
		Chartboost.didDismissInterstitial += DidDismissInterstitial;
		Chartboost.didCloseInterstitial += DidCloseInterstitial;
		Chartboost.didCacheRewardedVideo += DidCacheVideo;
		Chartboost.didFailToLoadInterstitial += FailedToLoadInterstitial;
		Chartboost.didFailToLoadRewardedVideo += FailedToLoadVideo;
	}

	void OnDisable() {
		Chartboost.didCompleteRewardedVideo -= VideoCompleted;
		Chartboost.didCacheInterstitial -= DidCacheInterstital;
		Chartboost.didDismissInterstitial -= DidDismissInterstitial;
		Chartboost.didCloseInterstitial -= DidCloseInterstitial;
		Chartboost.didCacheRewardedVideo -= DidCacheVideo;
		Chartboost.didFailToLoadInterstitial -= FailedToLoadInterstitial;
		Chartboost.didFailToLoadRewardedVideo -= FailedToLoadVideo;
	}



	public void VideoCompleted(CBLocation location, int reward) {
		canShowChartboostVideo = false;
		GameObject g = GameObject.Find("Store Controller");

		if(g != null) {
			g.GetComponent<StoreController>().WatchedVideoGiveAReward();
		}

		LoadChartboostVideoAds();
	}

	public void DidCacheInterstital(CBLocation location) {
		canShowChartboostInterstitial = true;
	}

	public void DidDismissInterstitial(CBLocation arg1) {
		canShowChartboostInterstitial = false;
		LoadChartboostVideoAds();
	}

	public void DidCloseInterstitial(CBLocation arg1) {
		canShowChartboostInterstitial = false;
		LoadChartboostVideoAds();
	}

	public void DidCacheVideo(CBLocation location) {
		canShowChartboostVideo = true;
	}

	void FailedToLoadInterstitial(CBLocation arg1, CBImpressionError arg2) {
		canShowChartboostInterstitial = false;
		LoadChartboostInterstitialAds();
	}

	void FailedToLoadVideo(CBLocation arg1, CBImpressionError arg2) {
		canShowChartboostVideo = false;

		GameObject g = GameObject.Find("Store Controller");
		if(g != null) {
			g.GetComponent<StoreController>().FailedToLoadTheVideo();
		}

		LoadChartboostVideoAds();

	}

	public void LoadChartboostVideoAds() {
		Chartboost.cacheRewardedVideo(CBLocation.Default);
	}

	public void LoadChartboostInterstitialAds() {
		Chartboost.cacheInterstitial(CBLocation.Default);
	}

	public void ShowChartboostInterstitial() {
		if(canShowChartboostInterstitial) {
			Chartboost.showInterstitial(CBLocation.Default);
		} else {
			LoadChartboostInterstitialAds();
		}
	}

	public void ShowChartboostChartboostVideo() {
		if(canShowChartboostVideo) {
			Chartboost.showRewardedVideo(CBLocation.Default);
		} else {
			LoadChartboostVideoAds();
		}
	}

	public void LoadUnityAds() {
		if(Advertisement.isSupported) {
			//Advertisement.allowPrecache = true;
			Advertisement.Initialize(app_id, false); //Change to false when game is in production!
		}
	}

	//Temporary -- use this in the game (Gameplay/Challenges Gameplay) itself to give extra lives or coins?
	public void ShowUnityAds() {
		if(Advertisement.IsReady()) {
			GameObject g = GameObject.Find("Emergency Store Controller");
			Advertisement.Show(null, new ShowOptions() {
				//pause = true,
				resultCallback = result => {
					switch(result) {
						case ShowResult.Finished:
						g.GetComponent<EmergencyStoreController>().VideoWatchedGiveBonusCoins();
						LoadUnityAds();
						break;

						case ShowResult.Failed:
						g.GetComponent<EmergencyStoreController>().VideoNotLoadedOrUserSkippedTheVideo();
						LoadUnityAds();
						break;

						case ShowResult.Skipped:
						g.GetComponent<EmergencyStoreController>().VideoNotLoadedOrUserSkippedTheVideo();
						LoadUnityAds();
						break;
					}
				}
			});
		} else {
			GameObject g = GameObject.Find("Emergency Store Controller");
			if(g != null) {
				g.GetComponent<EmergencyStoreController>().VideoNotLoadedOrUserSkippedTheVideo();
			}
			LoadUnityAds();
		}
	}
}
