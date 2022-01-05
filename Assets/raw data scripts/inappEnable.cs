using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inappEnable : MonoBehaviour
{
    public GameObject InAppPanel;

    // Start is called before the first frame update
    private void OnEnable()

    {
        if (PlayerPrefs.GetInt("Shop")==0)
        {
            InAppPanel.SetActive(true);
           
        }
       
    }
    private void OnDisable()
    {
    }
}
