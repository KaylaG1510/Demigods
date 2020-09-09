using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{
    public AudioMixer mixer;
    private bool isMuted;

    public void setLevel(float sliderValue)
    {
        mixer.SetFloat("MusicVolume", Mathf.Log10(sliderValue) * 20);
    }

    void Start()
    {
        setLevel(1);
        isMuted = false;
    }

    public void MutePressed()
    {
        isMuted = !isMuted;
        //AudioListener.pause = isMuted;
        if(isMuted)
        {
            setLevel(0.0001f);
        }
        else
        {
            setLevel(1);
        }
    }
}
