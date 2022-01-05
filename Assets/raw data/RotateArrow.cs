using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateArrow : MonoBehaviour
{
    float rotationspeed = 200;
    void Update()
    {

        float rotation = rotationspeed * Time.deltaTime;
        //if (rotationleft > rotation)
        //{
        //    rotationleft -= rotation;
        //}
        //else
        //{
        //    rotation = rotationleft;
        //    rotationleft = 0;
        //}
        transform.Rotate(rotation, 0, 0);
    }
}
