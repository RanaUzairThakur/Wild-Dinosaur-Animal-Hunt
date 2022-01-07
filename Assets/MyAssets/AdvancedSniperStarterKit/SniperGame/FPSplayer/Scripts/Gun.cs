using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(AudioSource))]


public class Gun : bulletRayHit
{

    public bool Active = true;
    public GameObject Bullets, dummyBullet; // Bullet prefeb
    public float LifeTimeBullet = 3;
    public GameObject Shell; // Shell prefeb
    public Transform ShellSpawn; // shell spawing position
    public Camera NormalCamera;// FPS camera
    public float FireRate = 0.2f;
    public float KickPower = 10;
    public float[] ZoomFOVLists;
    public int IndexZoom = 0;
    public Vector2 Offset = Vector2.zero;
    public float CooldownTime = 0.8f;
    public float BoltTime = 0.35f;
    public Texture2D CrosshairImg, CrosshairZoom;
    public bool HideGunWhileZooming = true;
    public AudioClip SoundGunFire;
    public AudioClip SoundBoltEnd;
    public AudioClip SoundBoltStart;
    public AudioClip SoundReloadStart;
    public AudioClip SoundReloadEnd;
    public float MouseSensitive = 1;
    public bool Zooming;
    public bool SemiAuto;
    public bool InfinityAmmo = true;
    public int BulletNum = 1;
    public int Spread = 0;
    public int Clip = 30;
    public int ClipSize = 30;
    public int AmmoIn = 1;
    public int AmmoPack = 90;
    public int AmmoPackMax = 90;
    private float MouseSensitiveZoom = 0.5f;
    private bool boltout;
    private float timefire = 0;
    private int gunState = 0;
    private AudioSource audiosource;
    [HideInInspector]
    public float fovTemp;
    private float cooldowntime = 0;
    private Quaternion rotationTemp;
    private Vector3 positionTemp;
    public string IdlePose = "Idle";
    public string ShootPose = "Shoot";
    public string ReloadPose = "Reload";
    public string BoltPose = "Bolt";
    [HideInInspector]
    public FPSController FPSmotor;
    public Image crossHairOwnUse, simpleAim;
    public Slider mainSlider;
    public Camera followCam, gunCam;
    public Vector3 offSet2, offSet3;
    GameObject bullet;
    bool gunviewChk;
    int camMoveCount;
    public Transform bulletSpawnPoint;
    public GameObject firstGun;
    public GameObject[] gunHideParts;
    public static Gun instance;
    public TimeManager time;
    public GameObject followCamEndPosition;
    public bool tempChk;
    public Transform followInitialPosition;
    Vector3 point;
    public static bool camDetachChk;
    public ParticleSystem fireGlow;
    public bool test;
    void Start()
    {
        // followInitialPosition.position = followCam.transform.position;
        instance = this;
        if (GetComponent<Animation>())
            GetComponent<Animation>().cullingType = AnimationCullingType.AlwaysAnimate;
        FPSmotor = transform.root.GetComponentInChildren<FPSController>();

        Zooming = false;
        if (GetComponent<AudioSource>())
        {
            audiosource = GetComponent<AudioSource>();
        }

        if (NormalCamera)
            fovTemp = NormalCamera.GetComponent<Camera>().fieldOfView;

        if (AmmoIn > 1)
            AmmoIn = 1;
    }

    void Awake()
    {
        rotationTemp = this.transform.localRotation;
        positionTemp = this.transform.localPosition;
        this.transform.localPosition = positionTemp - (Vector3.up);

    }

    public void SetActive(bool active)
    {
        Active = active;
        this.gameObject.SetActive(active);
        Zooming = false;
        IndexZoom = 0;
        this.transform.localPosition = positionTemp - (Vector3.up);

        if (NormalCamera)
            NormalCamera.GetComponent<Camera>().fieldOfView = fovTemp;

    }

    void FixedUpdate()
    {
        if (!FPSmotor || !Active)
            return;

        float magnitude = FPSmotor.motor.controller.velocity.magnitude * 0.5f;
        magnitude = Mathf.Clamp(magnitude, 0, 10);
        float swaySpeed = 0;
        float sizeY = 0.1f;
        float sizeX = 0.1f;
        // Gun sway volume depending on move velosity.
        if (magnitude > 2)
        {
            swaySpeed = 1.4f;
            sizeY = 0.2f;
            sizeX = 0.2f;
        }
        else
        {
            if (magnitude < 1.0f)
            {
                swaySpeed = 0;
                sizeY = 0.05f;
                sizeX = 0.05f;
            }
            else
            {
                swaySpeed = 1;
                sizeY = 0.1f;
                sizeX = 0.1f;

            }
        }
        float swayY = (Mathf.Cos(Time.time * 10 * swaySpeed) * 0.3f) * sizeY;
        float swayX = (Mathf.Sin(Time.time * 5 * swaySpeed) * 0.2f) * sizeX;
        this.transform.localPosition = Vector3.Lerp(this.transform.localPosition, positionTemp + new Vector3(swayX, swayY, 0), Time.fixedDeltaTime * 4);
        this.transform.localRotation = Quaternion.Lerp(this.transform.localRotation, Quaternion.Euler((rotationTemp.eulerAngles.x + (FPSmotor.rotationDif.x)), (rotationTemp.eulerAngles.y + (FPSmotor.rotationDif.y)), (rotationTemp.eulerAngles.z + (FPSmotor.direction.x * 7))), Time.fixedDeltaTime * 3);

    }
    void chkTrue()
    {
        gunviewChk = true;
    }

    void Update()
    {
        //  followInitialPosition.rotation = followCam.transform.rotation;

        //        Debug.Log("gun script time value :"+Time.timeScale);
        if (!bullet && Time.timeScale != 1)
        {
            // Debug.Log("Gun script time scale reset");
            time.DoSlowotion(1, 1f);
        }
        if (!Zooming)
        {
            mainSlider.value = 0;
        }

        if (HideGunWhileZooming && FPSmotor && NormalCamera.GetComponent<Camera>().enabled)
        {
            FPSmotor.HideGun(!Zooming);
        }

        if (!GetComponent<Animation>() || !Active)
            return;


        switch (gunState)
        {
            case 0:
                // Start Bolt
                if (AmmoIn <= 0)
                {
                    // Check Ammo in clip
                    if (Clip > 0)
                    {
                        GetComponent<Animation>().clip = GetComponent<Animation>()[BoltPose].clip;
                        GetComponent<Animation>().CrossFade(BoltPose, 0.5f, PlayMode.StopAll);
                        gunState = 2;
                        // scope rotation a bit when reloading
                        if (FPSmotor && Zooming)
                        {
                            FPSmotor.CameraForceRotation(new Vector3(0, 0, 20));
                            FPSmotor.Stun(0.2f);
                        }
                        if (SoundBoltStart && audiosource != null)
                        {
                            audiosource.PlayOneShot(SoundBoltStart);
                        }
                        Clip -= 1;
                    }
                    else
                    {
                        gunState = 3;
                    }
                }
                break;
            case 1:
                // Countdown to idle state
                if (Time.time >= cooldowntime + CooldownTime)
                {
                    gunState = 0;
                }
                break;
            case 2:
                GetComponent<Animation>().Play();
                // finish bold animation
                if (GetComponent<Animation>()[BoltPose].normalizedTime > BoltTime)
                {
                    if (Shell && ShellSpawn)
                    {
                        if (!boltout)
                        {
                            GameObject shell = (GameObject)Instantiate(Shell, ShellSpawn.position, ShellSpawn.rotation);
                            shell.GetComponent<Rigidbody>().AddForce(ShellSpawn.transform.right * 2);
                            shell.GetComponent<Rigidbody>().AddTorque(Random.rotation.eulerAngles * 10);
                            GameObject.Destroy(shell, 5);
                            boltout = true;
                            if (FPSmotor && Zooming)
                            {
                                FPSmotor.CameraForceRotation(new Vector3(0, 0, -5));
                                FPSmotor.Stun(0.1f);
                            }
                        }
                    }
                }
                if (GetComponent<Animation>()[BoltPose].normalizedTime > 0.9f)
                {
                    gunState = 0;
                    AmmoIn = 1;
                    GetComponent<Animation>().CrossFade(IdlePose);
                    if (SoundBoltEnd && audiosource != null)
                    {
                        audiosource.PlayOneShot(SoundBoltEnd);
                    }
                }
                break;
            case 3:
                // Start Reloading
                if (GetComponent<Animation>()[ReloadPose] && (AmmoPack > 0 || InfinityAmmo))
                {
                    GetComponent<Animation>().clip = GetComponent<Animation>()[ReloadPose].clip;
                    GetComponent<Animation>().CrossFade(ReloadPose, 0.5f, PlayMode.StopAll);
                    gunState = 4;
                    Zooming = false;
                    if (SoundReloadStart && audiosource != null)
                    {
                        audiosource.PlayOneShot(SoundReloadStart);
                    }
                }
                else
                {
                    gunState = 0;
                }
                break;
            case 4:

                if (GetComponent<Animation>()[ReloadPose])
                {
                    if (GetComponent<Animation>().clip.name != GetComponent<Animation>()[ReloadPose].name)
                    {
                        GetComponent<Animation>().clip = GetComponent<Animation>()[ReloadPose].clip;
                        GetComponent<Animation>().CrossFade(ReloadPose, 0.5f, PlayMode.StopAll);
                    }
                    if (GetComponent<Animation>()[ReloadPose].normalizedTime > 0.8f)
                    {
                        gunState = 0;
                        if (InfinityAmmo)
                        {
                            Clip = ClipSize;
                        }
                        else
                        {
                            if (AmmoPack >= ClipSize)
                            {
                                Clip = ClipSize;
                                AmmoPack -= ClipSize;
                            }
                            else
                            {
                                Clip = AmmoPack;
                                AmmoPack = 0;
                            }
                        }

                        if (Clip > 0)
                        {
                            GetComponent<Animation>().CrossFade(IdlePose);
                            if (SoundReloadEnd && audiosource != null)
                            {
                                audiosource.PlayOneShot(SoundReloadEnd);
                            }
                        }
                    }
                }
                else
                {
                    gunState = 0;
                }
                break;
        }

        if (FPSmotor)
        {
            if (Zooming)
            {
                FPSmotor.sensitivityXMult = MouseSensitiveZoom;
                FPSmotor.sensitivityYMult = MouseSensitiveZoom;
                FPSmotor.Noise = true;
            }
            else
            {
                FPSmotor.sensitivityXMult = MouseSensitive;
                FPSmotor.sensitivityYMult = MouseSensitive;
                FPSmotor.Noise = false;
            }
        }

        if (Zooming)
        {
            if (ZoomFOVLists.Length > 0)
            {
                MouseSensitiveZoom = ((MouseSensitive * 0.16f) / 10) * ZoomFOVLists[IndexZoom];
                NormalCamera.GetComponent<Camera>().fieldOfView += (ZoomFOVLists[IndexZoom] - NormalCamera.GetComponent<Camera>().fieldOfView) / 10;

            }
        }
        else
        {
            NormalCamera.GetComponent<Camera>().fieldOfView += (fovTemp - NormalCamera.GetComponent<Camera>().fieldOfView) / 10;
        }

        if (audiosource != null)
        {
            audiosource.pitch = Time.timeScale;
            if (audiosource.pitch < 0.5f)
            {
                audiosource.pitch = 0.5f;
            }
        }

    }

    private void LateUpdate()
    {

        if (bullet)
        {
            if (camDetachChk)
            {
                followCam.transform.parent = null;
                camDetachChk = false;
            }
        }
        if (follow.followChk == true && bullet)
        {
            if (tempChk == true)
            {
                followCam.transform.DOMove(bullet.transform.Find("point2").transform.position, 0.1f);
                print("Pointtttttttt");
            }

        }

        else
        {
            if (NormalCamera.GetComponent<Camera>().enabled == false)
            {
                NormalCamera.GetComponent<Camera>().enabled = true;
                gunCam.GetComponent<Camera>().enabled = true;
                // FPSController.instance.HideGun(false);
            }
            followCam.GetComponent<Camera>().enabled = false;
        }

    }
    public void sliderValue()
    {
        if (IndexZoom <= ZoomFOVLists.Length - 1)
        {
            IndexZoom = (int)mainSlider.value;
        }
        if (mainSlider.value > 0)
        {
            //GameObject.Find("GunView").GetComponent<Animator>().enabled = true;
            //crossHairOwnUse.gameObject.SetActive(true);
            if (Zooming == false)
                Zooming = true;
            simpleAim.gameObject.SetActive(false);

        }
        else if (mainSlider.value == 0)
        {
            Zooming = false;
            simpleAim.gameObject.SetActive(true);
            crossHairOwnUse.gameObject.SetActive(false);
        }

    }

    public void ZoomButton()
    {

        Zooming = true;
        mainSlider.value = 0.15f;
        mainSlider.gameObject.SetActive(true);
        simpleAim.gameObject.SetActive(false);




    }
    public void ZoomButton1()
    {

        Zooming = false;
        mainSlider.value = 0f;
        mainSlider.gameObject.SetActive(false);
        simpleAim.gameObject.SetActive(true);



    }
    public void ZoomDelta(int plus)
    {
        if (!Active)
            return;

        if (plus > 0)
        {
            if (IndexZoom < ZoomFOVLists.Length - 1)
            {
                IndexZoom += 1;
            }
        }
        else
        {
            if (IndexZoom > 0)
            {
                IndexZoom -= 1;
            }
        }
    }

    public void Reload()
    {
        if (gunState == 0)
        {
            AmmoIn = 0;
            Clip = 0;
            gunState = 3;
        }
    }

    public void Zoom()
    {
        Zooming = !Zooming;
    }

    public void ZoomToggle()
    {
        Zooming = true;

        if (IndexZoom < ZoomFOVLists.Length - 1)
        {
            IndexZoom += 1;
        }
        else
        {
            Zooming = false;
            IndexZoom = 0;
        }

    }

    public void OffsetAdjust(Vector2 adj)
    {
        Offset += adj;
    }

    public void Shoot()
    {
        //  print("Fired Pressed");
        //int totalenemy = wasii_GamePlay_UI_Handler.instance.kills[GlobalScripts.CurrLevelIndex];
        //int myenemy = DamageManager.KilledAnimal;
        if (!Active)
            return;
        if (timefire + FireRate < Time.time)
        {
            if (gunState == 0)
            {
                if (AmmoIn > 0)
                {
                    if (FPSmotor)
                        FPSmotor.Stun(KickPower);
                    if (SoundGunFire && audiosource != null)
                    {
                        audiosource.PlayOneShot(SoundGunFire);
                    }
                    for (int i = 0; i < BulletNum; i++)
                    {
                        if (Bullets)
                        {
                            Vector3 point = NormalCamera.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 0));
                            if (simpleAim.color == Color.green)
                            {
                                follow.followChk = true;
                            }
                            else
                            {
                                follow.followChk = false;
                            }
                            point = NormalCamera.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 0));
                            bullet = (GameObject)Instantiate(Bullets, point, NormalCamera.gameObject.transform.rotation);
                            bullet.transform.forward = NormalCamera.transform.forward + (new Vector3(Random.Range(-Spread, Spread) + Offset.x, Random.Range(-Spread, Spread) + Offset.y, Random.Range(-Spread, Spread)) * 0.001f);

                            if (follow.followChk == true && bullet)
                            {
                                followCam.transform.rotation = followInitialPosition.transform.rotation;
                                followCam.GetComponent<Camera>().enabled = true;

                                if (fireGlow)
                                    fireGlow.Play();
                                if (NormalCamera.enabled == true)
                                {
                                    NormalCamera.enabled = false;
                                    gunCam.enabled = false;
                                    firstGun.SetActive(true);
                                    for (int j = 0; j < gunHideParts.Length; j++)
                                    {
                                        gunHideParts[j].SetActive(false);

                                    }
                                    //
                                    //   Debug.Log("Bullet Instantiated here");
                                    GameObject.Find("AnimalCamera").GetComponent<Camera>().enabled = false;
                                    // Default Values
                                    //time.DoSlowotion(0.01f, 300f);
                                    //followCam.transform.DOMove(followCamEndPosition.transform.position, 0.045f);
                                    //followCam.transform.DOLookAt(bullet.transform.position, 0.03f);
                                    //Changes Value Here
                                    time.DoSlowotion(0.01f, 300f);
                                    followCam.transform.DOMove(followCamEndPosition.transform.position, 0.045f);
                                    //followCam.transform.DOLookAt(bullet.transform.position, 0.03f);
                                    //followCam.transform.rotation=followCamEndPosition.transform.rotation;
                                    crossHairOwnUse.gameObject.SetActive(false);
                                    simpleAim.gameObject.SetActive(false);
                                    // Invoke("OnGunParts", 0.35f);
                                    StartCoroutine("GunParts");

                                }
                            }
                            else
                            {
                                Invoke("bulletOn", 0.01f);
                            }

                        }
                    }
                    boltout = false;
                    GetComponent<Animation>().Stop();
                    GetComponent<Animation>().Play(ShootPose, PlayMode.StopAll);
                    timefire = Time.time;
                    cooldowntime = Time.time;
                    if (!SemiAuto)
                    {
                        gunState = 1;
                        AmmoIn -= 1;
                    }
                    else
                    {

                        if (Shell && ShellSpawn)
                        {
                            GameObject shell = (GameObject)Instantiate(Shell, ShellSpawn.position, ShellSpawn.rotation);
                            shell.GetComponent<Rigidbody>().AddForce(ShellSpawn.transform.right * 2);
                            shell.GetComponent<Rigidbody>().AddTorque(Random.rotation.eulerAngles * 10);
                            GameObject.Destroy(shell, 5);
                        }

                        if (Clip > 0)
                        {
                            AmmoIn = 1;
                            Clip -= 1;
                        }
                        else
                        {
                            gunState = 3;
                        }
                    }
                }

                if (Clip <= 0)
                {
                    gunState = 3;
                }

            }

        }
    }
    void bulletOn()
    {

        bullet.GetComponent<AS_Bullet>().enabled = true;
    }
    IEnumerator cameraSlowmotion()
    {

        if (NormalCamera.GetComponent<Camera>().enabled == true)
        {
            NormalCamera.GetComponent<Camera>().enabled = false;
            followCam.GetComponent<Camera>().enabled = true;
            //  Debug.Log("camfalse");
            gunCam.GetComponent<Camera>().enabled = false;

            for (int j = 0; j < gunHideParts.Length; j++)
            {
                gunHideParts[j].SetActive(false);

            }
        }

        yield return new WaitForSeconds(0.5f);
        firstGun.SetActive(true);
        bullet = (GameObject)Instantiate(Bullets, NormalCamera.transform.position, NormalCamera.gameObject.transform.rotation);

        tempChk = true;
        if (followCam)
        {
            followCam.transform.LookAt(bullet.transform);
            followCam.transform.Translate(bullet.transform.position + offSet2);
        }
        // Debug.Log("coroutine working Camerslowmotion");
    }
    Transform tempPoint;
    IEnumerator GunParts()
    {
        //  Debug.Log("coroutine working");
        yield return new WaitForSeconds(0.0455f);
        if (followCam)
        {
            followCam.transform.DOLookAt(bullet.transform.position, 0.01f);
            followCam.transform.SetParent(bullet.transform);
        }

        //followCam.transform.DOMove(bullet.transform.Find("point").transform.position, 0.005f);
        yield return new WaitForSeconds(0.0001f);
        if (bullet)
            bullet.GetComponent<AS_Bullet>().enabled = true;
        if (bulletDetector.instance)
            bulletDetector.instance.slowMotionNow(0.004f, 400);
        yield return new WaitForSeconds(0.002f);
        if (bullet.transform.GetChild(0))
            bullet.transform.GetChild(0).GetComponent<Animator>().enabled = true;//Removing first cover
        yield return new WaitForSeconds(0.008f);
        if (bullet.transform.GetChild(1))
            bullet.transform.GetChild(1).GetComponent<Animator>().enabled = true;//Removing Second cover
        yield return new WaitForSeconds(0.008f);
        if (bullet.transform.GetChild(0))
            bullet.transform.GetChild(0).gameObject.SetActive(false);////Disable first cover
        if (bullet.transform.GetChild(1))
            bullet.transform.GetChild(1).gameObject.SetActive(false);//Disable Second cover
        //followCam.gameObject.SetActive(false);
        //bullet.transform.GetChild(2).GetComponentInChildren<Camera>().enabled = true;
        //if (bullet.transform.find("behind"))
        //{

        //    // debug.logerror("find");
        //}
        //else
        //{
        //    //debug.logerror("not found");
        //}
        //followCam.transform.position=bullet.transform.Find("Behind").transform.position;
        // uncomment to follow from behind 
        //followCam.transform.DOMove(bullet.transform.Find("Behind").transform.position, 0.00005f);
        //followCam.transform.DOLookAt(bullet.transform.GetChild(2).position,0.01f);
        // follow from behind here
        if (followCam)
            followCam.GetComponent<SmoothFollow>().enabled = true;
        if (bullet)
            bullet.GetComponent<TrailRenderer>().enabled = true; // TrainRender On here
        if (bulletDetector.instance)
            bulletDetector.instance.slowMotionNow(0.3f, 400f);
        yield return new WaitForSeconds(0.004f);
        if (fireGlow)
            fireGlow.Stop();

        if (firstGun)
            firstGun.SetActive(false);
        for (int j = 0; j < gunHideParts.Length; j++)
        {
            if (gunHideParts[j])
                gunHideParts[j].SetActive(true);
        }
        StopAllCoroutines();
    }
    public void CamReset()
    {
        if (bulletRayHit.Bullet_hitted)
            bulletRayHit.Bullet_hitted = false;
        //  Debug.Log("cameraReset");
        Time.timeScale = 1f;
        //bulletDetector.instance.slowMotionNow(1, 1);
        if (followCam)
        {
            followCam.transform.SetParent(NormalCamera.transform);
            followCam.GetComponent<SmoothFollow>().enabled = false;
            followCam.transform.localPosition = new Vector3(-0.545f, 0.06999969f, -0.982f);
            followCam.transform.localRotation = Quaternion.Euler(0, 27.055f, 0);
        }
        if (simpleAim)
            simpleAim.gameObject.SetActive(true);
        //followCam.transform.position = followInitialPosition.position;
        //followcam.transform.rotation = followinitialposition.rotation;
        if (Zooming)
        {
            if (crossHairOwnUse)
                crossHairOwnUse.gameObject.SetActive(true);
            if (simpleAim)
                simpleAim.gameObject.SetActive(false);
        }
        else
        {
            if (simpleAim)
                simpleAim.gameObject.SetActive(true);
        }


    }


}
