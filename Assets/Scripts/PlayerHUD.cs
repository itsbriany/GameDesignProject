using UnityEngine;
using System.Collections;

public class PlayerHUD : MonoBehaviour {
	
	public Texture2D playerIcon;
	public Texture2D[] playerIcons;
	public Texture2D healthBar;
	public Texture2D staminaBar;
	public Texture2D emptyBar;

	private float playerHealth;
	private float playerStamina;

	// Use this for initialization
	void Start () {
		playerIcon = playerIcons[0];
		playerHealth = 50;
		playerStamina = 50;
	}
	
	// Update is called once per frame
	void Update () {
		// Update player health, pic, and stamina
	}

	void OnGUI () {
		GUI.BeginGroup(new Rect(20, 20, Screen.width/5, Screen.height/10));


		GUI.Box(new Rect(0, 0, Screen.width/5, Screen.height/10), "");

		// TODO: Change playerIcon depending on health value
		GUI.DrawTexture (new Rect (5, 5, Screen.height/12, Screen.height/12), playerIcon);

		float healthWidth = Screen.width/8 * (playerHealth/100);
		GUI.DrawTexture (new Rect (80, 10, Screen.width/8, 10), emptyBar);
		GUI.DrawTexture (new Rect (80, 10, healthWidth, 10), healthBar);


		float staminaWidth = Screen.width/8 * (playerStamina/100);
		GUI.DrawTexture (new Rect (80, 30, Screen.width/8, 10), emptyBar);
		GUI.DrawTexture (new Rect (80, 30, staminaWidth, 10), staminaBar);


		GUI.EndGroup();
	}
}
