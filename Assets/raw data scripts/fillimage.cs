using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class fillimage : MonoBehaviour
{
    public Image image;
    public GameObject[] ItemsToDisable;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        image.fillAmount += Time.deltaTime / 8;
        if (image.fillAmount >= 1)
        {
            this.gameObject.SetActive(false);
            image.fillAmount = 0;
            //GaminatorAds.Instance.ShowSmartBanner(7);
            foreach (GameObject g in ItemsToDisable)
            {
                print("item false");
                g.SetActive(false);
            }
        }
    }
}
