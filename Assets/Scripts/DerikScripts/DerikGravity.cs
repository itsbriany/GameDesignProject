﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof (Animator))]
[RequireComponent(typeof (Rigidbody))]
[RequireComponent(typeof (CapsuleCollider))]

public class DerikGravity : MonoBehaviour {

    public float jumpSpeed = 10.0f;
    public float vaultDistance = 2.0f;
    public float vaultAccelerateForce = 10000.0f;

    private Animator anim;

    private AnimatorStateInfo currentBaseState;
    private CapsuleCollider col;
    static int basicJumpState = Animator.StringToHash("Base Layer.DefaultJump");	
    static int vaultState = Animator.StringToHash("Base Layer.Vault");
    static int runState = Animator.StringToHash("Base Layer.RunForward");
	
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
           // rigidbody.AddForce(Vector3.up * jumpSpeed * Time.deltaTime, ForceMode.Impulse);
			if(!anim.IsInTransition(0)){
				col.height = anim.GetFloat("ColliderHeight");
			}
           // rigidbody.AddForce(rigidbody.transform.forward * vaultAccelerateForce, ForceMode.Impulse);
        }
        if(currentBaseState.nameHash == vaultState){
			if(!anim.IsInTransition(0)){
				col.height = anim.GetFloat("ColliderHeight");				
			}
            anim.SetBool("Vault", false);
        }

        if(currentBaseState.nameHash == runState){
            anim.applyRootMotion = true;
        }
        
        pullDown();
		
        vault();
	}

    void OnCollisionEnter(Collision collision){
      
    }

    //Checks for vaulting obstacles
    void vault(){
        Ray ray = new Ray(transform.position, rigidbody.transform.forward);
        Debug.DrawRay(transform.position, rigidbody.transform.forward);
        
		RaycastHit hitInfo = new RaycastHit();
		
		if (Physics.Raycast(ray, out hitInfo)){
            if(hitInfo.transform.CompareTag("Obstacle")){
                if (hitInfo.distance <= vaultDistance){
				    anim.SetBool("Vault", true);
				// MatchTarget allows us to take over animation and smoothly transition our character towards a location - the hit point from the ray.
				// Here we're telling the Root of the character to only be influenced on the Y axis (MatchTargetWeightMask) and only occur between 0.35 and 0.5
				// of the timeline of our animation clip
				//anim.MatchTarget(hitInfo.point, Quaternion.identity, AvatarTarget.Root, new MatchTargetWeightMask(new Vector3(0, 1, 0), 0), 0.35f, 0.5f);
			    }    
            }
			// ..if distance to the ground is more than 1.75, use Match Target
            
			
        }
    }

    //Accelerates the character upon vault
    void vaultAccelerate(){
        Debug.Log("Accelerating!");
        anim.applyRootMotion = false;
        rigidbody.AddForce(rigidbody.transform.forward * vaultAccelerateForce, ForceMode.Impulse);
        rigidbody.AddForce(rigidbody.transform.up * vaultAccelerateForce, ForceMode.Impulse);
    }

    //Pull the character down during animations
    void pullDown(){
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

}
