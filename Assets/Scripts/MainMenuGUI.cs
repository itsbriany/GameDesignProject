using UnityEngine;
using System.Collections;

public class MainMenuGUI : MonoBehaviour {

	private int menuNumber;
	private Vector2 scrollPosition = Vector2.zero;
	private float xpos = ((Screen.width/3)/2)-(Screen.width/3.5f)/2;
	private float buttonWidth = Screen.width/3.5f;
	private float buttonHeight = Screen.height/10;

	public Texture mainMenu;
	public Texture Level1;
	public Texture Level2;
	public Texture Level3;
	public Texture Level4;
	public Texture Options;
	//int ypos = (Screen.height)-(this_element.height)/2;

	// Use this for initialization
	void Start () {
		menuNumber = 1;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI () {

		GUI.BeginGroup(new Rect(Screen.width/10, 0, Screen.width/3, Screen.height));

		switch (menuNumber) 
		{
			case 1:
				DisplayMainMenu();
				break;

			case 2:
				DisplayLevelSelect();
				break;

			case 3:
				DisplayOptionMenu();
				break;
		}

		GUI.EndGroup();
	}

	void DisplayMainMenu() {
		float ypos = (Screen.height)/2 - (Screen.height/3);

		// Make a background box
		GUI.Box(new Rect(0, 0, Screen.width/3, Screen.height), "Main Menu");
		
		// Continue game from the last played level
		// TODO: Change text depending on whether there is any user progress or not
		string button1Text = "Continue";
		/* if (userProgress) {
		 *     button1Text = "Continue";
		 * } else {
		 * 	   button1Text = "Start Game";
		 * }
		 */

		if(GUI.Button(new Rect(xpos, ypos, buttonWidth, buttonHeight), button1Text)) {
			// Load Game from last level played
		}
		ypos += (Screen.height/6);

		// Select a level to play
		if(GUI.Button(new Rect(xpos, ypos, buttonWidth, buttonHeight), "Level Select")) {
			// Transition to the level select screen
			menuNumber = 2;
		}
		ypos += (Screen.height/6);

		// Change game options
		if(GUI.Button(new Rect(xpos, ypos, buttonWidth, buttonHeight), "Options")) {
			// Transition to the options screen
			menuNumber = 3;
		}
		ypos += (Screen.height/6);

		// Exit Game
		if(GUI.Button(new Rect(xpos, ypos, buttonWidth, buttonHeight), "Quit")) {
			// Save data then exit the game
			// NOTE: Application.Quit is ignored when called from the editor.
			Application.Quit();
		}

		this.renderer.material.mainTexture = mainMenu;
	}

	void DisplayLevelSelect() {
		string hover;
		float ypos = (Screen.height)/2 - (Screen.height/3);
		float scrollYPos = 0; //(Screen.height/4)/2 - Screen.height/3);

		// Make a background box
		GUI.Box(new Rect(0, 0, Screen.width/3, Screen.height), "Level Select");

		scrollPosition = GUI.BeginScrollView(new Rect(xpos, ypos, Screen.width/3.5f, Screen.height/2), scrollPosition, new Rect(0, 0, Screen.width/3.5f, (buttonHeight*4) + (Screen.height/4)), GUIStyle.none, new GUIStyle(GUI.skin.verticalScrollbar));
	
		if (GUI.Button(new Rect(0, scrollYPos, buttonWidth, buttonHeight), new GUIContent("Level 1", "Level 1"))) {
			// Load level 1
		}
		scrollYPos += (Screen.height/6);

		if (GUI.Button(new Rect(0, scrollYPos, buttonWidth, buttonHeight), new GUIContent("Level 2", "Level 2"))) {
			// Load level 2
		}
		scrollYPos += (Screen.height/6);

		if (GUI.Button(new Rect(0, scrollYPos, buttonWidth, buttonHeight), new GUIContent("Level 3", "Level 3"))) {
			// Load Level 3
		}
		scrollYPos += (Screen.height/6);

		if (GUI.Button(new Rect(0, scrollYPos, buttonWidth, buttonHeight), new GUIContent("Level 4", "Level 4"))) {
			// Load Level 4
		}

		hover = GUI.tooltip;
		if (hover == "Level 1") {
			this.renderer.material.mainTexture = Level1;
		} else if (hover == "Level 2") {
			this.renderer.material.mainTexture = Level2;
		} else if (hover == "Level 3") {
			this.renderer.material.mainTexture = Level3;
		} else if (hover == "Level 4") {
			this.renderer.material.mainTexture = Level4;
		}


		GUI.EndScrollView();
		
		// Main Menu
		if(GUI.Button(new Rect(xpos, ypos + (Screen.height/2) + (Screen.height/10), buttonWidth, buttonHeight), "Back")) {
			// Go back to the main menu
			menuNumber = 1;
		}
	}

	void DisplayOptionMenu() {
		float ypos = (Screen.height)/2 - (Screen.height/3);

		// Make a background box
		GUI.Box(new Rect(0, 0, Screen.width/3, Screen.height), "Main Menu");

		// Option1
		if(GUI.Button(new Rect(xpos, ypos, buttonWidth, buttonHeight), "Option1")) {
			// Option1
		}
		ypos += (Screen.height/6);
		
		// Option2
		if(GUI.Button(new Rect(xpos, ypos, buttonWidth, buttonHeight), "Option2")) {
			// Option2
		}
		ypos += (Screen.height/6);
		
		// Option3
		if(GUI.Button(new Rect(xpos, ypos, buttonWidth, buttonHeight), "Option3")) {
			// Option3
		}
		ypos += (Screen.height/6);
		
		// Main Menu
		if(GUI.Button(new Rect(xpos, ypos, buttonWidth, buttonHeight), "Back")) {
			// Go back to the main menu
			menuNumber = 1;
		}
	}
}
