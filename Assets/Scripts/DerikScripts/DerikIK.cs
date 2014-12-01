using UnityEngine;
using System;
using System.Collections;

[RequireComponent(typeof(Animator))] 

public class DerikIK : MonoBehaviour {

    protected Animator animator;

    public bool ikActive = false;
    public Transform gunHandle = null;
    

    private Transform rightHand = null;
    private Transform leftHand = null;
    private Transform gun = null;
    private Vector3 difference; //The difference between the gun's original position and the aiming position
    private bool holdAim;
    private AnimatorStateInfo currentBaseState;
    private Transform leftHandle;
    private Transform aimingScreen;
    private Vector3 aimPosition;
    private Quaternion gunRotation;
    

    static int idleState = Animator.StringToHash("Base Layer.Idle");

    void Start()
    {
        animator = GetComponent<Animator>();
        gun = gunHandle.parent;
        leftHandle = gun.FindChild("LeftHandle");
        difference = new Vector3(0.09107905f, -0.244905f, -0.3839359f);
        aimingScreen = transform.FindChild("AimScreen");
        holdAim = true;
    }

    //a callback for calculating IK
    void OnAnimatorIK()
    {
        currentBaseState = animator.GetCurrentAnimatorStateInfo(0);
        //Debug.Log("OnAnimatorIK working!");
        if (animator)
        {
            //Debug.Log("Animator is true!");
            //if the IK is active, set the position and rotation directly to the goal. 
            if (ikActive)
            {
                //Debug.Log("IK active!");
                //weight = 1.0 for the right hand means position and rotation will be at the IK goal (the place the character wants to grab)
                animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1.0f);
                animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1.0f);
                animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1.0f);
                animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1.0f);

                if (gunHandle != null)
                {
                    if (Input.GetKey(KeyCode.Z) && currentBaseState.nameHash == idleState && !animator.IsInTransition(0))
                    {
                        
                        if (holdAim)
                            gun.localPosition -= difference;
                        holdAim = false;
                        
                        //aimPosition = followCursor();
                        //gun.position = new Vector3(aimPosition.x, aimPosition.y, gun.position.z);
                        //gun.rotation = gunRotation;
                        
                        animator.SetIKPosition(AvatarIKGoal.RightHand, gunHandle.position);
                        animator.SetIKRotation(AvatarIKGoal.RightHand, gunHandle.rotation);
                        animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandle.position);
                        animator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandle.rotation);
                    }
                    else 
                    {
                        if (!holdAim)
                            gun.localPosition += difference;
                        animator.SetIKPosition(AvatarIKGoal.RightHand, gunHandle.position);
                        animator.SetIKRotation(AvatarIKGoal.RightHand, gunHandle.rotation);
                        holdAim = true;
                    }
                    
                    
                }
                
            }
            //if the IK is not active, set the position and rotation of the hand back to the original position
            else
            {
                animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
                animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);
                animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0);
                animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0);
            }
        }   
    }

    /*Allows inverse kinematics to follow the cursor on a given plane*/
    Vector3 followCursor()
    {
        Vector3 point = gun.position;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //Debug.DrawLine(Input.mousePosition, aimingScreen.position);
        Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);
        RaycastHit hitInfo = new RaycastHit();
        if (Physics.Raycast(ray, out hitInfo))
        {
            Debug.Log("Hit infot: " + hitInfo.transform.FindChild("AimScreen"));
            if (hitInfo.transform.FindChild("AimScreen") == aimingScreen)
            {
                point = hitInfo.point;
                Debug.Log("Hit!");
                return point;
            }
            
        }
        return point;
    }

}
