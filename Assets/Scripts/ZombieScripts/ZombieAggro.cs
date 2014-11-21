using UnityEngine;
using System.Collections;

[RequireComponent(typeof (AudioSource))]
public class ZombieAggro : MonoBehaviour {
    public float moveSpeed = 10f;
    public float rotationSpeed = 10f;
    public float attackDistance = 1f;

    bool aggro = false;
    GameObject player;
    
    // AudioSource components on the gameobject need to be added in this order
    AudioSource aggroSource, attackSource;
    AudioSource[] audSource;

	// Use this for initialization
	void Start () {
	    player = GameObject.Find("Derik");
        audSource = transform.GetComponents<AudioSource>();
        aggroSource = audSource[0];
        attackSource = audSource[1];
	}
	
	// Update is called once per frame
	void Update () {
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
        // loop normal zombie stagger animation?
    }

    void aggroMovement()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);

        Quaternion targetRotation = Quaternion.LookRotation(player.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        if (distance > attackDistance)
        {
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }
        else
        {
            attack();
        }
    }

    void attack()
    {
        attackSource.Play();
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
}
