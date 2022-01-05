using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaptersMoveAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    void OnEnable()
    {
        LeanTween.move(this.gameObject.GetComponent<RectTransform>(), new Vector3(0f, 0f, 0f), 0.5f).setEaseSpring().setIgnoreTimeScale(true);
    }

  
    void OnDisable()
    {
        LeanTween.move(this.gameObject.GetComponent<RectTransform>(), new Vector3(3000f, 0f, 0f), 0.5f).setEaseSpring().setIgnoreTimeScale(true);
    }
}
