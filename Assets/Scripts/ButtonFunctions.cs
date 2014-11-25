using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class ButtonFunctions : MonoBehaviour {

	private int currentLevel;

	public GameObject level2;
	public GameObject level3;

	// Use this for initialization
	void Start () {
		// TODO: Get the last level the user played on and set it to current level
		currentLevel = 1;
	}
	
	// Update is called once per frame
	void Update () {
		if (currentLevel == 1)
		{
			level2.GetComponent<Button>().interactable = false;
			level3.GetComponent<Button>().interactable = false;

		}
		else if (currentLevel == 2)
		{
			level2.GetComponent<Button>().interactable = true;
			level3.GetComponent<Button>().interactable = false;
		}
		else
		{
			level2.GetComponent<Button>().interactable = true;
			level3.GetComponent<Button>().interactable = true;
		}
	}

	public void PressedStartGame()
	{
		Application.LoadLevel("Chapter1");
	}

	public void LoadLevel(int levelNumber)
	{
		switch (levelNumber)
		{
		case 1:
			Application.LoadLevel("Chapter1");
			break;
		case 2:
			// AND SO ON...
			break;
		case 3:
			// And so on...
			break;
		}
	}

	public void ModifySFXVolume(float level)
	{
		// Set SFX volume to the float
	}

	public void ModifyBrightness(float level)
	{
		// Set Main Light brightness to the float
	}

	public void Exit()
	{
		// Save data then exit the game
		// NOTE: Application.Quit is ignored when called from the editor.
		Application.Quit();
	}

}
