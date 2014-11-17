using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {
    public float health = 0.0f;
    public bool isPlayer = false;

	public PlayerHUD hud;

	// Use this for initialization
	void Start () {
        if (health == 0)
        {
            Debug.Log("Set health object health to be greater than 0");
            this.enabled = false;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (isPlayer)
        {

            // Update HUD Health
			hud.setHealth(health);
        }

        if (health == 0)
        {
            Destroy(gameObject);
        }
	}

    public void modifyHealth(float points)
    {
        health += points;
    }

	public float getHealth()
	{
		return health;
	}
}
