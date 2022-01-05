using UnityEngine;
using UnityEngine.UI;

public class fillerimg : MonoBehaviour
{
    public Image img;
    //public Text loadingtext;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        img.fillAmount += Time.deltaTime / 10;
        //loadingtext.text = img.fillAmount.ToString("P0");

        
   
    }
}
