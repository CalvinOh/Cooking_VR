using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using UnityEngine;

public class VoTriggers : MonoBehaviour
{
    public bool isSpeaking;

    // these methods will play voice lines in 3D space from whatever gameObject this script is attached to
    // 
    // the syntax for calling a voice line is: "Play_vx_(index letter)_(index number)();"
    //
    // for example: "Play_vx_a_4();" or "Play_vx_f_1();"
    //
    // they also turn "isSpeaking" True when the voice event is playing and False at the end of the event
    // via Wwise "callbacks"

    
    void Start()
    {
        isSpeaking = false;
    }

    // this method makes "isSpeaking" false again at the end of a voice event
    void MakeIsSpeakingFalse(object in_cookie, AkCallbackType in_type, object in_info)
    {
        if (in_type == AkCallbackType.AK_EndOfEvent)
        {
            isSpeaking = false;
        }
    }

    private void PlayVoiceClip(string ClipName, float Delay)
    {
        StartCoroutine(DelayedPlay(ClipName,Delay));
    }

    private IEnumerator DelayedPlay(string ClipName,float Delay)
    {
        yield return new WaitForSeconds(Delay);
        isSpeaking = true;
        AkSoundEngine.PostEvent(ClipName, gameObject,(uint)AkCallbackType.AK_EndOfEvent, MakeIsSpeakingFalse,null);
    }

    private void OnEnable()
    {
        CharacterTriggers.VOTrigger += PlayVoiceClip;
        TutorialVO.VOTrigger += PlayVoiceClip;
    }

    private void OnDisable()
    {
        CharacterTriggers.VOTrigger -= PlayVoiceClip;
        TutorialVO.VOTrigger -= PlayVoiceClip;
    }

    // use this to make sure the bool is being toggled properly
    void Update()
    {
        //UnityEngine.Debug.Log($"isSpeaking = {isSpeaking}");
    }
}