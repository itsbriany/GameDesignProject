using UnityEngine;
using System.Collections;

[RequireComponent(typeof (Animator))]
[RequireComponent(typeof (Rigidbody))]
[RequireComponent(typeof (CapsuleCollider))]

public class DerikGravity : MonoBehaviour {

    public float jumpSpeed = 10.0f;

    private Animator anim;

    private AnimatorStateInfo currentBaseState;
    private CapsuleCollider col;
    static int basicJumpState = Animator.StringToHash("Base Layer.DefaultJump");	
	
	//static int waveState = Animator.StringToHash("Layer2.Wave")

	// Use this for initialization
	void Start () {
	    anim = transform.GetComponent<Animator>();
        col = transform.GetComponent<CapsuleCollider>();
       
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	    float h = Input.GetAxis("Horizontal");				// setup h variable as our horizontal input axis
		float v = Input.GetAxis("Vertical");				// setup v variables as our vertical input axis
		anim.SetFloat("Speed", v);							// set our animator's float parameter 'Speed' equal to the vertical input axis				
		anim.SetFloat("Direction", h); 	
        currentBaseState = anim.GetCurrentAnimatorStateInfo(0); //returns the current animation state at layer 0

        if(Input.GetKey(KeyCode.LeftShift)){
            anim.SetBool("Run", true);
        }else
            anim.SetBool("Run", false);

        if(Input.GetKeyDown(KeyCode.Space)){
            anim.SetBool("Jump", true);
        }else
            anim.SetBool("Jump", false);

        //Debug.Log(currentBaseState.ToString());

        if(currentBaseState.nameHash == basicJumpState){
            rigidbody.AddForce(Vector3.up * jumpSpeed * Time.deltaTime, ForceMode.Impulse);
			if(!anim.IsInTransition(0))
			{
				   
					// ..set the collider height to a float curve in the clip called ColliderHeight
				col.height = anim.GetFloat("ColliderHeight");
				
				// reset the Jump bool so we can jump again, and so that the state does not loop 
				//anim.SetBool("Jump", false);
			}
        }
        
		// Raycast down from the center of the character.. 
		Ray ray = new Ray(transform.position + Vector3.up, -Vector3.up);
		RaycastHit hitInfo = new RaycastHit();
		
		if (Physics.Raycast(ray, out hitInfo))
		{
			// ..if distance to the ground is more than 1.75, use Match Target
			if (hitInfo.distance > 1.75f)
			{
				
				// MatchTarget allows us to take over animation and smoothly transition our character towards a location - the hit point from the ray.
				// Here we're telling the Root of the character to only be influenced on the Y axis (MatchTargetWeightMask) and only occur between 0.35 and 0.5
				// of the timeline of our animation clip
				anim.MatchTarget(hitInfo.point, Quaternion.identity, AvatarTarget.Root, new MatchTargetWeightMask(new Vector3(0, 1, 0), 0), 0.35f, 0.5f);
			}
		}

	}

    /*
    void OnAnimatorMove(){
        Vector3 velocity = anim.deltaPosition / Time.deltaTime;
        velocity.y = rigidbody.velocity.y;
       // rigidbody.velocity = velocity;
    }*/
}
