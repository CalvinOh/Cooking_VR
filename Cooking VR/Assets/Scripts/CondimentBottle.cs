using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

namespace Valve.VR.InteractionSystem
{
    public class CondimentBottle : MonoBehaviour
    {
        [SerializeField]
        bool Automatic; //If true, will do the countdown and automatic fire. If false, will need to be squeezed

        [SerializeField]
        float SetTime; //Serialized Field to set the time between automatic fire in the editor

        public GameObject Condiment;
        public Transform SpawnPosition;
        public Interactable interactable;
        public float CountdownTime;

        void Start()
        {
            CountdownTime = SetTime;
            //in case the interactable script does not get set
            if(interactable == null)
            {
                interactable = GetComponent<Interactable>();
            }
        }

        // Update is called once per frame
        void Update()
        {
            //This might not currently work or may cause problems because i don't think it is waiting until...
            //...This object is in hand but that might also just be my lack of knowledge of SteamVR
            //This input needs to be set in the Binding UI of the SteamVR Input window
        
            if(interactable.attachedToHand)
            {
                //Automatic Fire, had to separate it into 2 ifs because of the added layer of checking for the input of squeeze
                if (CountdownTime <= 0.0f)
                {
                    Debug.Log("Squeeze");
                    SpawnCondiment();

                    //audio
                    PlaySoundUseCondiment();
                    CountdownTime = SetTime;
                }
                else if (!Automatic)
                {
                    if (SteamVR_Actions._default.SqueezeBottle.GetStateDown(SteamVR_Input_Sources.Any))
                    {
                        Debug.Log("Squeeze");
                        SpawnCondiment();

                        //audio
                        PlaySoundUseCondiment();
                    }
                }
                else
                {
                    CountdownTime -= Time.deltaTime;
                }
            }
            else
            {
                CountdownTime = SetTime;
            }
        }

        private void OnDrawGizmos()
        {
            Debug.DrawRay(this.transform.position, this.transform.up, Color.green);
        }

        //This method will spawn the condiment that will than be placed on the Dish
        void SpawnCondiment()
        {
            GameObject temp = Instantiate(Condiment, SpawnPosition);
            temp.GetComponent<Rigidbody>().AddForce(this.transform.up * 1000);
            temp.transform.localScale = new Vector3(1, 1, 1);
            temp.gameObject.transform.parent = null;
        }

        //audio
        private void PlaySoundUseCondiment()
        {
            AkSoundEngine.PostEvent("Use_Condiment", gameObject);
        }
    }
}

