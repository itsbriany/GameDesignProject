using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour {
    GameObject crossHair;

	// Use this for initialization
	void Start () {
        if (!Camera.main)
        {
            Debug.Log("Gun object needs main camera");
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire1"))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
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
