using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class AGameUtils : MonoBehaviour {



	public static string PRODUCT_NAME = "";

	public static string PACKAGE_NAME = ""+Application.identifier;
	public static string EMAIL_VERSION = "1.0";
	public static string Cipher_Passwords="Xikasdfadwrt4g69";
	//For DTS

	//For KickTime
	public static string EMAIL_ID = "";
	public static string MORE_APPS_DN = "";

	public static string INAPP_ID = "";
	public static string FLURRY_ID = "";
	
	public static void initAnalytics()
	{
//		print ("flry"+FLURRY_ID);
		//FlurryAgent.Instance.onStartSession(FLURRY_ID);

    }

	//public static void closeAnalytics()
//	{
		//FlurryAgent.Instance.onEndSession();
		//Analytics.Flurry.Instance.
  //  }

	public static void SendFeedbackMail()
	{
		AndroidJavaObject emailObject;
		
		AndroidJavaClass emailClass = new AndroidJavaClass( "com.oas.emailcompose.EmailActivity" )  ;
		emailObject = emailClass.CallStatic<AndroidJavaObject>("instance");
		emailObject.Call( "sendEmail", PRODUCT_NAME, PACKAGE_NAME, EMAIL_VERSION, EMAIL_ID  );
    }


	public static void rateUsLink()
	{	
//		string rateUsLink = "https://play.google.com/store/apps/details?id="+PACKAGE_NAME +"&hl=en";
		string rateUsLink = "market://details?id="+PACKAGE_NAME +"";
	
		Application.OpenURL(rateUsLink);
	}
	public static void moreAppsLink()
	{	
//		Application.OpenURL ("https://play.google.com/store/apps/developer?id="+MORE_APPS_DN);
//		market://search?q=pub:\
		Application.OpenURL ("market://search?q=pub:"+ MORE_APPS_DN);
    }





	public static void LogAnalyticEvent(string eventMessage)
	{
		
	}

}
