using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class wasii_GamePlay_UI_Handler : MonoBehaviour
{
	public static wasii_GamePlay_UI_Handler instance;
    public GameObject successPanel, failedPanel, pausedPanel, loadingPanel, loadingPanel1, timeUpText, BloodImage,UICanvas, ObjectivePanel;
	public GameObject[] GunsModel,DummyGuns,GunsBolt;

    public GameObject hitText,ScoreText;
    public Text HuntedAnimal;
    public static bool AnimalDected;
    public Text Distance;
    bool loading;
    public Image loadingimage;
    public int[] kills;
    public Text ObjectiveText,failedText;
    public string story;
    public Text GoodJobText;
    public GameObject GoodJobPanel;
    public GameObject CrossHair_Image;
    public AudioSource YouHit_Target;
    int index;
    private bool ShowAds;
    void Awake()
	{
       // GoodJobText.text = story.ToString();

          instance = this;
        ShowAds = false;
       // ObjectivePanel.SetActive(true);
        UICanvas.SetActive(true);
        for (int i = 0; i < GunsModel.Length; i++)
        {
            GunsModel[i].SetActive(false);
            GunsBolt[i].SetActive(false);
        }
        GunsModel[Guns_Selection.g_no].SetActive(true);
        GunsBolt[Guns_Selection.g_no].SetActive(true);

        for (int i = 0; i < DummyGuns.Length; i++)
        {
            DummyGuns[i].SetActive(false);
        }
        DummyGuns[Guns_Selection.g_no].SetActive(true);
       
        //print("Unlock Start Levevels NO Main End" + PlayerPrefs.GetInt("Unlocked"));
       // print(" Selected Animal Name " + LevelSelection_GZ1.AnimalName + "  " + GlobalScripts.CurrLevelIndex );
  
    }
    void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;


        Resources.UnloadUnusedAssets();
        //if (AdsManager.Instance)
        //    AdsManager.Instance.HideBannerAd();
        ObjectivePanel.SetActive(true);
        UICanvas.SetActive(true);
        if (GlobalScripts.CurrLevelIndex >= 15)
        {
            index = GlobalScripts.CurrLevelIndex - 15;
        }

    
    }

    void Update()
    {
        HuntedAnimal.text = "" + DamageManager.KilledAnimal.ToString() + "/" + kills[GlobalScripts.CurrLevelIndex];
        
        if (loading)
        {
            AudioListener.pause = true;
            loadingimage.fillAmount += Time.deltaTime / 8;
            if (loadingimage.fillAmount >= 1)
            {
                loading = false;
                StartCoroutine("Loading_off");
            }
        }
    }

    public void Level_Completed()
	{
		
		Time.timeScale = 0.001f;
        if (wasii_Game_Controller.Forest == true) { 
        if (PlayerPrefs.GetInt("Unlocked") <= GlobalScripts.CurrLevelIndex)
            PlayerPrefs.SetInt("Unlocked", PlayerPrefs.GetInt("Unlocked") + 1);
            Debug.Log("unlocked level" + PlayerPrefs.GetInt("Unlocked"));
        }
        if (wasii_Game_Controller.Forest == false)
        {
            if (PlayerPrefs.GetInt("Unlocked1") <= GlobalScripts.CurrLevelIndex)
                PlayerPrefs.SetInt("Unlocked1", PlayerPrefs.GetInt("Unlocked1") + 1);
            Debug.Log("unlocked level1" + PlayerPrefs.GetInt("Unlocked"));
        }
        
      
        
        PlayerPrefs.SetInt("Pressed", 1);
      

    }
    int i = 0;
    IEnumerator success_delay()
    {

        GoodJobPanel.SetActive(true);
        YouHit_Target.Play();
        foreach (char c in story)
        {
            GoodJobText.text += c;
            i++;
            yield return new WaitForSecondsRealtime(0.125f);

        }

        if (i == story.Length)
        {
            YouHit_Target.Stop();

            GoodJobText.gameObject.SetActive(false);
            GoodJobText.text = "";
            GoodJobPanel.SetActive(false);
           


            successPanel.SetActive(true);
            // Complete_level();
        }

    }
    public void Level_Failed()
	{

        Time.timeScale = 0.0001f;
        Resources.UnloadUnusedAssets();
        failedPanel.SetActive(true);
       

    }
    
    public void OnTapPause()
	{
       

        GlobalScripts.GameStarted = false;
		Time.timeScale = 0.001f;
        pausedPanel.SetActive(true);
        Resources.UnloadUnusedAssets();



    }
    public void OnTapResume()
	{
        GlobalScripts.GameStarted = true;
        Time.timeScale = 1f;
        pausedPanel.SetActive(false);
        Invoke("CloseAds",5f);
    
    }
    
     public void OnTapNext()
	{
       
        Resources.UnloadUnusedAssets();
        Time.timeScale = 1f;
        if (GlobalScripts.CurrLevelIndex >= 15)
        {
            SceneManager.LoadScene("wasii_Main_menu");
         //   _Wasii_MainMenuScript.instance.secondmodelock.SetActive(false);
        }
        else
        {
            DamageManager.KilledAnimal = 0;
            GlobalScripts.CurrLevelIndex++;
            
                SceneManager.LoadScene("wasii_LevelSelections");
        }
    }
    public void OnTapRestart()
	{
        Resources.UnloadUnusedAssets();
        Time.timeScale = 1f;
        // print("currentlevel :"+GlobalScripts.CurrLevelIndex);
     
        if (successPanel.activeInHierarchy ||failedPanel.activeInHierarchy || pausedPanel.activeInHierarchy)
        {
            successPanel.SetActive(false);
            failedPanel.SetActive(false);
            pausedPanel.SetActive(false);
            loadingPanel.SetActive(true);
        }
        
        loading = true;
        DamageManager.KilledAnimal = 0;
        
        //StartCoroutine("Loading_off");
    }

    IEnumerator Loading_off()
    {
        yield return new WaitForSeconds(0.5f);
        loadingimage.fillAmount = 0;
        loading=false;
        AudioListener.pause = false;
        loadingPanel.SetActive(false);
      
       
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnTapRestart_Success()
	{
        Resources.UnloadUnusedAssets();
        Time.timeScale = 1f;
        DamageManager.KilledAnimal = 0;
        //loadingPanel1.SetActive(true);
        
        SceneManager.LoadScene (SceneManager.GetActiveScene().name);
	}
    public void OnTapHome()
	{
        Resources.UnloadUnusedAssets();
        Time.timeScale = 1f;
        DamageManager.KilledAnimal = 0;
        SceneManager.LoadScene("wasii_Main_menu");
     
    }
    public void hitText_Off()
    {
        Invoke("text_off", 2.5f);
    }
    void text_off()
    {
        hitText.SetActive(false);
        ScoreText.SetActive(false);
    }
    public void HeadShotScore() {
        hitText.SetActive(true);
        ScoreText.SetActive(true);
        hitText.GetComponent<Text>().text = "HEAD SHOT";
        ScoreText.GetComponent<Text>().text= "$100";
       // PlayerPrefManager._instance.SetGameCoins(100);
        //PlayerPrefs.SetInt("cashin",PlayerPrefs.GetInt("cashin")+100);
        Invoke("text_off", 2.5f);
    }
    public void BodyShotScore() {
        hitText.SetActive(true);
        ScoreText.SetActive(true);
        hitText.GetComponent<Text>().text = "BODY SHOT";
        ScoreText.GetComponent<Text>().text = "$50";
       // PlayerPrefManager._instance.SetGameCoins(50);
        //PlayerPrefs.SetInt("cashin", PlayerPrefs.GetInt("cashin") + 50);
        Invoke("text_off", 2.5f);
    }
    public void BrainShotScore()
    {
        hitText.SetActive(true);
        ScoreText.SetActive(true);
        hitText.GetComponent<Text>().text = "BRAIN SHOT";
        ScoreText.GetComponent<Text>().text = "$200";
       // PlayerPrefManager._instance.SetGameCoins(200);
        //PlayerPrefs.SetInt("cashin", PlayerPrefs.GetInt("cashin") + 50);
        Invoke("text_off", 2.5f);
    }
    public void HeartShotScore()
    {
        hitText.SetActive(true);
        ScoreText.SetActive(true);
        hitText.GetComponent<Text>().text = "HEART SHOT";
        ScoreText.GetComponent<Text>().text = "$100";
       // PlayerPrefManager._instance.SetGameCoins(100);
        //PlayerPrefs.SetInt("cashin", PlayerPrefs.GetInt("cashin") + 50);
        Invoke("text_off", 2.5f);
    }
    public void LungsShotScore()
    {
        hitText.SetActive(true);
        ScoreText.SetActive(true);
        hitText.GetComponent<Text>().text = "LUNGS SHOT";
        ScoreText.GetComponent<Text>().text = "$150";
       // PlayerPrefManager._instance.SetGameCoins(150);
        //PlayerPrefs.SetInt("cashin", PlayerPrefs.GetInt("cashin") + 50);
        Invoke("text_off", 2.5f);
    }
    //public void CompleteLevelDelay()
    //{
        
        
    //}
    public void Complete_level() {
        if (GlobalScripts.CurrLevelIndex <= 15)
        {
            if (kills[GlobalScripts.CurrLevelIndex] <= DamageManager.KilledAnimal)
            {

                GlobalScripts.GameStarted = false;
                GlobalScripts.timeOver = true;
                GoodJobText.gameObject.SetActive(true);
                StartCoroutine("success_delay");
                Invoke("Lev_Com_Wait", 3f);

                if (GlobalScripts.CurrLevelIndex != 12 || GlobalScripts.CurrLevelIndex != 13 || GlobalScripts.CurrLevelIndex != 14)
                {
                    
                }
                if (GlobalScripts.CurrLevelIndex == 12 && bulletRayHit.lung_s)
                {
                   // Invoke("Lev_Com_Wait", 3f);
                }
                else if (GlobalScripts.CurrLevelIndex == 12 && !bulletRayHit.lung_s)
                {
                    //Invoke("Lev_Fail_Text", 1.5f);
                    //Invoke("Lev_Fail_Wait", 3f);
                }

                if (GlobalScripts.CurrLevelIndex == 13 && bulletRayHit.brain_s && !bulletRayHit.lung_s && !bulletRayHit.Heart_s)
                {
                   // Invoke("Lev_Com_Wait", 3f);
                }
                else if (GlobalScripts.CurrLevelIndex == 13 && !bulletRayHit.brain_s)
                {
                  //  Invoke("Lev_Fail_Text", 1.5f);
                   // Invoke("Lev_Fail_Wait", 3f);
                }
                if (GlobalScripts.CurrLevelIndex == 14 && bulletRayHit.Heart_s)
                {
                  //  Invoke("Lev_Com_Wait", 3f);
                }
                else if (GlobalScripts.CurrLevelIndex == 14 && !bulletRayHit.Heart_s)
                {
                  //  Invoke("Lev_Fail_Text", 1.5f);
                   // Invoke("Lev_Fail_Wait", 3f);
                }
            }
        }
        else
        {
            if (kills[index] <= DamageManager.KilledAnimal)
            {
                GlobalScripts.GameStarted = false;
                GlobalScripts.timeOver = true;

                if (index != 12 || index != 13 || index != 14)
                {
                    Invoke("Lev_Com_Wait", 3f);
                }
                if (index == 12 && bulletRayHit.lung_s)
                {
                    Invoke("Lev_Com_Wait", 3f);
                }
                else if (index == 12 && !bulletRayHit.lung_s)
                {
                    Invoke("Lev_Fail_Text", 1.5f);
                    Invoke("Lev_Fail_Wait", 3f);
                }

                if (index == 13 && bulletRayHit.brain_s && !bulletRayHit.lung_s && !bulletRayHit.Heart_s)
                {
                    Invoke("Lev_Com_Wait", 3f);
                }
                else if (index == 13 && !bulletRayHit.brain_s)
                {
                    Invoke("Lev_Fail_Text", 1.5f);
                    Invoke("Lev_Fail_Wait", 3f);
                }
                if (index == 14 && bulletRayHit.Heart_s)
                {
                    Invoke("Lev_Com_Wait", 3f);
                }
                else if (index == 14 && !bulletRayHit.Heart_s)
                {
                    Invoke("Lev_Fail_Text", 1.5f);
                    Invoke("Lev_Fail_Wait", 3f);
                }
            }
        }
    }
    public void Lev_Fail_Text()
    {
        if(CrossHair_Image.activeInHierarchy)
        {
            CrossHair_Image.SetActive(false);
        }
        failedText.gameObject.SetActive(true);
        failedText.text = "Sorry You Failed to Shoot the Target Point";
    }
    public void Lev_Fail_Wait()
    {
        Debug.Log("fail2");
        Level_Failed();
        failedText.gameObject.SetActive(false);
    }
    public void Lev_Com_Wait() {
       // StartCoroutine("success_delay");
        Level_Completed();
        
    }
    public void OnObjectiveOk()
    {

        GlobalScripts.GameStarted = true;
        GlobalScripts.timeOver = false;
        ObjectivePanel.SetActive(false);

    }
        public void unlockeverythings()
        {
           InApp_Manager.instance.unlockall();
        }

    public void remove_ads()
    {
        InApp_Manager.instance.RemoveAds();

    }
    public void unlockalllevels()
        {
          InApp_Manager.instance.unlocklevels();

       }







    
}
