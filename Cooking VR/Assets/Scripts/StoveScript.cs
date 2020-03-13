using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveScript : MonoBehaviour
{
    [SerializeField]
    private bool StoveIsOn;

    [SerializeField]//serialized for debugging
    private PanScript CurrentPanOnStove;

    [SerializeField]
    private GameObject Fire;

    private void Start()
    {
        CurrentPanOnStove = null;
        Fire.SetActive(false);
    }

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
        Fire.SetActive(true);
        StoveIsOn = true;
        CurrentPanOnStove.StartCooking();
        
    }

    public void TurnOff()
    {
        Fire.SetActive(false);
        StoveIsOn = false;
        CurrentPanOnStove.StopCooking();
        
    }
}
