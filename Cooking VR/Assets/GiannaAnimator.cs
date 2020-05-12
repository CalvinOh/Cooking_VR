using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiannaAnimator : MonoBehaviour
{


    private Animator MyAnimator;
    public FacialExpressions Face;
    void Start()
    {
        Face = GetComponentInChildren<FacialExpressions>();
        MyAnimator = GetComponent<Animator>();

        StartCoroutine(AnimationsShowcase(2));

        //Debug.Log(Face.gameObject.name);
    }

    void Update()
    {
        
    }

    private IEnumerator AnimationsShowcase(float Delay)
    {
        yield return new WaitForSeconds(Delay);
        PlayImpressed();
        yield return new WaitForSeconds(5.2f);
        PlayAngry();
        yield return new WaitForSeconds(5.2f);
        PlaySassy();
        yield return new WaitForSeconds(5.2f);
        PlayDisgust();
        yield return new WaitForSeconds(5.2f);
        PlayDisappointed();
    }
    

    public void PlayImpressed()
    {
        MyAnimator.Play("Impressed");
        Face.Surprised(MyAnimator.GetCurrentAnimatorStateInfo(0).length, 100);
        Face.Grin(MyAnimator.GetCurrentAnimatorStateInfo(0).length, 100);
    }

    public void PlayAngry()
    {
        MyAnimator.Play("Angry");
        Face.Neutral(MyAnimator.GetCurrentAnimatorStateInfo(0).length, 100);
        Face.MouthO(MyAnimator.GetCurrentAnimatorStateInfo(0).length, 5);
        Face.MouthWShape(MyAnimator.GetCurrentAnimatorStateInfo(0).length, 15);
    }

    public void PlaySassy()
    {
        MyAnimator.Play("Sassy");
        Face.Smirk(MyAnimator.GetCurrentAnimatorStateInfo(0).length, 100);
        Face.Question(MyAnimator.GetCurrentAnimatorStateInfo(0).length, 100);
    }

    public void PlayDisgust()
    {
        MyAnimator.Play("Disgust");
        Face.Neutral(MyAnimator.GetCurrentAnimatorStateInfo(0).length, 100);
        Face.Surprised(MyAnimator.GetCurrentAnimatorStateInfo(0).length, 30);
    }

    public void PlayDisappointed()
    {
        MyAnimator.Play("Disappointed");
        Face.CloseEyes(MyAnimator.GetCurrentAnimatorStateInfo(0).length, 100);
        Face.Neutral(MyAnimator.GetCurrentAnimatorStateInfo(0).length, 40);
    }


}
