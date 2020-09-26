using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacialExpressions : MonoBehaviour
{
    [SerializeField]
    private float ExpressionChangeSpeed = 8;
    [SerializeField]
    private float BlinkSpeed = 40;
    [SerializeField]
    private VoTriggers MyVOTrigger;

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
    private bool Talking;
    
    


    void Start()
    {
        
        MySkin = GetComponent<SkinnedMeshRenderer>();

        Blink = true;
        StartCoroutine(ConstantBlink());

        
        Talking = true;
        StartCoroutine(Talk());
        
        BlendShapeSize = MySkin.sharedMesh.blendShapeCount;
        SetStates = new float[BlendShapeSize];

        //Smile(5);

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
                    MySkin.SetBlendShapeWeight(i, Mathf.Clamp(MySkin.GetBlendShapeWeight(i) + step(MySkin.GetBlendShapeWeight(i), SetStates[i]), 0, 100));
            }
            
        }
    }

    private float step(float From, float Target)
    {
        int decider = 0;

        if (Target - From > 0)
            decider = 1;
        else
            decider = -1;

        if (Mathf.Abs(From - Target) > Time.deltaTime * ExpressionChangeSpeed*50)
        {
            return Time.deltaTime * ExpressionChangeSpeed * 50 * decider;
        }
        else
        {
            return Mathf.Abs(From - Target) * decider;
        }

    }

    private float Blinkstep(float target)
    {
        return (50 - target) * Time.deltaTime * BlinkSpeed * -1;
    }


    public IEnumerator ConstantBlink()
    {
        while (Blink)
        { 
        yield return new WaitForSeconds(Random.Range(1f, 3.5f));
        SetStates[1] = 100;
        yield return new WaitForSeconds(0.2f);
        SetStates[1] = 0;
        }
    }



    public IEnumerator Talk()
    {
        while (true)
        {
            if(MyVOTrigger.isSpeaking)
                StartCoroutine(TalkOnce());
            yield return new WaitForSeconds(0.7f);
        }
        
    }

    private IEnumerator TalkOnce()
    {
        switch (Random.Range(0,4))
        {
            case 0:
                SetStates[6] = 100;
                yield return new WaitForSeconds(0.3f);
                SetStates[6] = 0;
                break;
            case 1:
                SetStates[6] = 64;
                SetStates[0] = 46;
                yield return new WaitForSeconds(0.3f);
                SetStates[0] = 0;
                SetStates[6] = 0;
                break;
            case 2:
                SetStates[0] = 70.8f;
                SetStates[6] = 47.2f;
                yield return new WaitForSeconds(0.3f);
                SetStates[0] = 0;
                SetStates[6] = 0;
                break;
            case 3:
                SetStates[0] = 52.8f;
                SetStates[6] = 27f;
                SetStates[7] = 12.4f;
                yield return new WaitForSeconds(0.3f);
                SetStates[0] = 0;
                SetStates[6] = 0;
                SetStates[7] = 0;
                break;
            case 4:
                SetStates[6] = 43.8f;
                yield return new WaitForSeconds(0.3f);
                SetStates[6] = 0;
                break;
        }
    }



    private IEnumerator Express(int i, float duration,float Amount)
    {
        SetStates[i] = Amount;
        yield return new WaitForSeconds(duration);
        SetStates[i] = 0;
    }

    public void RessetFace()
    {
        for (int i = 0; i < SetStates.Length; i++)
            SetStates[i] = 0;
    }

    public void Smile(float duration,float Amount)
    {
        StartCoroutine(Express(0,duration, Amount));
    }


    public void CloseEyes(float duration, float Amount)
    {
        StartCoroutine(Express(1,duration, Amount));
    }


    public void Smirk(float duration, float Amount)
    {
        StartCoroutine(Express(2,duration, Amount));
    }

    public void Question(float duration, float Amount)
    {
        StartCoroutine(Express(3, duration, Amount));
    }

    public void Neutral(float duration, float Amount)
    {
        StartCoroutine(Express(4, duration, Amount));
    }

    public void Surprised(float duration, float Amount)
    {
        StartCoroutine(Express(5, duration, Amount));
    }

    public void MouthO(float duration, float Amount)
    {
        StartCoroutine(Express(6, duration, Amount));
    }

    public void MouthOpenFrown(float duration, float Amount)
    {
        StartCoroutine(Express(7, duration, Amount));
    }

    public void MouthWShape(float duration, float Amount)
    {
        StartCoroutine(Express(8, duration, Amount));
    }

    public void Grin(float duration, float Amount)
    {
        StartCoroutine(Express(9, duration, Amount));
    }
}
