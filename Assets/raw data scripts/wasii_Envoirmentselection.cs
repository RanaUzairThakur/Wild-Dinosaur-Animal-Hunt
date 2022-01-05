using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class wasii_Envoirmentselection : MonoBehaviour
{
    public GameObject[] borders;
    public GameObject showmsg;
    public GameObject lockimg;
    public AudioSource ButtonClickSound;
    private int x;
    private void Start()
    {
        AllBordersoff();
        borders[0].SetActive(true);
           x = PlayerPrefs.GetInt("Unlocked");
        if(x>14)
        {
            lockimg.SetActive(false);
        }
}
    public void Environmen_Sel(bool Forest)
    {
       
        ButtonClickSound.Play();
        Debug.Log("Up Value:" + "   " + x);
        if (Forest)
        {

            wasii_Game_Controller.Forest = true;
            AllBordersoff();
            borders[0].SetActive(true);
            PlayerPrefs.SetInt("Select_Levels", 0);
            SceneManager.LoadScene("wasii_LevelSelections");
        }
        else
        {
            if (x >= 14)
            {
                //Debug.Log("Inside Value:" + "   " + x);
               
                wasii_Game_Controller.Forest = false;
                AllBordersoff();
                borders[1].SetActive(true);
                PlayerPrefs.SetInt("Select_Levels", 1);

                SceneManager.LoadScene("wasii_LevelSelections");
            }else
            {
                showmsg.SetActive(true);
            }
        }

         
    }
    public void AllBordersoff()
    {
        for (int i = 0; i < borders.Length; i++)
        {
            borders[i].gameObject.SetActive(false);
        }
    }
}
