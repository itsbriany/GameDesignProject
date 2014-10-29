using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    public GameObject player;

	// Use this for initialization
	void Start () {
        if (!player)
        {
            Debug.Log("Game manager object needs a player");
            this.enabled = false;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (!player)
        {
            //do some game over stuff here
        }
	}

    // messages sent by other objects for GUI updates
    void updateGUI(string type)
    {
        switch(type){
            case "score":
                break;
            case "health":
                break;
            case "energy":
                break;
            default:
                Debug.Log("Invalid type for updateGUI");
                this.enabled = false;
                break;
        }
    }
}
