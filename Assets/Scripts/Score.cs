using UnityEngine;
using System.Collections;

	/*
		 * Add points for each objective cleared
		 * Remove points for each death
		 * At EOG, add bonus points for time completion, zombies killed
	 */

public class Score : MonoBehaviour {
    public int userScore = 0;
	public int zombiesKilled = 0;
	public float clearTime = 0.0f;
	public bool displayScore = true;

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(this.gameObject);
		this.gameObject.tag = "WontBeDestroyed";
		userScore = 0;
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnGUI () {
		if (displayScore)
		{
			GUI.Box(new Rect(Screen.width - Screen.width/8, Screen.height/25, Screen.width/10, Screen.height/20), "Score: " + userScore);
		}
	}

	public void addPoints(int points)
	{
		if (userScore + points < 0)
		{
			userScore = 0;
		}
		else
		{
			userScore += points;
		}
	}

	public void addZombieKill()
	{
		zombiesKilled += 1;
	}

	public void setClearTime (float time)
	{
		clearTime = time;
	}
}
