using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuseBox : MonoBehaviour
{

    private List<Fuse> AttachedFuses;

    [SerializeField]
    private GameObject Switch;




    public void TriggerEvent(int BrokenFuseNum)
    {
        if (BrokenFuseNum == 0)
        {
        }
        if (BrokenFuseNum == 1)
        {
            AttachedFuses[Random.Range(0, 2)].Break(true);
        }
        else if (BrokenFuseNum == 2)
        {
            foreach (Fuse a in AttachedFuses)
            {
                a.Break(true);
            }
            AttachedFuses[Random.Range(0, 2)].Break(false);
        }
        else
        {
            foreach (Fuse a in AttachedFuses)
            {
                a.Break(true);
            }
        }

        //move switch to off position and prepare other things for evennt start



    }

    public void AttemptFix()
    {
        Switch.transform.parent = this.gameObject.transform;
    }

}
