using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanel : MonoBehaviour {
	//public GameObject Music,Sound,MusicON,MusciOF,SoundOn,SoundOF;
	public Slider MySLider;
	public static bool issound, ismusic;
	// Use this for initialization
	
	public void QualityFun()
	{
		int num =(int) MySLider.value;

		Debug.Log (num);

		QualitySettings.SetQualityLevel (num);
	}
	public void Musicfun()
	{
		if (ismusic) {
			ismusic = false;
			//Music.SetActive (false);
			//MusicON.SetActive (false);
			//MusciOF.SetActive (true);
			
		}
		else 
		{
			ismusic = true;
			//Music.SetActive (true);
			//MusicON.SetActive (true);
			//MusciOF.SetActive (false);
		}
		
		
	}
	public void Soundfun()
	{
		if (issound) {
            AudioListener.pause = false;
			issound = false;
			//Sound.SetActive (false);
			//SoundOn.SetActive (false);
			//SoundOF.SetActive (true);
		}
		else 
		{
			issound = true;
			//Sound.SetActive (true);
			//SoundOn.SetActive (true);
			//SoundOF.SetActive (false);
            AudioListener.pause = true;
        }
		
	}
}
