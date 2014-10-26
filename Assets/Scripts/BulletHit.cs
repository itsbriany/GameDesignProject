using UnityEngine;
using System.Collections;

public class BulletHit : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
    void bulletHit(string type)
    {
        // different damage for different bullet types?
        if (type == "type")
        {
            gameObject.SendMessage("modifyHealth", 1);
        }
    }
}
