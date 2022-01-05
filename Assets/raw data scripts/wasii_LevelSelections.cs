using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class wasii_LevelSelections : MonoBehaviour 
{
    public GameObject  LevelsSelectionPanel;
    public GameObject[] lookAtArray;
	public GameObject[] Levels;
    public GameObject[] LevelSelectors;

    public GameObject MainCamera;
    public GameObject loading;

    public Text loadingText;
    public GameObject loadingBar;


    int UnlockedLevels;


	
    public Button Start_button;
    public GameObject firstscrollbar, secondcrollbar;
    public Text Obj_text;

    public static string AnimalName;
    void OnEnable()
	{
        //PlayerPrefs.SetInt("Unlocked", 29);

        Start_ ();
	}
    void OnDisable()
    {
       

        //Start_button.gameObject.SetActive(false);
        
      
       
    }
    // Use this for initialization
    void Start_ () 
	{

        //Debug.Log( PlayerPrefs.GetInt("Unlocked"));
        //Game_Controller_GZ1.Forest = true;
        if (PlayerPrefs.GetInt("Select_Levels") == 0)
        {


            firstscrollbar.gameObject.SetActive(true);
            secondcrollbar.gameObject.SetActive(false);

        }
        if (PlayerPrefs.GetInt("Select_Levels") == 1)
        {


            secondcrollbar.gameObject.SetActive(true);
            firstscrollbar.gameObject.SetActive(false);

        }

        UnlockedLevels = PlayerPrefs.GetInt("Unlocked");
        if (UnlockedLevels>=Levels.Length)
		{
			PlayerPrefs.SetInt ("Unlocked", Levels.Length-1);

		}
		int i = 0;
		
			for (i = 0; i < Levels.Length; i++) 
			{
				Levels [i].SetActive (true);
			}
		

		for (i = 0; i < Levels.Length; i++) 
		{
			if (i <= UnlockedLevels) 
			{
			
                Levels[i].gameObject.transform.GetChild(1).gameObject.SetActive(false);

                Levels [i].GetComponent<UnityEngine.UI.Button> ().interactable = true;
			}
			else
			{
	
                Levels[i].gameObject.transform.GetChild(1).gameObject.SetActive(true);

			}
		}
        
    }


    public void SelectLevelAnimal(string animalname)
    {
        AnimalName = animalname;
    }
  
    
    public void AllSelectorsOff()
    {
        for(int i =0; i< LevelSelectors.Length; i++)
        {
            LevelSelectors[i].SetActive(false);
        }
    }

    public void SelectLevel(int index)
	{

        GlobalScripts.CurrLevelIndex = index;
        AllSelectorsOff();
        LevelSelectors[index].SetActive(true);
        

        switch (index)
        {
            case 0:
                Obj_text.text = "Aim for the Body";
                break;
            case 1:
                Obj_text.text = "Kill With Head Shot ";
                break;
            case 2:
                Obj_text.text="Hunt The Deer With LUNGS Shot";
                break;
            case 3:
                Obj_text.text="Hunt With a BRAIN SHOT";
                break;
            case 4:
               Obj_text.text="Kill Bear With HEART SHOT";
                break;
            case 5:
                Obj_text.text = "Kill Hippo With HEAD SHOT";
                break;
            case 6:
                 Obj_text.text = "Kill RHINO With HEAD SHOT"; 
                break;
            case 7:
                Obj_text.text = "Becarfull From Attack"; 
                break;
            case 8:
                Obj_text.text = "Kill ZEBRA With HEAD SHOT"; 
                break;
            case 9:
                Obj_text.text = "Kill Wolf With a HEAD SHOT";
                break;
            case 10:
                Obj_text.text = "Kill Wolf With a HEAD SHOT"; 
                break;
            case 11:
                Obj_text.text = "Kill Wolf With a HEAD SHOT"; 
                break;
            case 12:
                Obj_text.text = "Kill Wolf With a HEAD SHOT"; 
                break;
            case 13:
                Obj_text.text = "Kill Wolf With a HEAD SHOT"; 
                break;
            case 14:
                Obj_text.text = "Kill Wolf With a HEAD SHOT"; 
                break;

        }
    
       

        if(index<=UnlockedLevels)
        Start_button.gameObject.SetActive(true);
        else
        Start_button.gameObject.SetActive(false);

      
       // GameObject.Find("ButtonClickSound").GetComponent<AudioSource>().Play();

 
    }
    public void Start_Game()
    {

        AudioListener.pause = true;
        if (loading)
            loading.SetActive(true);

        //LevelSelection.SetActive(false);
       // GameObject.Find("ButtonClickSound").GetComponent<AudioSource>().Play();

        StartCoroutine(LoadScene());




        //_PZMainMenuScript.instance.OnOK();

        //CustomAnalytics.logLevelStarted("Play", "Gun_Selection");
    }


    IEnumerator LoadScene()
    {
        yield return null;

        //Begin to load the Scene you specify
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("wasii_GamePlay");
        //Don't let the Scene activate until you allow it to
        asyncOperation.allowSceneActivation = false;
        //Debug.Log("Pro :" + asyncOperation.progress);
        //When the load is still in progress, output the Text and progress bar
        while (!asyncOperation.isDone)
        {
            //Output the current progress
            loadingBar.GetComponent<Image>().fillAmount += 0.002f;
            string percent = (loadingBar.GetComponent<Image>().fillAmount * 100).ToString("F0");
            loadingText.text = string.Format("<size=35>{0}%</size>", percent);
            // Check if the load has finished
            if (asyncOperation.progress >= 0.9f && loadingBar.GetComponent<Image>().fillAmount == 1.0f)
            {
                //Change the Text to show the Scene is ready
                asyncOperation.allowSceneActivation = true;

            }

            yield return null;
        }
    }
    public void Backtomain()
    {
        for ( int i = 0; i < Levels.Length; i++)
        {
            //Levels[i].gameObject.transform.GetChild(4).gameObject.SetActive(false);
        }
       // GameObject.Find("ButtonClickSound").GetComponent<AudioSource>().Play();
    }
   
	
    public void backtoLevel()
    {
        //CustomAnalytics.logLevelStarted("Back", "Environment_Sel");
        
    }
    public void Environmen_Sel(bool Forest)
    {
        //GameObject.Find("ButtonClickSound").GetComponent<AudioSource>().Play();

        if (Forest)
        {
            wasii_Game_Controller.Forest = true;
        }
        else
        {
            wasii_Game_Controller.Forest = false;
        }

    }

    public void MainBack()
        {
        SceneManager.LoadScene("wasii_Main_menu");
    }

    public void UnlockallLevels()
    {
        InApp_Manager.instance.unlocklevels();
    }
}
