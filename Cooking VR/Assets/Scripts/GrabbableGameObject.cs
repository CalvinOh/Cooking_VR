using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Valve.VR.InteractionSystem;

namespace Assets.Scripts
{
    public class GrabbableGameObject : MonoBehaviour
    {
        [SerializeField]
        Interactable interactable;
        [SerializeField]
        public bool wasGrabbed;

        protected virtual void InitGrabbable()
        {
            this.interactable = this.GetComponent<Interactable>();
            wasGrabbed = false;
        }
        protected virtual void CheckGrabbed()
        {
            if (this.interactable.attachedToHand)
            {
                wasGrabbed = true;
            }

            if (wasGrabbed && !this.interactable.attachedToHand)
            {
                wasGrabbed = false;
            }
        }
    }
}
