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

    private PattyScript CurrentPattyOnStove;

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
        else if (other.CompareTag("Patty"))
        {
            StopAllCoroutines();
            CurrentPattyOnStove = other.GetComponent<PattyScript>();
            StartCoroutine(BurnPatty());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Pan"))
        {

            CurrentPanOnStove.StopCooking();
            CurrentPanOnStove = null;
        }
        else if (other.CompareTag("Patty"))
        {
            CurrentPattyOnStove = null;
        }

    }

    private IEnumerator BurnPatty()
    {
        PattyScript holder = CurrentPattyOnStove;
        yield return new WaitForSeconds(2f);
        if (CurrentPattyOnStove == holder)
        {
            holder.BurnPatty();
        }
    }

    public void TurnOn()
    {
        Fire.SetActive(true);
        StoveIsOn = true;
        if(CurrentPanOnStove!=null)
        CurrentPanOnStove.StartCooking();

        //audio
        PlaySoundStoveOn();
    }

    public void TurnOff()
    {
        Fire.SetActive(false);
        StoveIsOn = false;
        if (CurrentPanOnStove != null)
            CurrentPanOnStove.StopCooking();

        //audio
        StopSoundStoveOn();
    }

    //audio
    private void PlaySoundStoveOn()
    {
        AkSoundEngine.PostEvent("Stove_On", gameObject);

        Debug.Log("Sound: Stove On");
    }

    private void StopSoundStoveOn()
    {
        AkSoundEngine.PostEvent("Stove_Off", gameObject);

        Debug.Log("Sound: Stove Off");
    }
}