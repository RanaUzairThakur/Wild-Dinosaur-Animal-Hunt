using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShiningItem : MonoBehaviour {

	public float shiningTime = 1f;
	public float width = 0.2f;
    public float Max;
    public float Min;
	Image sr;

	// Use this for initialization
	void Start () {

		sr = GetComponent<Image> ();
        StartCoroutine(Shine());

    }

	void Update () {
		//Vector3 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		//mousePos.z = 0;
		//RaycastHit2D hit = Physics2D.Raycast (mousePos, Vector3.zero, int.MaxValue);
		//if (hit && !isShining) {
		//	isShining = true;
			
		//}
	}

    private void OnEnable()
    {
        sr = GetComponent<Image>();
        StartCoroutine(Shine());
    }


    IEnumerator Shine () {
		float currentTime = 0;
		float speed = 1f / shiningTime;

		sr.material.SetFloat ("_Width", width);

		while (currentTime <= shiningTime) {
			currentTime += Time.deltaTime;
			float value = Mathf.Lerp (Min, Max, speed * currentTime);
			sr.material.SetFloat ("_TimeController", value);
			yield return null;
		}
		yield return new WaitForSeconds (0.1f);
		sr.material.SetFloat ("_Width", 0);
        StartCoroutine(Shine());
    }

    private void OnDisable()
    {
        StopCoroutine(Shine());
    }

    /*
        Rect GetUVs(Sprite sprite)	{
            Rect uv = sprite.rect;
            uv.x /= sprite.texture.width;
            uv.width /= sprite.texture.width;
            uv.y /= sprite.texture.height;
            uv.height /= sprite.texture.height;
            return uv;
        }
    */


}
