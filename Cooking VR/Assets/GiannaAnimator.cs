using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiannaAnimator : MonoBehaviour
{


    private Animator MyAnimator;
    private FacialExpressions Face;
    // Start is called before the first frame update
    void Start()
    {
        Face = GetComponentInChildren<FacialExpressions>();
        MyAnimator = GetComponent<Animator>();

        StartCoroutine(DelayStuff(5));

        Debug.Log(Face.gameObject.name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator DelayStuff(float a)
    {
        yield return new WaitForSeconds(a);
        Face.Smile(5);
        yield return new WaitForSeconds(5.2f);
        Face.Neutral(5);
    }


}
