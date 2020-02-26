using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Garbage_Disposal : MonoBehaviour
{
    [SerializeField]
    List<GameObject> garbage;

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
    }

    private void OnTriggerStay(Collider other)
    {
       
    }

    void DeleteItem(bool isFull = false)
    {

    }
}
