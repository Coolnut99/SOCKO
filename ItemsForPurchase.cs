using Soomla.Store;

public class ItemsForPurchase : IStoreAssets {

	public const string COINS_450 = "socko_450_coins";
	public const string COINS_1000 = "socko_1000_coins";
	public const string COINS_2750 = "socko_2750_coins";
	public const string COINS_6000 = "socko_6000_coins";
	public const string COINS_10000 = "socko_10000_coins";
	public const string REMOVE_ADS = "socko_remove_ads";

	// Uodate 0 if you add more available items later or you will get errors
	public int GetVersion() {
		return 0;
	}

	public VirtualCurrency[] GetCurrencies()
	{
		return new VirtualCurrency[]{};
	}

	public VirtualCurrencyPack[] GetCurrencyPacks()
	{
		return new VirtualCurrencyPack[] {};
	}

	public VirtualCategory[] GetCategories()
	{
		return new VirtualCategory[] {};
	}

	public VirtualGood[] GetGoods() {
		return new VirtualGood[] {COINS450, COINS1000, COINS2750, COINS6000, COINS10000, ADS_REMOVE};
	}

	public static VirtualGood COINS450 = new SingleUseVG(
		"450 coins",
		"450 coins to use for fists, lives, powerups, and the like!",
		COINS_450,
		new PurchaseWithMarket(new MarketItem(COINS_450, 1.99))
		//new PurchaseWithMarket(new MarketItem(COINS_450, MarketItem.Consumable.CONSUMABLE, 1.99))
		);

	public static VirtualGood COINS1000 = new SingleUseVG(
		"1000 coins",
		"1000 coins to use for fists, lives, powerups, and the like!",
		COINS_1000,
		new PurchaseWithMarket(new MarketItem(COINS_1000, 3.99))
		);

	public static VirtualGood COINS2750 = new SingleUseVG(
		"2750 coins",
		"2750 coins to use for fists, lives, powerups, and the like!",
		COINS_2750,
		new PurchaseWithMarket(new MarketItem(COINS_2750, 9.99))
		);

	public static VirtualGood COINS6000 = new SingleUseVG(
		"6000 coins",
		"6000 coins to use for fists, lives, powerups, and the like!",
		COINS_6000,
		new PurchaseWithMarket(new MarketItem(COINS_6000, 19.99))
		);

	public static VirtualGood COINS10000 = new SingleUseVG(
		"10000 coins",
		"10000 coins to use for fists, lives, powerups, and the like!",
		COINS_10000,
		new PurchaseWithMarket(new MarketItem(COINS_10000, 39.99))
		);

	public static VirtualGood ADS_REMOVE = new LifetimeVG(
		"Remove all ads",
		"Permanently makes the game (mostly) ad-free! You can still watch ads for coins.",
		REMOVE_ADS,
		new PurchaseWithMarket(REMOVE_ADS, 1.99)
		);
}
