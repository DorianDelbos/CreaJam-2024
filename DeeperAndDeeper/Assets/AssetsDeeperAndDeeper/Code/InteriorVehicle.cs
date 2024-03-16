using System.Collections;
using UnityEngine;

public class InteriorVehicle : MonoBehaviour
{
    [SerializeField] private Transform SunScreenLeft;
    [SerializeField] private Transform SunScreenRight;
    [SerializeField] private Transform WipperLeft;
    [SerializeField] private Transform WipperRight;
    [SerializeField] private Transform GloveBox;
    [SerializeField] private Transform CrankWipper;
    [SerializeField] private Transform CrankLight;
    [SerializeField] private Light[] lights;

    private bool sunScreenLeftActive = false;
    private bool sunScreenRightActive = false;
    private bool gloveBoxActive = false;
    private bool crankWipperActive = false;
    private bool crankLightActive = false;

    private Coroutine sunScreenLeftRoutine;
    private Coroutine sunScreenRightRoutine;
    private Coroutine GloveBoxRoutine;
    private Coroutine CrankWipperRoutine;
    private Coroutine WipperLeftRoutine;
    private Coroutine WipperRightRoutine;
    private Coroutine CrankLightRoutine;

    public void ToggleSteering()
    {
        // Klaxon
    }

    public void ToggleLight()
    {
        if (crankLightActive)
        {
            if (CrankLightRoutine != null)
                StopCoroutine(CrankLightRoutine);
            CrankLightRoutine = StartCoroutine(LerpFromTo(CrankLight, CrankLight.eulerAngles, new Vector3(-4.209f, -169.079f, 80f)));

            foreach (var light in lights)
            {
                light.enabled = false;
            }
        }
        else
        {
            if (CrankLightRoutine != null)
                StopCoroutine(CrankLightRoutine);
            CrankLightRoutine = StartCoroutine(LerpFromTo(CrankLight, CrankLight.eulerAngles, new Vector3(-4.209f, -169.079f, 65.334f)));

            foreach (var light in lights)
            {
                light.enabled = true;
            }
        }

        crankLightActive = !crankLightActive;
    }

    public void ToggleSunScreenLeft()
    {
        if (sunScreenLeftActive)
        {
            if (sunScreenLeftRoutine != null)
                StopCoroutine(sunScreenLeftRoutine);
            sunScreenLeftRoutine = StartCoroutine(LerpFromTo(SunScreenLeft, SunScreenLeft.eulerAngles, Vector3.right * 100));
        }
        else
        {
            if (sunScreenLeftRoutine != null)
                StopCoroutine(sunScreenLeftRoutine);
            sunScreenLeftRoutine = StartCoroutine(LerpFromTo(SunScreenLeft, SunScreenLeft.eulerAngles, Vector3.zero));
        }

        sunScreenLeftActive = !sunScreenLeftActive;
    }

    public void ToggleSunScreenRight()
    {
        if (sunScreenRightActive)
        {
            if (sunScreenRightRoutine != null)
                StopCoroutine(sunScreenRightRoutine);
            sunScreenRightRoutine = StartCoroutine(LerpFromTo(SunScreenRight, SunScreenRight.eulerAngles, Vector3.right * 100));
        }
        else
        {
            if (sunScreenRightRoutine != null)
                StopCoroutine(sunScreenRightRoutine);
            sunScreenRightRoutine = StartCoroutine(LerpFromTo(SunScreenRight, SunScreenRight.eulerAngles, Vector3.zero));
        }

        sunScreenRightActive = !sunScreenRightActive;
    }

    public void ToggleGloveBox()
    {
        if (gloveBoxActive)
        {
            if (GloveBoxRoutine != null)
                StopCoroutine(GloveBoxRoutine);
            GloveBoxRoutine = StartCoroutine(LerpFromTo(GloveBox, GloveBox.eulerAngles, Vector3.right * 100));
        }
        else
        {
            if (GloveBoxRoutine != null)
                StopCoroutine(GloveBoxRoutine);
            GloveBoxRoutine = StartCoroutine(LerpFromTo(GloveBox, GloveBox.eulerAngles, Vector3.zero));
        }

        gloveBoxActive = !gloveBoxActive;
    }

    public void ToggleWipper()
    {
        if (crankWipperActive)
        {
            if (CrankWipperRoutine != null)
                StopCoroutine(CrankWipperRoutine);
            CrankWipperRoutine = StartCoroutine(WipperLerpFromTo(WipperRight, WipperRight.eulerAngles, new Vector3(25f, 0f, 65f)));
        }

        crankWipperActive = !crankWipperActive;
    }

    private IEnumerator LerpFromTo(Transform transform, Vector3 from, Vector3 to, float timer = 1f)
    {
        float elapsed = 0.0f;

        while (elapsed < timer)
        {
            elapsed = Mathf.Min(elapsed + Time.deltaTime, timer);
            transform.localEulerAngles = Vector3.Lerp(from, to, elapsed / timer);
            yield return null;
        }
    }

    private IEnumerator WipperLerpFromTo(Transform transform, Vector3 start, Vector3 end, float timer = 1f)
    {
        while (crankWipperActive)
        {
            float elapsed = 0.0f;

            while (elapsed < timer)
            {
                elapsed = Mathf.Min(elapsed + Time.deltaTime, timer);
                transform.localEulerAngles = Vector3.Lerp(start, end, elapsed / timer);
                yield return null;
            }

            while (elapsed > 0)
            {
                elapsed = Mathf.Max(elapsed - Time.deltaTime, 0);
                transform.localEulerAngles = Vector3.Lerp(end, start, 1 - elapsed / timer);
                yield return null;
            }
        }
    }
}
