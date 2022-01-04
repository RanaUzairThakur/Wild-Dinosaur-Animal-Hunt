
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ThirdPersonCover;

public enum ObjectiveTypes{Secure,Kill,Rescue};
[System.Serializable]
public class AssociatedObject
{
	public bool DisableOnNext;
	public GameObject Prefab;
	public GameObject AObject;
	public GameObject Instanced;
}
[System.Serializable]
public class Objective
{
	public ObjectiveTypes type;
	public bool SwitchPlayer;
	public Transform PositionRef;
	public bool Use;
	public GameObject[] AsociatedObjectsToEnable;
	public GameObject[] AsociatedObjectsToDisable; 
	public AssociatedObject[] AsociatedObjects;



//	public GameObject SecurePickUp;
}
[System.Serializable]
public class Level
{
	public bool SwitchPlayer;
	public Transform PositionRef;
	public string Name;
	public Objective[] All;
}
//[System.Serializable]
//public class TargetToSwitch
//{
//	public int MissionIndex;
//	public int ProgressIndex;
//	public Transform PositionRef;
//}
public class ObjectiveHandler : MonoBehaviour {

	public static ObjectiveHandler instance;

	public Level[] AllObjectives;
	public Image toFade;
	[HideInInspector]
	public int currentLevel;
	bool Started;

	public int Progress;
	public int Enemies;
	public Text lastcheckpointtext; 
	bool fading;
	bool fadeIn;
	bool fadeOut;

	public int[] LevelsOrder;
	public Level[] AllObjectivesTemp;
	public GameObject [] missionobj;

	public GameObject players,players1;
    public Text killedEnemies;
    public int  TotalEnemies,KilledEnem;

    Transform Player;
	int w;
	void Awake()
	{

		if (PlayerPrefs.GetInt ("Selection") == 0) 
		{
			//MissionController.instance.Player = players;
            players.SetActive(true);
            players.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().enabled = false;
            players.transform.GetChild(2).gameObject.SetActive(false);
        }
		else if (PlayerPrefs.GetInt ("Selection") == 1) 
		{
			//MissionController.instance.Player = players1;
            players1.SetActive(true);
            players1.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().enabled = false;
            players1.transform.GetChild(2).gameObject.SetActive(false);
        }

        //if (MissionController.instance.Player != null)
        //    Player = MissionController.instance.Player.transform;

        instance = this;
		AllObjectivesTemp = new Level[AllObjectives.Length];
		for (int j = 0; j < AllObjectives.Length; j++) 
		{
			AllObjectivesTemp [j] = AllObjectives [j];
		}
//		AllObjectivesTemp = AllObjectives;

		for (int i = 0; i < AllObjectives.Length; i++) 
		{
//			print ("i: "+i+" and order: "+LevelsOrder[i]);
		//	AllObjectives [i] = AllObjectivesTemp [LevelsOrder[i]];
		}
		currentLevel = GlobalScripts.CurrLevelIndex;
        

	///	Debug.Log ("mohsin"+currentLevel);
	}

	// Use this for initialization
	void Start () 
	{
		Started = false;

	}
	public void PreObjective()
	{
		Progress = 0;
		//------------------------------

		if(AllObjectives [currentLevel].All [Progress].Use)
		{
			foreach (GameObject o in AllObjectives [currentLevel].All [Progress].AsociatedObjectsToEnable) 
			{
				if (o)
					o.SetActive (true);
			}

			foreach (GameObject b in AllObjectives [currentLevel].All [Progress].AsociatedObjectsToDisable) 
			{
				if (b)
					b.SetActive (false);
			}
		}

		//------------------------------
	}
    void CalculateLevelTime()
    {
        if (GlobalScripts.CurrLevelIndex <= 5)
        {
            Timer.timemanager.time = 80 * Enemies;
        }
        else if (GlobalScripts.CurrLevelIndex > 5 && GlobalScripts.CurrLevelIndex < 11)
        {
            Timer.timemanager.time = 60 * Enemies;
        }
        else if (GlobalScripts.CurrLevelIndex >= 11)
        {
            Timer.timemanager.time = 40 * Enemies;
        }
        //Debug.Log("TImer " + Timer.timemanager.time);
    }
    public void InitObjeiveMech()
	{
		Started = true;
		Progress = 0;

		currentLevel = GlobalScripts.CurrLevelIndex;
      //  currentLevel = 4;
        //print("currentLevel :"+currentLevel);
      

		if (AllObjectives [currentLevel].All [Progress].type == ObjectiveTypes.Kill)
			Enemies = AllObjectives [currentLevel].All [Progress].AsociatedObjects.Length;

        CalculateLevelTime();
    //	NGUIDebug.Log("Assosiated object lenght = "+Enemies);
        foreach (AssociatedObject g in AllObjectives[currentLevel].All[Progress].AsociatedObjects)
		{
			if (g.Prefab != null) 
			{
                    //g.AObject = this.GetComponent<LevelsContoller>().EnemyGenerator();
                    g.Instanced = Instantiate (g.Prefab, g.AObject.transform.position, g.AObject.transform.rotation);
//				
					//g.Instanced.GetComponent<AIController> ().SightDistance = 25;
					//g.Instanced.GetComponent<AIController> ().NoticeDistance = 16;
					//g.Instanced.GetComponent<AIController> ().FriendHurtDistance = 10;
					//g.Instanced.GetComponent<AIController> ().MinStandDistance = 4;


				//if (currentLevel == 4) {
				//	g.Instanced.GetComponent<AIController> ().MaxStandDistance = 7;
				//} else
				//{
				//	g.Instanced.GetComponent<AIController> ().MaxStandDistance = 20;
				//}

					//g.Instanced.GetComponent<AIController> ().FieldOfView = 90;

				//	Debug.Log ("Asosiated objects " +AsociatedObjects );


//				if (Progress == 0 && currentLevel== 0) 
//				{
//					g.Instanced = Instantiate (g.Prefab, g.AObject.transform.position, g.AObject.transform.rotation);
//					g.Instanced.GetComponent<AIController> ().MaxStandDistance = 55;
//					//Debug.Log ("dsfnkjfvnxv.nx.,mvnx.vnxkvj.njv.nv.vkj");
//				}
			}

			else
				g.AObject.SetActive (true);
		}
        /*
		if (currentLevel== 0) 
		{

		AllObjectives [currentLevel].All [0].AsociatedObjects [0].Instanced.GetComponent<AIController> ().MaxStandDistance = 7;
		AllObjectives [currentLevel].All [0].AsociatedObjects [0].Instanced.GetComponent<AIController> ().SightDistance = 12;
		AllObjectives [currentLevel].All [0].AsociatedObjects [0].Instanced.GetComponent<AIController> ().FriendHurtDistance = 30;

		//AllObjectives [currentLevel].All [0].AsociatedObjects [1].Instanced.GetComponent<AIController> ().MaxStandDistance = 60;
		//AllObjectives [currentLevel].All [0].AsociatedObjects [1].Instanced.GetComponent<AIController> ().SightDistance  = 35;
	//	AllObjectives [currentLevel].All [0].AsociatedObjects [2].Instanced.GetComponent<AIController> ().MaxStandDistance = 8;
		}

		if (currentLevel== 1) 
		{

			AllObjectives [currentLevel].All [0].AsociatedObjects [0].Instanced.GetComponent<AIController> ().MaxStandDistance = 7;
			//AllObjectives [currentLevel].All [0].AsociatedObjects [0].Instanced.GetComponent<AIController> ().SightDistance = 25;
			AllObjectives [currentLevel].All [0].AsociatedObjects [0].Instanced.GetComponent<AIController> ().FriendHurtDistance = 13;

			AllObjectives [currentLevel].All [0].AsociatedObjects [1].Instanced.GetComponent<AIController> ().MaxStandDistance = 13;
			AllObjectives [currentLevel].All [0].AsociatedObjects [1].Instanced.GetComponent<AIController> ().FriendHurtDistance = 15;
			//AllObjectives [currentLevel].All [0].AsociatedObjects [1].Instanced.GetComponent<AIController> ().SightDistance  = ;

			AllObjectives [currentLevel].All [0].AsociatedObjects [2].Instanced.GetComponent<AIController> ().MaxStandDistance = 60;
			AllObjectives [currentLevel].All [0].AsociatedObjects [2].Instanced.GetComponent<AIController> ().FriendHurtDistance = 20;

			//AllObjectives [currentLevel].All [0].AsociatedObjects [3].Instanced.GetComponent<AIController> ().MaxStandDistance = 7;
			//AllObjectives [currentLevel].All [0].AsociatedObjects [3].Instanced.GetComponent<AIController> ().FriendHurtDistance = 20;

//			AllObjectives [currentLevel].All [0].AsociatedObjects [4].Instanced.GetComponent<AIController> ().MaxStandDistance = 15;
//			AllObjectives [currentLevel].All [0].AsociatedObjects [4].Instanced.GetComponent<AIController> ().FriendHurtDistance = 20;
//
//			AllObjectives [currentLevel].All [0].AsociatedObjects [5].Instanced.GetComponent<AIController> ().MaxStandDistance = 60;
//			AllObjectives [currentLevel].All [0].AsociatedObjects [5].Instanced.GetComponent<AIController> ().FriendHurtDistance = 20;
//			AllObjectives [currentLevel].All [0].AsociatedObjects [5].Instanced.GetComponent<AIController> ().SightDistance = 33;
//
//			AllObjectives [currentLevel].All [0].AsociatedObjects [6].Instanced.GetComponent<AIController> ().MaxStandDistance = 50;
//			AllObjectives [currentLevel].All [0].AsociatedObjects [6].Instanced.GetComponent<AIController> ().FriendHurtDistance =20;
//			AllObjectives [currentLevel].All [0].AsociatedObjects [6].Instanced.GetComponent<AIController> ().SightDistance = 30;
//			AllObjectives [currentLevel].All [0].AsociatedObjects [6].Instanced.GetComponent<AIController> ().NoticeDistance = 15;
//
//			AllObjectives [currentLevel].All [0].AsociatedObjects [7].Instanced.GetComponent<AIController> ().SightDistance = 33;
//			AllObjectives [currentLevel].All [0].AsociatedObjects [7].Instanced.GetComponent<AIController> ().MaxStandDistance = 7;
//			AllObjectives [currentLevel].All [0].AsociatedObjects [7].Instanced.GetComponent<AIController> ().FriendHurtDistance =20;
//
//			AllObjectives [currentLevel].All [0].AsociatedObjects [8].Instanced.GetComponent<AIController> ().SightDistance = 33;
//			AllObjectives [currentLevel].All [0].AsociatedObjects [8].Instanced.GetComponent<AIController> ().MaxStandDistance = 10;
//			AllObjectives [currentLevel].All [0].AsociatedObjects [8].Instanced.GetComponent<AIController> ().FriendHurtDistance =15;
//
//			AllObjectives [currentLevel].All [0].AsociatedObjects [9].Instanced.GetComponent<AIController> ().SightDistance = 35;
//			AllObjectives [currentLevel].All [0].AsociatedObjects [9].Instanced.GetComponent<AIController> ().MaxStandDistance = 60;
//			AllObjectives [currentLevel].All [0].AsociatedObjects [9].Instanced.GetComponent<AIController> ().FriendHurtDistance =20;
//			AllObjectives [currentLevel].All [0].AsociatedObjects [6].Instanced.GetComponent<AIController> ().NoticeDistance = 20;
		}

		if (currentLevel== 2) 
		{
			AllObjectives [currentLevel].All [0].AsociatedObjects [0].Instanced.GetComponent<AIController> ().SightDistance = 20;
			AllObjectives [currentLevel].All [0].AsociatedObjects [0].Instanced.GetComponent<AIController> ().FriendHurtDistance = 20;

			AllObjectives [currentLevel].All [0].AsociatedObjects [1].Instanced.GetComponent<AIController> ().MaxStandDistance = 7;
			AllObjectives [currentLevel].All [0].AsociatedObjects [1].Instanced.GetComponent<AIController> ().FriendHurtDistance = 15;
			AllObjectives [currentLevel].All [0].AsociatedObjects [1].Instanced.GetComponent<AIController> ().SightDistance = 15;

			//AllObjectives [currentLevel].All [0].AsociatedObjects [2].Instanced.GetComponent<AIController> ().MaxStandDistance = 60;
			//AllObjectives [currentLevel].All [0].AsociatedObjects [2].Instanced.GetComponent<AIController> ().FriendHurtDistance = 20;
			//AllObjectives [currentLevel].All [0].AsociatedObjects [2].Instanced.GetComponent<AIController> ().SightDistance = 15;

			//AllObjectives [currentLevel].All [0].AsociatedObjects [3].Instanced.GetComponent<AIController> ().MaxStandDistance = 15;
			//AllObjectives [currentLevel].All [0].AsociatedObjects [3].Instanced.GetComponent<AIController> ().FriendHurtDistance =7;


			//AllObjectives [currentLevel].All [0].AsociatedObjects [4].Instanced.GetComponent<AIController> ().FriendHurtDistance = 20;

			//AllObjectives [currentLevel].All [0].AsociatedObjects [5].Instanced.GetComponent<AIController> ().MaxStandDistance = 7;
			//AllObjectives [currentLevel].All [0].AsociatedObjects [5].Instanced.GetComponent<AIController> ().FriendHurtDistance = 20;


//			AllObjectives [currentLevel].All [0].AsociatedObjects [7].Instanced.GetComponent<AIController> ().MaxStandDistance = 60;
//			AllObjectives [currentLevel].All [0].AsociatedObjects [7].Instanced.GetComponent<AIController> ().FriendHurtDistance = 15;
//		//	AllObjectives [currentLevel].All [0].AsociatedObjects [5].Instanced.GetComponent<AIController> ().MaxStandDistance = 40;
//			AllObjectives [currentLevel].All [0].AsociatedObjects [9].Instanced.GetComponent<AIController> ().FriendHurtDistance = 10;
//			AllObjectives [currentLevel].All [0].AsociatedObjects [9].Instanced.GetComponent<AIController> ().SightDistance = 15;
//			//	AllObjectives [currentLevel].All [0].AsociatedObjects [2].Instanced.GetComponent<AIController> ().MaxStandDistance = 8;
//			AllObjectives [currentLevel].All [0].AsociatedObjects [10].Instanced.GetComponent<AIController> ().MaxStandDistance = 10;
//			AllObjectives [currentLevel].All [0].AsociatedObjects [10].Instanced.GetComponent<AIController> ().FriendHurtDistance = 10;
	}

		if (currentLevel== 3) 
		{    //   
			AllObjectives [currentLevel].All [0].AsociatedObjects [0].Instanced.GetComponent<AIController> ().FriendHurtDistance = 25;
			AllObjectives [currentLevel].All [0].AsociatedObjects [1].Instanced.GetComponent<AIController> ().FriendHurtDistance = 15;
			AllObjectives [currentLevel].All [0].AsociatedObjects [2].Instanced.GetComponent<AIController> ().FriendHurtDistance = 15;
			AllObjectives [currentLevel].All [0].AsociatedObjects [2].Instanced.GetComponent<AIController> ().SightDistance = 10;

			AllObjectives [currentLevel].All [0].AsociatedObjects [3].Instanced.GetComponent<AIController> ().MaxStandDistance = 40;
			AllObjectives [currentLevel].All [0].AsociatedObjects [3].Instanced.GetComponent<AIController> ().FriendHurtDistance = 20;

//			AllObjectives [currentLevel].All [0].AsociatedObjects [4].Instanced.GetComponent<AIController> ().MaxStandDistance = 7;
////			AllObjectives [currentLevel].All [0].AsociatedObjects [4].Instanced.GetComponent<Animator> ().SetLayerWeight(AllObjectives [currentLevel].All [0].AsociatedObjects [4].Instanced.GetComponent<Animator> ().GetLayerIndex("Base Layer1"), 1);

////			anims[1].SetLayerWeight(anim.GetLayerIndex("AnimationLayer2"), animWeight);
//			AllObjectives [currentLevel].All [0].AsociatedObjects [5].Instanced.GetComponent<AIController> ().MaxStandDistance = 7;
//			AllObjectives [currentLevel].All [0].AsociatedObjects [5].Instanced.GetComponent<AIController> ().SightDistance = 15;

//			AllObjectives [currentLevel].All [0].AsociatedObjects [6].Instanced.GetComponent<AIController> ().MaxStandDistance = 7;
//			AllObjectives [currentLevel].All [0].AsociatedObjects [7].Instanced.GetComponent<AIController> ().MaxStandDistance = 40;
//			AllObjectives [currentLevel].All [0].AsociatedObjects [8].Instanced.GetComponent<AIController> ().MaxStandDistance = 40;
//			AllObjectives [currentLevel].All [0].AsociatedObjects [8].Instanced.GetComponent<AIController> ().FriendHurtDistance = 12;

//			AllObjectives [currentLevel].All [0].AsociatedObjects [9].Instanced.GetComponent<AIController> ().MaxStandDistance = 40;
//			//AllObjectives [currentLevel].All [0].AsociatedObjects [10].Instanced.GetComponent<AIController> ().MaxStandDistance = 60;
			//AllObjectives [currentLevel].All [0].AsociatedObjects [10].Instanced.GetComponent<AIController> ().FriendHurtDistance = 15;

			//AllObjectives [currentLevel].All [0].AsociatedObjects [11].Instanced.GetComponent<AIController> ().MaxStandDistance = 60;
			//AllObjectives [currentLevel].All [0].AsociatedObjects [11].Instanced.GetComponent<AIController> ().FriendHurtDistance = 15;
			//AllObjectives [currentLevel].All [0].AsociatedObjects [11].Instanced.GetComponent<AIController> ().SightDistance = 35;
			////	AllObjectives [currentLevel].All [0].AsociatedObjects [2].Instanced.GetComponent<AIController> ().MaxStandDistance = 8;
			//AllObjectives [currentLevel].All [0].AsociatedObjects [12].Instanced.GetComponent<AIController> ().SightDistance = 35;
			//AllObjectives [currentLevel].All [0].AsociatedObjects [13].Instanced.GetComponent<AIController> ().SightDistance = 35;
			//AllObjectives [currentLevel].All [0].AsociatedObjects [12].Instanced.GetComponent<AIController> ().MaxStandDistance = 60;
			//AllObjectives [currentLevel].All [0].AsociatedObjects [13].Instanced.GetComponent<AIController> ().MaxStandDistance = 60;
		}


		if (currentLevel== 4) 
		{
		    AllObjectives [currentLevel].All [0].AsociatedObjects [0].Instanced.GetComponent<AIController> ().MaxStandDistance = 40;
			AllObjectives [currentLevel].All [0].AsociatedObjects [4].Instanced.GetComponent<AIController> ().MaxStandDistance = 40;
//			AllObjectives [currentLevel].All [0].AsociatedObjects [4].Instanced.GetComponent<Animator> ().runtimeAnimatorController = anims [1] as RuntimeAnimatorController;
			//AllObjectives [currentLevel].All [0].AsociatedObjects [7].Instanced.GetComponent<AIController> ().MaxStandDistance = 40;

			//AllObjectives [currentLevel].All [0].AsociatedObjects [7].Instanced.GetComponent<AIController> ().FriendHurtDistance = 20;
			//AllObjectives [currentLevel].All [0].AsociatedObjects [9].Instanced.GetComponent<AIController> ().FriendHurtDistance = 25;

			//AllObjectives [currentLevel].All [0].AsociatedObjects [10].Instanced.GetComponent<AIController> ().MaxStandDistance = 40;
			//AllObjectives [currentLevel].All [0].AsociatedObjects [10].Instanced.GetComponent<AIController> ().SightDistance = 40;
		

			//AllObjectives [currentLevel].All [0].AsociatedObjects [12].Instanced.GetComponent<AIController> ().MaxStandDistance = 40;
			//AllObjectives [currentLevel].All [0].AsociatedObjects [12].Instanced.GetComponent<AIController> ().SightDistance = 40;

//			AllObjectives [currentLevel].All [0].AsociatedObjects [19].Instanced.GetComponent<AIController> ().FriendHurtDistance = 15;
//			AllObjectives [currentLevel].All [0].AsociatedObjects [20].Instanced.GetComponent<AIController> ().FriendHurtDistance = 15;
//			AllObjectives [currentLevel].All [0].AsociatedObjects [21].Instanced.GetComponent<AIController> ().FriendHurtDistance = 15;
//
//			if (Progress == 1) {
//				AllObjectives [currentLevel].All [1].AsociatedObjects [0].Instanced.GetComponent<AIController> ().MaxStandDistance = 60;
//
//			}
//			AllObjectives [currentLevel].All [0].AsociatedObjects [3].Instanced.GetComponent<AIController> ().MaxStandDistance = 40;
//			AllObjectives [currentLevel].All [0].AsociatedObjects [4].Instanced.GetComponent<AIController> ().MaxStandDistance = 18;
//			AllObjectives [currentLevel].All [0].AsociatedObjects [5].Instanced.GetComponent<AIController> ().MaxStandDistance = 18;
//			AllObjectives [currentLevel].All [0].AsociatedObjects [6].Instanced.GetComponent<AIController> ().MaxStandDistance = 18;
//			AllObjectives [currentLevel].All [0].AsociatedObjects [7].Instanced.GetComponent<AIController> ().MaxStandDistance = 40;
//			AllObjectives [currentLevel].All [0].AsociatedObjects [8].Instanced.GetComponent<AIController> ().MaxStandDistance = 40;
//			AllObjectives [currentLevel].All [0].AsociatedObjects [9].Instanced.GetComponent<AIController> ().MaxStandDistance = 40;
//			AllObjectives [currentLevel].All [0].AsociatedObjects [10].Instanced.GetComponent<AIController> ().MaxStandDistance = 40;
//			AllObjectives [currentLevel].All [0].AsociatedObjects [10].Instanced.GetComponent<AIController> ().FriendHurtDistance = 15;
//			//	AllObjectives [currentLevel].All [0].AsociatedObjects [2].Instanced.GetComponent<AIController> ().MaxStandDistance = 8;
		}

        if (currentLevel == 10)
        {

            AllObjectives[currentLevel].All[0].AsociatedObjects[0].Instanced.GetComponent<AIController>().MaxStandDistance = 3;
            AllObjectives[currentLevel].All[0].AsociatedObjects[0].Instanced.GetComponent<AIController>().SightDistance = 3;
            AllObjectives[currentLevel].All[0].AsociatedObjects[0].Instanced.GetComponent<AIController>().FriendHurtDistance = 3;

            AllObjectives[currentLevel].All[0].AsociatedObjects[0].Instanced.GetComponent<AIController>().NoticeDistance = 3;

            AllObjectives[currentLevel].All[0].AsociatedObjects[1].Instanced.GetComponent<AIController>().MaxStandDistance = 5;
            AllObjectives[currentLevel].All[0].AsociatedObjects[1].Instanced.GetComponent<AIController>().SightDistance = 7;

            AllObjectives[currentLevel].All[0].AsociatedObjects[1].Instanced.GetComponent<AIController>().NoticeDistance = 7;
            //	AllObjectives [currentLevel].All [0].AsociatedObjects [2].Instanced.GetComponent<AIController> ().MaxStandDistance = 8;
        }

        if (currentLevel == 11)
        {

            AllObjectives[currentLevel].All[0].AsociatedObjects[0].Instanced.GetComponent<AIController>().MaxStandDistance = 3;
            AllObjectives[currentLevel].All[0].AsociatedObjects[0].Instanced.GetComponent<AIController>().SightDistance = 3;
            AllObjectives[currentLevel].All[0].AsociatedObjects[0].Instanced.GetComponent<AIController>().FriendHurtDistance = 3;

            AllObjectives[currentLevel].All[0].AsociatedObjects[0].Instanced.GetComponent<AIController>().NoticeDistance = 3;

            AllObjectives[currentLevel].All[0].AsociatedObjects[1].Instanced.GetComponent<AIController>().MaxStandDistance = 5;
            AllObjectives[currentLevel].All[0].AsociatedObjects[1].Instanced.GetComponent<AIController>().SightDistance = 7;

            AllObjectives[currentLevel].All[0].AsociatedObjects[1].Instanced.GetComponent<AIController>().NoticeDistance = 7;
            //	AllObjectives [currentLevel].All [0].AsociatedObjects [2].Instanced.GetComponent<AIController> ().MaxStandDistance = 8;
        }
        */
        //------------------------------

        if (AllObjectives [currentLevel].All [Progress].SwitchPlayer)
			SwitchPlayerPosition (AllObjectives [currentLevel].All [Progress].PositionRef);

		//------------------------------

		if(AllObjectives [currentLevel].All [Progress].Use)
		{
			foreach (GameObject o in AllObjectives [currentLevel].All [Progress].AsociatedObjectsToEnable) 
			{
				if (o)
					o.SetActive (true);
			}

			foreach (GameObject b in AllObjectives [currentLevel].All [Progress].AsociatedObjectsToDisable) 
			{
				if (b)
					b.SetActive (false);
			}
		}

        //------------------------------
        TotalEnemies = Enemies;
        killedEnemies.text = KilledEnem.ToString("0")+"/"+ TotalEnemies.ToString("0");
    }
	// Update is called once per frame
	void Update ()
	{
		if (!Started)
			return;
        
		if (fading) 
		{
			if (fadeIn)
			{
				FadingIn ();
			}
			else if(fadeOut)
			{
				FadingOut ();
			}
		}
	}
	void FadingIn()
	{
		Debug.Log ("in: "+toFade.color.a);
		if (toFade.color.a < 1)
			toFade.color = new Vector4 (toFade.color.r, toFade.color.g, toFade.color.b, toFade.color.a + Time.deltaTime);
		else 
		{
			fadeIn = false;
			fadeOut = true;
			OutOfViewProcess ();
		}
	}
	void FadingOut()
	{
		Debug.Log ("out: "+toFade.color.a);
		if (toFade.color.a > 0)
			toFade.color = new Vector4 (toFade.color.r, toFade.color.g, toFade.color.b, toFade.color.a - Time.deltaTime);
		else
		{
			fadeIn = false;
			fadeOut = false;
			fading = false;
			toFade.gameObject.SetActive (false);
		}
	}
	void OutOfViewProcess()
	{
		foreach (AssociatedObject g in AllObjectives[currentLevel].All[Progress-1].AsociatedObjects)
		{
			if (g.Prefab != null)
			{
				if (g.DisableOnNext && g.Instanced != null)
					Destroy (g.Instanced);
			} 
			else 
			{
				if (g.DisableOnNext)
					g.AObject.SetActive (false);
			}
		}


		foreach (AssociatedObject g in AllObjectives[currentLevel].All[Progress].AsociatedObjects)
		{
			if (g.Prefab != null) 
			{
				//Instantiate (g.Prefab,g.AObject.transform.position,g.AObject.transform.rotation);
				g.Instanced = Instantiate (g.Prefab, g.AObject.transform.position, g.AObject.transform.rotation);
//				g.Instanced.GetComponentInChildren<Gun> ().Damage = 50f;
				//g.Instanced.GetComponent<AIController> ().SightDistance = 25;
				//g.Instanced.GetComponent<AIController> ().NoticeDistance = 10;
				//g.Instanced.GetComponent<AIController> ().FriendHurtDistance = 10;
				//g.Instanced.GetComponent<AIController> ().MinStandDistance = 4;
				//if (currentLevel == 4) {
				//	g.Instanced.GetComponent<AIController> ().MaxStandDistance = 10;
				//} else
				//{
				//	g.Instanced.GetComponent<AIController> ().MaxStandDistance = 20;
				//}



			} 
			else
			{
				if (g.AObject)
					g.AObject.SetActive (true);
			}


		}
        /*

		if (currentLevel== 4 && Progress == 1) {
			AllObjectives [currentLevel].All [1].AsociatedObjects [7].Instanced.GetComponent<AIController> ().MaxStandDistance = 30;
			AllObjectives [currentLevel].All [1].AsociatedObjects [8].Instanced.GetComponent<AIController> ().MaxStandDistance = 30;
			AllObjectives [currentLevel].All [1].AsociatedObjects [9].Instanced.GetComponent<AIController> ().MaxStandDistance = 30;

			//AllObjectives [currentLevel].All [0].AsociatedObjects [2].Instanced.GetComponent<AIController> ().SightDistance = 40;
			AllObjectives [currentLevel].All [0].AsociatedObjects [2].Instanced.GetComponent<AIController> ().FriendHurtDistance = 25;
			AllObjectives [currentLevel].All [0].AsociatedObjects [4].Instanced.GetComponent<AIController> ().FriendHurtDistance = 20;
			AllObjectives [currentLevel].All [0].AsociatedObjects [5].Instanced.GetComponent<AIController> ().FriendHurtDistance = 20;
			AllObjectives [currentLevel].All [0].AsociatedObjects [3].Instanced.GetComponent<AIController> ().FriendHurtDistance = 15;
			AllObjectives [currentLevel].All [0].AsociatedObjects [12].Instanced.GetComponent<AIController> ().FriendHurtDistance = 15;
			AllObjectives [currentLevel].All [0].AsociatedObjects [8].Instanced.GetComponent<AIController> ().FriendHurtDistance = 20;

		}

		if (currentLevel == 4 && Progress == 3) {

			AllObjectives [currentLevel].All [1].AsociatedObjects [15].Instanced.GetComponent<AIController> ().MaxStandDistance = 30;
			AllObjectives [currentLevel].All [1].AsociatedObjects [16].Instanced.GetComponent<AIController> ().MaxStandDistance = 30;
		//	AllObjectives [currentLevel].All [1].AsociatedObjects [9].Instanced.GetComponent<AIController> ().MaxStandDistance = 30;

		}

    */
		switch(AllObjectives[currentLevel].All[Progress].type)
		{
		case ObjectiveTypes.Kill:
			Enemies = AllObjectives [currentLevel].All [Progress].AsociatedObjects.Length;
			break;
//		case ObjectiveTypes.Secure:
//			w = MissionController.instance.Player.GetComponent<ThirdPersonCover.CharacterMotor> ()._currentWeapon;
//			MissionController.instance.Player.GetComponent<ThirdPersonCover.CharacterMotor> ().InputWeapon (0);
//			MissionController.instance.Player.GetComponent<Animator> ().SetBool ("Carry",true);
//			break;
//		case ObjectiveTypes.Rescue:
//			MissionController.instance.Player.GetComponent<ThirdPersonCover.CharacterMotor> ().InputWeapon (w);
//			MissionController.instance.Player.GetComponent<Animator> ().SetBool ("Carry",false);
//			break;
		}
		//switch(AllObjectives[currentLevel].All[Progress-1].type)
		//{
		//case ObjectiveTypes.Secure:
		//	w = MissionController.instance.Player.GetComponent<ThirdPersonCover.CharacterMotor> ()._currentWeapon;
		//	MissionController.instance.Player.GetComponent<ThirdPersonCover.CharacterMotor> ().InputWeapon (0);
		//	MissionController.instance.Player.GetComponent<Animator> ().SetBool ("Carry",true);
		//	break;
		//case ObjectiveTypes.Rescue:
		//	MissionController.instance.Player.GetComponent<ThirdPersonCover.CharacterMotor> ().InputWeapon (w);
		//	MissionController.instance.Player.GetComponent<Animator> ().SetBool ("Carry",false);
		//	break;
		//}

		//------------------------------

		if (AllObjectives [currentLevel].All [Progress].SwitchPlayer)
			SwitchPlayerPosition (AllObjectives [currentLevel].All [Progress].PositionRef);

		//------------------------------

		if(AllObjectives [currentLevel].All [Progress].Use)
		{
			foreach (GameObject o in AllObjectives [currentLevel].All [Progress].AsociatedObjectsToEnable) 
			{
				if (o)
					o.SetActive (true);
			}

			foreach (GameObject b in AllObjectives [currentLevel].All [Progress].AsociatedObjectsToDisable) 
			{
				if (b)
					b.SetActive (false);
			}
		}

		//------------------------------
	}
	public void SwitchPlayerPosition(Transform Position)
	{
		if (Player) {
            //Position = this.GetComponent<LevelsContoller>().GetPlayerPosition();
            Player.position = Position.position;
        
		
		}
	}
	public void Progressed()
	{

//		Debug.LogError ("Objevtive Complete");
		Progress++;
		level_caseScenerio ();
		if (Progress >= AllObjectives [currentLevel].All.Length)
		{
			// level complete
//			Debug.LogError ("Level Complete");
			if (currentLevel + 1 >= AllObjectives.Length) 
			{
				foreach (AssociatedObject g in AllObjectives[currentLevel].All[Progress-1].AsociatedObjects)
				{
					if (g.Instanced != null) 
					{
						if(g.DisableOnNext)
							Destroy (g.Instanced);
					}
					else
					{
						if (g.DisableOnNext)
							g.AObject.SetActive (false);
					}
				}

				//GameManager_TN90.instance.OnSuccessAfter (true);
			}
			else
			{
				foreach (AssociatedObject g in AllObjectives[currentLevel].All[Progress-1].AsociatedObjects)
				{
					if (g.Instanced != null) 
					{
						if (g.DisableOnNext)
							Destroy (g.Instanced);
					}
					else
					{
						if (g.DisableOnNext)
							g.AObject.SetActive (false);
					}
				}
				//GameManager_TN90.instance.OnSuccessAfter ();
			}
			//switch(AllObjectives[currentLevel].All[Progress-1].type)
			//{
			//case ObjectiveTypes.Rescue:
			//	MissionController.instance.Player.GetComponent<Animator> ().SetBool ("Carry",false);
			//	MissionController.instance.Player.GetComponent<ThirdPersonCover.CharacterMotor> ().InputWeapon (w);
			//	break;
			//}
		}
		else
		{
			toFade.gameObject.SetActive (true);
			fading = true;
			fadeIn = true;
			fadeOut = false;
		}
	}
	public void Pauseenemies()
	{
		if (AllObjectives [currentLevel].All [Progress].type != ObjectiveTypes.Kill) 
		{
			return;
		}
		foreach (AssociatedObject g in AllObjectives[currentLevel].All[Progress].AsociatedObjects) {
			if (g.DisableOnNext)
				g.AObject.SetActive (false);
		}
	}
	public void Resumeenemies()
	{
		if (AllObjectives [currentLevel].All [Progress].type != ObjectiveTypes.Kill) 
		{
			return;
		}
		foreach (AssociatedObject g in AllObjectives[currentLevel].All[Progress].AsociatedObjects) {
			if (g.DisableOnNext)
				g.AObject.SetActive (true);
		}
	}
	public void EnemyKilled()
	{
		if (AllObjectives [currentLevel].All [Progress].type != ObjectiveTypes.Kill && Enemies == 0)
        {
			return;
		}


		Enemies--;
        KilledEnem++;
        killedEnemies.text = KilledEnem.ToString("0") + "/" + TotalEnemies.ToString("0");
        if (Enemies == 0)
        {
         //   FindObjectOfType<Slow_Motion_Eft>().Bullet_Spawn();
        }


        // Level Complete Comment here(F)
        //      if (Enemies <= 0)
        //{

        //          //GameObject.FindGameObjectWithTag("MainCamera").GetComponent<UnityStandardAssets.ImageEffects.VignetteAndChromaticAberration>().enabled = true;

        //          Progress++;
        //	level_caseScenerio ();
        //	if (Progress >= AllObjectives [currentLevel].All.Length) {
        //              // level complete
        //              Debug.Log("Level Complete ");
        //              if (currentLevel + 1 >= AllObjectives.Length) 
        //		{
        //			GameManager_TN90.instance.OnSuccessAfter (false);//awa (true)
        //		}
        //		else
        //			GameManager_TN90.instance.OnSuccessAfter ();
        //	}
        //	else
        //	{
        //		toFade.gameObject.SetActive (true);
        //		fading = true;
        //		fadeIn = true;
        //		fadeOut = false;
        //	}
        //}



    }

    public void level_caseScenerio(){
		if (currentLevel == 0) {
			//Debug.Log ("Current " + Progress);


			if (Progress == 1) {
				//				AllObjectives [currentLevel].All [0].AsociatedObjects [0].AObject.GetComponent<Animator> ().SetTrigger ("run");
				//				AllObjectives [currentLevel].All [0].AsociatedObjects [1].AObject.GetComponent<Animator> ().SetTrigger ("run");
				//				AllObjectives [currentLevel].All [1].AsociatedObjects [0].AObject.GetComponent<FightingPed> ().dead = false;
				//Debug.Log ("Current Progress 1");
				//checklastobjective ("Now Destroy the Helicopter ");
			}
			if (Progress == 2) {
				//checklastobjective (" \nYou Completed the mission");
			}
			if (Progress == 3) {
				//checklastobjective (" \nDrive Train to next station");
			}
			if (Progress == 4) {
				//		GameObject.FindGameObjectWithTag ("Player").GetComponent<hoMoveTrain> ().controller.value = 0;
			}
		} else if (currentLevel == 1) {


			if (Progress == 1) {
				//				AllObjectives [currentLevel].All [0].AsociatedObjects [0].AObject.GetComponent<Animator> ().SetTrigger ("run");
				//				AllObjectives [currentLevel].All [0].AsociatedObjects [1].AObject.GetComponent<Animator> ().SetTrigger ("run");
				//				AllObjectives [currentLevel].All [1].AsociatedObjects [0].AObject.GetComponent<FightingPed> ().dead = false;
				Debug.Log ("Current Progress 1");
				//checklastobjective ("Good work. Now Pick up Explosive Barrel");
			}
			if (Progress == 2) {
				//	checklastobjective (" Place Barral near the Tank ");
			}
			if (Progress == 3) {
				checklastobjective (" Now Shoot Barral to Destroy Tank ");
			}
			if (Progress == 4) {
				checklastobjective (" Go to the Final Check Point. ");
				//		GameObject.FindGameObjectWithTag ("Player").GetComponent<hoMoveTrain> ().controller.value = 0;
			}
			//			if (Progress == 1) {
			//				AllObjectives [currentLevel].All [0].AsociatedObjects [6].AObject.GetComponent<Animator> ().SetTrigger ("run");
			//				AllObjectives [currentLevel].All [0].AsociatedObjects [7].AObject.GetComponent<Animator> ().SetTrigger ("run");
			//				AllObjectives [currentLevel].All [1].AsociatedObjects [0].AObject.GetComponent<FightingPed> ().dead = false;
			//				AllObjectives [currentLevel].All [1].AsociatedObjects [1].AObject.GetComponent<EnemyShoot> ().dead = false;
			//			}

		} else if (currentLevel == 2) {

			//			Debug.Log();
			if (Progress == 1) {
				//settextontext ("Get Keys of Door from Security Office");
				//	checklastobjective ("Get Keys of Door from Security Office");
			}

			if (Progress == 2) {
				//AllObjectives [currentLevel].All [0].AsociatedObjects [1].AObject.SetActive (false);
				//Invoke ("Progressed", 3f);
				//	checklastobjective ("Now go and Open the Door");

			}
			if (Progress == 3) {
				//AllObjectives [currentLevel].All [0].AsociatedObjects [1].AObject.SetActive (false);
				//Invoke ("Progressed", 3f);
				//missionobj [0].SetActive (true);
				checklastobjective ("Congratulations All prisoners are free Now");
				Invoke ("Progressed", 3f);

			}


		} else if (currentLevel == 3) {
			//TransformButton.SetActive (true);
			if (Progress == 1) {
				//		checklastobjective ("Get the Guns From Roof");
				//AllObjectives [currentLevel].All [0].AsociatedObjects [1].AObject.SetActive (false);

				//Invoke ("Progressed", 3f);
			}
			if (Progress == 2) {
				//	checklastobjective ("There is a Airplane in the base Find and Secure it.");
				//AllObjectives [currentLevel].All [0].AsociatedObjects [1].AObject.SetActive (false);

				//Invoke ("Progressed", 3f);
			}

			if (Progress == 3) {
				//	checklastobjective ("Now Get out From Here");
				//AllObjectives [currentLevel].All [0].AsociatedObjects [1].AObject.SetActive (false);

				//Invoke ("Progressed", 3f);
			}

			if (Progress == 4) {
				
				//AllObjectives [currentLevel].All [0].AsociatedObjects [1].AObject.SetActive (false);
				//	GameObject.FindWithTag("MainCamera").SetActive(false);
				//GameObject.FindWithTag ("camcontrol").GetComponent<camcontroller> ().lastcam ();
				//Invoke ("Progressed", 4f);
			}

		}
			else if (currentLevel == 4) {
				//TransformButton.SetActive (true);
				if (Progress == 1) {
					//		checklastobjective ("Get the Guns From Roof");
					//AllObjectives [currentLevel].All [0].AsociatedObjects [1].AObject.SetActive (false);

					//Invoke ("Progressed", 3f);
				}
				if (Progress == 2) {
					//	checklastobjective ("There is a Airplane in the base Find and Secure it.");
					//AllObjectives [currentLevel].All [0].AsociatedObjects [1].AObject.SetActive (false);

					//Invoke ("Progressed", 3f);
				}

				if (Progress == 3) {
					//	checklastobjective ("Now Get out From Here");
					//AllObjectives [currentLevel].All [0].AsociatedObjects [1].AObject.SetActive (false);

					//Invoke ("Progressed", 3f);
				}

				if (Progress == 4) {

					//AllObjectives [currentLevel].All [0].AsociatedObjects [1].AObject.SetActive (false);
					//	GameObject.FindWithTag("MainCamera").SetActive(false);
					//GameObject.FindWithTag ("camcontrol").GetComponent<camcontroller> ().lastcam ();
					//Invoke ("Progressed", 4f);
				}




		} else if (currentLevel == 10) {
			//Debug.Log ("Current " + Progress);
			//if (Progress == 1) {
			//	Debug.Log ("Current Progress 1");
			//	checklastobjective ("Nice Work Now Get to the Next Station ");
			//}
			//if (Progress == 2) {
			//	checklastobjective (" \nYou Completed the mission");
			//}
			//if (Progress == 3) {
			//	checklastobjective (" \nCongurlate You Saved Station");
			//}
		}
		//			if (Progress == 4) {
		//				GameObject.FindGameObjectWithTag ("Player").GetComponent<hoMoveTrain> ().controller.value = 0;
		//			}
		else if (currentLevel == 11) {

			if (Progress == 2) {
				AllObjectives [currentLevel].All [0].AsociatedObjects [1].AObject.SetActive (false);
				Invoke ("Progressed", 3f);

			}
		} else if (currentLevel == 12) {

			if (Progress == 2) {
				AllObjectives [currentLevel].All [0].AsociatedObjects [1].AObject.SetActive (false);
				Invoke ("Progressed", 3f);

			}
		} else if (currentLevel == 13) {

			if (Progress == 2) {
				AllObjectives [currentLevel].All [0].AsociatedObjects [2].AObject.SetActive (false);
				AllObjectives [currentLevel].All [0].AsociatedObjects [3].AObject.SetActive (false);
				//Invoke ("Progressed",3f);

			}
		}
		else if (currentLevel == 14) {

			if (Progress == 2) {
				AllObjectives [currentLevel].All [0].AsociatedObjects [1].AObject.SetActive (false);
				//AllObjectives [currentLevel].All [0].AsociatedObjects [3].AObject.SetActive (false);
				//Invoke ("Progressed",3f);

			}
		}
	}

	void checklastobjective(string text){

		//		if (Progress == AllObjectives [currentLevel].All.Length-1) {
		lastcheckpointtext.text = text;
		Invoke ("on_highlightedtext",3f);
		Invoke ("off_highlightedtext",8f);
		//}
	}
	public void on_highlightedtext(){
		lastcheckpointtext.transform.parent.gameObject.SetActive(true);

	}

	public void off_highlightedtext(){
		lastcheckpointtext.transform.parent.gameObject.SetActive(false);
	}

	public void settextontext(string settext){
		lastcheckpointtext.text = "" + settext;
		Invoke ("on_highlightedtext",0.1f);
		Invoke ("off_highlightedtext",3f);
	}






}
