using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour, IInteractable
{
    [SerializeField] private Light[] lights = new Light[2];

    public void Interact(GameObject interactor)
    {
        foreach (Light light in lights)
        {
            if (light.enabled)
                light.enabled = false;
            else if (!light.enabled)
                light.enabled = true;
        }
    }
}
