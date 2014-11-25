using UnityEngine;
using System.Collections;
//using UnityEngine.UI;

public class ScoreScreen : MonoBehaviour {

	public GameObject currentScoreField;
	public GameObject zombieKillsField;
	public GameObject clearTimeField;
	public GameObject finalScoreField;

	private GameObject gui;
	private Score score;
	private int currentScore;
	private int zombieKills;
	private float clearTime;
	private int finalScore;

	// Use this for initialization
	void Start () {
		gui = GameObject.FindGameObjectWithTag("WontBeDestroyed");
		score = gui.GetComponent<Score>();
		score.displayScore = false;
		currentScore = score.userScore;
		zombieKills = score.zombiesKilled;
		clearTime = score.clearTime;
		SetScoreScreen();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.anyKey)
		{
			Destroy(gui);
			Application.LoadLevel ("MainMenu");
		}
	}

	void SetScoreScreen()
	{
		int zombieKillsBonus = (200 * zombieKills);
		int clearTimePenalty = (int)clearTime * -10;
		int finalScoreSum = currentScore + zombieKillsBonus + clearTimePenalty;

		//currentScoreField.GetComponent<Text>().text = "CURRENT SCORE: " + currentScore;
		//zombieKillsField.GetComponent<Text>().text = "ZOMBIE KILLS: 200 * " + zombieKills + " = " + (200*zombieKills);
		//clearTimeField.GetComponent<Text>().text = "CLEAR TIME: " + clearTimePenalty;
		//finalScoreField.GetComponent<Text>().text = "FINAL SCORE: " + finalScoreSum;
	}


}
