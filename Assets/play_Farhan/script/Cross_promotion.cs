using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cross_promotion : MonoBehaviour
{
    public Sprite[] Icons;
    public string[] IconURLs;
    int TotalIcons;
    string LinkToOpen;
    void Start()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            gameObject.SetActive(false);
        }
        TotalIcons = Icons.Length;
        gameObject.GetComponent<Image>().sprite = Icons[PlayerPrefs.GetInt("IconNum")];
        LinkToOpen = IconURLs[PlayerPrefs.GetInt("IconNum")];
        Invoke("ChangeIcon", 2.5f);
    }

    void ChangeIcon()
    {
        if (PlayerPrefs.GetInt("IconNum") < TotalIcons-1)
        {
            PlayerPrefs.SetInt("IconNum", PlayerPrefs.GetInt("IconNum") + 1);
        }
        else
        {
            PlayerPrefs.SetInt("IconNum", 0);
        }

        gameObject.GetComponent<Image>().sprite = Icons[PlayerPrefs.GetInt("IconNum")];
        LinkToOpen = IconURLs[PlayerPrefs.GetInt("IconNum")];

        Invoke("ChangeIcon", 2.5f);
    }

    public void OpenCrossPromoLink()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            Application.OpenURL(LinkToOpen);
        }
    }
}
