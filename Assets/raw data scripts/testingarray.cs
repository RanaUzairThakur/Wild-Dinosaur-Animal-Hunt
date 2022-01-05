using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testingarray : MonoBehaviour
{
    public int[] arr = new int[6] { 23, 43, 26, 65, 98, 76 };
    int n, i, j, k, min, max, secondmax,secondmin;

    // Start is called before the first frame update
    void Start()
    {
        n = 6;
        min = arr[0];
        max = arr[0];

        for (i = 1; i < n; i++)
        {
            if (arr[i] > max)
            {
                max = arr[i];
                j = i;
            }
            
        }
        for(i=1; i<n; i++)
        {
            if (arr[i] < min)
            {
                min = arr[i];
                k = i;
            }
        }
        secondmax = arr[0];
        for (i = 1; i < n; i++)
        {
            if (i == j)
            {
                i++;
                i--;
            }
            else
            {
                if(arr[i] > secondmax)
                {
                    secondmax = arr[i];
                }
            }
        }
        secondmin = arr[0];
        for (i = 1; i < n; i++)
        {
            if (i == k)
            {
                i++;
                i--;
            }
            else
            {
                if (arr[i] < secondmin)
                {
                    secondmin = arr[i];
                }
            }
        }
        print("min" + min);
        print("max" + max);
        print("secondmin" + secondmin);
        print("secondmax" + secondmax);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
