﻿using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class VoTriggers : MonoBehaviour
{
    public bool isSpeaking = false;

    // these methods will play voice lines in 3D space from whatever gameObject this script is attached to
    // 
    // the syntax for calling a voice line is: "Play_vx_(index letter)_(index number)();"
    //
    // for example: "Play_vx_a_4();" or "Play_vx_f_1();"


    // VO index A
    private void Play_vx_a_1()
    {
        isSpeaking = true;

        //AkSoundEngine.PostEvent("Play_vx_a_1", gameObject, (uint) AKCallbackType.AK_EndOfEvent, MakeIsSpeakingFalse(), gameObject);
        AkSoundEngine.PostEvent("Play_vx_a_1", gameObject);
    }

    private void Play_vx_a_2()
    {
        AkSoundEngine.PostEvent("Play_vx_a_2", gameObject);
    }

    private void Play_vx_a_3()
    {
        AkSoundEngine.PostEvent("Play_vx_a_3", gameObject);
    }

    private void Play_vx_a_4()
    {
        AkSoundEngine.PostEvent("Play_vx_a_4", gameObject);
    }

    private void Play_vx_a_5()
    {
        AkSoundEngine.PostEvent("Play_vx_a_5", gameObject);
    }

    // VO index B
    private void Play_vx_b_1()
    {
        AkSoundEngine.PostEvent("Play_vx_b_1", gameObject);
    }

    private void Play_vx_b_2()
    {
        AkSoundEngine.PostEvent("Play_vx_b_2", gameObject);
    }

    private void Play_vx_b_3()
    {
        AkSoundEngine.PostEvent("Play_vx_b_3", gameObject);
    }

    private void Play_vx_b_4()
    {
        AkSoundEngine.PostEvent("Play_vx_b_4", gameObject);
    }

    private void Play_vx_b_5()
    {
        AkSoundEngine.PostEvent("Play_vx_b_5", gameObject);
    }

    private void Play_vx_b_6()
    {
        AkSoundEngine.PostEvent("Play_vx_b_6", gameObject);
    }

    private void Play_vx_b_7()
    {
        AkSoundEngine.PostEvent("Play_vx_b_7", gameObject);
    }

    // VO index C
    private void Play_vx_c_1()
    {
        AkSoundEngine.PostEvent("Play_vx_c_1", gameObject);
    }

    private void Play_vx_c_2()
    {
        AkSoundEngine.PostEvent("Play_vx_c_2", gameObject);
    }

    private void Play_vx_c_3()
    {
        AkSoundEngine.PostEvent("Play_vx_c_3", gameObject);
    }

    // VO index D
    private void Play_vx_d_1()
    {
        AkSoundEngine.PostEvent("Play_vx_d_1", gameObject);
    }

    private void Play_vx_d_2()
    {
        AkSoundEngine.PostEvent("Play_vx_d_2", gameObject);
    }

    private void Play_vx_d_3()
    {
        AkSoundEngine.PostEvent("Play_vx_d_3", gameObject);
    }

    // VO index E
    private void Play_vx_e_1()
    {
        AkSoundEngine.PostEvent("Play_vx_e_1", gameObject);
    }

    private void Play_vx_e_2()
    {
        AkSoundEngine.PostEvent("Play_vx_e_2", gameObject);
    }

    private void Play_vx_e_3()
    {
        AkSoundEngine.PostEvent("Play_vx_e_3", gameObject);
    }

    private void Play_vx_e_4()
    {
        AkSoundEngine.PostEvent("Play_vx_e_4", gameObject);
    }

    // VO index F
    private void Play_vx_f_1()
    {
        AkSoundEngine.PostEvent("Play_vx_f_1", gameObject);
    }

    private void Play_vx_f_2()
    {
        AkSoundEngine.PostEvent("Play_vx_f_2", gameObject);
    }

    // VO index G
    private void Play_vx_g_1()
    {
        AkSoundEngine.PostEvent("Play_vx_g_1", gameObject);
    }

    private void Play_vx_g_2()
    {
        AkSoundEngine.PostEvent("Play_vx_g_2", gameObject);
    }

    private void Play_vx_g_3()
    {
        AkSoundEngine.PostEvent("Play_vx_g_3", gameObject);
    }

    // VO index H
    private void Play_vx_h_1()
    {
        AkSoundEngine.PostEvent("Play_vx_h_1", gameObject);
    }
    private void Play_vx_h_2()
    {
        AkSoundEngine.PostEvent("Play_vx_h_2", gameObject);
    }
    private void Play_vx_h_3()
    {
        AkSoundEngine.PostEvent("Play_vx_h_3", gameObject);
    }

    // VO index I
    private void Play_vx_i_1()
    {
        AkSoundEngine.PostEvent("Play_vx_i_1", gameObject);
    }
    private void Play_vx_i_2()
    {
        AkSoundEngine.PostEvent("Play_vx_i_2", gameObject);
    }
    private void Play_vx_i_3()
    {
        AkSoundEngine.PostEvent("Play_vx_i_3", gameObject);
    }
    private void Play_vx_i_4()
    {
        AkSoundEngine.PostEvent("Play_vx_i_4", gameObject);
    }
    private void Play_vx_i_5()
    {
        AkSoundEngine.PostEvent("Play_vx_i_5", gameObject);
    }
    private void Play_vx_i_6()
    {
        AkSoundEngine.PostEvent("Play_vx_i_6", gameObject);
    }
    private void Play_vx_i_7()
    {
        AkSoundEngine.PostEvent("Play_vx_i_7", gameObject);
    }
    private void Play_vx_i_8()
    {
        AkSoundEngine.PostEvent("Play_vx_i_8", gameObject);
    }
    private void Play_vx_i_9()
    {
        AkSoundEngine.PostEvent("Play_vx_i_9", gameObject);
    }

    // VO index J
    private void Play_vx_j_1()
    {
        AkSoundEngine.PostEvent("Play_vx_j_1", gameObject);
    }
    private void Play_vx_j_2()
    {
        AkSoundEngine.PostEvent("Play_vx_j_2", gameObject);
    }
    private void Play_vx_j_3()
    {
        AkSoundEngine.PostEvent("Play_vx_j_3", gameObject);
    }
    private void Play_vx_j_4()
    {
        AkSoundEngine.PostEvent("Play_vx_j_4", gameObject);
    }
    private void Play_vx_j_5()
    {
        AkSoundEngine.PostEvent("Play_vx_j_5", gameObject);
    }
    private void Play_vx_j_6()
    {
        AkSoundEngine.PostEvent("Play_vx_j_6", gameObject);
    }
    private void Play_vx_j_7()
    {
        AkSoundEngine.PostEvent("Play_vx_j_7", gameObject);
    }

    // VO index L
    private void Play_vx_l_1()
    {
        AkSoundEngine.PostEvent("Play_vx_l_1", gameObject);
    }
    private void Play_vx_l_2()
    {
        AkSoundEngine.PostEvent("Play_vx_l_2", gameObject);
    }
    private void Play_vx_l_3()
    {
        AkSoundEngine.PostEvent("Play_vx_l_3", gameObject);
    }
    private void Play_vx_l_4()
    {
        AkSoundEngine.PostEvent("Play_vx_l_4", gameObject);
    }
    private void Play_vx_l_5()
    {
        AkSoundEngine.PostEvent("Play_vx_l_5", gameObject);
    }
    private void Play_vx_l_6()
    {
        AkSoundEngine.PostEvent("Play_vx_l_6", gameObject);
    }
    private void Play_vx_l_7()
    {
        AkSoundEngine.PostEvent("Play_vx_l_7", gameObject);
    }
    private void Play_vx_l_8()
    {
        AkSoundEngine.PostEvent("Play_vx_l_8", gameObject);
    }
    private void Play_vx_l_9()
    {
        AkSoundEngine.PostEvent("Play_vx_l_9", gameObject);
    }
    private void Play_vx_l_10()
    {
        AkSoundEngine.PostEvent("Play_vx_l_10", gameObject);
    }
    private void Play_vx_l_11()
    {
        AkSoundEngine.PostEvent("Play_vx_l_11", gameObject);
    }
    private void Play_vx_l_12()
    {
        AkSoundEngine.PostEvent("Play_vx_l_12", gameObject);
    }
    private void Play_vx_l_13()
    {
        AkSoundEngine.PostEvent("Play_vx_l_13", gameObject);
    }
    private void Play_vx_l_14()
    {
        AkSoundEngine.PostEvent("Play_vx_l_14", gameObject);
    }

    // VO index M
    private void Play_vx_m_1()
    {
        AkSoundEngine.PostEvent("Play_vx_m_1", gameObject);
    }
    private void Play_vx_m_2()
    {
        AkSoundEngine.PostEvent("Play_vx_m_2", gameObject);
    }
    private void Play_vx_m_3()
    {
        AkSoundEngine.PostEvent("Play_vx_m_3", gameObject);
    }
    private void Play_vx_m_4()
    {
        AkSoundEngine.PostEvent("Play_vx_m_4", gameObject);
    }
    private void Play_vx_m_5()
    {
        AkSoundEngine.PostEvent("Play_vx_m_5", gameObject);
    }
    private void Play_vx_m_6()
    {
        AkSoundEngine.PostEvent("Play_vx_m_6", gameObject);
    }
    private void Play_vx_m_7()
    {
        AkSoundEngine.PostEvent("Play_vx_m_7", gameObject);
    }
    private void Play_vx_m_8()
    {
        AkSoundEngine.PostEvent("Play_vx_m_8", gameObject);
    }
    private void Play_vx_m_9()
    {
        AkSoundEngine.PostEvent("Play_vx_m_9", gameObject);
    }
    private void Play_vx_m_10()
    {
        AkSoundEngine.PostEvent("Play_vx_m_10", gameObject);
    }
    private void Play_vx_m_11()
    {
        AkSoundEngine.PostEvent("Play_vx_m_11", gameObject);
    }


    private void PlayVoiceClip(string ClipName, float Delay)
    {
        StartCoroutine(DelayedPlay(ClipName,Delay));
    }

    private IEnumerator DelayedPlay(string ClipName,float Delay)
    {
        yield return new WaitForSeconds(Delay);
        AkSoundEngine.PostEvent(ClipName, gameObject);
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

    //void MakeIsSpeakingFalse(object in_cookie, AkCallbackType in_type, object in_info)
    //{
    //    if (in_type == AK_EndOfEvent)
    //    {
    //        AkEventCallbackInfo info = (AkEventCallbackInfo)in_info;

    //        isSpeaking = false;
    //    }
    //}

    //void Update()
    //{
    //    UnityEngine.Debug.Log($"isSpeaking = {isSpeaking}");
    //}
}
