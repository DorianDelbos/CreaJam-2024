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
        if (!crankLightActive)
        {
            if (CrankLightRoutine != null)
                StopCoroutine(CrankLightRoutine);
            CrankLightRoutine = StartCoroutine(LerpFromTo(CrankLight, CrankLight.localEulerAngles, new Vector3(0f, 0f, 16f), 0.2f));

            foreach (var light in lights)
            {
                light.enabled = false;
            }
        }
        else
        {
            if (CrankLightRoutine != null)
                StopCoroutine(CrankLightRoutine);
            CrankLightRoutine = StartCoroutine(LerpFromTo(CrankLight, CrankLight.localEulerAngles, new Vector3(0f, 0f, 0f), 0.2f));

            foreach (var light in lights)
            {
                light.enabled = true;
            }
        }

        crankLightActive = !crankLightActive;
    }

    public void ToggleSunScreenLeft()
    {
        if (!sunScreenLeftActive)
        {
            if (sunScreenLeftRoutine != null)
                StopCoroutine(sunScreenLeftRoutine);
            sunScreenLeftRoutine = StartCoroutine(LerpFromTo(SunScreenLeft, SunScreenLeft.localEulerAngles, Vector3.right * 90));
        }
        else
        {
            if (sunScreenLeftRoutine != null)
                StopCoroutine(sunScreenLeftRoutine);
            sunScreenLeftRoutine = StartCoroutine(LerpFromTo(SunScreenLeft, SunScreenLeft.localEulerAngles, Vector3.zero));
        }

        sunScreenLeftActive = !sunScreenLeftActive;
    }

    public void ToggleSunScreenRight()
    {
        if (!sunScreenRightActive)
        {
            if (sunScreenRightRoutine != null)
                StopCoroutine(sunScreenRightRoutine);
            sunScreenRightRoutine = StartCoroutine(LerpFromTo(SunScreenRight, SunScreenRight.localEulerAngles, Vector3.right * 90));
        }
        else
        {
            if (sunScreenRightRoutine != null)
                StopCoroutine(sunScreenRightRoutine);
            sunScreenRightRoutine = StartCoroutine(LerpFromTo(SunScreenRight, SunScreenRight.localEulerAngles, Vector3.zero));
        }

        sunScreenRightActive = !sunScreenRightActive;
    }

    public void ToggleGloveBox()
    {
        if (!gloveBoxActive)
        {
            if (GloveBoxRoutine != null)
                StopCoroutine(GloveBoxRoutine);
            GloveBoxRoutine = StartCoroutine(LerpFromTo(GloveBox, GloveBox.localEulerAngles, Vector3.right * 90));
        }
        else
        {
            if (GloveBoxRoutine != null)
                StopCoroutine(GloveBoxRoutine);
            GloveBoxRoutine = StartCoroutine(LerpFromTo(GloveBox, GloveBox.localEulerAngles, Vector3.zero));
        }

        gloveBoxActive = !gloveBoxActive;
    }

    public void ToggleWipper()
    {
        if (!crankWipperActive)
        {
            if (CrankWipperRoutine != null)
                StopCoroutine(CrankWipperRoutine);
            if (WipperLeftRoutine != null)
                StopCoroutine(WipperLeftRoutine);
            if (WipperRightRoutine != null)
                StopCoroutine(WipperRightRoutine);
            CrankWipperRoutine = StartCoroutine(LerpFromTo(CrankWipper, CrankWipper.localEulerAngles, new Vector3(0f, 0f, -16f), 0.2f));
            WipperLeftRoutine = StartCoroutine(LerpFromTo(WipperLeft, WipperLeft.localEulerAngles, new Vector3(25f, 0f, -10f)));
            WipperRightRoutine = StartCoroutine(LerpFromTo(WipperRight, WipperRight.localEulerAngles, new Vector3(25f, 0f, -10f)));
        }
        else
        {
            if (CrankWipperRoutine != null)
                StopCoroutine(CrankWipperRoutine);
            CrankWipperRoutine = StartCoroutine(LerpFromTo(CrankWipper, CrankWipper.localEulerAngles, new Vector3(0f, 0f, 0f), 0.2f));
            WipperLeftRoutine = StartCoroutine(LerpFromTo(WipperLeft, WipperLeft.localEulerAngles, new Vector3(25f, 0f, -85f)));
            WipperRightRoutine = StartCoroutine(LerpFromTo(WipperRight, WipperRight.localEulerAngles, new Vector3(25f, 0f, -85f)));
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
}
