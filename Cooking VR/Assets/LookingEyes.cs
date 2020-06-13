using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookingEyes : MonoBehaviour
{

    [SerializeField]
    private float LookAngle = 60;

    [SerializeField]
    private float RotateSpeed = 1;

    [SerializeField]
    private GameObject Leye;

    [SerializeField]
    private GameObject Reye;

    public Transform LookTarget;

    private Vector3 StartRotation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(Leye.transform.position, transform.forward, Color.red);
        Debug.DrawRay(Leye.transform.position, StartRotation, Color.blue);

        Debug.DrawRay(Reye.transform.position, transform.forward, Color.red);
        Debug.DrawRay(Reye.transform.position, StartRotation, Color.blue);
    }

    void Look()
    {


        Debug.DrawRay(this.transform.position, transform.forward, Color.red);
        Debug.DrawRay(this.transform.position, StartRotation, Color.blue);
    }

    bool InLookCone()
    {
        if( Vector3.Angle(Vector3.forward,LookTarget.transform.position-Leye.transform.position)>LookAngle|| Vector3.Angle(Vector3.forward, LookTarget.transform.position - Reye.transform.position) > LookAngle)
            return false;
        return true;
    }

    void LookAtTarget(Vector3 Target, GameObject Eye)
    {

        Vector3 newDir = Vector3.RotateTowards(Eye.transform.forward, Target - Eye.transform.position, RotateSpeed * Time.deltaTime, 0.0f);
        Debug.DrawRay(Eye.transform.position, newDir, Color.red);

        // Move our position a step closer to the target.
        Eye.transform.rotation = Quaternion.LookRotation(newDir);
    }

}
