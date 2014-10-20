using UnityEngine;
using System.Collections;

public class CrossHair : MonoBehaviour {
    public Texture2D cursorTexture;
    CursorMode cursorMode = CursorMode.Auto;
    Vector2 hotSpot = Vector2.zero;

	// Use this for initialization
	void Start () {
        if (!cursorTexture)
        {
            Debug.Log("Add cursor texture!");
            this.enabled = false;
        }
         Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
	}
}
