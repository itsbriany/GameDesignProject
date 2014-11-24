using UnityEngine;
using System.Collections;

public class Damage : MonoBehaviour {

	public float damageAmt;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionStay(Collision collision)
	{
		if (collision.gameObject.name == "Derik")
		{
            Debug.Log("JDKFJ");
			Health playerHealth = collision.gameObject.GetComponent<Health>();
			playerHealth.modifyHealth(-damageAmt);
		}
	}
}
