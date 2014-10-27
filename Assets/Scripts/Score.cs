using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour {
    int score = 0;
    public GameObject gameManager;

	// Use this for initialization
	void Start () {
        if (!gameManager)
        {
            Debug.Log("Add game manager object to the health object");
            this.enabled = false;
        }
	}
	
	// Update is called once per frame
	void Update () {
        gameManager.SendMessage("updateGUI", "score");
	}

    void modifyScore(int points)
    {
        score += points;
    }
}
