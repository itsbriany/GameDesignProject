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

    private bool footstepFlag;

	// Use this for initialization
	void Start () {
	    //anim = gameObject.GetComponent<Animator>();
        Debug.Log("The gameObject: " + gameObject);
        DerikControls derikControls = gameObject.GetComponent<DerikControls>();
        Debug.Log("Animator from Sounds.cs: " + derikControls.anim);
        footstepFlag = true;
	}
	
	// Update is called once per frame
	void Update () {
	    //playFootsteps();
	}

    /*Plays the footsteps sound*/
    void playFootsteps() {
        if (!audio.isPlaying) {
            if (footstepFlag)
            {
                audio.PlayOneShot(footsteps1, 0.3f);
                footstepFlag = false;
            } 
            else 
            {
                audio.PlayOneShot(footsteps2, 0.7f);
                footstepFlag = true;
            }
                
        }
    }
}
