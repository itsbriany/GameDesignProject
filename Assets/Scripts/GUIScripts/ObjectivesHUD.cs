using UnityEngine;
using System.Collections;

public class ObjectivesHUD : MonoBehaviour {

	public Texture2D objectiveSquare;
	public Texture2D objectivePointer;

	// Temp
	public GameObject currentObjective;
	public bool onScreen;
	public Vector3 currentScreenPos;
	public float pointerAngle;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		// Update current objective here...
		// GameObject currentObjective = new current objective
		isTargetOnScreen(currentObjective);
	}

	void isTargetOnScreen(GameObject obj) {

		Vector3 screenPos = Camera.main.WorldToScreenPoint(obj.transform.position);
		//Vector3 screenPos2 = Camera.main.ViewportToScreenPoint(obj.transform.position);

		if(screenPos.z > 0 &&
		   screenPos.x > 0 && screenPos.x < Screen.width &&
		   screenPos.y > 0 && screenPos.y < Screen.height)
		{
			//the target is on-screen, use an overlay, or do nothing.
			onScreen = true;
			currentScreenPos = screenPos;
		}
		else
		{
			//the target is off-screen, find indicator position.
			Vector3 screenCenter = new Vector3(Screen.width, Screen.height, 0)/2;

			// Make 00 center of the screen instead of bottom left.
			screenPos -= screenCenter;

			float angle = Mathf.Atan2(screenPos.y, screenPos.x);
			if (screenPos.z < 0)
			{
				angle = Mathf.Atan2(-screenPos.y, -screenPos.x);
			}
			angle -= 90 * Mathf.Deg2Rad;


			float cos = Mathf.Cos(angle);
			float sin = -Mathf.Sin(angle);

			screenPos = screenCenter + new Vector3(sin*150, cos*150, 0);

			//y = mx+b format
			float m = cos / sin;

			Vector3 screenBounds = screenCenter * 0.9f;

			// Check up and down first
			if (cos > 0)
			{
				screenPos = new Vector3 (screenBounds.y/m, screenBounds.y, 0);
			} else {
				screenPos = new Vector3 (-screenBounds.y/m, -screenBounds.y, 0);
			}

			// If out of bounds, get point on appropriate side
			if (screenPos.x > screenBounds.x) // Out of bounds on the right
			{
				screenPos = new Vector3 (screenBounds.x, screenBounds.x*m, 0);
			} 
			else if (screenPos.x < -screenBounds.x) // Out of bounds on the left
			{
				screenPos = new Vector3 (-screenBounds.x, -screenBounds.x*m, 0);
			}
			// Otherwise, it is in bounds


			// Remove coordinate translation
			screenPos += screenCenter;

			onScreen = false;
			currentScreenPos = screenPos;
			pointerAngle = -angle * Mathf.Rad2Deg;
		}
	}

	void OnGUI()
	{
		if (onScreen)
		{
			GUI.DrawTexture(new Rect(currentScreenPos.x, Screen.height - currentScreenPos.y, 10, 10), objectiveSquare);
		}
		else
		{
			GUIUtility.RotateAroundPivot (pointerAngle, new Vector2(currentScreenPos.x, Screen.height - currentScreenPos.y));
			GUI.DrawTexture(new Rect(currentScreenPos.x, Screen.height - currentScreenPos.y, 10, 10), objectivePointer);
		}
	}
}
