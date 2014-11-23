using UnityEngine;
using System;
using System.Collections;

[RequireComponent(typeof(Animator))] 

public class DerikIK : MonoBehaviour {

    protected Animator animator;

    public bool ikActive = false;
    public Transform rightHandObj = null;
    public Transform meleeWeapon = null;

    private Transform rightHand = null;
    private Transform leftHand = null;

    void Start()
    {
        animator = GetComponent<Animator>();
        rightHand = GameObject.Find("RightHandThumb1").transform;
    }

    //a callback for calculating IK
    void OnAnimatorIK()
    {
        Debug.Log("OnAnimatorIK working!");
        if (animator)
        {
            Debug.Log("Animator is true!");
            //if the IK is active, set the position and rotation directly to the goal. 
            if (ikActive)
            {
                Debug.Log("IK active!");
                //weight = 1.0 for the right hand means position and rotation will be at the IK goal (the place the character wants to grab)
                animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1.0f);
                animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1.0f);
                animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1.0f);
                animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1.0f);

                //set the position and the rotation of the right hand where the external object is
                if (rightHandObj != null)
                {
                    animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandObj.position);
                    animator.SetIKRotation(AvatarIKGoal.RightHand, rightHandObj.rotation);
                }
                if (meleeWeapon != null)
                {
                    //Vector3 handleOffset = meleeWeapon.localPosition;
                   // meleeWeapon.position = rightHand.position;
                    meleeWeapon.parent.position = rightHand.position;
                   // meleeWeapon.parent.position += meleeWeapon.localPosition;
                    meleeWeapon.parent.position += new Vector3(0, meleeWeapon.position.y, 0);
                   
                    animator.SetIKPosition(AvatarIKGoal.RightHand, meleeWeapon.position);
                    animator.SetIKRotation(AvatarIKGoal.RightHand, meleeWeapon.rotation);
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
}
