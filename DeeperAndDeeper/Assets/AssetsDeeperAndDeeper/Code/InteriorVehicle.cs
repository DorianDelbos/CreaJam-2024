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

    public void ToggleSteering()
    {
        // Klaxon
    }

    public void ToggleLight()
    {
        if (crankLightActive)
        {
            WipperLeft.eulerAngles = new Vector3(-4.209f, -169.079f, 80f);

            foreach (var light in lights)
            {
                light.enabled = false;
            }
        }
        else
        {
            WipperLeft.eulerAngles = new Vector3(-4.209f, -169.079f, 65.334f);

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
            SunScreenLeft.eulerAngles = Vector3.right * 100;
        }
        else
        {
            SunScreenLeft.eulerAngles = Vector3.zero;
        }

        sunScreenLeftActive = !sunScreenLeftActive;
    }

    public void ToggleSunScreenRight()
    {
        if (sunScreenRightActive)
        {
            SunScreenRight.eulerAngles = Vector3.right * 100;
        }
        else
        {
            SunScreenRight.eulerAngles = Vector3.zero;
        }

        sunScreenRightActive = !sunScreenRightActive;
    }

    public void ToggleGloveBox()
    {
        if (gloveBoxActive)
        {
            GloveBox.eulerAngles = Vector3.right * 100;
        }
        else
        {
            GloveBox.eulerAngles = Vector3.zero;
        }

        gloveBoxActive = !gloveBoxActive;
    }

    public void ToggleWipper()
    {
        if (crankWipperActive)
        {
            WipperRight.eulerAngles = new Vector3(-4.209f, -9.079f, 80f);
        }
        else
        {
            WipperRight.eulerAngles = new Vector3(-4.209f, -9.079f, 65.334f);
        }

        crankWipperActive = !crankWipperActive;
    }
}
