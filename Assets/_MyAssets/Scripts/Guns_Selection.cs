using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Guns_Selection : MonoBehaviour
{
    public GameObject GunsPrefebs;
    public GameObject GunsBuy;
    public GameObject inAppPanel;
    public GameObject[] Gun_Model;
    public GameObject[] Gun_Specification;
    public static int  g_no = 0;

    public GameObject Selectbtn, PurchasedText, LowCashText, LockImage;

    private int ModleNum;
    public static bool isnextClick = false;
    public Text cash, CashRequired;

    private void OnEnable()
    {
        if (PlayerPrefs.GetInt("AllWeapons") == 0)
        {
            inAppPanel.SetActive(true);
            GunsPrefebs.SetActive(false);
        }
        else
        {
            inAppPanel.SetActive(false);
            GunsPrefebs.SetActive(true);
        }
    }
    void Start()
    {
        
        
        if (PlayerPrefs.GetInt("Reward") == 0)
        {

           // PlayerPrefs.SetInt("Coins", 40000);
            PlayerPrefs.SetInt("Reward", 1);
        }
       // GunsPrefebs.SetActive(true);
        // GunsButtons.SetActive(true);
        //  cash.text = ("$ " + PlayerPrefs.GetInt("cashin"));
        cash.text = ("$ " + PlayerPrefs.GetInt("Coins"));
       
        //Gun_Model[g_no].SetActive(true);
        Gun_Model[g_no].SetActive(true);
        Gun_Specification[g_no].SetActive(true);

        if(PlayerPrefs.GetInt("isGuns") ==1)
        {
           // InAppGuns.SetActive(false);
        }
    }



    //------------------------------------------Select Any Weapons(Pistols\Guns\Rifle)-----------------------------------------


    public void Guns_Selected()
    {

        GunsPrefebs.SetActive(true);
       // GunsButtons.SetActive(true);
        if (g_no == 0)
        {
            GunsBuy.SetActive(false);
            Selectbtn.SetActive(true);
            LockImage.SetActive(false);
            CashRequired.text = ("");
        }
        else if (g_no == 1 && PlayerPrefs.GetInt("Gun1") == 0)
        {
            GunsBuy.SetActive(true);
            Selectbtn.SetActive(false);
            LockImage.SetActive(true);
            CashRequired.text = ("$15,000");
        }
        else if (g_no == 2 && PlayerPrefs.GetInt("Gun2") == 0)
        {
            GunsBuy.SetActive(true);
            Selectbtn.SetActive(false);
            LockImage.SetActive(true);
            CashRequired.text = ("$20,000");
        }
        else if (g_no == 3 && PlayerPrefs.GetInt("Gun3") == 0)
        {
            GunsBuy.SetActive(true);
            Selectbtn.SetActive(false);
            LockImage.SetActive(true);
            CashRequired.text = ("$25,000");
        }
        else if (g_no == 4 && PlayerPrefs.GetInt("Gun4") == 0)
        {
            GunsBuy.SetActive(true);
            Selectbtn.SetActive(false);
            LockImage.SetActive(true);
            CashRequired.text = ("$30,000");
        }
        else if (g_no == 5 && PlayerPrefs.GetInt("Gun5") == 0)
        {
            GunsBuy.SetActive(true);
            Selectbtn.SetActive(false);
            LockImage.SetActive(true);
            CashRequired.text = ("$50,000");
        }
        else
        {
            GunsBuy.SetActive(false);
            Selectbtn.SetActive(true);
            LockImage.SetActive(false);
            CashRequired.text = ("");
        }
        GameObject.Find("ButtonClickSound").GetComponent<AudioSource>().Play();
    }
    public void NextModleBtn()
    {
        if (PurchasedText.activeInHierarchy)
            PurchasedText.SetActive(false);
        //g_no 
        if ( g_no < Gun_Model.Length-1)
        {
            StartCoroutine("NextGun");
        }

        else
        {
            for (int k = 0; k < Gun_Model.Length; k++)
            {
                Gun_Model[k].SetActive(false);
                Gun_Specification[k].SetActive(false);
            }
            Gun_Model[0].SetActive(true);
            Gun_Specification[0].SetActive(true);
            g_no = 0;
            if(g_no == 0)
            {
                GunsBuy.SetActive(false);
                Selectbtn.SetActive(true);
                LockImage.SetActive(false);
                CashRequired.text = ("");
            }
            
            //g_no = 0;
            Debug.Log ("Next Gun no"+g_no);
            GunCheck();
        }
        GameObject.Find("ButtonClickSound").GetComponent<AudioSource>().Play();
    }
    public void PreviousModleBtn()
    {
        if (PurchasedText.activeInHierarchy)
            PurchasedText.SetActive(false);
        // ModelNum
        if (g_no > 0)
        {
            StartCoroutine("PreviousGun");
        }
        else
        {
            for (int k = 0; k < Gun_Model.Length; k++)
            {
                Gun_Model[k].SetActive(false);
                Gun_Specification[k].SetActive(false);
            }
            Gun_Model[4].SetActive(true);
            Gun_Specification[4].SetActive(true);
            //g_no = 8;
            g_no = 4;
           
            GunCheck();
            Debug.Log("Prev Gun no" + g_no);

        }
        GameObject.Find("ButtonClickSound").GetComponent<AudioSource>().Play();
    }
    
        IEnumerator PreviousGun()
        {
       
        Gun_Model[g_no].SetActive(false);
        Gun_Specification[g_no].SetActive(false);
        yield return new WaitForSeconds(0.001f);
            g_no--;

            Gun_Model[g_no].SetActive(true);
            Gun_Specification[g_no].SetActive(true);
        GunCheck();
        if (g_no == 0)
        {
            GunsBuy.SetActive(false);
            Selectbtn.SetActive(true);
            LockImage.SetActive(false);
            CashRequired.text = ("");
        }
        Debug.Log("Prev Gun no IEnumerator" + g_no);

    }
        IEnumerator NextGun()
        {
            //print ("routine");
            yield return new WaitForSeconds(0.001f);
            Gun_Model[g_no].SetActive(false);
            Gun_Specification[g_no].SetActive(false);
        g_no++;
            Gun_Model[g_no].SetActive(true);
            Gun_Specification[g_no].SetActive(true);
        GunCheck();
        Debug.Log("NextGun Gun no IEnumerator" + g_no);

    }
        private void GunCheck()
        {
            //print ("GunChecked");
            if (g_no == 0)
            {
                GunsBuy.SetActive(false);
                Selectbtn.SetActive(true);
                LockImage.SetActive(false);
            }
            else if (g_no == 1 && PlayerPrefs.GetInt("Gun1") == 1)
            {
                GunsBuy.SetActive(false);
                Selectbtn.SetActive(true);
                LockImage.SetActive(false);

            }
            else if (g_no == 2 && PlayerPrefs.GetInt("Gun2") == 1)
            {
                GunsBuy.SetActive(false);
                Selectbtn.SetActive(true);
                LockImage.SetActive(false);

            }
            else if (g_no == 3 && PlayerPrefs.GetInt("Gun3") == 1)
            {
                GunsBuy.SetActive(false);
                Selectbtn.SetActive(true);
                LockImage.SetActive(false);

            }
            else if (g_no == 4 && PlayerPrefs.GetInt("Gun4") == 1)
            {
                GunsBuy.SetActive(false);
                Selectbtn.SetActive(true);
                LockImage.SetActive(false);

            }
            else if (g_no == 5 && PlayerPrefs.GetInt("Gun5") == 1)
            {
                GunsBuy.SetActive(false);
                Selectbtn.SetActive(true);
                LockImage.SetActive(false);

            }
            else if (g_no == 6 && PlayerPrefs.GetInt("Gun6") == 1)
            {
                GunsBuy.SetActive(false);
                Selectbtn.SetActive(true);
                LockImage.SetActive(false);

            }
            else if (g_no == 7 && PlayerPrefs.GetInt("Gun7") == 1)
            {
                GunsBuy.SetActive(false);
                Selectbtn.SetActive(true);
                LockImage.SetActive(false);

            }
            else if (g_no == 8 && PlayerPrefs.GetInt("Gun8") == 1)
            {
                GunsBuy.SetActive(false);
                Selectbtn.SetActive(true);
                LockImage.SetActive(false);

            }
            else if (g_no == 9 && PlayerPrefs.GetInt("Gun9") == 1)
            {
                GunsBuy.SetActive(false);
                Selectbtn.SetActive(true);
                LockImage.SetActive(false);
            }
            else
            {
                GunsBuy.SetActive(true);
                Selectbtn.SetActive(false);
                LockImage.SetActive(true);
                if (g_no == 1)
                {
                    CashRequired.text = ("$15,000");
                }
                else if (g_no == 2)
                {
                    CashRequired.text = ("$20,000");
                }
                else if (g_no == 3)
                {
                    CashRequired.text = ("$25,000");
                }
                else if (g_no == 4)
                {
                    CashRequired.text = ("$30,000");
                }
                else if (g_no == 5)
                {
                    CashRequired.text = ("$50,000");
                }
                else if (g_no == 6)
                {
                    CashRequired.text = ("$4500");
                }
                else if (g_no == 7)
                {
                    CashRequired.text = ("$1500");
                }
                else if (g_no == 8)
                {
                    CashRequired.text = ("$5000");
                }
                else if (g_no == 9)
                {
                    CashRequired.text = ("$6000");
                }


            }
        }
        
        //------------------------------------------Select Pistols, Guns, Rifle Models -----------------------------------------

        public void Select_Gun(int Gun)
    {
        g_no = Gun;
        //print("g_no " + g_no);
        for (int i = 0; i < Gun_Specification.Length; i++)
        {
            Gun_Specification[i].SetActive(false);
        }
        if (g_no == 0)
        {
            GunsBuy.SetActive(false);
            Selectbtn.SetActive(true);
            LockImage.SetActive(false);
            CashRequired.text = ("");
            Gun_Specification[g_no].SetActive(true);
        }
        else if (g_no == 1 && PlayerPrefs.GetInt("Gun1") == 0)
        {
            GunsBuy.SetActive(true);
            Selectbtn.SetActive(false);
            LockImage.SetActive(true);
            CashRequired.text = ("$15,000");
            Gun_Specification[g_no].SetActive(true);
        }
        else if (g_no == 2 && PlayerPrefs.GetInt("Gun2") == 0)
        {
            GunsBuy.SetActive(true);
            Selectbtn.SetActive(false);
            LockImage.SetActive(true);
            CashRequired.text = ("$20,000");
            Gun_Specification[g_no].SetActive(true);
        }
        else if (g_no == 3 && PlayerPrefs.GetInt("Gun3") == 0)
        {
            GunsBuy.SetActive(true);
            Selectbtn.SetActive(false);
            LockImage.SetActive(true);
            CashRequired.text = ("$25,000");
            Gun_Specification[g_no].SetActive(true);
        }
        else if (g_no == 4 && PlayerPrefs.GetInt("Gun4") == 0)
        {
            GunsBuy.SetActive(true);
            Selectbtn.SetActive(false);
            LockImage.SetActive(true);
            CashRequired.text = ("$30,000");
            Gun_Specification[g_no].SetActive(true);
        }
        else if (g_no == 5 && PlayerPrefs.GetInt("Gun5") == 0)
        {
            GunsBuy.SetActive(true);
            Selectbtn.SetActive(false);
            LockImage.SetActive(true);
            CashRequired.text = ("$50,000");
            Gun_Specification[g_no].SetActive(true);
        }
        else
        {
            if (g_no == 0)
            {
                GunsBuy.SetActive(false);
                Selectbtn.SetActive(true);
                LockImage.SetActive(false);
                CashRequired.text = ("");
                Gun_Specification[g_no].SetActive(true);
            }
            else if (g_no == 1)
            {
                GunsBuy.SetActive(false);
                Selectbtn.SetActive(true);
                LockImage.SetActive(false);
                CashRequired.text = ("");
                Gun_Specification[g_no].SetActive(true);
            }
            else if (g_no == 2)
            {
                GunsBuy.SetActive(false);
                Selectbtn.SetActive(true);
                LockImage.SetActive(false);
                CashRequired.text = ("");
                Gun_Specification[g_no].SetActive(true);
            }
            else if (g_no == 3)
            {
                GunsBuy.SetActive(false);
                Selectbtn.SetActive(true);
                LockImage.SetActive(false);
                CashRequired.text = ("");
                Gun_Specification[g_no].SetActive(true);
            }
            else if (g_no == 4)
            {
                GunsBuy.SetActive(false);
                Selectbtn.SetActive(true);
                LockImage.SetActive(false);
                CashRequired.text = ("");
                Gun_Specification[g_no].SetActive(true);
            }
            //GunsBuy.SetActive(false);
            //Selectbtn.SetActive(true);
            //LockImage.SetActive(false);
            //CashRequired.text = ("");
        }
        GameObject.Find("ButtonClickSound").GetComponent<AudioSource>().Play();
    }
    
    
 

    public void buy_Guns()
    {
        if (g_no == 1 && PlayerPrefs.GetInt("Coins") >= 15000)
        {
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") - 15000);
            PlayerPrefs.SetInt("Gun1", 1);
            cash.text = ("$ " + PlayerPrefs.GetInt("Coins"));
            GunsBuy.SetActive(false);
            PurchasedText.SetActive(true);
            Selectbtn.SetActive(true);
            LockImage.SetActive(false);
            CashRequired.text = "";
            Invoke("textoff", 3f);
        }

        else if (g_no == 2 && PlayerPrefs.GetInt("Coins") >= 20000)
        {
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") - 20000);
            PlayerPrefs.SetInt("Gun2", 1);
            cash.text = ("$ " + PlayerPrefs.GetInt("Coins"));
            GunsBuy.SetActive(false);
            PurchasedText.SetActive(true);
            Selectbtn.SetActive(true);
            LockImage.SetActive(false);
            CashRequired.text = "";
            Invoke("textoff", 3f);
        }
        else if (g_no == 3 && PlayerPrefs.GetInt("Coins") >= 25000)
        {
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") - 25000);
            PlayerPrefs.SetInt("Gun3", 1);
            cash.text = ("$ " + PlayerPrefs.GetInt("Coins"));
            GunsBuy.SetActive(false);
            PurchasedText.SetActive(true);
            Selectbtn.SetActive(true);
            LockImage.SetActive(false);
            CashRequired.text = "";
            Invoke("textoff", 3f);
        }
        else if (g_no == 4 && PlayerPrefs.GetInt("Coins") >= 30000)
        {
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") - 30000);
            PlayerPrefs.SetInt("Gun4", 1);
            cash.text = ("$ " + PlayerPrefs.GetInt("Coins"));
            GunsBuy.SetActive(false);
            PurchasedText.SetActive(true);
            Selectbtn.SetActive(true);
            LockImage.SetActive(false);
            CashRequired.text = "";
            Invoke("textoff", 3f);
        }
        else if (g_no == 5 && PlayerPrefs.GetInt("Coins") >= 50000)
        {
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") - 50000);
            PlayerPrefs.SetInt("Gun5", 1);
            cash.text = ("$ " + PlayerPrefs.GetInt("Coins"));
            GunsBuy.SetActive(false);
            PurchasedText.SetActive(true);
            Selectbtn.SetActive(true);
            LockImage.SetActive(false);
            CashRequired.text = "";
            Invoke("textoff", 3f);
        }
        else
        {
            LowCashText.SetActive(true);
            Invoke("textoff", 3f);
        }
        GameObject.Find("ButtonClickSound").GetComponent<AudioSource>().Play();
    }

   
    private void textoff()
    {
        PurchasedText.SetActive(false);
        LowCashText.SetActive(false);
    }

    void OnDisable()
    {
        
       //GaminatorAds.Instance.ShowSmartBanner();
        //GaminatorAds.Instance.ShowInterstitial();
    }
    public void onCrossInApp()
    {
        //CustomAnalytics.logLevelStarted("InAppCrossButton ", "WeaponSelection");
    }
}
