using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

namespace Valve.VR.InteractionSystem
{
    public class FlashLight : MonoBehaviour
    {

        private Light MyLight;
        private Interactable interactable;

        private bool IsOn;

        // Start is called before the first frame update
        void Start()
        {
            interactable = GetComponent<Interactable>();
            MyLight = GetComponentInChildren<Light>();
        }

        // Update is called once per frame
        void Update()
        { 
                if (SteamVR_Actions._default.TriggerPulled.GetStateDown(SteamVR_Input_Sources.Any)&& interactable.attachedToHand)
                {
                Turn(!IsOn);
                }
            
        }

        private void Turn(bool on)
        {
            if (on)
            {
                IsOn = true;

                // audio
                AkSoundEngine.PostEvent("Flashlight_On", gameObject);
            }
            else
            {
                IsOn = false;

                // audio
                AkSoundEngine.PostEvent("Flashlight_Off", gameObject);
            }

            MyLight.enabled = IsOn;
        }

    }
}
