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
        [Tooltip("if set to true, flash light will toggle on when held and off when released")]
        [SerializeField]
        private bool Automatic;

        private bool IsOn;
        private bool WasHeld;

        // Start is called before the first frame update
        void Start()
        {
            interactable = GetComponent<Interactable>();
            MyLight = GetComponentInChildren<Light>();
            WasHeld = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (!Automatic)
            {
                if (SteamVR_Actions._default.TriggerPulled.GetStateDown(SteamVR_Input_Sources.Any) && interactable.attachedToHand)
                    Turn(!IsOn);
            }
            else
            {
                if (interactable.attachedToHand && !WasHeld)
                    Turn(true);
                else if (!interactable.attachedToHand && WasHeld)
                    Turn(false);

                WasHeld = interactable.attachedToHand;
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
