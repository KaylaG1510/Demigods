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
    public AudioClip levelStingClip;	//The sting played when the scene loads

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
        current = this;
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
    public static void PlayMusicAudio()
    {
        //Set the clip for music audio, tell it to loop, and then tell it to play
        current.musicSource.clip = current.musicClip;
        current.musicSource.loop = true;
        current.musicSource.Play();

        //Play the audio that repeats whenever the level reloads
        PlaySceneRestartAudio();
    }

    public static void PlaySceneRestartAudio()
    {
        //If there is no current AudioManager, exit
        if (current == null)
            return;

        //Set the level reload sting clip and tell the source to play
        current.stingSource.clip = current.levelStingClip;
        current.stingSource.Play();
    }

    public static void PlayFootstepAudio()
    {
        if (current == null || current.playerSource.isPlaying)
        {
            return;
        }

        //int index = Random.Range(0, current.runFootstepsClip.Length);

        current.playerSource.clip = current.runFootstepsClip;
        current.playerSource.Play();
    }

    public static void PlayJumpAudio()
    {
        if(current == null)
        {
            return;
        }

        current.playerSource.clip = current.jumpClip;
        current.playerSource.Play();
    }

    public static void PlayButtonHoverAudio()
    {
        if(current == null)
        {
            return;
        }

        current.playerSource.clip = current.buttonHoverClip;
        current.playerSource.Play();
    }

    public static void PlayButtonCLickAudio()
    {
        if (current == null)
        {
            return;
        }

        current.playerSource.clip = current.buttonClickClip;
        current.playerSource.Play();
    }

    public static void PlayGameOverAudio()
    {
        if (current == null)
        {
            return;
        }

        current.playerSource.clip = current.gameOverDiedClip;
        current.playerSource.Play();
    }

    public static void PlayGameWonAudio()
    {
        if (current == null)
        {
            return;
        }

        current.playerSource.clip = current.gameWonClip;
        current.playerSource.Play();
    }

    public static void PlayLevelStingAudio()
    {
        if (current == null)
        {
            return;
        }

        current.playerSource.clip = current.levelStingClip;
        current.playerSource.Play();
    }

    public static void PlayRollAudio()
    {
        if (current == null)
        {
            return;
        }

        current.playerSource.clip = current.rollClip;
        current.playerSource.Play();
    }

    public static void PlayBlockAudio()
    {
        if (current == null)
        {
            return;
        }

        current.playerSource.clip = current.blockClip;
        current.playerSource.Play();
    }

    public static void PlayAttackAudio()
    {
        if (current == null)
        {
            return;
        }

        current.playerSource.clip = current.meleeClip;
        current.playerSource.Play();
    }

    public static void PlayProjectileAudio()
    {
        if (current == null)
        {
            return;
        }

        current.playerSource.clip = current.projectileClip;
        current.playerSource.Play();
    }

    public static void PlayDeathAudio()
    {
        if (current == null)
        {
            return;
        }

        current.playerSource.clip = current.deathClip;
        current.playerSource.Play();
    }

}
