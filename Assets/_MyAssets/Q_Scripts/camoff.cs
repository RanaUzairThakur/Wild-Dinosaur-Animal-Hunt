using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camoff : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Off",2.5f);
    }
    void Off()
    {
        gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
