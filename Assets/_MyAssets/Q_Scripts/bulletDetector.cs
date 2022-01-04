using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletDetector : MonoBehaviour
{
    public AS_ActionPreset ActionPresets;
    public TimeManager time;
    public static bulletDetector instance;
    public bool Detected;
    public bool folowChkSecond;
    public AS_ActionPreset GetPresets()
    {

        if (ActionPresets)
        {
            return null;

        }
        AS_ActionPreset res = ActionPresets;
     
                res = ActionPresets;
        
        return res;
    }
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        Inetialize();
    }
    void Inetialize()
    {
        ActionPresets.Initialize();
    }
    public void TimeReset()
    {
        time.DoSlowotion(1,0.1f);
    }
    public void slowMotionNow(float timeValue,float duration)
    {
        time.DoSlowotion(timeValue,duration);
        //print("time Farhan" + timeValue);
        //print("time duration Farhan" + duration);
    }
   public void chkFunction()
    {
        if(folowChkSecond)
        {
            follow.followChk = true;
        }
        else
        {
            follow.followChk = false;
        }
    }
}
