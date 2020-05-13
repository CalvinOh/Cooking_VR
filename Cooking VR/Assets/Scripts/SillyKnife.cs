using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SillyKnife : MonoBehaviour
{
    //Set the Minimum Size for the entire object
    [SerializeField]
    float ScaleMin = 1;

    //Set the Max Size for the knife  blade to change to
    [SerializeField]
    Vector3 BladeScale;

    //Set the Max Size for the knife handle to change to
    [SerializeField]
    Vector3 HandleScale;

    [SerializeField]
    Transform BladePoint, HandlePoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeSize()
    {
        Debug.Log("Called Change Size");
        ChangeBladeSize();
        ChangeHandleSize();

        BladePoint.position = HandlePoint.position; //blade needs to be below the handlepoint 
    }

    private void ChangeBladeSize()
    {
        //this.transform.GetChild(0).transform.localScale = new Vector3(Random.Range(ScaleMin, BladeScale.x), Random.Range(ScaleMin, BladeScale.y), Random.Range(ScaleMin, BladeScale.z));
        HandlePoint.parent.localScale = new Vector3(Random.Range(ScaleMin, BladeScale.x), Random.Range(ScaleMin, BladeScale.y), Random.Range(ScaleMin, BladeScale.z));

    }

    private void ChangeHandleSize()
    {
        //this.transform.GetChild(1).transform.localScale = new Vector3(Random.Range(ScaleMin, HandleScale.x), Random.Range(ScaleMin, HandleScale.y), Random.Range(ScaleMin, HandleScale.z));
        BladePoint.parent.localScale = new Vector3(Random.Range(ScaleMin, HandleScale.x), Random.Range(ScaleMin, HandleScale.y), Random.Range(ScaleMin, HandleScale.z));
    }
}
