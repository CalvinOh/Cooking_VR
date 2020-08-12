using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Valve.VR.InteractionSystem
{
    public class Cutting : MonoBehaviour
    {
        [SerializeField]
        private bool isCutting;

        [SerializeField]
        private float cutTime;

        [SerializeField]
        private float nextCut;

        [SerializeField]
        private gameObject cutOnion;

        private void Start()
        {
            isCutting = false;
            nextCut = (1f / 3);
            cutTime = 0f;
        }

        //private void OnCollisionEnter(Collision collision)
        //{

        //    if (collision.gameObject.CompareTag("Cutable"))
        //    {
        //        Debug.Log("Cutting");

        //        int temp = collision.gameObject.transform.childCount;

        //        Debug.Log(collision.gameObject.transform.childCount + " & " /*+ collision.gameObject.transform.GetChild(temp - 1).name*/);

        //        collision.gameObject.transform.GetChild(temp - 1).GetComponent<Interactable>().enabled = true;
        //        collision.gameObject.transform.GetChild(temp - 1).GetComponent<Rigidbody>().isKinematic = false;
        //        collision.gameObject.transform.GetChild(temp - 1).transform.parent = null;
        //    }
        //}


        private void OnTriggerEnter(Collider other)
        {
            if (isCutting == false && cutTime <= Time.time)
            {
                isCutting = true;
                Debug.Log(isCutting);
                cutTime = Time.time + nextCut;

                if (other.gameObject.CompareTag("Cutable"))
                {
                    //audio
                    PlaySoundCutGeneric();

                    Debug.Log("Cutting");

                    int temp = other.gameObject.transform.childCount;

                    Debug.Log(other.gameObject.transform.childCount + " & " /*+ collision.gameObject.transform.GetChild(temp - 1).name*/);

                    other.gameObject.transform.GetChild(temp - 1).GetComponent<MeshCollider>().enabled = true;
                    other.gameObject.transform.GetChild(temp - 1).GetComponent<Interactable>().enabled = true;

                    if (other.gameObject.transform.GetChild(temp - 1).TryGetComponent(out SphereCollider sc))
                        sc.enabled = true;

                    other.gameObject.transform.GetChild(temp - 1).GetComponent<Rigidbody>().isKinematic = false;
                    other.gameObject.transform.GetChild(temp - 1).transform.parent = null;
                }
                else if(other.gameObject.CompareTag("Onion"))
                {
                    Instantiate(cutOnion, other.gameObject.transform.position, other.gameObject.transform.rotation);

                    Destroy(other.gameObject);
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            isCutting = false;
            Debug.Log(isCutting);
        }

        //audio
        private void PlaySoundCutGeneric()
        {
            AkSoundEngine.PostEvent("Knife_Cut_Generic", gameObject);
        }
    }
}