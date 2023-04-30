using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlaySFX : MonoBehaviour
{
    public string soundID;
    public float delay;
    public bool playOnStart;
    
    SoundEffectManager sfxm;
    public void PlaySoundEffect()
    {
        if(sfxm == null)
        {
            sfxm = GameObject.FindGameObjectWithTag("SFXManager").GetComponent<SoundEffectManager>();
        }
        sfxm.PlaySoundEffectWithDelay(soundID,delay);
    }


    private void OnEnable()
    {
        if (playOnStart)
        {
            PlaySoundEffect();
        }
    }
}
