using UnityEngine;
using System.Collections;

public class ZombieSounds : MonoBehaviour {

    public AudioClip zombieAttack1Sound;
    public AudioClip zombieAttack2Sound;
    public AudioClip zombieAttack3Sound;
    public AudioClip curiousZombieSound;
    public AudioClip zombieChaseSound;

	// Use this for initialization
	void Start () {
	
	}

    /*Zombie is idleing*/
    public void curiousNoise() {
        if (!audio.isPlaying)
        {
            audio.PlayOneShot(curiousZombieSound, 0.3f);
        }
    }

    /*The zombie is chasing you!*/
    public void chasingNoise() {
        if (!audio.isPlaying)
        {
            audio.PlayOneShot(zombieChaseSound);
        }
    }

    /*The zombie is attacking you!*/
    public void zombieAttackNoise() {
        if (!audio.isPlaying)
        {
            int zomAtkType = Random.Range(1, 4);
            if (zomAtkType == 1)
            {
                audio.PlayOneShot(zombieAttack1Sound);
            }
            else if (zombieAttack2Sound)
            {
                audio.PlayOneShot(zombieAttack2Sound);
            }
            else
            {
                audio.PlayOneShot(zombieAttack3Sound);
            }
        }
    }

    

}
