using UnityEngine;
using System.Collections;
using UnityEngine.UI;
 
public class AutoType : MonoBehaviour {
 
	public float letterPause = 0.2f;
    public float StartTime = 0.5f;
	//public AudioClip sound;
    public Text guiText;
    public string message1;
    string message;

    // Use this for initialization
    
    void OnEnable () {
		message = message1;
        guiText.text = "";
        StartCoroutine(TypeText ());
        
	}
   
    IEnumerator TypeText () {
        yield return new WaitForSeconds(StartTime);
        foreach (char letter in message.ToCharArray()) {
			guiText.text += letter;
			//if (sound)
			//	audio.PlayOneShot (sound);
				yield return 0;
			yield return new WaitForSeconds (letterPause);
		}      
	}

    

}