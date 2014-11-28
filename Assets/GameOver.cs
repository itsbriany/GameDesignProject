using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour {

	public AudioSource music;

	// Use this for initialization
	void Start () {
		music.volume = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		if (music.volume != 1.0f)
		{
			music.volume += 0.001f;
		}

		if (Input.anyKey)
		{
			Application.LoadLevel ("MainMenu");
		}
	}
}
