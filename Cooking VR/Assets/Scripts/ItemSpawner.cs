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
        private bool leftPlayerClicked, rightPlayerClicked;
        [SerializeField]
        GameObject objectToSpawn;
        [SerializeField]
        private string itemName;

        [SerializeField]
        private GameObject spawnedObject;

        [SerializeField]
        public Hand leftPlayerHand, rightPlayerHand;
        private Hand[] tempHands;


        // Start is called before the first frame update
        void Start()
        {
            hasSpawned = false;
            leftPlayerClicked = false;
            rightPlayerClicked = false;
            isGrabbing.AddOnStateDownListener(TriggerDown, handType);
            isGrabbing.AddOnStateUpListener(TriggerUp, handType);
            spawnedObject = null;

            if(leftPlayerHand == null || rightPlayerHand == null)
            {
                tempHands = FindObjectsOfType<Hand>();
                //FindObjectOfType<Hand>();

                foreach(Hand h in tempHands)
                {
                    if(h.handType == SteamVR_Input_Sources.LeftHand)
                    {
                        leftPlayerHand = h;
                        rightPlayerHand = h.otherHand;
                    }
                }

                //if(leftPlayerHand.handType != SteamVR_Input_Sources.LeftHand)
                //{
                //    rightPlayerHand = leftPlayerHand;
                //    leftPlayerHand = rightPlayerHand.otherHand;
                //}

                //leftPlayerHand = GameObject.Find("RightHand").GetComponent<Hand>().otherHand;
                Debug.Log("Call me, maybe?");
            }
            //if (rightPlayerHand == null)
            //{
            //    rightPlayerHand = GameObject.Find("LeftHand").GetComponent<Hand>().otherHand;
            //}


        }

        private void TriggerUp(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
        {
           // Debug.Log("Release");
            leftPlayerClicked = false;
            rightPlayerClicked = false;
            hasSpawned = false;
            //UnParentObject();
        }

        private void TriggerDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
        {
           // Debug.Log("Clicked");
            leftPlayerClicked = true;
            rightPlayerClicked = true;
        }

        // Update is called once per frame
        void Update()
        {
        
                SpawnObject();

        }

        void SpawnObject()
        {
            if (Vector3.Distance(this.gameObject.transform.position, leftPlayerHand.transform.position) < 0.2f )
            {
               
                if (leftPlayerClicked && !hasSpawned)
                {
                    Debug.Log("Plate Spawned");
                    spawnedObject = Instantiate(objectToSpawn, leftPlayerHand.transform.position, leftPlayerHand.transform.rotation);
                    leftPlayerHand.AttachObject(spawnedObject, GrabTypes.Pinch);
                    spawnedObject.name = itemName;
                    hasSpawned = true;
                    //Debug.Break();
                    
                }
            }
            else if (Vector3.Distance(this.gameObject.transform.position, rightPlayerHand.transform.position) < 0.2f)
            {

                if (rightPlayerClicked && !hasSpawned)
                {
                    Debug.Log("Plate Spawned");
                    spawnedObject = Instantiate(objectToSpawn, rightPlayerHand.transform.position, rightPlayerHand.transform.rotation);
                    rightPlayerHand.AttachObject(spawnedObject, GrabTypes.Pinch);
                    spawnedObject.name = itemName;
                    hasSpawned = true;
                    //Debug.Break();

                }
            }
        }



    }

}


