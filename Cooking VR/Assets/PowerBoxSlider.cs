using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class PowerBoxSlider : MonoBehaviour
{
    FuseBox MyFuseBox;

    [SerializeField]
    private Transform Start;
    [SerializeField]
    private Transform End;

    private Valve.VR.InteractionSystem.LinearMapping MyLMap;

    private bool WasOn;
    private bool WasOff;

    // Start is called before the first frame update
    void Awake()
    {
        MyFuseBox = GetComponentInParent<FuseBox>();
        MyLMap = GetComponent<Valve.VR.InteractionSystem.LinearMapping>();
    }

    // Update is called once per frame
    void Update()
    {
        if (InOnPosition()&&!WasOn)
        {
            if (!MyFuseBox.AttemptFix())
            {
                ResetPosition();
            }
        }


        WasOff = InOffPosition();
        WasOn = InOnPosition();
    }

    bool InOnPosition()
    {
        return Vector3.Distance(End.position, transform.position) < 0.1f;
    }

    bool InOffPosition()
    {
        return Vector3.Distance(Start.position, transform.position) < 0.1f;
    }

    public void ResetPosition()
    {
        Debug.Log("Reset Slider");
        transform.position = Start.position;
        MyLMap.value = 0 ;
    }



    
}
