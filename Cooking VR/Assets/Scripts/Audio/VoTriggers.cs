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


    // EVENT DECLARATIONS //////////////////////////////////////////////////////////////
    // A
    public AK.Wwise.Event wPlay_vx_a_1;
    public AK.Wwise.Event wPlay_vx_a_2;
    public AK.Wwise.Event wPlay_vx_a_3;
    public AK.Wwise.Event wPlay_vx_a_4;
    public AK.Wwise.Event wPlay_vx_a_5;

    // B
    public AK.Wwise.Event wPlay_vx_b_1;
    public AK.Wwise.Event wPlay_vx_b_2;
    public AK.Wwise.Event wPlay_vx_b_3;
    public AK.Wwise.Event wPlay_vx_b_4;
    public AK.Wwise.Event wPlay_vx_b_5;
    public AK.Wwise.Event wPlay_vx_b_6;
    public AK.Wwise.Event wPlay_vx_b_7;

    // C
    public AK.Wwise.Event wPlay_vx_c_1;
    public AK.Wwise.Event wPlay_vx_c_2;
    public AK.Wwise.Event wPlay_vx_c_3;

    // D
    public AK.Wwise.Event wPlay_vx_d_1;
    public AK.Wwise.Event wPlay_vx_d_2;
    public AK.Wwise.Event wPlay_vx_d_3;

    // E
    public AK.Wwise.Event wPlay_vx_e_1;
    public AK.Wwise.Event wPlay_vx_e_2;
    public AK.Wwise.Event wPlay_vx_e_3;
    public AK.Wwise.Event wPlay_vx_e_4;

    // F
    public AK.Wwise.Event wPlay_vx_f_1;
    public AK.Wwise.Event wPlay_vx_f_2;

    // G
    public AK.Wwise.Event wPlay_vx_g_1;
    public AK.Wwise.Event wPlay_vx_g_2;
    public AK.Wwise.Event wPlay_vx_g_3;

    // H
    public AK.Wwise.Event wPlay_vx_h_1;
    public AK.Wwise.Event wPlay_vx_h_2;
    public AK.Wwise.Event wPlay_vx_h_3;

    // I
    public AK.Wwise.Event wPlay_vx_i_1;
    public AK.Wwise.Event wPlay_vx_i_2;
    public AK.Wwise.Event wPlay_vx_i_3;
    public AK.Wwise.Event wPlay_vx_i_4;
    public AK.Wwise.Event wPlay_vx_i_5;
    // there is no i_6
    public AK.Wwise.Event wPlay_vx_i_7;
    public AK.Wwise.Event wPlay_vx_i_8;
    public AK.Wwise.Event wPlay_vx_i_9;

    // J
    public AK.Wwise.Event wPlay_vx_j_1;
    public AK.Wwise.Event wPlay_vx_j_2;
    public AK.Wwise.Event wPlay_vx_j_3;
    public AK.Wwise.Event wPlay_vx_j_4;
    public AK.Wwise.Event wPlay_vx_j_5;
    public AK.Wwise.Event wPlay_vx_j_6;
    public AK.Wwise.Event wPlay_vx_j_7;

    // There is no K in-game; these were promotional lines for the trailer.

    // L
    public AK.Wwise.Event wPlay_vx_l_1;
    public AK.Wwise.Event wPlay_vx_l_2;
    public AK.Wwise.Event wPlay_vx_l_3;
    public AK.Wwise.Event wPlay_vx_l_4;
    public AK.Wwise.Event wPlay_vx_l_5;
    public AK.Wwise.Event wPlay_vx_l_6;
    public AK.Wwise.Event wPlay_vx_l_7;
    public AK.Wwise.Event wPlay_vx_l_8;
    public AK.Wwise.Event wPlay_vx_l_9;
    public AK.Wwise.Event wPlay_vx_l_10;
    public AK.Wwise.Event wPlay_vx_l_11;
    public AK.Wwise.Event wPlay_vx_l_12;
    public AK.Wwise.Event wPlay_vx_l_13;
    public AK.Wwise.Event wPlay_vx_l_14;

    // M
    public AK.Wwise.Event wPlay_vx_m_1;
    public AK.Wwise.Event wPlay_vx_m_2;
    public AK.Wwise.Event wPlay_vx_m_3;
    public AK.Wwise.Event wPlay_vx_m_4;
    public AK.Wwise.Event wPlay_vx_m_5;
    public AK.Wwise.Event wPlay_vx_m_6;
    public AK.Wwise.Event wPlay_vx_m_7;
    public AK.Wwise.Event wPlay_vx_m_8;
    public AK.Wwise.Event wPlay_vx_m_9;
    public AK.Wwise.Event wPlay_vx_m_10;
    public AK.Wwise.Event wPlay_vx_m_11;
    // END OF EVENT DECLARATIONS //////////////////////////////////////////////////////////////

    void Start()
    {
        isSpeaking = false;
    }


    // WWISE EVENT METHODS ////////////////////////////////////////////////////////////////////

    // VO index A
    private void Play_vx_a_1()
    {
        isSpeaking = true;

        wPlay_vx_a_1.Post(gameObject, (uint)AkCallbackType.AK_EndOfEvent, MakeIsSpeakingFalse);
    }
    private void Play_vx_a_2()
    {
        isSpeaking = true;

        wPlay_vx_a_2.Post(gameObject, (uint)AkCallbackType.AK_EndOfEvent, MakeIsSpeakingFalse);
    }
    private void Play_vx_a_3()
    {
        isSpeaking = true;

        wPlay_vx_a_3.Post(gameObject, (uint)AkCallbackType.AK_EndOfEvent, MakeIsSpeakingFalse);
    }
    private void Play_vx_a_4()
    {
        isSpeaking = true;

        wPlay_vx_a_4.Post(gameObject, (uint)AkCallbackType.AK_EndOfEvent, MakeIsSpeakingFalse);
    }
    private void Play_vx_a_5()
    {
        isSpeaking = true;

        wPlay_vx_a_5.Post(gameObject, (uint)AkCallbackType.AK_EndOfEvent, MakeIsSpeakingFalse);
    }

    // VO index B
    private void Play_vx_b_1()
    {
        isSpeaking = true;

        wPlay_vx_b_1.Post(gameObject, (uint)AkCallbackType.AK_EndOfEvent, MakeIsSpeakingFalse);
    }
    private void Play_vx_b_2()
    {
        isSpeaking = true;

        wPlay_vx_b_2.Post(gameObject, (uint)AkCallbackType.AK_EndOfEvent, MakeIsSpeakingFalse);
    }
    private void Play_vx_b_3()
    {
        isSpeaking = true;

        wPlay_vx_b_3.Post(gameObject, (uint)AkCallbackType.AK_EndOfEvent, MakeIsSpeakingFalse);
    }
    private void Play_vx_b_4()
    {
        isSpeaking = true;

        wPlay_vx_b_4.Post(gameObject, (uint)AkCallbackType.AK_EndOfEvent, MakeIsSpeakingFalse);
    }
    private void Play_vx_b_5()
    {
        isSpeaking = true;

        wPlay_vx_b_5.Post(gameObject, (uint)AkCallbackType.AK_EndOfEvent, MakeIsSpeakingFalse);
    }
    private void Play_vx_b_6()
    {
        isSpeaking = true;

        wPlay_vx_b_6.Post(gameObject, (uint)AkCallbackType.AK_EndOfEvent, MakeIsSpeakingFalse);
    }
    private void Play_vx_b_7()
    {
        isSpeaking = true;

        wPlay_vx_b_7.Post(gameObject, (uint)AkCallbackType.AK_EndOfEvent, MakeIsSpeakingFalse);
    }

    // VO index C
    private void Play_vx_c_1()
    {
        isSpeaking = true;

        wPlay_vx_c_1.Post(gameObject, (uint)AkCallbackType.AK_EndOfEvent, MakeIsSpeakingFalse);
    }
    private void Play_vx_c_2()
    {
        isSpeaking = true;

        wPlay_vx_c_2.Post(gameObject, (uint)AkCallbackType.AK_EndOfEvent, MakeIsSpeakingFalse);
    }
    private void Play_vx_c_3()
    {
        isSpeaking = true;

        wPlay_vx_c_3.Post(gameObject, (uint)AkCallbackType.AK_EndOfEvent, MakeIsSpeakingFalse);
    }

    // VO index D
    private void Play_vx_d_1()
    {
        isSpeaking = true;

        wPlay_vx_d_1.Post(gameObject, (uint)AkCallbackType.AK_EndOfEvent, MakeIsSpeakingFalse);
    }
    private void Play_vx_d_2()
    {
        isSpeaking = true;

        wPlay_vx_d_2.Post(gameObject, (uint)AkCallbackType.AK_EndOfEvent, MakeIsSpeakingFalse);
    }
    private void Play_vx_d_3()
    {
        isSpeaking = true;

        wPlay_vx_d_3.Post(gameObject, (uint)AkCallbackType.AK_EndOfEvent, MakeIsSpeakingFalse);
    }

    // VO index E
    private void Play_vx_e_1()
    {
        isSpeaking = true;

        wPlay_vx_e_1.Post(gameObject, (uint)AkCallbackType.AK_EndOfEvent, MakeIsSpeakingFalse);
    }
    private void Play_vx_e_2()
    {
        isSpeaking = true;

        wPlay_vx_e_2.Post(gameObject, (uint)AkCallbackType.AK_EndOfEvent, MakeIsSpeakingFalse);
    }
    private void Play_vx_e_3()
    {
        isSpeaking = true;

        wPlay_vx_e_3.Post(gameObject, (uint)AkCallbackType.AK_EndOfEvent, MakeIsSpeakingFalse);
    }
    private void Play_vx_e_4()
    {
        isSpeaking = true;

        wPlay_vx_e_4.Post(gameObject, (uint)AkCallbackType.AK_EndOfEvent, MakeIsSpeakingFalse);
    }

    // VO index F
    private void Play_vx_f_1()
    {
        isSpeaking = true;

        wPlay_vx_f_1.Post(gameObject, (uint)AkCallbackType.AK_EndOfEvent, MakeIsSpeakingFalse);
    }
    private void Play_vx_f_2()
    {
        isSpeaking = true;

        wPlay_vx_f_2.Post(gameObject, (uint)AkCallbackType.AK_EndOfEvent, MakeIsSpeakingFalse);
    }

    // VO index G
    private void Play_vx_g_1()
    {
        isSpeaking = true;

        wPlay_vx_g_1.Post(gameObject, (uint)AkCallbackType.AK_EndOfEvent, MakeIsSpeakingFalse);
    }
    private void Play_vx_g_2()
    {
        isSpeaking = true;

        wPlay_vx_g_2.Post(gameObject, (uint)AkCallbackType.AK_EndOfEvent, MakeIsSpeakingFalse);
    }
    private void Play_vx_g_3()
    {
        isSpeaking = true;

        wPlay_vx_g_3.Post(gameObject, (uint)AkCallbackType.AK_EndOfEvent, MakeIsSpeakingFalse);
    }

    // VO index H
    private void Play_vx_h_1()
    {
        isSpeaking = true;

        wPlay_vx_h_1.Post(gameObject, (uint)AkCallbackType.AK_EndOfEvent, MakeIsSpeakingFalse);
    }
    private void Play_vx_h_2()
    {
        isSpeaking = true;

        wPlay_vx_h_2.Post(gameObject, (uint)AkCallbackType.AK_EndOfEvent, MakeIsSpeakingFalse);
    }

    private void Play_vx_h_3()
    {
        isSpeaking = true;

        wPlay_vx_h_3.Post(gameObject, (uint)AkCallbackType.AK_EndOfEvent, MakeIsSpeakingFalse);
    }

    // VO index I
    private void Play_vx_i_1()
    {
        isSpeaking = true;

        wPlay_vx_i_1.Post(gameObject, (uint)AkCallbackType.AK_EndOfEvent, MakeIsSpeakingFalse);
    }
    private void Play_vx_i_2()
    {
        isSpeaking = true;

        wPlay_vx_i_2.Post(gameObject, (uint)AkCallbackType.AK_EndOfEvent, MakeIsSpeakingFalse);
    }
    private void Play_vx_i_3()
    {
        isSpeaking = true;

        wPlay_vx_i_3.Post(gameObject, (uint)AkCallbackType.AK_EndOfEvent, MakeIsSpeakingFalse);
    }
    private void Play_vx_i_4()
    {
        isSpeaking = true;

        wPlay_vx_i_4.Post(gameObject, (uint)AkCallbackType.AK_EndOfEvent, MakeIsSpeakingFalse);
    }
    private void Play_vx_i_5()
    {
        isSpeaking = true;

        wPlay_vx_i_5.Post(gameObject, (uint)AkCallbackType.AK_EndOfEvent, MakeIsSpeakingFalse);
    }
    // there is no i_6
    private void Play_vx_i_7()
    {
        isSpeaking = true;

        wPlay_vx_i_7.Post(gameObject, (uint)AkCallbackType.AK_EndOfEvent, MakeIsSpeakingFalse);
    }
    private void Play_vx_i_8()
    {
        isSpeaking = true;

        wPlay_vx_i_8.Post(gameObject, (uint)AkCallbackType.AK_EndOfEvent, MakeIsSpeakingFalse);
    }
    private void Play_vx_i_9()
    {
        isSpeaking = true;

        wPlay_vx_i_9.Post(gameObject, (uint)AkCallbackType.AK_EndOfEvent, MakeIsSpeakingFalse);
    }

    // VO index J
    private void Play_vx_j_1()
    {
        isSpeaking = true;

        wPlay_vx_j_1.Post(gameObject, (uint)AkCallbackType.AK_EndOfEvent, MakeIsSpeakingFalse);
    }
    private void Play_vx_j_2()
    {
        isSpeaking = true;

        wPlay_vx_j_2.Post(gameObject, (uint)AkCallbackType.AK_EndOfEvent, MakeIsSpeakingFalse);
    }
    private void Play_vx_j_3()
    {
        isSpeaking = true;

        wPlay_vx_j_3.Post(gameObject, (uint)AkCallbackType.AK_EndOfEvent, MakeIsSpeakingFalse);
    }
    private void Play_vx_j_4()
    {
        isSpeaking = true;

        wPlay_vx_j_4.Post(gameObject, (uint)AkCallbackType.AK_EndOfEvent, MakeIsSpeakingFalse);
    }
    private void Play_vx_j_5()
    {
        isSpeaking = true;

        wPlay_vx_j_5.Post(gameObject, (uint)AkCallbackType.AK_EndOfEvent, MakeIsSpeakingFalse);
    }
    private void Play_vx_j_6()
    {
        isSpeaking = true;

        wPlay_vx_j_6.Post(gameObject, (uint)AkCallbackType.AK_EndOfEvent, MakeIsSpeakingFalse);
    }
    private void Play_vx_j_7()
    {
        isSpeaking = true;

        wPlay_vx_j_7.Post(gameObject, (uint)AkCallbackType.AK_EndOfEvent, MakeIsSpeakingFalse);
    }

    // VO index L
    private void Play_vx_l_1()
    {
        isSpeaking = true;

        wPlay_vx_l_1.Post(gameObject, (uint)AkCallbackType.AK_EndOfEvent, MakeIsSpeakingFalse);
    }
    private void Play_vx_l_2()
    {
        isSpeaking = true;

        wPlay_vx_l_2.Post(gameObject, (uint)AkCallbackType.AK_EndOfEvent, MakeIsSpeakingFalse);
    }
    private void Play_vx_l_3()
    {
        isSpeaking = true;

        wPlay_vx_l_3.Post(gameObject, (uint)AkCallbackType.AK_EndOfEvent, MakeIsSpeakingFalse);
    }
    private void Play_vx_l_4()
    {
        isSpeaking = true;

        wPlay_vx_l_4.Post(gameObject, (uint)AkCallbackType.AK_EndOfEvent, MakeIsSpeakingFalse);
    }
    private void Play_vx_l_5()
    {
        isSpeaking = true;

        wPlay_vx_l_5.Post(gameObject, (uint)AkCallbackType.AK_EndOfEvent, MakeIsSpeakingFalse);
    }
    private void Play_vx_l_6()
    {
        isSpeaking = true;

        wPlay_vx_l_6.Post(gameObject, (uint)AkCallbackType.AK_EndOfEvent, MakeIsSpeakingFalse);
    }
    private void Play_vx_l_7()
    {
        isSpeaking = true;

        wPlay_vx_l_7.Post(gameObject, (uint)AkCallbackType.AK_EndOfEvent, MakeIsSpeakingFalse);
    }
    private void Play_vx_l_8()
    {
        isSpeaking = true;

        wPlay_vx_l_8.Post(gameObject, (uint)AkCallbackType.AK_EndOfEvent, MakeIsSpeakingFalse);
    }
    private void Play_vx_l_9()
    {
        isSpeaking = true;

        wPlay_vx_l_9.Post(gameObject, (uint)AkCallbackType.AK_EndOfEvent, MakeIsSpeakingFalse);
    }
    private void Play_vx_l_10()
    {
        isSpeaking = true;

        wPlay_vx_l_10.Post(gameObject, (uint)AkCallbackType.AK_EndOfEvent, MakeIsSpeakingFalse);
    }
    private void Play_vx_l_11()
    {
        isSpeaking = true;

        wPlay_vx_l_11.Post(gameObject, (uint)AkCallbackType.AK_EndOfEvent, MakeIsSpeakingFalse);
    }
    private void Play_vx_l_12()
    {
        isSpeaking = true;

        wPlay_vx_l_12.Post(gameObject, (uint)AkCallbackType.AK_EndOfEvent, MakeIsSpeakingFalse);
    }
    private void Play_vx_l_13()
    {
        isSpeaking = true;

        wPlay_vx_l_13.Post(gameObject, (uint)AkCallbackType.AK_EndOfEvent, MakeIsSpeakingFalse);
    }
    private void Play_vx_l_14()
    {
        isSpeaking = true;

        wPlay_vx_l_14.Post(gameObject, (uint)AkCallbackType.AK_EndOfEvent, MakeIsSpeakingFalse);
    }

    // VO index M
    private void Play_vx_m_1()
    {
        isSpeaking = true;

        wPlay_vx_m_1.Post(gameObject, (uint)AkCallbackType.AK_EndOfEvent, MakeIsSpeakingFalse);
    }
    private void Play_vx_m_2()
    {
        isSpeaking = true;

        wPlay_vx_m_2.Post(gameObject, (uint)AkCallbackType.AK_EndOfEvent, MakeIsSpeakingFalse);
    }
    private void Play_vx_m_3()
    {
        isSpeaking = true;

        wPlay_vx_m_3.Post(gameObject, (uint)AkCallbackType.AK_EndOfEvent, MakeIsSpeakingFalse);
    }
    private void Play_vx_m_4()
    {
        isSpeaking = true;

        wPlay_vx_m_4.Post(gameObject, (uint)AkCallbackType.AK_EndOfEvent, MakeIsSpeakingFalse);
    }
    private void Play_vx_m_5()
    {
        isSpeaking = true;

        wPlay_vx_m_5.Post(gameObject, (uint)AkCallbackType.AK_EndOfEvent, MakeIsSpeakingFalse);
    }
    private void Play_vx_m_6()
    {
        isSpeaking = true;

        wPlay_vx_m_6.Post(gameObject, (uint)AkCallbackType.AK_EndOfEvent, MakeIsSpeakingFalse);
    }
    private void Play_vx_m_7()
    {
        isSpeaking = true;

        wPlay_vx_m_7.Post(gameObject, (uint)AkCallbackType.AK_EndOfEvent, MakeIsSpeakingFalse);
    }
    private void Play_vx_m_8()
    {
        isSpeaking = true;

        wPlay_vx_m_8.Post(gameObject, (uint)AkCallbackType.AK_EndOfEvent, MakeIsSpeakingFalse);
    }
    private void Play_vx_m_9()
    {
        isSpeaking = true;

        wPlay_vx_m_9.Post(gameObject, (uint)AkCallbackType.AK_EndOfEvent, MakeIsSpeakingFalse);
    }
    private void Play_vx_m_10()
    {
        isSpeaking = true;

        wPlay_vx_m_10.Post(gameObject, (uint)AkCallbackType.AK_EndOfEvent, MakeIsSpeakingFalse);
    }
    private void Play_vx_m_11()
    {
        isSpeaking = true;

        wPlay_vx_m_11.Post(gameObject, (uint)AkCallbackType.AK_EndOfEvent, MakeIsSpeakingFalse);
    }
    // END OF WWISE EVENT METHODS /////////////////////////////////////////////////////////////


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

    // use this to make sure the bool is being toggled properly
    void Update()
    {
        //UnityEngine.Debug.Log($"isSpeaking = {isSpeaking}");
    }
}