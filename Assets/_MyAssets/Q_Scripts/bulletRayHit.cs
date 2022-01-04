using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bulletRayHit : MonoBehaviour
{
    public  bool firstDetecte, runningDetect, hitTarget;
    private void Start()
    {
        Gun.camDetachChk = false;
        lung_s = false;
    }
    void FixedUpdate()
    {
        if(follow.followChk==true && ! firstDetecte)
        {
            FirstDetected();
        }
        if(follow.followChk&& firstDetecte)
        {
            RunningDetected();
        }
        if (follow.followChk && runningDetect)
        {
            HittingDetect();
           // print("bullet hitted to animal in onetime");
        }
    }
    public void FirstDetected()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out hit, 2000f))
        {

            if (hit.collider.GetComponent<AS_BulletHiter>())
            {


               // Debug.Log("First Detect my Sc:" + hit.collider.gameObject);
                //Time.timeScale = 1f;
                //bulletDetector.instance.slowMotionNow(0.002f, 400f);
                //Destroy(gameObject);
                firstDetecte = true;
                
            }
            else
            {
                follow.followChk = false;
            }
        }
        //Debug.DrawRay(transform.position, transform.forward, Color.black);
    }
    public void RunningDetected()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out hit, 30f))
        {

            if (hit.collider.GetComponent<AS_BulletHiter>())
            {

              //  Debug.Log("Running my bulletRayScript:" + hit.collider.gameObject);

               // bulletDetector.instance.slowMotionNow(0.04f, 300f );
                
                //Destroy(gameObject);
              //  Gun.camDetachChk = true;
                runningDetect = true;

            }
            else
            {
                Gun.camDetachChk = true;
                follow.followChk = false;
            }
        }
    }
    public static bool Bullet_hitted=false;
    public static bool lung_s;
    public static bool Heart_s;
    public static bool brain_s;

    public void HittingDetect()
    {
        RaycastHit hit1;
        Ray ray1 = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray1, out hit1, 1f))
        {
            RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position, transform.forward, 2000f);

        if (hits[hits.Length - 1].collider.gameObject.tag == "Head" && !Bullet_hitted)
        {
                wasii_GamePlay_UI_Handler.instance.hitText.SetActive(true);
                wasii_GamePlay_UI_Handler.instance.ScoreText.SetActive(true);
                wasii_GamePlay_UI_Handler.instance.hitText.GetComponent<Text>().text = "HEAD SHOT";
                wasii_GamePlay_UI_Handler.instance.ScoreText.GetComponent<Text>().text = "$100";
                //PlayerPrefManager._instance.SetGameCoins(100);
                PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + 100);
                wasii_GamePlay_UI_Handler.instance.hitText_Off();
            Bullet_hitted = true;
        }

        else if (hits[hits.Length - 1].collider.gameObject.tag == "Brain" && !Bullet_hitted)
        {

                wasii_GamePlay_UI_Handler.instance.hitText.SetActive(true);
                wasii_GamePlay_UI_Handler.instance.ScoreText.SetActive(true);
                wasii_GamePlay_UI_Handler.instance.hitText.GetComponent<Text>().text = "BRAIN SHOT";
                wasii_GamePlay_UI_Handler.instance.ScoreText.GetComponent<Text>().text = "$200";
                //PlayerPrefManager._instance.SetGameCoins(200);
                PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + 200);
                wasii_GamePlay_UI_Handler.instance.hitText_Off();
            Bullet_hitted = true;
        }
        else if (hits[hits.Length - 1].collider.gameObject.tag == "Heart" && !Bullet_hitted)
        {
                wasii_GamePlay_UI_Handler.instance.hitText.SetActive(true);
                wasii_GamePlay_UI_Handler.instance.ScoreText.SetActive(true);
                wasii_GamePlay_UI_Handler.instance.hitText.GetComponent<Text>().text = "HEART SHOT";
                wasii_GamePlay_UI_Handler.instance.ScoreText.GetComponent<Text>().text = "$150";
                // PlayerPrefManager._instance.SetGameCoins(150);
                PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins")+150);
                wasii_GamePlay_UI_Handler.instance.hitText_Off();
            Bullet_hitted = true;
        }
        else if (hits[hits.Length - 1].collider.gameObject.tag == "Lungs" && !Bullet_hitted)
        {
                wasii_GamePlay_UI_Handler.instance.hitText.SetActive(true);
                wasii_GamePlay_UI_Handler.instance.ScoreText.SetActive(true);
                wasii_GamePlay_UI_Handler.instance.hitText.GetComponent<Text>().text = "LUNGS SHOT";
                wasii_GamePlay_UI_Handler.instance.ScoreText.GetComponent<Text>().text = "$150";
                //PlayerPrefManager._instance.SetGameCoins(150);
                PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + 150);
                wasii_GamePlay_UI_Handler.instance.hitText_Off();
            
            Bullet_hitted = true;
        }
        else if (hits[hits.Length - 1].collider.gameObject.tag == "Body" && !Bullet_hitted)
        {
                wasii_GamePlay_UI_Handler.instance.hitText.SetActive(true);
                wasii_GamePlay_UI_Handler.instance.ScoreText.SetActive(true);
                wasii_GamePlay_UI_Handler.instance.hitText.GetComponent<Text>().text = "BODY SHOT";
                wasii_GamePlay_UI_Handler.instance.ScoreText.GetComponent<Text>().text = "$50";
                //PlayerPrefManager._instance.SetGameCoins(50);
                PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + 50);
                wasii_GamePlay_UI_Handler.instance.hitText_Off();
            Bullet_hitted = true;
        }
    }
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out hit, 10f))
        {
			
            if (hit.collider.GetComponent<AS_BulletHiter>())
            {
                if (hit.collider.gameObject.name == "LungAnim")
                    lung_s = true;
                if (hit.collider.gameObject.name == "Heart")
                    Heart_s = true;
                if (hit.collider.gameObject.name == "Brain Prefeb")
                    brain_s = true;

                //Debug.LogError("HIt in my bulletRayScript:" + hit.collider.gameObject);

                bulletDetector.instance.slowMotionNow(0.015f, 400f);
                //Destroy(gameObject);
                //hitTarget = true;
                //  Gun.instance.tempChk = true;
               
            }
            else
            {
                Gun.camDetachChk = true;
                follow.followChk = false;
            }
        }
    }

}
