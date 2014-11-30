using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour {
    GameObject crossHair;
	public Score scoreSystem;

    private GameObject derik;
    private Sounds derikSounds;
    private GameObject muzzleFlash;

	// Use this for initialization
	void Start () {
        muzzleFlash = transform.FindChild("Pistol").FindChild("MuzzleFlash").gameObject;
        muzzleFlash.renderer.enabled = false;
        if (!Camera.main)
        {
            Debug.Log("Gun object needs main camera");
        }
        derik = GameObject.Find("Derik");
        if (derik)
        {
            derikSounds = derik.GetComponent<Sounds>();
        }
        else
        {
            Debug.Log("Gun object cannot find Derik");
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire1"))
        {
            muzzleFlash.renderer.enabled = true;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;


            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.tag == "Zombie")
                {   
                    hit.collider.SendMessage("bulletHit", "gun");
					scoreSystem.addPoints(100);
					scoreSystem.addZombieKill();
                }
            }
        }else
        muzzleFlash.renderer.enabled = false;
	}
}
