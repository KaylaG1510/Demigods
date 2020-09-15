using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    //Static reference to itself to make sure we only ever have
    //one AudioManager at a time
    static AudioManager current;

    [Header("Ambient Audio")] 
    public AudioClip musicClip;     //background music

    [Header("Stings")]
    public AudioClip buttonHoverClip;
    public AudioClip buttonClickClip;
    public AudioClip gameOverDiedClip;
    public AudioClip gameWonClip;

    [Header("Player Audio")]
    public AudioClip runFootstepsClip;
    public AudioClip rollClip;
    public AudioClip jumpClip;
    public AudioClip blockClip;
    public AudioClip meleeClip;
    public AudioClip projectileClip;
    public AudioClip deathClip;

    [Header("Mixer Groups")]
    public AudioMixerGroup musicGroup;
    public AudioMixerGroup stingGroup;
    public AudioMixerGroup playerGroup;


    AudioSource musicSource;
    AudioSource stingSource;
    AudioSource playerSource;

    private void Awake()
    {
        //AudioManager exists other than this...
        if(current != null && current != this)
        {
            Destroy(gameObject);
            return;
        }

        //current AudioManager should stay between scene loads
        DontDestroyOnLoad(gameObject);

        //Generates AudioScource "channels" for game audio
        musicSource = gameObject.AddComponent<AudioSource>() as AudioSource;
        stingSource = gameObject.AddComponent<AudioSource>() as AudioSource;
        playerSource = gameObject.AddComponent<AudioSource>() as AudioSource;

        //Assign each source to its corresponding Mixer
        musicSource.outputAudioMixerGroup = musicGroup;
        stingSource.outputAudioMixerGroup = stingGroup;
        playerSource.outputAudioMixerGroup = playerGroup;
    }
}
