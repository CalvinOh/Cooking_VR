using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanScript : MonoBehaviour
{
    [SerializeField]
    private bool IsHeated;

    private PattyScript CurrentPattyOnPan;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Patty"))
        {
            CurrentPattyOnPan = other.GetComponent<PattyScript>();
            if(IsHeated)
            CurrentPattyOnPan.StartCooking();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Patty"))
        {
            CurrentPattyOnPan.StopCooking();
            CurrentPattyOnPan = null;
        }
    }

    public void StartCooking()
    {
        IsHeated = true;
        CurrentPattyOnPan.StartCooking();
    }

    public void StopCooking()
    {
        IsHeated = false;
        CurrentPattyOnPan.StopCooking();
    }

}
