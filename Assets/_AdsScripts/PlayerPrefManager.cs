using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerPrefManager : MonoBehaviour {

	public static PlayerPrefManager _instance;

	private void Awake()
	{
		if (_instance == null)
		{
			_instance = this;
		}
		else if (_instance != this)
		{
			Destroy(gameObject);
		}
	}

	public static PlayerPrefManager Instance
	{

		get
		{

			if (_instance == null)
			{

				try
				{
					_instance = GameObject.FindObjectOfType<PlayerPrefManager>();
				}
				catch (Exception e)
				{
					Debug.Log(e.Message);
					_instance = new PlayerPrefManager();
				}

			}

			return _instance;

		}

	}


	void Start()
	{
		Debug.Log("initialized PlayerPrefManager");

	}
    public void RemoveAds()
    {
		AdsManager.Instance.HideBannerAd();
		AdsManager.Instance.HideMediumRectangleAd();
        PlayerPrefs.SetInt("RemoveAds", 1);
    }
    public void unlocklevels()
    {
        PlayerPrefs.SetInt("Unlocked1", 15);
    }


    public bool IsAdsRemoved()
    {

        if (PlayerPrefs.GetInt("RemoveAds") == 0)
            return false;
        else
            return true;
    }

    #region SetReward Function

    public void SetGameCoins(int coins)
	{
		int TempCoins=PlayerPrefs.GetInt("Coins");
		TempCoins +=coins;
		PlayerPrefs.SetInt("Coins", TempCoins);
	}

	public void SetGameDiamonds(int diamond)
	{
		int Tempdiamonds=PlayerPrefs.GetInt("Diamonds");
		Tempdiamonds +=diamond;
		PlayerPrefs.SetInt("Diamonds", Tempdiamonds);
	}

	public void SetGameGems(int Gems)
	{
		int TempGems=PlayerPrefs.GetInt("Gems");
		TempGems +=Gems;
		PlayerPrefs.SetInt("Gems", TempGems);
	}

	public void SetGameReward(int coins,int diamond,int Gems)
	{
		SetGameCoins (coins);
		SetGameDiamonds (diamond);
		SetGameGems (Gems);

	}

	#endregion

	#region GetReward Function

	public int GetGameCoins()
	{
		int TempCoins=PlayerPrefs.GetInt("Coins");

		return TempCoins;
	}

	public int GetGameDiamonds()
	{
		int Tempdiamonds=PlayerPrefs.GetInt("Diamonds");

		return Tempdiamonds;
	}

	public int GetGameGems()
	{
		int TempGems=PlayerPrefs.GetInt("Gems");

		return TempGems;
	}

	#endregion

}
