using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Garbage_Disposal : MonoBehaviour
{
    [SerializeField]
    List<GameObject> garbage;
    [SerializeField]
    BoxCollider entryCollider;

    // Start is called before the first frame update
    void Start()
    {
        garbage = new List<GameObject>();
       
     
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        garbage.Add(other.gameObject);
        Debug.Log("Trash has entered the field");
    }

  
    void DeleteItem(bool isFull = false)
    {
        foreach(GameObject trash in garbage)
        {
            garbage.Remove(trash);
            Destroy(trash);
        }
    }
}
