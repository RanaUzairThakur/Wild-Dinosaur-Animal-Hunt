using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LevelSelections_wasii : MonoBehaviour 
{
    public GameObject  LevelsSelectionPanel;
    public GameObject[] lookAtArray;
    public GameObject[] lookAtimage;
	public GameObject[] Levels;
    public GameObject[] LevelSelectors;

    public GameObject MainCamera;
    public GameObject loading;

    public Text loadingText;
    public GameObject loadingBar;


    int UnlockedLevels;


	
    public Button Start_button;
    public GameObject firstscrollbar, secondcrollbar;
   
    public AudioSource ButtonClickSound;
    private int count1;
    private int count21;
    public static string AnimalName;
    private int coun2;

    
    void OnEnable()
	{
          Start_ ();
	}
    void OnDisable()
    {
       

        Start_button.gameObject.SetActive(false);
        int i;
       
       
    }
 
  
    void Start_ () 
	{

        

       // count1 = PlayerPrefs.GetInt("count");
       // Debug.Log(count1);
        //PlayerPrefs.SetInt("Select_Levels", 1);
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
        
        if (UnlockedLevels>Levels.Length)
		{
			PlayerPrefs.SetInt ("Unlocked", Levels.Length);

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
                //Debug.Log("Mode_1" + UnlockedLevels);
               
                   Levels[i].gameObject.transform.GetChild(1).gameObject.SetActive(false);
                   Levels[i].GetComponent<Button>().interactable = true;
                    Levels[UnlockedLevels].gameObject.transform.GetChild(2).gameObject.SetActive(true);
                    Levels[UnlockedLevels].GetComponent<Animator>().enabled = true;
                if (PlayerPrefs.GetInt("Select_Levels") == 1)
                {
                    //Debug.Log("Mode_2" + PlayerPrefs.GetInt("Unlocked1")) ;
                    lookAtArray[PlayerPrefs.GetInt("Unlocked1")].gameObject.transform.GetChild(2).gameObject.SetActive(true);
                    lookAtArray[PlayerPrefs.GetInt("Unlocked1")].GetComponent<Animator>().enabled = true;

                    //Debug.Log("Mode_3" + " & " + PlayerPrefs.GetInt("Unlocked1"));
                }
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
        

      
       

        if(index<=UnlockedLevels)
        Start_button.gameObject.SetActive(true);
        else
        Start_button.gameObject.SetActive(false);

       
      
        ButtonClickSound.Play();

      
    }
    public void Start_Game()
    {

        AudioListener.pause = true;
        if (loading)
            loading.SetActive(true);

        //LevelSelection.SetActive(false);
        ButtonClickSound.Play();

        StartCoroutine(LoadScene());
       

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
        ButtonClickSound.Play();
    }
   
	
    public void backtoLevel()
    {
        //CustomAnalytics.logLevelStarted("Back", "Environment_Sel");
    }
    public void Environmen_Sel(bool Forest)
    {
        ButtonClickSound.Play();
    
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
