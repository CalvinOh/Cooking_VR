using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectiveSounds : MonoBehaviour
{
    // audio
    // this script instructs the object to make collision sounds on collisions with certain objects
    // but not with others, such as cuttable items.

    public string WwiseEventName;

    void Start()
    {
        CheckPrefabName();
    }

    void CheckPrefabName()
    {
        if (this.gameObject.name == "spatula_prefab")
        {
            WwiseEventName = "Impact_Spatula";
        }

        if (this.gameObject.name == "kitchen_knife")
        {
            WwiseEventName = "Impact_Knife";
        }
    }

    // detect type of object collided with and play appropriate sound
    private void OnCollisionEnter(Collision collision)
    {
        // if knife collides with cuttable object, play cut sound

        if (this.gameObject.name == "kitchen_knife" && collision.gameObject.GetComponent<SoftDeleteCutable>())
        {
            AkSoundEngine.PostEvent("Knife_Cut_Generic", gameObject);
        }

        // if collision is with anything other than cuttable objects or slices, play impact sound

        else if(!collision.gameObject.CompareTag("Slice") && !collision.gameObject.GetComponent<SoftDeleteCutable>())
        {
            AkSoundEngine.PostEvent(WwiseEventName, gameObject);
        }
    }
}
