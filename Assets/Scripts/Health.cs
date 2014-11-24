using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {
    public float health = 100.0f;
    public bool isPlayer = false;
	public int scoreAmt = -200;
	private Vector3 spawnPosition;

	public PlayerHUD hud;
	public Score scoresystem;

    private float maxHealth;

	// Use this for initialization
	void Start () {
		spawnPosition = this.gameObject.transform.position;
        maxHealth = health;
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

		if (isPlayer && health == 0)
		{
			killDerik();
		}
        else if (health == 0)
        {
            // zombie? ragdoll?
            Destroy(gameObject);
        }
	}

    public void modifyHealth(float points)
    {
		if (health + points > maxHealth)
		{
			health = maxHealth;
		}
		else if (health + points < 0.0f)
		{
			health = 0.0f;
		}
		else {
			health += points;
		}
    }

	public float getHealth()
	{
		return health;
	}

	void killDerik()
	{
		// play music
		// remove points
		// move Derik back to respawn position and add health back
		this.gameObject.transform.position = spawnPosition;
		health = 100.0f;
		scoresystem.addPoints(scoreAmt);
	}
}
