using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagingAudio : MonoBehaviour
{
    public static AudioClip PlayerRunning, PlayerMeelee, PlayerJump, PlayerBlock, PlayerDeath;
    static AudioSource AudioSrc;

    void Start()
    {
        PlayerRunning = Resources.Load<AudioClip>("Walk");
        PlayerMeelee = Resources.Load<AudioClip>("Melee");
        PlayerJump = Resources.Load<AudioClip>("Jump");
        PlayerBlock = Resources.Load<AudioClip>("ESkill");
        PlayerDeath = Resources.Load<AudioClip>("Death");

        AudioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlaySound(string clip)
    {
        switch(clip)
        {
            case "Walk":
                AudioSrc.PlayOneShot(PlayerRunning);
                break;
            case "Melee":
                AudioSrc.PlayOneShot(PlayerMeelee);
                break;
            case "Jump":
                AudioSrc.PlayOneShot(PlayerJump);
                break;
            case "ESkill":
                AudioSrc.PlayOneShot(PlayerBlock);
                break;
            case "Death":
                AudioSrc.PlayOneShot(PlayerDeath);
                break;
        }
    }
}
