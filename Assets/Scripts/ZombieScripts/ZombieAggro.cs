using UnityEngine;
using System.Collections;

[RequireComponent(typeof (AudioSource))]

public class ZombieAggro : MonoBehaviour {
    public float moveSpeed = 10f;
    public float rotationSpeed = 10f;
    public float attackDistance = 1f;

    private Animator anim;
    private AnimatorStateInfo currentStateInfo;
    private ZombieSounds zombieSounds;
    private TyrantZombieSounds tyrantZombieSounds;

    //Zombie animation states
    static int idleState = Animator.StringToHash("Base Layer.idle0");
    static int attack1State = Animator.StringToHash("Base Layer.attack0");
    static int attack2State = Animator.StringToHash("Base Layer.attack1");
    static int deathState = Animator.StringToHash("Base Layer.death");
    static int runState = Animator.StringToHash("Base Layer.run");
    static int walkState = Animator.StringToHash("Base Layer.walk");

    bool aggro = false;
    GameObject player;
    
    // AudioSource components on the gameobject need to be added in this order
    AudioSource aggroSource, attackSource;
    AudioSource[] audSource;

	// Use this for initialization
	void Start () {
	    player = GameObject.Find("Derik");
        anim = transform.GetComponent<Animator>();
        audSource = transform.GetComponents<AudioSource>();
        if (gameObject.name == "TyrantZombie")
        {
            tyrantZombieSounds = gameObject.GetComponent<TyrantZombieSounds>();
        }
        else
        {
            zombieSounds = gameObject.GetComponent<ZombieSounds>();
        }   
        aggroSource = audSource[0];
        attackSource = audSource[1];
	}
	
	// Update is called once per frame
	void Update () {
        resetZombieAnimator();
        if (aggro)
        {
            aggroMovement();
        }
        else
        {
            normalMovement();
        }
	}

    void normalMovement()
    {
        //anim.SetBool("Idle", true);
		anim.SetBool ("Walk", false);
    }

    void aggroMovement()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);

        Quaternion targetRotation = Quaternion.LookRotation(player.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        if (distance > attackDistance)
        {   
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
            anim.SetBool("Walk", true);
            if (!audio.isPlaying)
            {
                if (tyrantZombieSounds)
                {
                    tyrantZombieSounds.tyrantIdleing();
                }
                else if (zombieSounds)
                {
                    zombieSounds.curiousNoise();
                }
            }
        }
        else
        {
         
                attack(); 
        }
    }

    void attack()
    {
      
        int attackType = Random.Range(1, 3);
        Debug.Log(attackType);
        if (attackType == 1 && currentStateInfo.nameHash != attack1State && currentStateInfo.nameHash != attack2State)
        {
            if (currentStateInfo.nameHash == attack1State)
            anim.SetBool("Attack1", true);
            anim.SetBool("Attack2", false);
            if (!audio.isPlaying)
            {
                if (tyrantZombieSounds)
                    tyrantZombieSounds.tyrantZombieAttackNoise();
                else if (zombieSounds)
                    zombieSounds.zombieAttackNoise();
            }
        }
        else if (attackType != 1 && currentStateInfo.nameHash != attack1State && currentStateInfo.nameHash != attack2State)
        {
            anim.SetBool("Attack2", true);
            anim.SetBool("Attack1", false);
            if (!audio.isPlaying)
            {
                if (tyrantZombieSounds)
                    tyrantZombieSounds.tyrantZombieAttackNoise();
                else if (zombieSounds)
                    zombieSounds.zombieAttackNoise();
            }
        }

    }

    /*Reset anim variables*/
    void resetZombieAnimator() {
        Debug.Log("Resetting animator variables");
        anim.SetBool("Attack1", false);
        anim.SetBool("Attack2", false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            aggro = true;
            aggroSource.Play();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            aggro = false;
        }
    }

    //Makes the zombie run
    void zombieRun() {
        Debug.Log("Zombie speed: " + rigidbody.velocity);
        rigidbody.velocity = new Vector3(moveSpeed, 0, 0);
        anim.SetBool("Run", true);
        if (tyrantZombieSounds)
            tyrantZombieSounds.tyrantChasingNoise();
        else if (zombieSounds)
            zombieSounds.chasingNoise();
    }

    /*An animator event check to see if the zombie is tyrant
      so that the walking sounds will work
     */
    void tyrantZombieWalk()
    {
        if (gameObject.name == "TyrantZombie" && tyrantZombieSounds)
        {
            tyrantZombieSounds.tyrantWalk();
        }
    }

    /*An animator event check to see if the zombie is tyrant
      so that the giant footstep sounds will work
     */
    void tyrantZombieStomp()
    {
        if (gameObject.name == "TyrantZombie" && tyrantZombieSounds)
        {
            tyrantZombieSounds.tyrantStomp();
        }
    }



}
