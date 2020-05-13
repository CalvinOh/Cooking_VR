using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoTriggers : MonoBehaviour
{
    // these methods will play voice lines in 3D space from whatever gameObject this script is attached to
    // 
    // the syntax for calling a voice line is: "Play_vx_(index letter)_(index number)();"
    //
    // for example: "Play_vx_a_4();" or "Play_vx_f_1();"
    

    // VO index A
    private void Play_vx_a_1()
    {
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
    }

    private void OnDisable()
    {
        CharacterTriggers.VOTrigger -= PlayVoiceClip;
    }

}
