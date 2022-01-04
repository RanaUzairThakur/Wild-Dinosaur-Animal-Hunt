using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiveScene : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] live;
    public GameObject Camera1 ;
    public void OnEnable()
    {
        for (int i = 0; i <= live.Length; i++)
        {
            live[i].gameObject.SetActive(true);
        }
        Camera1.SetActive(false);
       // camera2.SetActive(true);
    }
    public void OnDisable()
    {
        for (int i = 0; i <= live.Length; i++)
        {
            live[i].gameObject.SetActive(false);
        }
        Camera1.SetActive(true);
        //camera2.SetActive(false);
    }

}
