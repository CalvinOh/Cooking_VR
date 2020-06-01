using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vacuum : GrabbableGameObject
{
    [SerializeField]
    Collider col;

    // Start is called before the first frame update
    void Start()
    {
        InitGrabbable();
        if(col == null)
        {
            col = gameObject.GetComponent<BoxCollider>();
            col.isTrigger = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckGrabbed();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!other.tag.Equals("Plate") && other.gameObject.TryGetComponent<ManualStack>(out ManualStack ms))
        {
            Debug.Log($"{other.gameObject.name} was deleted by vacuum");
            Destroy(other.gameObject);
        }
        else if(other.gameObject.TryGetComponent<Vacuum>(out Vacuum v))
        {
            Debug.Log($"You fool, you've created a paradox!");
            Debug.Break();
        }
    }
}
