using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

using UnityEngine.UI;
using EncryptStringSample;
public class PrivacyTextGetter : MonoBehaviour {
//	public static string privacy_Nautoriouz="";
//	public static string privacy_Microclip="";
//	public static string privacy_Crushiz="";
//	public static string privacy_Appitix="";
//	public static string privacy_TheEntertainmentMaster="";
//	public static string privacy_Trenzy="";
//	public static string privacy_Trendish="";
	public static string privacy_Content="";
	string filePath = "",imagePath="";
	public GameObject G_loadingsplash;

	Sprite tempaddsprite;
	void Awake(){
		
	}
//	// Use this for initialization
//	void Start () {
//		
//	}
	
//	// Update is called once per frame
//	void Update () {
//		
//	}

	public void onloadprivacycontent(){
//		filePath = Application.streamingAssetsPath + "/"+AGameUtils.MORE_APPS_DN+".txt";
		filePath = Application.streamingAssetsPath +  "/"+PlayerPrefs.GetString ("GameId")+"UserContent.txt";
		imagePath=Application.streamingAssetsPath +  "/"+PlayerPrefs.GetString ("GameId")+"UserSplash.jpg";
//		select_loadingimage ();
		if (Application.platform == RuntimePlatform.Android) {
			StartCoroutine (privacytext_Path1 ()); 
			StartCoroutine (loadsplash_Image ()); 
		} else {
			privacytext_Path ();
			imagePath="file:///"+ Application.streamingAssetsPath + "/"+PlayerPrefs.GetString ("GameId")+"UserSplash.jpg";
			StartCoroutine (loadsplash_Image ()); 
		}
	}

	IEnumerator privacytext_Path1()
	{
		WWW www = new WWW (filePath);
		yield return www;

		if (www.error != null) {

			print ("filenotfound");
		} else {
			string result = www.text;
			TextReader textReader = new StringReader (result);
			var filecontents = textReader.ReadToEnd ();
			result=StringCipher.Decrypt(filecontents, AGameUtils.Cipher_Passwords);
			privacy_Content = result;
		}
	}

	IEnumerator loadsplash_Image()
	{
		Texture2D text = new Texture2D(512, 512, TextureFormat.DXT1, false);
		WWW www = new WWW (imagePath);
		yield return www;

		if (www.error != null) {

			print ("filenotfound"+www.error);
		} else {
			www.LoadImageIntoTexture (text);
			if(G_loadingsplash.gameObject.GetComponent(typeof(UnityEngine.UI.Image))!=null){
				G_loadingsplash.gameObject.GetComponent<UnityEngine.UI.Image> ().enabled = true;
				tempaddsprite = Sprite.Create(text, new Rect(0f, 0f, text.width,text.height), new Vector2(0.5f, 0.5f),128f);
				G_loadingsplash.gameObject.GetComponent<UnityEngine.UI.Image> ().sprite = tempaddsprite;
			}
		}
	}


	void privacytext_Path() 
	{
		var stream = new StreamReader(filePath);
		var filecontents = stream.ReadToEnd ();
		string result = filecontents.ToString ();
		result=StringCipher.Decrypt(filecontents, AGameUtils.Cipher_Passwords);
		privacy_Content = result;
	}
		
}
