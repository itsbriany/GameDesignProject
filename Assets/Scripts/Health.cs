using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {
    public float health = 100.0f;
    public bool isPlayer = false;
	public int scoreAmt = -200;

    public Transform ragdoll;

	public PlayerHUD hud;
	public Score scoresystem;

	private Vector3 spawnPosition;

    private float maxHealth;
    private GameObject derik;
    private Sounds derikSounds;

	private GameObject gui;
	private Score score;

	// Use this for initialization
	void Start () {
        maxHealth = health;
        if (health == 0)
        {
            Debug.Log("Set health object health to be greater than 0");
            this.enabled = false;
        }
        derik = GameObject.Find("Derik");
        if (derik)
        {
            derikSounds = derik.GetComponent<Sounds>();
        }
        else
        { 
            Debug.Log("Cannot find Derik");
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
            if(ragdoll){
                Transform rag = Instantiate(ragdoll, transform.position, transform.rotation) as Transform;
                Destroy(gameObject);
            }
            else{
                Destroy(gameObject);
            }
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
        if (derikSounds)
        {
            derikSounds.dyingSound();
        }
		StartCoroutine(gameOver());
	}

	IEnumerator gameOver() {
		yield return new WaitForSeconds(4);
		gui = GameObject.FindGameObjectWithTag("WontBeDestroyed");
		score = gui.GetComponent<Score>();
		score.displayScore = false;
		Application.LoadLevel("GameOver");
	}
}
