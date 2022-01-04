using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TypingText : MonoBehaviour {

	// Use this for initialization

	public float TimePause = 0.0001f;
	string Complete_Text=null;
	char[] CharText=null;
	//public AudioClip _AudioClip;
	//AudioSource _AudioSource;

	public Text DlgBar_Text;
    public AudioSource audio;
    public bool audiooff = true;
    //public GameObject Joystick;

    void Start () {
		//ShowDlgBar ("Welcome commander !. it's me Grace in this mission you need to clear up the town from the Criminals... Be cautious commander..! We don't wanna lose our most skilled officer.");
	switch (GlobalScripts.CurrLevelIndex) {
            case 0:
                ShowDlgBar("Kill The Wild Animals " + wasii_GamePlay_UI_Handler.instance.kills[GlobalScripts.CurrLevelIndex] + " Loin" + " With In " + "2" + " Mins");
               break;
            case 1:
                ShowDlgBar("Kill The Wild Animals  " + wasii_GamePlay_UI_Handler.instance.kills[GlobalScripts.CurrLevelIndex] + " Deer" + " With In " + "2" + " Mins");
                //  ShowDlgBar("Welcome to the forest Hunt The Wild Animals " + wasii_GamePlay_UI_Handler.instance.kills[GlobalScripts.CurrLevelIndex] + " Hippo that are showing on your world map " + " With In " + wasii_LevelsContoller.instance.TimeCount[GlobalScripts.CurrLevelIndex] + " Seconds");
                break;
            case 2:
                ShowDlgBar("Kill The Wild Animals  " + wasii_GamePlay_UI_Handler.instance.kills[GlobalScripts.CurrLevelIndex] + " Dino" + " With In " + "2" + " Mins");
                break;
            case 3:
                ShowDlgBar("Kill The Wild Animals   " + wasii_GamePlay_UI_Handler.instance.kills[GlobalScripts.CurrLevelIndex] + " Wolf" + " With In " + "2" + " Mins");
                //ShowDlgBar("Kill  " + GamePlay_UI_Handler.instance.kills[GlobalScripts.CurrLevelIndex] + " Elephant " + " With In " + LevelsContoller.instance.TimeCount[GlobalScripts.CurrLevelIndex] + " Seconds");
                break;
            case 4:
                ShowDlgBar("Kill The Wild Animals " + wasii_GamePlay_UI_Handler.instance.kills[GlobalScripts.CurrLevelIndex]+ "Rhinoceros " + " With In " + "2" + " Mins");
                break;
            case 5:
                ShowDlgBar("Kill The Wild Animals   " + wasii_GamePlay_UI_Handler.instance.kills[GlobalScripts.CurrLevelIndex] + " Stag " + " With In " + "2" + " Mins");
                break;
            case 6:
                ShowDlgBar("Kill The Wild Animals  " + wasii_GamePlay_UI_Handler.instance.kills[GlobalScripts.CurrLevelIndex] + " Boar "  + " With In " + "2" + " Mins");
                break;
            case 7:
                ShowDlgBar("Kill The Wild Animals  " + wasii_GamePlay_UI_Handler.instance.kills[GlobalScripts.CurrLevelIndex] + " Bear " + " With In " + "2" + " Mins");
                break;
            case 8:
                ShowDlgBar("Kill The Wild Animals   " + wasii_GamePlay_UI_Handler.instance.kills[GlobalScripts.CurrLevelIndex] + " Zabra "  + " With In " + "2" + " Mins");
                break;
            case 9:
                ShowDlgBar("Kill The Wild Animals  " + wasii_GamePlay_UI_Handler.instance.kills[GlobalScripts.CurrLevelIndex] + " Loin "  + " With In " + "2" + " Mins");
                break;
            case 10:
                ShowDlgBar("Kill The Wild Animals " + wasii_GamePlay_UI_Handler.instance.kills[GlobalScripts.CurrLevelIndex] + " Rhinoceros " + " With In " + "2" + " Mins");
                break;
            case 11:
                ShowDlgBar("Kill The Wild Animals  " + wasii_GamePlay_UI_Handler.instance.kills[GlobalScripts.CurrLevelIndex] + " Zebra "  + " With In " + "2" + " Mins");
                break;
            case 12:
                ShowDlgBar("Kill The Wild Animals   " + wasii_GamePlay_UI_Handler.instance.kills[GlobalScripts.CurrLevelIndex] + " Hippo " + " With In " + "2" + " Mins");
                break;
            case 13:
                ShowDlgBar("Kill The Wild Animals " + wasii_GamePlay_UI_Handler.instance.kills[GlobalScripts.CurrLevelIndex] + " Bear "+ " With In " + "2" + " Mins");
                break;
            case 14:
                ShowDlgBar("Kill The Wild Animals  " + wasii_GamePlay_UI_Handler.instance.kills[GlobalScripts.CurrLevelIndex] + " Boar"  + " With In " + "2" + " Mins");
                break; 
            case 15:
                ShowDlgBar("Kill The Wild Animals  " + wasii_GamePlay_UI_Handler.instance.kills[GlobalScripts.CurrLevelIndex] + " Elephant " + "that are showing on your world map" + " With In " + "2" + " Mins");
                break;
          
            case 16:
                ShowDlgBar("Welcome to the forest Hunt The Wild Animals2 Pigs, 1 Beer and 1 Boar that are showing on your world map " + " With In " + "2" + " Mins");
                break;
            case 17:
                ShowDlgBar("Welcome to the forest Hunt The Wild Animals 1 Elephant, 2 Fox, 1 Lion and 1 Wolf that are showing on your world map With In " + wasii_LevelsContoller.instance.TimeCount[GlobalScripts.CurrLevelIndex] + " Seconds");
                break;
            case 18:
                ShowDlgBar("Welcome to the forest Hunt The Wild Animals 2 Rhinoceros, 2 Elephants and 2 Hippopotamus that are showing on your world map With In " + wasii_LevelsContoller.instance.TimeCount[GlobalScripts.CurrLevelIndex] + " Seconds");
                break;
            case 19:
                ShowDlgBar("Welcome to the forest Hunt The Wild Animals2 Lion, 2 Stag, 1 Wolf and 2 Boar that are showing on your world map With In " + wasii_LevelsContoller.instance.TimeCount[GlobalScripts.CurrLevelIndex] + " Seconds");
                break;
            case 20:
                ShowDlgBar("Welcome to the forest Hunt The Wild Animals 1 Deer, 2 Elephant,2 Fox, 2 Zebra and 1 Lion that are showing on your world map With In " + wasii_LevelsContoller.instance.TimeCount[GlobalScripts.CurrLevelIndex] + " Seconds");
                break;
            case 21:
                ShowDlgBar("Welcome to the forest Hunt The Wild Animals 2 Bear, 2 Boar, 1 Fox, 2 Wolf, 1 Zebra and 1 Lion that are showing on your world map With In " + wasii_LevelsContoller.instance.TimeCount[GlobalScripts.CurrLevelIndex] + " Seconds");
                break;
            case 22:
                ShowDlgBar("Welcome to the forest Hunt The Wild Animals 2 Elephant, 1 Hippo, 1 Pig, 1 Rhino, 1 Wolf, 2 Lion, 1 Boar and 1 Deer that are showing on your world map With In " + wasii_LevelsContoller.instance.TimeCount[GlobalScripts.CurrLevelIndex] + " Seconds");
                break;
            case 23:
                ShowDlgBar("Welcome to the forest Hunt The Wild Animals 1 Elephant, 2 Fox, 1 Lion and 1 Wolf that are showing on your world map With In " + wasii_LevelsContoller.instance.TimeCount[GlobalScripts.CurrLevelIndex] + " Seconds");
                break;
            case 24:
                ShowDlgBar("Welcome to the forest Hunt The Wild Animals 1 Deer, 2 Elephant,2 Fox, 2 Zebra and 1 Lion that are showing on your world map With In " + wasii_LevelsContoller.instance.TimeCount[GlobalScripts.CurrLevelIndex] + " Seconds");
                break;
            case 25:
                ShowDlgBar("Welcome to the forest Hunt The Wild Animals 2 Lion, 2 Stag, 1 Wolf and 2 Boar that are showing on your world map With In " + wasii_LevelsContoller.instance.TimeCount[GlobalScripts.CurrLevelIndex] + " Seconds");
                break;
            case 26:
                ShowDlgBar("Welcome to the forest Hunt The Wild Animals 2 Pigs, 1 Deer and 1 Boar that are showing on your world map With In " + wasii_LevelsContoller.instance.TimeCount[GlobalScripts.CurrLevelIndex] + " Seconds");
                break;
            case 27:
                ShowDlgBar("Welcome to the forest Hunt The Wild Animals 2 Bear, 2 Boar, 1 Fox, 2 Wolf, 1 Zebra and 1 Lion that are showing on your world map With In " + wasii_LevelsContoller.instance.TimeCount[GlobalScripts.CurrLevelIndex] + " Seconds");
                break;
            case 28:
                ShowDlgBar("Welcome to the forest Hunt The Wild Animals 2 Rhinoceros, 2 Elephants and 2 Hippopotamus that are showing on your world map With In " + wasii_LevelsContoller.instance.TimeCount[GlobalScripts.CurrLevelIndex] + " Seconds");
                break;
            case 29:
                ShowDlgBar("Welcome to the forest Hunt The Wild Animals 2 Elephant, 1 Hippo, 1 Pig, 1 Rhino, 1 Wolf, 2 Lion, 1 Boar and 1 Deer  that are showing on your world map With In " + wasii_LevelsContoller.instance.TimeCount[GlobalScripts.CurrLevelIndex] + " Seconds");
                break;
        }
    }
	public void ShowDlgBar(string messege)
	{
		Time.timeScale = 1f;
	//	DlgBar.SetActive (true);
		//Joystick.GetComponent<Image> ().enabled = false;
		//Debug.Log ("Type TExt\t"+messege);
		CharText = messege.ToCharArray();
		Complete_Text = null;
		DlgBar_Text.text=null;
		DlgBar_Text.enabled = true;
		StartCoroutine (typeText());

		}

	IEnumerator typeText()
	{
		Time.timeScale = 1f;
		for (int i = 0; i <=CharText.Length; i++) {
			if (i!=CharText.Length) {
                if(audiooff)
                audio.Play();
				Complete_Text += CharText [i].ToString();
				DlgBar_Text.text = Complete_Text;
			} 
			yield return new WaitForSeconds (TimePause);
			//Time.timeScale = 0;

		}
	}
    public void sounoff()
    {
        audiooff = false;
        audio.Stop();

    }


}
