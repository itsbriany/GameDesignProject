using UnityEngine;
using System.Collections;

[RequireComponent(typeof (Animator))]
[RequireComponent(typeof (Rigidbody))]
[RequireComponent(typeof (CapsuleCollider))]
[RequireComponent(typeof (DerikControls))]
[RequireComponent(typeof(AudioSource))]

public class Sounds : MonoBehaviour {

    public AudioClip footsteps1;
    public AudioClip footsteps2;
    public AudioClip damagedSoundEffect;
    public AudioClip shootingSoundEffect;
    public AudioClip screamingSoundEffect;
    public AudioClip jumpWhooshSoundEffect;

    private bool footstepFlag;

	// Use this for initialization
	void Start () {
        DerikControls derikControls = gameObject.GetComponent<DerikControls>();
        footstepFlag = true;
	}

    /*Plays the footsteps sound*/
    public void playFootsteps() {
        if (footstepFlag)
        {
            audio.PlayOneShot(footsteps1);
            footstepFlag = false;
        }
        else
        {
            audio.PlayOneShot(footsteps2);
            footstepFlag = true;
        }   
    }

    /*Derik is damaged!*/
    public void damagedSound() {
         audio.PlayOneShot(damagedSoundEffect, 1.0f);
    }

    /*Derik is shooting*/
    public void shootingSound() {
       audio.PlayOneShot(shootingSoundEffect, 1.0f);
    }

    /*Derik Screaming in pain*/
    public void dyingSound() {
        if (!audio.isPlaying)
            audio.PlayOneShot(screamingSoundEffect, 1.0f);
    }

    public void jumpWhooshSound() { 
       audio.PlayOneShot(jumpWhooshSoundEffect);
    }
}
