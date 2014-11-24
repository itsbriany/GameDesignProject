using UnityEngine;
using System.Collections;

public class BulletHit : MonoBehaviour {
    public int damage = 1;

	// Use this for initialization
	void Start () {
	
	}
	
    void bulletHit(string type)
    {
        // different damage for different bullet types?
        if (type == "gun")
        {
            gameObject.SendMessage("modifyHealth", -damage);
        }
    }
}
