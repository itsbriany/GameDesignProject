using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {
    public int health = 0;    
    public GameObject gameManager;
    public bool isPlayer = false;

	// Use this for initialization
	void Start () {
        if (health == 0)
        {
            Debug.Log("Set health object health to be greater than 0");
            this.enabled = false;
        }

        if (!gameManager)
        {
            Debug.Log("Add game manager object to the health object");
            this.enabled = false;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (isPlayer)
        {
            gameManager.SendMessage("updateGUI", "health");
        }

        if (health == 0)
        {
            Destroy(gameObject);
        }
	}

    void modifyHealth(int points)
    {
        health += points;
    }
}
