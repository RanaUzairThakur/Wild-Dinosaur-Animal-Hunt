using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Typing : MonoBehaviour {

    public string message = "Hello World";
    public Text textComp;
    public float startdelay = 2.0f;
    public float typingDelay = 0.01f;
    AudioSource source;
   // Use this for initialization
  
    public void Typer(string msg, float startTime)
    {
        message = msg;
        startdelay = startTime;
        StartCoroutine("TypeIn");
    }
    void Awake()
    {
        textComp = GetComponent<Text>();
    }

   public IEnumerator TypeIn()
    {
        yield return new WaitForSeconds(startdelay);
        for (int i = 0; i <= message.Length; i++)
        {
            textComp.text = message.Substring(0, i);
       //     source.Play();
             yield return new WaitForSeconds(typingDelay);
        }
      //  StartCoroutine("clearText");
    }

   public IEnumerator TypeOff()
   {
       for (int i = message.Length; i >= 0; i--)
       {
           textComp.text = message.Substring(0, i);
           yield return new WaitForSeconds(typingDelay);
       }
   }
	
    IEnumerator clearText()
    {
        StopCoroutine("TypeIn");
        yield return new WaitForSeconds(0.0f);
        transform.gameObject.GetComponent<Text>().text = "";
      //  this.gameObject.SetActive(false);
        StopCoroutine("clearText");
    }

    public void ClearData()
    {
        StartCoroutine("clearText");
    }
}
