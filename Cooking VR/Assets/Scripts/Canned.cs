using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;


namespace Valve.VR.InteractionSystem
{
    public class Canned : MonoBehaviour
    {


        [SerializeField]
        private float TippingAngle;
        [SerializeField]
        private GameObject Contents;
        [SerializeField]
        private Transform SpawnPoint;
        [SerializeField]
        private int Amount = 1;

        private Interactable interactable;

        // Start is called before the first frame update
        void Start()
        {
            interactable = GetComponent<Interactable>();
        }

        // Update is called once per frame
        void Update()
        {
            //Debug.Log("Current Angle: " + Vector3.Angle(transform.up, -Vector3.up) + " Tipped: " + Tipped());

            if (interactable.attachedToHand)
            {
                if (Tipped() && Amount > 0)
                {
                    Instantiate(Contents, SpawnPoint.transform.position, SpawnPoint.transform.rotation, null);
                    Amount--;
                }
            }
            
        }



        private bool Tipped()
        {
            return (Vector3.Angle(transform.up, -Vector3.up) < TippingAngle);
        }
    }
}
