using UnityEngine;
using System.Collections;

public class Ragdoll : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine("cleanupRagdoll");	
	}

    IEnumerable cleanupRagdoll(){
        yield return new WaitForSeconds(8);
        Destroy(gameObject);
    }
}
