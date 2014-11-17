using UnityEngine;
using System.Collections;

public class PauseMenuGUI : MonoBehaviour {

	private bool showMenu = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.Escape))
		{
			Time.timeScale = 0.0f;
			showMenu = true;
		}
	}

	void OnGUI () {
		if (showMenu)
		{
			GUI.BeginGroup(new Rect(Screen.width/3, Screen.height/8, Screen.width/3, Screen.height/2));

			GUI.Box(new Rect(0, 0, Screen.width/3, Screen.height/2), "Pause Menu");

			// Resume
			if(GUI.Button(new Rect(Screen.width/20, Screen.height/10, Screen.width/4, Screen.height/10), "Resume")) {
				// Transition to the level select screen
				showMenu = false;
				Time.timeScale = 1.0f;
			}
			
			// Main Menu
			if(GUI.Button(new Rect(Screen.width/20, 3*Screen.height/10, Screen.width/4, Screen.height/10), "Main Menu")) {
				// Save data then exit the game
				Time.timeScale = 1.0f;
				Application.LoadLevel("MainMenu");
			}

			GUI.EndGroup();
		}
	}
}
