using UnityEngine;
using System.Collections;

public class DerikGravity : MonoBehaviour {

    private Animator anim;

	// Use this for initialization
	void Start () {
	    anim = transform.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	    float h = Input.GetAxis("Horizontal");				// setup h variable as our horizontal input axis
		float v = Input.GetAxis("Vertical");				// setup v variables as our vertical input axis
		anim.SetFloat("Speed", v);							// set our animator's float parameter 'Speed' equal to the vertical input axis				
		anim.SetFloat("Direction", h); 	
	}

    void OnAnimatorMove(){
        Vector3 velocity = anim.deltaPosition / Time.deltaTime;
        velocity.y = rigidbody.velocity.y;
        rigidbody.velocity = velocity;
    }
}
