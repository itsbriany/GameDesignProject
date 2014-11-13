using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof (Animator))]
[RequireComponent(typeof (Rigidbody))]
[RequireComponent(typeof (CapsuleCollider))]

public class DerikControls : MonoBehaviour {

    public float jumpSpeed = 10.0f; 
    public float vaultDistance = 2.0f; //Distance between character and obstable to trigger vault animation
    public float vaultAccelerateForce = 10.0f; //Force at which the character will accelerate at when vaulting
    public float animatorSpeed = 1.0f; //Speed at which the animations will play at
    public float JumpOffRaycastDistance = 3.0f; //Tolernace distance to jump off something
    public float jumpForward = 0.5f; //How much power to put into a jump
    public float rollTolerance = -4.0f; //Tolerance z velocity for rolling
    public float climbRaycastDistanceUp = 4.0f;//Distance betweem character and climbing trigger
    public float climbRaycastDistanceForward = 0; //Distance between the character and the wall to be climbed

    private Animator anim;
    private AnimatorStateInfo currentBaseState;
    private CapsuleCollider col;
    private Transform JumpOffRaycast; 
    private Transform ClimbUpRaycast;
    private bool isGrounded;
    //private Dictionary<int, int> stateDictionary = new Dictionary<int,int>(); //Hash map for animation states

    static int basicJumpState = Animator.StringToHash("Base Layer.DefaultJump");	
    static int vaultState = Animator.StringToHash("Base Layer.Vault");
    static int runState = Animator.StringToHash("Base Layer.RunForward");
    static int idleTurnState = Animator.StringToHash("Base Layer.TurnAround");
    static int idleState = Animator.StringToHash("Base Layer.Idle");
    static int forwardWalkingState = Animator.StringToHash("Base Layer.WalkForward");
    static int backwardWalkingState = Animator.StringToHash("Base Layer.WalkBackwards");
    static int jumpOffState = Animator.StringToHash("Base Layer.JumpOff");
    static int fallingState = Animator.StringToHash("Base Layer.Falling");
    static int climbHighState = Animator.StringToHash("Base Layer.ClimbHigh");

    
	
	//static int waveState = Animator.StringToHash("Layer2.Wave")

	// Use this for initialization
	void Start () {
	    anim = transform.GetComponent<Animator>();
        col = transform.GetComponent<CapsuleCollider>();
        JumpOffRaycast = transform.FindChild("JumpOffRaycast");
        ClimbUpRaycast = transform.FindChild("ClimbUpRaycast");
        isGrounded = true;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if(Input.GetKey(KeyCode.Z)){
            aimMode();
        }
        else{
            normalMode();
        }
	}

    void normalMode(){
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
        }else{
            anim.SetBool("Jump", false);
        }

        if (currentBaseState.nameHash == forwardWalkingState || currentBaseState.nameHash == backwardWalkingState) { 
            if(Input.GetKey(KeyCode.Space))
                anim.speed = 1.5f;
            else
                anim.speed = animatorSpeed;
        }else{
            anim.speed = animatorSpeed;
        }

        if(currentBaseState.nameHash == basicJumpState){
            anim.speed = 1.3f;
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

        if (currentBaseState.nameHash == idleTurnState) {
            if (!anim.IsInTransition(0)) {
                anim.speed = 2.0f;
                if (anim.GetFloat("Direction") == 0) { 
                    
                }
            }else{ 
                anim.speed = animatorSpeed;
            }
        }

        if(currentBaseState.nameHash == idleState){
            resetAnimVariables(); //Reset certain animator variables such as "Climb"
        }

        if(currentBaseState.nameHash == runState){
            resetAnimVariables();
        }
        
        //Animation functionalities
        vault();
        jumpOff();
        falling();
        roll();
        climbHigh();
        grounded();

    }

    void aimMode(){

    }

    //Checks for vaulting obstacles
    void vault(){
        Ray ray = new Ray(transform.position, rigidbody.transform.forward);
        //Debug.DrawRay(transform.position, rigidbody.transform.forward);
		RaycastHit hitInfo = new RaycastHit();	
		if (Physics.Raycast(ray, out hitInfo)){
            if(hitInfo.transform.CompareTag("Obstacle")){
                if (hitInfo.distance <= vaultDistance){
				    anim.SetBool("Vault", true);
			    }    
            }
        }
    }

    //Accelerates the character upon vault
    void vaultAccelerate(){
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

    /*Checks if the character is jumping off something*/
    void jumpOff(){
        Ray ray = new Ray(JumpOffRaycast.position, -rigidbody.transform.up);
       // Debug.DrawRay(JumpOffRaycast.position, -rigidbody.transform.up);
        RaycastHit hitInfo = new RaycastHit();
        if(Physics.Raycast(ray, out hitInfo)){
            if(hitInfo.distance > JumpOffRaycastDistance){
                anim.SetBool("JumpOff", true);
            }else{
                anim.SetBool("JumpOff", false);
            }
        }
        
        if(currentBaseState.nameHash == jumpOffState){ 
            rigidbody.velocity = rigidbody.velocity;
            anim.applyRootMotion = false;
            anim.rigidbody.AddForce(anim.rigidbody.transform.up * jumpForward, ForceMode.Impulse);
            anim.rigidbody.AddForce(anim.rigidbody.transform.forward * jumpForward, ForceMode.Impulse);
        }
    }

    /*Checks if the character is falling*/
    void falling() {
        if(!isGrounded){
            if(!anim.IsInTransition(0)){
                anim.SetBool("Falling", true);
                anim.SetBool("JumpOff", false);
            }
            if(anim.GetBool("Falling") == true){
                anim.applyRootMotion = false;
                rigidbody.velocity = rigidbody.velocity;
            }
        } else { 
            anim.SetBool("Falling", false);
            anim.applyRootMotion = true;
        }
    }

    /*Checks if the character is grounded*/
    void grounded(){
        if(currentBaseState.nameHash != vaultState 
            && currentBaseState.nameHash != basicJumpState
            && currentBaseState.nameHash != climbHighState){
            //Draw a raycast that will detect if the feet are on the ground
            Ray groundRay = new Ray(rigidbody.position + Vector3.up, -Vector3.up);
           // Debug.DrawRay(rigidbody.position + Vector3.up, -Vector3.up);
            RaycastHit hitInfo = new RaycastHit();
            if(Physics.Raycast(groundRay, out hitInfo)){
                if(hitInfo.distance <= 1.5f){
                    isGrounded = true;
                }else{
                    isGrounded = false;
                }
            }
        }
    }

    /*Checks if the character is rolling*/
    void roll(){
        //Debug.Log(transform.InverseTransformDirection(rigidbody.velocity).y);
        if(transform.InverseTransformDirection(rigidbody.velocity).z >= rollTolerance
            && transform.InverseTransformDirection(rigidbody.velocity).y <= -rollTolerance){
            anim.SetBool("Roll", true);
        }else{
            anim.SetBool("Roll", false);
        }
    }

    /*Makes the character climb up the high obstacle*/
    void climbHigh(){
        Ray climbRayUp = new Ray(ClimbUpRaycast.position, ClimbUpRaycast.up);
        Debug.DrawRay(ClimbUpRaycast.position, ClimbUpRaycast.up);
        RaycastHit upHitInfo = new RaycastHit();
        Ray climbRayForward = new Ray(rigidbody.position + Vector3.up, rigidbody.transform.forward);
        Debug.DrawRay(rigidbody.position + Vector3.up, rigidbody.transform.forward);
        RaycastHit forwardHitInfo = new RaycastHit();
        if(Physics.Raycast(climbRayUp, out upHitInfo)){ //Shoot the upwards raycast
            Transform climbTrigger = upHitInfo.transform;
            if(climbTrigger != null){ //Check if the upwards raycast hits the climbing trigger
                Debug.Log("ClimbTrigger detected!");
                if(Physics.Raycast(climbRayForward, out forwardHitInfo)){ //Shoot the forward raycast
                    Transform wallTrigger = forwardHitInfo.transform;
                    if(wallTrigger != null){ //Check if there is a wall in front of the character
                        Debug.Log("Wall detected!");
                        if(upHitInfo.distance <= climbRaycastDistanceUp){ //Check if the trigger is low enough for the character to climb up
                            Debug.Log("ClimbTrigger reached!");
                            if(forwardHitInfo.distance <= climbRaycastDistanceForward){  //Check if the forwards raycast hits the wall
                                Debug.Log("Wall reached!");                                 
                                anim.SetBool("Climb", true);
                                if(currentBaseState.nameHash == climbHighState){
                                    anim.applyRootMotion = false;
                                    rigidbody.useGravity = false;
                                    float footPositionY = transform.FindChild("Feet").position.y;
                                    Debug.Log("Obstacle Height: " + forwardHitInfo.transform.localScale.y);
                                    Debug.Log("Feet position y-cooridnate: " + transform.FindChild("Feet").position.y);
                                    transform.position = new Vector3(transform.position.x, Mathf.Lerp(footPositionY, forwardHitInfo.transform.localScale.y + footPositionY + 10, Time.deltaTime), transform.position.z);
                                    Debug.Log("Root motion disabled!");
                                    
                                }
                            }
                        }
                    }
                }
            }
        }
        /*
        if(Physics.Raycast(climbRayUp, out upHitInfo)){ //Shoot the upwards raycast
            Debug.Log(upHitInfo.transform.FindChild("ClimbTrigger"));
            Transform climbTrigger = upHitInfo.transform.FindChild("ClimbTrigger");
            if(climbTrigger != null){ //Check if the upwards raycast hits the climbing trigger
                Debug.Log("ClimbTrigger detected!");
                if(Physics.Raycast(climbRayForward, out forwardHitInfo)){ //Shoot the forward raycast
                    Transform wallTrigger = forwardHitInfo.transform.FindChild("WallTrigger");
                    if(wallTrigger != null){ //Check if there is a wall in front of the character
                        Debug.Log("Wall detected!");
                        if(upHitInfo.distance <= climbRaycastDistanceUp){ //Check if the trigger is low enough for the character to climb up
                            Debug.Log("ClimbTrigger reached!");
                            if(forwardHitInfo.distance <= climbRaycastDistanceForward){  //Check if the forwards raycast hits the wall
                                Debug.Log("Wall reached!");                                 
                                anim.SetBool("Climb", true);
                                if(currentBaseState.nameHash == climbHighState){
                                    anim.applyRootMotion = false;
                                    rigidbody.useGravity = false;
                                    float footPositionY = transform.FindChild("Feet").position.y;
                                    Debug.Log("Obstacle Height: " + forwardHitInfo.transform.localScale.y);
                                    Debug.Log("Feet position y-cooridnate: " + transform.FindChild("Feet").position.y);
                                    transform.position = new Vector3(transform.position.x, Mathf.Lerp(footPositionY, forwardHitInfo.transform.localScale.y + footPositionY, Time.deltaTime*2), transform.position.z);
                                    Debug.Log("Root motion disabled!");
                                    
                                }
                            }
                        }
                    }
                }
            }
        }*/
    }

    /*Resets the animator variables and applies root motion and gravity*/
    void resetAnimVariables() { 
        anim.SetBool("Climb", false);
        anim.applyRootMotion = true;
        rigidbody.useGravity = true;
    }

}
