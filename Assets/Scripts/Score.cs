using UnityEngine;
using System.Collections;

	/*
		 * Add points for each objective cleared
		 * Remove points for each death
		 * At EOG, add bonus points for time completion, zombies killed
	 */

public class Score : MonoBehaviour {
    int score = 0;

	// Use this for initialization
	void Start () {
		score = 0;
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnGUI () {
		GUI.Box(new Rect(Screen.width - Screen.width/8, Screen.height/25, Screen.width/10, Screen.height/20), "Score: " + score);
	}

	public void addPoints(int points)
	{
		if (score + points < 0)
		{
			score = 0;
		}
		else
		{
			score += points;
		}
	}
}
