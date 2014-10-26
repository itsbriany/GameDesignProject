using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour {
    GameObject crossHair;

	// Use this for initialization
	void Start () {
        crossHair = GameObject.Find("crossHair");
        if (!crossHair)
        {
            Debug.Log("Gun object needs crosshair");
            this.enabled = false;
        }

        if (!Camera.main)
        {
            Debug.Log("Gun object needs main camera");
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire1"))
        {
            Ray ray = Camera.main.ScreenPointToRay(Camera.main.WorldToScreenPoint(crossHair.transform.position));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.tag == "Zombie")
                {
                    hit.collider.SendMessage("bulletHit", "type");
                }
            }
        }
	}
}
