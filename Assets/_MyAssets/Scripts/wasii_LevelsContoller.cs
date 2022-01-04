using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class wasii_LevelsContoller : MonoBehaviour
{

    public static wasii_LevelsContoller instance;
    public int[] isallocated;//enemy position check
   // public GameObject level1animals, level2animals, level3animals, level4animals, level5animals;
    public GameObject Targets_Root, Player_Root;

    public int Total_TargetCount;

    GameObject MainPlayer;

    public float[] TimeCount;

    public int[] TargetCount;

    public GameObject[] Targets_Position_Holder, Player_PositionHolder;

    public Material[] DummyMaterials, StandredMaterial;

    public GameObject[] Animals;
    public GameObject[] AnimalLungs, AnimalBrain, AnimalHeart;

    public GameObject TargetsObject;

    public GameObject[] RandomPostions;

    public GameObject[] PlayerPostions;

    int EnvironmentNo, PlayerPosNo;
    // public static int LevelNo;
    public static bool Played_once = false, isReached = false;

    // 
    public Image infraimg;
    //public Material terrain_mate;


    void Awake()
    {
        instance = this;
        if (GlobalScripts.CurrLevelIndex >= 15)
        {
            Total_TargetCount = wasii_Level_Managerss.instance.levels[wasii_Level_Managerss.instance.index].GetComponent<MissionController>().Total_TargetCount;
        }
        else
        {
            Total_TargetCount = TargetCount[GlobalScripts.CurrLevelIndex];
        }

        Timer.timemanager.time = TimeCount[GlobalScripts.CurrLevelIndex];

    }

    // Start is called before the first frame update
    int index;
    private void Start()
    {
        //if (GlobalScripts.CurrLevelIndex == 0 )
        //{
        //    level1animals.SetActive(true);
        //}
        //if (GlobalScripts.CurrLevelIndex == 1)
        //{
        //    level2animals.SetActive(true);
        //}
        //if (GlobalScripts.CurrLevelIndex == 2)
        //{
        //    level3animals.SetActive(true);
        //}
        //if (GlobalScripts.CurrLevelIndex == 3)
        //{
        //    level4animals.SetActive(true);
        //}
        //if (GlobalScripts.CurrLevelIndex == 4)
        //{
        //    level5animals.SetActive(true);
        //}
        if (GlobalScripts.CurrLevelIndex >= 15)
        {
            index = GlobalScripts.CurrLevelIndex - 15;
        }
        if (PlayerPrefs.GetInt("Pressed") == 0)
        {
            //infraimg.GetComponentInParent<Animator>().enabled = true;
        }
        //if (GlobalScripts.CurrLevelIndex >= 15)
        //{
        //    GlobalScripts.CurrLevelIndex = GlobalScripts.CurrLevelIndex - 15;
        //}
        int TempIndex = 0;
        //Total_TargetCount = TargetCount[GlobalScripts.CurrLevelIndex];
        // Timer.timemanager.time =TimeCount[GlobalScripts.CurrLevelIndex];
        //EnvironmentGenerator(TempIndex);//Environment Generator

        AssignedTarget_RandomPostion(TempIndex);//Assigned Target Random Position

        AssignedPlayer_RandomPostion(TempIndex);//Assigned Player Random Position

        Target_Instantiater();
        // ActivateTimer();

    }
    bool isInfrared;
    public float infraTime = 15;

    public Renderer TerrainMesh;
    public Material ForestTerrain, SnowTerrain, TransparentTerrain;
    void Update()
    {
        if (isInfrared)
        {
            infraTime -= Time.deltaTime;
            infraimg.fillAmount -= Time.deltaTime / 15f;
        }
        if (infraTime <= 0 && isInfrared)
        {
            infraTime = 15f;
            isInfrared = false;
            Infrared();
            infra_btn.interactable = true;
        }
    }
    Button infra_btn;

    public void Infra_Active(Button btn)
    {
        infra_btn = btn;
        btn.interactable = isInfrared;
        btn.animator.enabled = false;
        PlayerPrefs.SetInt("Pressed", 1);
        isInfrared = true;
        Infrared();
        infraimg.fillAmount = 1;
    }
    void Infrared()
    {

        if (isInfrared)
        {
            for (int i = 0; i < Animals.Length; i++)
            {


                GameObject.Find("FPSCamera").GetComponent<CameraFilterPack_Color_RGB>().enabled = true;
                GameObject.Find("AnimalCamera").GetComponent<Camera>().enabled = true;
                RenderSettings.fog = false;
                if (Animals[i] != null)
                {
                    if (GlobalScripts.CurrLevelIndex == 0 || GlobalScripts.CurrLevelIndex == 12 || GlobalScripts.CurrLevelIndex == 13)
                    {
                        Animals[i].transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material = DummyMaterials[0];

                    }
                    if (GlobalScripts.CurrLevelIndex == 14)
                    {
                        Animals[i].transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material = DummyMaterials[1];

                    }
                }

                if (AnimalBrain[i] != null)
                {
                    AnimalBrain[i].transform.GetChild(0).gameObject.SetActive(true);
                }
                if (AnimalHeart[i] != null)
                {
                    AnimalHeart[i].transform.GetChild(0).gameObject.SetActive(true);
                }
                if (AnimalLungs[i] != null)
                {
                    AnimalLungs[i].transform.GetChild(0).gameObject.SetActive(true);
                }
            }

        }
        else
        {
            GameObject.Find("FPSCamera").GetComponent<CameraFilterPack_Color_RGB>().enabled = false;
            GameObject.Find("AnimalCamera").GetComponent<Camera>().enabled = false;
            RenderSettings.fog = true;

            for (int i = 0; i < Animals.Length; i++)
            {
                if (Animals[i] != null)
                {
                    if (GlobalScripts.CurrLevelIndex == 0 || GlobalScripts.CurrLevelIndex == 12 || GlobalScripts.CurrLevelIndex == 13)
                    {
                        Animals[i].transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material = StandredMaterial[0];

                    }
                    if (GlobalScripts.CurrLevelIndex == 14)
                    {
                        Animals[i].transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material = StandredMaterial[1];

                    }
                }
                //AnimalLungs[i].transform.GetChild(0).gameObject.SetActive(false);
                //AnimalHeart[i].transform.GetChild(0).gameObject.SetActive(false);
                //AnimalBrain[i].transform.GetChild(0).gameObject.SetActive(false);
            }
        }

    }


    public void Target_Instantiater()
    {
        if (Total_TargetCount <= 0)
            return;
        Animals = new GameObject[Total_TargetCount];

        for (int k = 0; k < Total_TargetCount; k++)
        {
            EnemyGenerator(k);
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
        

        MainPlayer.transform.position=PlayerPostions[PlayerPosNo].transform.position;
		MainPlayer.transform.rotation=PlayerPostions[PlayerPosNo].transform.rotation;
		MainPlayer.SetActive (true);


    }

    
    bool isExist;

    public void EnemyGenerator(int animalCount)
    {
        if (GlobalScripts.CurrLevelIndex <= 14)
        {
            bool tempallocate = false;
            int TempIndex;
            
            if (GlobalScripts.CurrLevelIndex >= 0 && GlobalScripts.CurrLevelIndex <= 4)
            {
               // Debug.Log("enemy");
                TempIndex = Random.Range(0, 4);
            }
            else
            {
                TempIndex = Random.Range(0, RandomPostions.Length);
            }




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

                EnemyGenerator(animalCount);
            }
            else
            {

                if (GlobalScripts.CurrLevelIndex == 13)
                {
                    Transform temp = Instantiate(TargetsObject.transform.GetChild(0), RandomPostions[TempIndex].transform.position, RandomPostions[TempIndex].transform.rotation) as Transform;
                    //  TargetsObject[TempIndex].SetActive(true);
                    Animals[animalCount] = temp.gameObject;
                }
                else if (GlobalScripts.CurrLevelIndex == 14)
                {
                    Transform temp = Instantiate(TargetsObject.transform.GetChild(1), RandomPostions[TempIndex].transform.position, RandomPostions[TempIndex].transform.rotation) as Transform;
                    //  TargetsObject[TempIndex].SetActive(true);
                    Animals[animalCount] = temp.gameObject;
                }
                else
                {
                    Transform temp = Instantiate(TargetsObject.transform.GetChild(GlobalScripts.CurrLevelIndex), RandomPostions[TempIndex].transform.position, RandomPostions[TempIndex].transform.rotation) as Transform;
                    //  TargetsObject[TempIndex].SetActive(true);
                    Animals[animalCount] = temp.gameObject;
                }
                //F
                AnimalLungs = GameObject.FindGameObjectsWithTag("Lungs");
                AnimalBrain = GameObject.FindGameObjectsWithTag("Brain");
                AnimalHeart = GameObject.FindGameObjectsWithTag("Heart");
            }
        }
        


    }
}
