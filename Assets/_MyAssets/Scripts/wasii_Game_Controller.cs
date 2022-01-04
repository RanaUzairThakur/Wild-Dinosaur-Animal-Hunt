using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wasii_Game_Controller : MonoBehaviour
{


    public static bool Forest;
    public GameObject Snowfalling, Raining,water;

    [Header("Snow Materials")]
    public Material SnowCliff;
    public Material SnowDirtEdge,SnowGroudBrush, SnowTrees, SnowWallRoof, SnowPiper, SnowTerrain, SnowTexFased;

    [Header("Forest Materials")]
    public Material ForestCliff;
    public Material ForestDirtEdge, ForestGroudBrush, ForestTrees, ForestWallRoof, ForestPiper, ForestTerrain, ForestTexFased;

    [Header("Environmet Meshes")]
    public Renderer[] CliffMesh;
    public Renderer[] DirtEdgeMesh,GroundBrushMesh,TreeMesh,WallMesh,PipeMesh,TerrainMesh,TexFasedMesh;

    [HideInInspector]
    [Header("Animal Meshes")]
    public GameObject AnimalsParent;

    [HideInInspector]
    [Header("Animal Standard Material")]
    public Material StagMaterial;
    [HideInInspector]
    public Material BearMaterial, DeerMaterial, ElephantMaterial, BoarMaterial, HippoMaterial, LionMaterial, RhinoMaterial, ZebraMaterial, WolfMaterial;

    [HideInInspector]
    [Header("Animal Transparent Material")]
    public Material StagTransparent;
    [HideInInspector]
    public Material BearTransparent, DeerTransparent, ElephantTransparent, BoarTransparent, HippoTransparent, LionTransparent, RhinoTransparent, ZebraTransparent, WolfTransparent;

   

    [Header("Skybox")]
    public Material[] skyboxes;
    public GameObject SnowSound, ForestSound;

    [Header("Scope Material")]
    public Material ScopeMaterial;

    [Header("Scope CubeMap")]
    public Texture[] ScopeCupemap;

    public GameObject ForestTree;

    public GameObject InfraredBtn;

    public static wasii_Game_Controller instance;
    private void Awake()
    {
        instance = this;
        AudioListener.pause = false;
        
       
        if(Forest)
        {
            water.SetActive(true);
            Raining.SetActive(true);
            ForestSound.SetActive(true);
            ForestTree.SetActive(true);

            RenderSettings.fog = true;
            RenderSettings.fogDensity = 0.005f;
            RenderSettings.fogColor = new Color32(63, 36, 03, 255);
            RenderSettings.skybox = skyboxes[1];
            ScopeMaterial.SetTexture("_Cube", ScopeCupemap[1]);

            int i;
            for (i = 0; i < CliffMesh.Length; i++)
            {
                CliffMesh[i].material = ForestCliff;
            }
            for (i = 0; i < DirtEdgeMesh.Length; i++)
            {
                DirtEdgeMesh[i].material = ForestDirtEdge;
            }
            for (i = 0; i < GroundBrushMesh.Length; i++)
            {
                GroundBrushMesh[i].material = ForestGroudBrush;
            }
            for (i = 0; i < TreeMesh.Length; i++)
            {
                TreeMesh[i].material = ForestTrees;
            }
            for (i = 0; i < WallMesh.Length; i++)
            {
                WallMesh[i].material = ForestWallRoof;
            }
            for (i = 0; i < PipeMesh.Length; i++)
            {
                PipeMesh[i].material = ForestPiper;
            }
            for (i = 0; i < TerrainMesh.Length; i++)
            {
                TerrainMesh[i].material = ForestTerrain;
            }
            for (i = 0; i < TexFasedMesh.Length; i++)
            {
                TexFasedMesh[i].material = ForestTexFased;
            }
        }
        else
        {   //Instantiate(ArabicCity, ArabicCityPosition.transform.position, ArabicCityPosition.transform.rotation);
            water.SetActive(false);
            Snowfalling.SetActive(true);
            SnowSound.SetActive(true);

            RenderSettings.fog = true;
            RenderSettings.fogDensity = 0.013f;
            RenderSettings.fogColor = new Color32(152, 152, 152, 255);
            RenderSettings.skybox = skyboxes[0];
            ScopeMaterial.SetTexture("_Cube", ScopeCupemap[0]);

            int i;
            for (i = 0; i < CliffMesh.Length; i++)
            {
                CliffMesh[i].material = SnowCliff;
            }
            for (i = 0; i < DirtEdgeMesh.Length; i++)
            {
                DirtEdgeMesh[i].material = SnowDirtEdge;
            }
            for (i = 0; i < GroundBrushMesh.Length; i++)
            {
                GroundBrushMesh[i].material = SnowGroudBrush;
            }
            for (i = 0; i < TreeMesh.Length; i++)
            {
                TreeMesh[i].material = SnowTrees;
            }
            for (i = 0; i < WallMesh.Length; i++)
            {
                WallMesh[i].material = SnowWallRoof;
            }
            for (i = 0; i < PipeMesh.Length; i++)
            {
                PipeMesh[i].material = SnowPiper;
            }
            for (i = 0; i < TerrainMesh.Length; i++)
            {
                TerrainMesh[i].material = SnowTerrain;
            }
            for (i = 0; i < TexFasedMesh.Length; i++)
            {
                TexFasedMesh[i].material = SnowTexFased;
            }



        }
    }

    private void Start()
    {
        if (GlobalScripts.CurrLevelIndex == 0 || GlobalScripts.CurrLevelIndex == 12 || GlobalScripts.CurrLevelIndex == 13 || GlobalScripts.CurrLevelIndex == 14)
        {
            InfraredBtn.SetActive(true);
        }

    }

  

  
}
