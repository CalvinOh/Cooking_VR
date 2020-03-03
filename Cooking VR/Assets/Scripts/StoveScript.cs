using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveScript : MonoBehaviour
{
    [SerializeField]
    private bool StoveIsOn;

    private PanScript CurrentPanOnStove;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pan") )
        {
            CurrentPanOnStove = other.GetComponent<PanScript>();
            if(StoveIsOn)
            CurrentPanOnStove.StartCooking();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Pan"))
        {
            
            CurrentPanOnStove.StopCooking();
            CurrentPanOnStove = null;
        }
    }

    public void TurnOn()
    {
        StoveIsOn = true;
        CurrentPanOnStove.StartCooking();
    }

    public void TurnOff()
    {
        StoveIsOn = false;
        CurrentPanOnStove.StopCooking();
    }
}
