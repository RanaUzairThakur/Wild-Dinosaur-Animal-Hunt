
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public float slowDownFactor;
    public float slowDownLength;

    private void Update()
    {

        Time.timeScale += (1f / slowDownLength) * Time.unscaledDeltaTime;
        Time.timeScale = Mathf.Clamp(Time.timeScale, 0.000001f, 0.5f);
    }
    public void DoSlowotion(float timeValue,float duration)
    {
        slowDownFactor = timeValue;
        slowDownLength = duration;
        Time.timeScale = slowDownFactor;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }
}
