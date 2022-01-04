using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class _Wasii_MainMenuScript : MonoBehaviour {

	
	bool yesLoadLevel;


	public static _Wasii_MainMenuScript instance;


	public GameObject ExitPanel,MainMenuPanel,LevelSelection;

	public GameObject Playbtn, RateBtn, Morebtn, ExitBtn;

	public static bool adsalternative = false;
	public AudioSource ButtonClickSound;
	public GameObject secondmodelock; 


	public static int index;
    public void Awake()
	{

  
        Time.timeScale = 1f;
        instance = this;
	
	}
    public void Start()
    {
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
	}
    public void ChangePlayer()
	{
		MainMenuPanel.SetActive (false);
		ButtonClickSound.Play ();
	}

	public void OnBackToMain()
	{
		MainMenuPanel.SetActive (true);
		LevelSelection.SetActive (false);

		ButtonClickSound.Play ();

    }

	public void OnOkay()
	{
		MainMenuPanel.SetActive (true);
		ButtonClickSound.Play ();
	}

	public void OnPlayButtonClicked()
	{
       
        PlayerPrefs.SetInt("Selection", index);
        MainMenuPanel.SetActive (false);
		LevelSelection.SetActive (true);

		ButtonClickSound.Play ();	

	}

	public void OnRateButtonClicked()
	{


		ButtonClickSound.Play();
		Application.OpenURL("https://play.google.com/store/apps/details?id=com.animal.hunter.free.hunting.game");
	}

	public void OnMoreButtonClicked()
	{

		ButtonClickSound.Play();
		Application.OpenURL("https://play.google.com/store/apps/developer?id=Hi+Gamez");
        // Privacy
        //Application.OpenURL("https://higames266.blogspot.com/2020/11/hi-gamez.html");

    }

    public void startScene()
	{
		
		
		SceneManager.LoadScene(5);
		
	}
	public void OnClickYes()
	{

		ButtonClickSound.Play ();	
		Application.Quit();

    }
	public void OnExit()
	{
		ButtonClickSound.Play();
        ExitPanel.SetActive (true);
	}
    


    public void OnClickNo()
	{
		

		ButtonClickSound.Play ();
		ExitPanel.SetActive (false);

	}
    public void OnClickContinue()
	{
	//	if(loading)
	//		loading.SetActive (true);
		Invoke("LoadTheScene",10f);
		ButtonClickSound.Play ();


	}

	public void OnClickNewGame()
	{
	//	if(loading)
	//		loading.SetActive (true);
		

		PlayerPrefs.SetInt ("CurrentMission", 0);
		GlobalScripts.CurrLevelIndex = 0;

		ButtonClickSound.Play ();
			Invoke ("LoadTheScene", 15f);
	}

	public void OnClickPrivacy()
	{
		ButtonClickSound.Play ();
		RateBtn.SetActive (false);
		Morebtn.SetActive (false);
		ExitBtn.SetActive (false);
		Playbtn.SetActive (false);
	}
	public void OnClickCross()
	{
		ButtonClickSound.Play ();
		RateBtn.SetActive (true);
		Morebtn.SetActive (true);
		ExitBtn.SetActive (true);
		Playbtn.SetActive (true);
	}
	public void OnReset()
	{
		PlayerPrefs.SetInt ("CurrentMission", 0);
		GlobalScripts.CurrLevelIndex = 0;

		ButtonClickSound.Play ();
	}
	public void OnOK()
	{
        AudioListener.pause = true;


		LevelSelection.SetActive (false);
		ButtonClickSound.Play ();

	  //  StartCoroutine(LoadScene());
		
	}
	void LoadTheScene()
	{
		SceneManager.LoadScene("_PZ_GamePlay");
	}


	IEnumerator GamePlay_Scene()
	{
		yield return new WaitForSeconds (0.1f);
		SceneManager.LoadScene(2);
	}


  

    public void SelectBtton()
    {
        PlayerPrefs.SetInt("Selection", index);
		_Wasii_MainMenuScript.instance.MainMenuPanel.SetActive(false);
		_Wasii_MainMenuScript.instance.LevelSelection.SetActive(true);
    }

    public void Back()
    {

		_Wasii_MainMenuScript.instance.MainMenuPanel.SetActive(true);
		_Wasii_MainMenuScript.instance.LevelSelection.SetActive(false);
    }
    public void RemovedADS()
    {
        InApp_Manager.instance.RemoveAds();
        ButtonClickSound.Play();

    }
    public void unlockeverythings()
    {
        InApp_Manager.instance.unlockall();
        ButtonClickSound.Play();
	
	}

    public void unlockalllevels()
    {
        InApp_Manager.instance.unlocklevels();
        ButtonClickSound.Play();
		
		

	}

}
