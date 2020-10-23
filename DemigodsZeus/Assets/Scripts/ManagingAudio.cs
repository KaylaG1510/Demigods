using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using System.Threading;
using UnityEngine;

public class ManagingAudio : MonoBehaviour
{
    //used audio clips
    public static AudioClip PlayerMeelee, PlayerJump, PlayerLanding, PlayerBlock, PlayerDeath, PlayerHurt;
    static AudioSource AudioSrc;

    void Start()
    {
        PlayerMeelee = Resources.Load<AudioClip>("Melee");
        PlayerJump = Resources.Load<AudioClip>("Jump");
        PlayerLanding = Resources.Load<AudioClip>("Landing");
        PlayerBlock = Resources.Load<AudioClip>("ESkill");
        PlayerDeath = Resources.Load<AudioClip>("Death");
        PlayerHurt = Resources.Load<AudioClip>("Hurt");

        AudioSrc = GetComponent<AudioSource>();
    }

    //Play correct player sound at right time
    public static void PlaySound(string clip)
    {
        switch(clip)
        {
            //user is melee attacking
            case "Melee":
                AudioSrc.volume = 0.8f;
                AudioSrc.PlayOneShot(PlayerMeelee);
                break;
            //user is jumping
            case "Jump":
                AudioSrc.volume = 1.0f;
                AudioSrc.PlayOneShot(PlayerJump);
                break;
            //user lands on ground
            case "Landing":
                AudioSrc.PlayOneShot(PlayerLanding);
                break;
            //User blocks
            case "ESkill":
                AudioSrc.volume = 0.4f;
                AudioSrc.PlayOneShot(PlayerBlock);
                break;
            //user gets hit
            case "Hurt":
                AudioSrc.PlayOneShot(PlayerHurt);
                break;
            //user dies
            case "Death":
                AudioSrc.PlayOneShot(PlayerDeath);
                break;
        }
    }
}
