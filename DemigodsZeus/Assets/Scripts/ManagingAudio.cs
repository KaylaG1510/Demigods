using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using System.Threading;
using UnityEngine;

public class ManagingAudio : MonoBehaviour
{
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

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlaySound(string clip)
    {
        switch(clip)
        {
            case "Melee":
                AudioSrc.volume = 0.8f;
                AudioSrc.PlayOneShot(PlayerMeelee);
                break;
            case "Jump":
                AudioSrc.volume = 1.0f;
                AudioSrc.PlayOneShot(PlayerJump);
                break;
            case "Landing":
                AudioSrc.PlayOneShot(PlayerLanding);
                break;
            case "ESkill":
                AudioSrc.volume = 0.4f;
                AudioSrc.PlayOneShot(PlayerBlock);
                break;
            case "Hurt":
                AudioSrc.PlayOneShot(PlayerHurt);
                break;
            case "Death":
                AudioSrc.PlayOneShot(PlayerDeath);
                break;
        }
    }
}
