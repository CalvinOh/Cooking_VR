using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiannaAnimator : MonoBehaviour
{


    private Animator MyAnimator;
    private FacialExpressions Face;
    void Start()
    {
        Face = GetComponentInChildren<FacialExpressions>();
        MyAnimator = GetComponent<Animator>();

        StartCoroutine(DelayStuff(5));

        //Debug.Log(Face.gameObject.name);
    }

    void Update()
    {
        
    }

    private IEnumerator DelayStuff(float a)
    {
        yield return new WaitForSeconds(a);
        MyAnimator.Play("Impressed");
        Face.Surprised(5);
        yield return new WaitForSeconds(5.2f);
        MyAnimator.Play("Angry");
        Face.Neutral(5);
        yield return new WaitForSeconds(5.2f);
        MyAnimator.Play("Sassy");
        Face.Smirk(5);
        Face.Question(5);
        yield return new WaitForSeconds(5.2f);
        MyAnimator.Play("Disgust");
        Face.Neutral(5);
        yield return new WaitForSeconds(5.2f);
        MyAnimator.Play("Disappointed");
        Face.CloseEyes(5);
    }


}
