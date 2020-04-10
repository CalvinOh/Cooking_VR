using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacialExpressions : MonoBehaviour
{
    [SerializeField]
    private float ExpressionChangeSpeed = 8;
    [SerializeField]
    private float BlinkSpeed = 40;

    private SkinnedMeshRenderer MySkin;


    //these are the values that the expressions should be at now, example: smile = 100 makes the character start to smile
    /*
    private float Smile;
    private float Blink;
    private float Smirk;
    private float Question;
    private float Neutral;
    private float Surprised;
    private float MouthO;
    private float MouthOpenFrown;
    */
    private int BlendShapeSize;
    private float[] SetStates;
    private bool Blink;
    
    


    void Start()
    {
        Blink = true;
        MySkin = GetComponent<SkinnedMeshRenderer>();
        StartCoroutine(ConstantBlink());
        BlendShapeSize = MySkin.sharedMesh.blendShapeCount;
        SetStates = new float[BlendShapeSize];

    }




    void Update()
    {
        UpdateFacialExpression();
            
    }

    private void UpdateFacialExpression()
    {
        for (int i = 0; i < BlendShapeSize; i++)
        {
            if (i == 1)//blinking works on a different speed
            {
                if (MySkin.GetBlendShapeWeight(i) != SetStates[i])
                    MySkin.SetBlendShapeWeight(i, Mathf.Clamp(MySkin.GetBlendShapeWeight(i) + Blinkstep(SetStates[i]), 0, 100));
            }
            else
            {
                if (MySkin.GetBlendShapeWeight(i) != SetStates[i])
                    MySkin.SetBlendShapeWeight(i, Mathf.Clamp(MySkin.GetBlendShapeWeight(i) + step(SetStates[i]), 0, 100));
            }
            
        }
    }

    private float step(float target)
    {
        return (50 - target) * Time.deltaTime * ExpressionChangeSpeed*-1;
    }

    private float Blinkstep(float target)
    {
        return (50 - target) * Time.deltaTime * BlinkSpeed * -1;
    }


    public IEnumerator ConstantBlink()
    {
        while (Blink)
        { 
        yield return new WaitForSeconds(Random.Range(0.5f, 3.5f));
        SetStates[1] = 100;
        yield return new WaitForSeconds(0.2f);
        SetStates[1] = 0;
        }
    }



    private IEnumerator Express(int i, float duration)
    {
        SetStates[i] = 100;
        yield return new WaitForSeconds(duration);
        SetStates[i] = 0;
    }

    public void RessetFace()
    {
        for (int i = 0; i < SetStates.Length; i++)
            SetStates[i] = 0;
    }

    public void Smile(float duration)
    {
        StartCoroutine(Express(0,duration));
    }


    public void CloseEyes(float duration)
    {
        StartCoroutine(Express(1,duration));
    }


    public void Smirk(float duration)
    {
        StartCoroutine(Express(2,duration));
    }

    public void Question(float duration)
    {
        StartCoroutine(Express(3, duration));
    }

    public void Neutral(float duration)
    {
        StartCoroutine(Express(4, duration));
    }

    public void Surprised(float duration)
    {
        StartCoroutine(Express(5, duration));
    }

    public void MouthO(float duration)
    {
        StartCoroutine(Express(6, duration));
    }

    public void MouthOpenFrown(float duration)
    {
        StartCoroutine(Express(7, duration));
    }



}
