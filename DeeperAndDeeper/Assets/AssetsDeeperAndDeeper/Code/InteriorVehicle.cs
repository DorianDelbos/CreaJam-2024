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
            foreach (var light in lights)
            {
                light.enabled = false;
            }
        }
        else
        {
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

        }
        else
        {

        }

        sunScreenLeftActive = !sunScreenLeftActive;
    }

    public void ToggleSunScreenRight()
    {
        if (sunScreenRightActive)
        {

        }
        else
        {

        }

        sunScreenRightActive = !sunScreenRightActive;
    }

    public void ToggleGloveBox()
    {
        if (gloveBoxActive)
        {

        }
        else
        {

        }

        gloveBoxActive = !gloveBoxActive;
    }

    public void ToggleWipper()
    {
        if (crankWipperActive)
        {

        }
        else
        {

        }

        crankWipperActive = !crankWipperActive;
    }
}
