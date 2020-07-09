using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectiveSounds : MonoBehaviour
{
    // audio
    // this script instructs the object to make collision sounds on collisions with certain objects
    // but not with others, such as cuttable items.

    public string WwiseEventName;

    // Start is called before the first frame update
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

    private void OnCollisionEnter(Collision collision)
    {
        AkSoundEngine.PostEvent(WwiseEventName, gameObject);
    }
}
