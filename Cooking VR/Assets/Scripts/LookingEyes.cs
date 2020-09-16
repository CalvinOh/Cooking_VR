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
    private GameObject LeyeSocket;

    [SerializeField]
    private GameObject ReyeSocket;

    [SerializeField]
    private List<string> LookTargetWhiteList;

    private GameObject Leye;

    private GameObject Reye;

    public Transform LookTarget;

    private Vector3 StartRotation;

    private Transform Player;
    private Transform PlayerLHand;
    private Transform PlayerRHand;

    private List<Transform> PotentialLookTargets;
    

    // Start is called before the first frame update
    void Start()
    {
        Leye = LeyeSocket.transform.GetChild(0).gameObject;
        Reye = ReyeSocket.transform.GetChild(0).gameObject;
        FindPlayerObjects();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(Leye.transform.position, Leye.transform.forward, Color.red);
        Debug.DrawRay(LeyeSocket.transform.position, LeyeSocket.transform.forward, Color.blue);

        Debug.DrawRay(Reye.transform.position, Reye.transform.forward, Color.red);
        Debug.DrawRay(ReyeSocket.transform.position, ReyeSocket.transform.forward, Color.blue);

        Look();


    }

    void Look()
    {
        if (InLookCone(LookTarget))
        {
            LookAtTarget(LookTarget.position, Leye);
            LookAtTarget(LookTarget.position, Reye);
        }
    }

    bool InLookCone(Transform CheckTarget)
    {
        if( Vector3.Angle(LeyeSocket.transform.forward,CheckTarget.transform.position-LeyeSocket.transform.position)>LookAngle/2|| Vector3.Angle(ReyeSocket.transform.forward, CheckTarget.transform.position - ReyeSocket.transform.position) > LookAngle / 2)
            return false;
        return true;
    }

    void LookAtTarget(Vector3 Target, GameObject Eye)
    {

        Vector3 newDir = Vector3.RotateTowards(Eye.transform.forward, Target - Eye.transform.position, RotateSpeed * Time.deltaTime, 0.0f);
        Debug.DrawRay(Eye.transform.position, newDir, Color.red);

        // Move our position a step closer to the target.
        Eye.transform.rotation = Quaternion.LookRotation(newDir,transform.up);
    }

    public bool RecieveLookTarget(GameObject ObjectToRecieve)
    {
        if (InLookCone(ObjectToRecieve.transform))
        {
            LookTarget = ObjectToRecieve.transform;
            return true;
        }
        else
        {
            return false;
        }

    }

    private void FindPlayerObjects()
    {
        Player =  GameObject.Find("VRCamera").transform;
        PlayerLHand = GameObject.Find("LeftHand").transform;
        PlayerRHand = GameObject.Find("RightHand").transform;
        if (Player == null)
        {
            Debug.Log("Gianna Looking eyes cannot find player");
        }
        if (PlayerLHand == null)
        {
            Debug.Log("Gianna Looking eyes cannot find LeftHand");
        }
        if (PlayerRHand == null) 
        {
            Debug.Log("Gianna Looking eyes cannot find RightHand");
        }
    }

    public Transform FindNewLookingTarget()
    {
        float decider = Random.Range(0,100);
        if (decider > 70)
        {
            return Player;
        }
        else if (decider > 50)
        {
            if (Random.Range(0, 10) > 5)
                return PlayerLHand;
            else
                return PlayerRHand;
        }
        else if (decider > 10)
        {
            return PotentialLookTargets[Random.Range(0,PotentialLookTargets.Count)];
            return null;
        }
        else 
        {
            return LookTarget;
        }
    }

    public void LookAtPlayer(float duration)
    {
        StartCoroutine(LookAtPlayerForDuration(duration));
    }

    public void LookAtPlayer()
    {
        LookTarget = Player;
    }

    private IEnumerator LookAtPlayerForDuration(float duration)
    {
        LookTarget = Player;
        yield return new WaitForSeconds(duration);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (LookTargetWhiteList.Contains(other.tag))
        {
            if (!PotentialLookTargets.Contains(other.transform))
            {
                PotentialLookTargets.Add(other.transform);
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (PotentialLookTargets.Contains(other.transform))
        {
            PotentialLookTargets.Remove(other.transform);
        }
    }

}
