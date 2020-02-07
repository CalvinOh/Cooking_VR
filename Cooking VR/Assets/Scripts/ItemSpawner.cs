using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

namespace Valve.VR.InteractionSystem
{
    public class ItemSpawner : MonoBehaviour
    {

        public SteamVR_Action_Boolean isGrabbing;
        public SteamVR_Input_Sources handType;


        [SerializeField]
        private bool hasSpawned;
        [SerializeField]
        private bool playerClicked;
        [SerializeField]
        GameObject objectToSpawn;

        [SerializeField]
        private GameObject spawnedObject;

        [SerializeField]
        Hand playerHand;

        // Start is called before the first frame update
        void Start()
        {
            hasSpawned = false;
            playerClicked = false;
            isGrabbing.AddOnStateDownListener(TriggerDown, handType);
            isGrabbing.AddOnStateUpListener(TriggerUp, handType);
        }

        private void TriggerUp(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
        {
            Debug.Log("Release");
            playerClicked = false;
            hasSpawned = false;
        }

        private void TriggerDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
        {
            Debug.Log("Clicked");
            playerClicked = true;
        }

        // Update is called once per frame
        void Update()
        {
            SpawnObject();
            UnParentObject();
        }

        void SpawnObject()
        {
            if (Vector3.Distance(this.gameObject.transform.position, playerHand.transform.position) < 1f)
            {
               
                if (playerClicked && !hasSpawned)
                {
                    Debug.Log("Plate Spawned");
                    spawnedObject = Instantiate(objectToSpawn, playerHand.transform) as GameObject;
                    spawnedObject.transform.parent = playerHand.transform;
                    hasSpawned = true;
                   
                    
                }
            }
        }

        void UnParentObject()
        {
            if(!playerClicked)
            {
                spawnedObject.transform.parent = null;
            }
        }
        //private void OnTriggerEnter(Collider other)
        //{
        //    if(other.CompareTag("Hand"))
        //    {
        //        handInTrigger = true;
        //    }
        //}
        //private void OnTriggerExit(Collider other)
        //{
        //    handInTrigger = false;
        //}



    }

}


