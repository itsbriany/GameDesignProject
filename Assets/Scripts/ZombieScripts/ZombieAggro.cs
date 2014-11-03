using UnityEngine;
using System.Collections;

public class ZombieAggro : MonoBehaviour {
    public float moveSpeed = 10f;
    public float rotationSpeed = 10f;
    public float attackDistance = 1f;

    bool aggro = false;
    GameObject player;

	// Use this for initialization
	void Start () {
	    player = GameObject.Find("Derik");
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

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            aggro = true;
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
