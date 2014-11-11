using UnityEngine;
using System.Collections;

public class Energy : MonoBehaviour {
    public float energy = 0.0f;    
    public PlayerHUD hud;

	// Use this for initialization
	void Start () {
        if (energy == 0)
        {
            Debug.Log("Set energy object energy to be greater than 1");
            this.enabled = false;
        }
	}
	
	// Update is called once per frame
	void Update () {
		hud.setEnergy(energy);

        if (energy == 0)
        {
            // disable certain movements here
        }
	}

    public void modifyEnergy(float points)
    {
        energy += points;
    }

	public float getEnergy()
	{
		return energy;
	}
}
