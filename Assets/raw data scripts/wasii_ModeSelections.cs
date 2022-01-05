using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class wasii_ModeSelections : MonoBehaviour
{
    public static wasii_ModeSelections inst;

    public AudioClip buttonSound;
    public int levelOpen;
  
    public GameObject Scrol_1, Scrol_2;
    
    public void Awake()
    {
        inst = this;
    }

    void Start()
    {


        Time.timeScale = 1f;


        if (PlayerPrefs.GetInt("ModeOpen") <= 0)
        {
            PlayerPrefs.SetInt("ModeOpen", 1);
        }
        if (PlayerPrefs.GetInt("ModeOpen") >= 2)
        {
            PlayerPrefs.SetInt("ModeOpen", 2);
        }

        levelOpen = PlayerPrefs.GetInt("ModeOpen");
        //Debug.Log(levelOpen);
        PlayerPrefs.Save();

        
    }

    }
