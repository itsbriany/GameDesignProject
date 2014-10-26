using UnityEngine;
using System.Collections;

public class Energy : MonoBehaviour {
    public int energy = 0;    
    public GameObject gameManager;

	// Use this for initialization
	void Start () {
        if (energy == 0)
        {
            Debug.Log("Set energy object energy to be greater than 1");
            this.enabled = false;
        }

        if (!gameManager)
        {
            Debug.Log("Add game manager object to the energy object");
            this.enabled = false;
        }
	}
	
	// Update is called once per frame
	void Update () {
        gameManager.SendMessage("updateGUI", gameObject.tag);

        if (energy == 0)
        {
            // disable certain movements here
        }
	}

    void modifyEnergy(int points)
    {
        energy += points;
    }
}
