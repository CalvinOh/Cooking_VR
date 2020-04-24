using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;


namespace Valve.VR.InteractionSystem
{
    public class ShrinkRay : MonoBehaviour
    {
        [SerializeField]
        private Transform Tip;

        [SerializeField]
        private float ShrinkScale = 0.5f;

        [SerializeField]
        [Tooltip("What tags this item can shrink, pleas enter exactly as tag is named")]
        private List<string> TagWhiteList = new List<string>();

        [SerializeField]
        private float LaserWidth = 0.1f;

        [SerializeField]
        private Transform Ray;

        [SerializeField]
        float CorrectionFactor = 16.8f;

        [SerializeField]
        private Material LaserYes;

        [SerializeField]
        private Material LaserNo;

        private RaycastHit Hit;
        private GameObject PreviousHitObject;
        private bool PreviousHit;
        private MeshRenderer LaserMesh;
        private Transform TargetObject;
        private Interactable interactable;
        private bool WasHeld;


        void Start()
        {
            PreviousHit = false;
            PreviousHitObject = null;
            LaserMesh = Ray.GetComponentInChildren<MeshRenderer>();
            interactable = GetComponent<Interactable>();

            //following test shots are for testing
            //StartCoroutine(DelayedFire(5));
            //StartCoroutine(DelayedFire(7));
        }

        public void Fire()
        {
            Debug.Log("Shrink Ray Fired.");
            RaycastHit Hitinfo;
            if (Physics.Raycast(Tip.position, Tip.forward, out Hitinfo))
            {
                if (TagWhiteList.Contains(Hitinfo.transform.gameObject.tag))
                {
                    Hitinfo.transform.localScale *= ShrinkScale;
                    Debug.Log("Shrunk: " + Hitinfo.transform.gameObject.name);
                }
                else
                {
                    Debug.Log("Cannot shrink: " + Hitinfo.transform.gameObject.name);
                }

            }
        }

        private void Update()
        {

            //if(SteamVR_Actions._default.)

            if (interactable.attachedToHand)
            {
                if (!WasHeld)
                {
                    transform.localPosition = new Vector3(0,0,0);
                    transform.localRotation = Quaternion.Euler(0,0,0);
                    Ray.gameObject.SetActive(true);
                }
                if (SteamVR_Actions._default.TriggerPulled.GetStateDown(SteamVR_Input_Sources.Any))
                {
                    Fire();
                }


                if (Physics.Raycast(Tip.position, Tip.forward, out Hit))
                {
                    Vector3 a = Ray.localScale;
                    a.y = Vector3.Distance(Tip.position, Hit.point) * CorrectionFactor;
                    Ray.localScale = a;

                    if (Hit.transform.gameObject != PreviousHitObject || PreviousHit != true)
                    {
                        DetermineHitObject();
                    }

                    PreviousHit = true;
                    PreviousHitObject = Hit.transform.gameObject;
                }
                else
                {
                    Vector3 a = Ray.localScale;
                    a.y = 3300;
                    Ray.localScale = a;

                    if (PreviousHit != false)
                    {
                        LaserMesh.material = LaserNo;
                    }

                    PreviousHit = false;
                }

            }
            else if (WasHeld)
            { 
                    Ray.gameObject.SetActive(false);
            }



            WasHeld = interactable.attachedToHand;
        }

        private void DetermineHitObject()
        {
            if (TagWhiteList.Contains(Hit.transform.gameObject.tag))
            {
                LaserMesh.material = LaserYes;
            }
            else
            {
                LaserMesh.material = LaserNo;
            }
        }


        private IEnumerator DelayedFire(float delay)
        {
            //this function was mainly used during testing when no VR rig was accessable
            yield return new WaitForSeconds(delay);
            Fire();
        }


    }
}
