using UnityEngine;
using System.Collections;

public class TyrantZombieSounds : MonoBehaviour {

    public AudioClip tyrantZombieAttackSound;
    public AudioClip tyrantZombieChaseSound;
    public AudioClip tyrantZombieIdle1Sound;
    public AudioClip tyrantZombieIdle2Sound;
    public AudioClip tyrantZombieWalkSound;
    public AudioClip tyrantZombieFootstepSound;

	// Use this for initialization
	void Start () {
	
	}

    /*Tyrant zombie is idleing*/
    public void tyrantIdleing()
    {
        if (!audio.isPlaying)
        {
            int tyrantIdleSoundType = Random.Range(1, 3);

            if (tyrantIdleSoundType == 1)
            {
                audio.PlayOneShot(tyrantZombieIdle1Sound);
            }
            else
            {
                audio.PlayOneShot(tyrantZombieIdle2Sound);
            }
        }
    }

    /*The tyrant zombie is attacking you!*/
    public void tyrantZombieAttackNoise()
    {
        if (!audio.isPlaying)
        {
            audio.PlayOneShot(tyrantZombieAttackSound);
        }
    }

    /*The tyrant zomibie is chasing you!*/
    public void tyrantChasingNoise()
    {
        if (!audio.isPlaying)
        {
            audio.PlayOneShot(tyrantZombieAttackSound);
        }
    }

    /*The tyrant zombie stomped (running footstep sound)*/
    public void tyrantStomp() {
        //if (!audio.isPlaying)
       // {
            audio.PlayOneShot(tyrantZombieFootstepSound, 1.0f);
        //}
    }

    /*The tyrant zombie is walking (Walking footstep sound)*/
    public void tyrantWalk() {
        //if (!audio.isPlaying)
        //{
            audio.PlayOneShot(tyrantZombieWalkSound, 0.5f);
       // }
    }
}
