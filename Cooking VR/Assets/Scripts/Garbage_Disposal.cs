using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Garbage_Disposal : MonoBehaviour
{
    [SerializeField]
    List<GameObject> garbage;
    [SerializeField]
    BoxCollider entryCollider;

    [SerializeField]
    float fullCheckTime;
    [SerializeField]
    float fullCheckTimeInterval;


    // Start is called before the first frame update
    void Start()
    {
        garbage = new List<GameObject>();

        fullCheckTimeInterval = 3;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!garbage.Contains(other.gameObject))
        {
            garbage.Add(other.gameObject);
            //Debug.Log("Trash has entered the field");
            Debug.Log($"{other.gameObject.name} has entered the trash can");
            fullCheckTime = Time.time + fullCheckTimeInterval;
        }
            
    }

    private void OnTriggerStay(Collider other)
    {
        foreach(GameObject trash in garbage)
        {
            if(other.gameObject.Equals(trash))
            {
                if (Time.time >= fullCheckTime)
                {
                  if(!other.CompareTag("Pan") || !other.CompareTag("Knife") || !other.CompareTag("Item"))
                    {
                        Debug.Log("KNOW YOUR PLACE TRASH!");
                        DeleteItem(true);
                    }
                }
                    
            }
        }
        
    }

    void DeleteItem(bool isFull = false)
    {
       if(isFull == true)
        {
            //foreach (GameObject trash in garbage)
            for(int i = garbage.Count -1; i >= 0; i--)
            {
                Debug.Log($"{garbage[i].name} has been deleted!");
                Destroy(garbage[i]);
                garbage.Remove(garbage[i]);
            }
        }
    }
}
