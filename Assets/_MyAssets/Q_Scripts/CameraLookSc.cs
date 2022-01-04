using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using ControlFreak2;
public class CameraLookSc : MonoBehaviour
{
    public Transform backBut, mag, scope, body, muzzle;
    public Transform backbuLook, magLook, scopeLook, bodyLook, muzzleLook,allPos,allLook;
    public Transform targetPos, targetLook;
    bool rotate;
    int horizontalValue;
    public GameObject gun;
    float speed;
    private void Start()
    {
        rotate = true;
        speed =1;
    }
    private void Update()
    {
        horizontalValue = (int)CF2Input.GetAxis("Mouse X");
        if(horizontalValue>0)
        {
            speed = 1;
        }
        else if(horizontalValue<0)
        {
            speed = -1;
        }

        if (rotate)
        {
            gun.transform.Rotate(Vector3.down * speed * 10f * Time.deltaTime);
        }
    }
   
    private void LateUpdate()
    {
        
        if(rotate )
        {
           gun.transform.Rotate(Vector3.down*horizontalValue*10f*Time.deltaTime);
           
        }
        else
        {
            gameObject.transform.position = Vector3.Lerp(transform.position, targetPos.position, 2f * Time.deltaTime);
            gameObject.transform.DOLookAt(targetLook.transform.position, 2f);
        }
       
    }
    public void ButtFun()
    {
        rotate = false;
        //transform.position = targetPos.position;
        targetPos.position = backBut.position;
        targetLook.position = backbuLook.position;
    }
    public void bodyFun()
    {
        rotate = false;
        //transform.position = targetPos.position;
        targetPos.position = body.position;
        targetLook.position = bodyLook.position;
    }
    public void MagFun()
    {
        rotate = false;
        //transform.position = targetPos.position;
        targetPos.position = mag.position;
        targetLook.position = magLook.position;
    }
    public void scopeFun()
    {
        rotate = false;
        //transform.position = targetPos.position;
        targetPos.position = scope.position;
        targetLook.position = scopeLook.position;
    }
    public void MuzzleFun()
    {
        rotate = false;
        //transform.position = targetPos.position;
        targetPos.position = muzzle.position;
        targetLook.position = muzzleLook.position;
    }
   
    public void Back()
    {
        rotate = true;
        gameObject.transform.DOMove(new Vector3(0,0,-2),2f);
        gameObject.transform.DORotate(new Vector3(0, 0, 0), 2f);
      //  targetLook.position = allLook.position;
    }
}
