using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using EncryptStringSample;
using UnityEngine.SceneManagement;
public class Privacy_PolicyUIText : MonoBehaviour {
	public Text[] privacyUI;
	string filePath="";
	public static string result="";
	public static bool check=false;
	// Use this for initialization
	void Awake(){


	}
		
	void Start () {
		if (SceneManager.GetActiveScene ().buildIndex == 0) {
			if (!check && PlayerPrefs.GetInt ("ConsentAd") == 0) {
				check = true;
				onloadprivacycontent ();
			}
		} else {
			if (!check) {
				check = true;
				onloadprivacycontent ();
			} else {
				loadtext_to_ui ();
			}
		}
	}
	void onloadprivacycontent(){
		filePath = Application.streamingAssetsPath +  "/"+PlayerPrefs.GetString ("GameId")+"UserContent.txt";
		//		select_loadingimage ();
		if (Application.platform == RuntimePlatform.Android) {
			StartCoroutine (privacytext_Path1 ()); 
		} else {
			privacytext_Path ();
		}
	}
//	// Update is called once per frame
//	void Update () 
//	{
//		
//	}
	IEnumerator privacytext_Path1()
	{
		WWW www = new WWW (filePath);
		yield return www;

		if (www.error != null) {

			print ("filenotfound");
		} else {
			result = www.text;
			TextReader textReader = new StringReader (result);
			var filecontents = textReader.ReadToEnd ();
			result=StringCipher.Decrypt(filecontents, AGameUtils.Cipher_Passwords);
			loadtext_to_ui ();
		}
	}
	void privacytext_Path() 
	{
		var stream = new StreamReader(filePath);
		var filecontents = stream.ReadToEnd ();
		result = filecontents.ToString ();
		result=StringCipher.Decrypt(filecontents, AGameUtils.Cipher_Passwords);
		loadtext_to_ui ();
	}

	void loadtext_to_ui(){
		if (privacyUI.Length > 0) {
			foreach (Text privacytext in privacyUI) {
				privacytext.text = result;
			}
		}
	}
	public void PrivacyEnd()
	{
		print ("PrivacyEnd");
	}
	public void OnPrivacyClick (){
		

	}
}
