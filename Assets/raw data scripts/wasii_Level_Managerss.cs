using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wasii_Level_Managerss : MonoBehaviour
{
    public static wasii_Level_Managerss instance;
    public GameObject[] levels;
    public GameObject Environment;
    public int index;
     void Awake()
    {
        instance = this;
        Environment.SetActive(true);
    }

    void Start()
        
    {
       int index = GlobalScripts.CurrLevelIndex - 15;
        
        if (GlobalScripts.CurrLevelIndex >= 15)
            {
                levels[index].SetActive(true);
            }

    }
}
