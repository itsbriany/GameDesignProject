using UnityEngine;
using System.Collections;

public class OpeningScript : MonoBehaviour {

	public AudioSource atmosphere;
	
	// Use this for initialization
	void Start () {
		atmosphere.volume = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		if (atmosphere.volume != 1.0f)
		{
			atmosphere.volume += 0.001f;
		}

		if (atmosphere.time > 20.0f)
		{
			atmosphere.PlayScheduled(5.0f);
		}

		if (Input.anyKey)
		{
			Application.LoadLevel ("MainMenu");
		}
	}
}
