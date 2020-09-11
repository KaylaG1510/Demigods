using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{
    //set up audio mixer entered through inspector
    public AudioMixer mixer;
    //whether or not volume is muted
    private bool isMuted;

    /**
     * Sets the linear slider volume into logarithmic
     * decibel value so slider is accurate to volume output.
     * */
    public void setLevel(float sliderValue)
    {
        mixer.SetFloat("MusicVolume", Mathf.Log10(sliderValue) * 20);
    }

    //At start of script
    void Start()
    {
        setLevel(1);      //volume starts on full 
        isMuted = false;  //volume is not muted
    }

    /**
     * User toggles to mute toggle in options menu
     */
    public void MutePressed()
    {
        isMuted = !isMuted; //invert the current state of sound
        //AudioListener.pause = isMuted;    //pauses soundtrack
        //conditional to set volume to 0 instead of pause
        if(isMuted) //user wants volume muted
        {
            setLevel(0.0001f);  //min slider value
        }
        else   //user wants sound on
        {
            setLevel(1);    //max slider value
        }
    }
}
