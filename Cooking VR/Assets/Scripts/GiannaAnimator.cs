using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiannaAnimator : MonoBehaviour
{


    private Animator MyAnimator;
    public FacialExpressions Face;
    private LookingEyes Eyes;

    void Start()
    {
        Face = GetComponentInChildren<FacialExpressions>();
        MyAnimator = GetComponent<Animator>();
        Eyes = GetComponent<LookingEyes>();

        //StartCoroutine(AnimationsShowcase(2));

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

    public void PlayImpressed(float Delay)
    {
        StartCoroutine(DelayedAnimation(0, Delay));
    }

    public void PlayAngry(float Delay)
    {
        StartCoroutine(DelayedAnimation(1, Delay));
    }

    public void PlaySassy(float Delay)
    {
        StartCoroutine(DelayedAnimation(2, Delay));
    }

    public void PlayDisgust(float Delay)
    {
        StartCoroutine(DelayedAnimation(3, Delay));
    }

    public void PlayDisappointed(float Delay)
    {
        StartCoroutine(DelayedAnimation(4, Delay));
    }

    private IEnumerator DelayedAnimation(int AnimationNum ,float Delay)
    {
        yield return new WaitForSeconds(Delay);
        switch (AnimationNum)
        {
            case 0:
                PlayImpressed();
                break;
            case 1:
                PlayAngry();
                break;
            case 2:
                PlaySassy();
                break;
            case 3:
                PlayDisgust();
                break;
            case 4:
                PlayDisappointed();
                break;
        }
    }

}
