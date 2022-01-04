using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionController : MonoBehaviour
{
    public static MissionController instance;
    public int[] isallocated;//enemy position check

    public GameObject Targets_Root, Player_Root;

    public int Total_TargetCount;

    GameObject MainPlayer;

    
    public int[] TargetCount;

    public GameObject[] Targets_Position_Holder, Player_PositionHolder;

    public GameObject[] TargetsObject;

    public GameObject[] RandomPostions;

    public GameObject[] PlayerPostions;

    int EnvironmentNo, PlayerPosNo;
    public GameObject[] EnableObjects,DisableObjects;

    void Awake()
    {
        instance = this;
        // Total_TargetCount=TargetCount[GlobalScripts.CurrLevelIndex];
    }

    // Start is called before the first frame update
    private void Start()
    {
        
        int TempIndex = 0;

        //EnvironmentGenerator(TempIndex);//Environment Generator

        AssignedTarget_RandomPostion(TempIndex);//Assigned Target Random Position

        AssignedPlayer_RandomPostion(TempIndex);//Assigned Player Random Position

        Target_Instantiater();

        foreach(GameObject objc in EnableObjects)
        {
            objc.SetActive(true);
        }

        foreach (GameObject objc in DisableObjects)
        {
            objc.SetActive(false);
        }
    }

   

    public void Target_Instantiater()
    {
        if (Total_TargetCount <= 0)
            return;

        int m = 0,n=0;

        for (int k = 0; k < TargetCount.Length; k++)
        {
            while(m < TargetCount[n])
            {
                //Debug.Log("k   " + k + "    m   " + m +"    n    "+n);
                EnemyGenerator(k);
                m++;
            }
            n++;
            m = 0;
            //Debug.Log("k   " + k + "    n    " + n);
        }
    }

    //Assigned Target Positions
    public void AssignedTarget_RandomPostion(int TempIndex)
    {

        RandomPostions = new GameObject[Targets_Root.transform.childCount];

        for (int j = 0; j < RandomPostions.Length; j++)
        {
            RandomPostions[j] = Targets_Root.transform.GetChild(j).gameObject;
        }

        isallocated = new int[RandomPostions.Length];



    }

    public void AssignedPlayer_RandomPostion(int TempIndex)
    {

        PlayerPostions = new GameObject[Player_Root.transform.childCount];


        for (int j = 0; j < PlayerPostions.Length; j++)
        {
            PlayerPostions[j] = Player_Root.transform.GetChild(j).gameObject;

        }



    }


    public void EnvironmentGenerator(int TempIndex)
    {

    }

    public void ActiveMainPlayer(GameObject player)
    {
        MainPlayer = player;


        MainPlayer.transform.position = PlayerPostions[PlayerPosNo].transform.position;
        MainPlayer.transform.rotation = PlayerPostions[PlayerPosNo].transform.rotation;
        MainPlayer.SetActive(true);

        //GlobalScripts.GameStarted = true;
        //GlobalScripts.timeOver = false;

    }

    //Enemy Prefeb  Instantiater w.r.t Environment
    bool isExist;

    public void EnemyGenerator(int TargetIndex)
    {
        bool tempallocate = false;

        int TempIndex = Random.Range(0, RandomPostions.Length);



        if (isallocated[TempIndex] == 0)
        {
            isallocated[TempIndex] = 1;
            tempallocate = true;
        }
        else
        {
            TempIndex += 1;
            for (int k = TempIndex; k < RandomPostions.Length; k++)
            {
                if (isallocated[k] == 0)
                {
                    isallocated[k] = 1;

                    TempIndex = k;
                    tempallocate = true;
                    break;
                }
            }

            if (!tempallocate)
            {
                for (int k = TempIndex; k < 0; k--)
                {
                    if (isallocated[k] == 0)
                    {
                        isallocated[k] = 1;

                        TempIndex = k;
                        tempallocate = true;
                        break;
                    }
                }
            }


        }
        if (!tempallocate)
        {

            EnemyGenerator(TargetIndex);
        }
        else
        {
            //Debug.Log("TargetIndex    "+ TargetIndex + "     TempIndex     " + TempIndex);
            Instantiate(TargetsObject[TargetIndex], RandomPostions[TempIndex].transform.position, RandomPostions[TempIndex].transform.rotation);



        }




    }

}
