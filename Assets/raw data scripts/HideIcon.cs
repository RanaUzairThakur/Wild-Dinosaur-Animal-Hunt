using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideIcon : MonoBehaviour
{
    public GameObject icon;
    // Start is called before the first frame update
    private void OnEnable()
    {
        icon.SetActive(false);
    }
    private void OnDisable()
    {
        icon.SetActive(true);
    }
}
