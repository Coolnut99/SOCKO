using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Soomla;
using Soomla.Store;
using Soomla.Profile;

public class SocialMediaController : MonoBehaviour {

	public static SocialMediaController instance;

	private VirtualGood[] goods = null;

	private ItemsForPurchase items;

	void Awake() {
		MakeSingleton();
	}

	// Use this for initialization
	void Start () {
		SoomlaProfile.Initialize();
		items = new ItemsForPurchase();
		SoomlaStore.Initialize(items);
	}
	
	void OnEnable() {
		ProfileEvents.OnLoginFailed += LogInFailed;
		ProfileEvents.OnLoginCancelled += LogInCanceled;
		ProfileEvents.OnLoginFinished += LogInFinished;
		ProfileEvents.OnLogoutFinished += LogOutFinished;
		ProfileEvents.OnLogoutFailed += LogOutFailed;
		ProfileEvents.OnSocialActionFinished += SocialActionFinished;
		ProfileEvents.OnSocialActionFailed += SocialActionFailed;
		ProfileEvents.OnSocialActionCancelled += SocialActionCanceled;

		StoreEvents.OnSoomlaStoreInitialized += StoreInitialized;
		StoreEvents.OnItemPurchased += ItemPurchased;
		
	}

	void OnDisable() {
		ProfileEvents.OnLoginFailed -= LogInFailed;
		ProfileEvents.OnLoginCancelled -= LogInCanceled;
		ProfileEvents.OnLoginFinished -= LogInFinished;
		ProfileEvents.OnLogoutFinished -= LogOutFinished;
		ProfileEvents.OnLogoutFailed -= LogOutFailed;
		ProfileEvents.OnSocialActionFinished -= SocialActionFinished;
		ProfileEvents.OnSocialActionFailed -= SocialActionFailed;
		ProfileEvents.OnSocialActionCancelled -= SocialActionCanceled;

		StoreEvents.OnSoomlaStoreInitialized -= StoreInitialized;
		StoreEvents.OnItemPurchased -= ItemPurchased;

	}

	void MakeSingleton() {
		if (instance != null) {
			Destroy(gameObject);
		} else {
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
	}

	//informing us when the store is initialized
	void StoreInitialized() {
		goods = items.GetGoods();
	}

	//item is purchased from the store
	void ItemPurchased(PurchasableVirtualItem pvi, string payload) {
		BuyCoinsController bcc = GameObject.Find("Buy Coins Controller").GetComponent<BuyCoinsController>();
		bcc.ItemPurchased(pvi.ItemId);
	}

	public void SocialActionFinished(Provider provider, SocialActionType action, string payload) {

	}

	public void SocialActionFailed(Provider provider, SocialActionType action, string message, string payload) {

	}

	public void SocialActionCanceled(Provider provider, SocialActionType action, string payload) {

	}

	//Informs us when login fails
	void LogInFailed(Provider provider, string message, bool b, string payload) {

	}

	//Informs us when login is canceled
	void LogInCanceled(Provider provider, bool b, string payload) {

	}

	//Informs us when login is finished
	void LogInFinished(UserProfile userProfileJson, bool b, string payload) {

	}

	void LogOutFinished(Provider provider) {

	}

	void LogOutFailed(Provider provider, string message) {

	}

	public bool isLoggedIn() {
		return SoomlaProfile.IsLoggedIn(Provider.FACEBOOK);
	}

	public void RateOurApp() {

	}

	public void LogIn() {
		SoomlaProfile.Login (Provider.FACEBOOK);
	}

	public void LogOut() {
		SoomlaProfile.Logout(Provider.FACEBOOK);
	}

	public void Buy(string id) {

		switch(id) {

			case "socko_450_coins":
			VirtualGood item = goods[0];
			try { StoreInventory.BuyItem(item.ItemId);
			} catch(Exception e) {

			}
			break;


			case "socko_1000_coins":
			VirtualGood coins1000 = goods[1];
			try { StoreInventory.BuyItem(coins1000.ItemId);
			} catch(Exception e) {

			}
			break;

			case "socko_2750_coins":
			VirtualGood coins2750 = goods[2];
			try { StoreInventory.BuyItem(coins2750.ItemId);
			} catch(Exception e) {

			}
			break;

			case "socko_6000_coins":
			VirtualGood coins6000 = goods[3];
			try { StoreInventory.BuyItem(coins6000.ItemId);
			} catch(Exception e) {

			}
			break;

			case "socko_10000_coins":
			VirtualGood coins10000 = goods[4];
			try { StoreInventory.BuyItem(coins10000.ItemId);
			} catch(Exception e) {

			}
			break;

			case "socko_remove_ads":
			VirtualGood remove_ads = goods[5];
			try { StoreInventory.BuyItem(remove_ads.ItemId);
			} catch(Exception e) {

			}
			break;


		}

	}
}
