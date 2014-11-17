using UnityEngine;
using System.Collections;

public class ObjectivesHUD : MonoBehaviour {

	public Texture2D objectiveSquare;
	public Texture2D objectivePointer;
	public GameObject derikPosition;

	// Temp
	private GameObject currentObjective;
	private string currentTask;
	private bool displayObjectiveTask = false;
	private int i;
	public GameObject[] objectives;
	public string[] objectiveTask;
	public bool onScreen;
	public Vector3 currentScreenPos;
	public float pointerAngle;

	private string levelCleared = "Congrats! You cleared the level!";

	// Use this for initialization
	void Start () {
		i = 0;
		currentObjective = objectives[i];
		currentTask = objectiveTask[i];
		displayObjectiveTask = true;
	}
	
	// Update is called once per frame
	void Update () {
		// Update current objective here...
		// GameObject currentObjective = new current objective
		isTargetOnScreen(currentObjective);
		if (isObjectiveCleared())
		{
			//Switch to next objective.
			i++;
			if (i < objectives.Length)
			{
				currentObjective = objectives[i];
				currentTask = objectiveTask[i];
				displayObjectiveTask = true;
			}
			if (i == objectives.Length)
			{
				//Last objective cleared. Next level
				currentTask = levelCleared;
				displayObjectiveTask = true;
			}
		}
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
		if (displayObjectiveTask)
		{
			GUI.Box(new Rect(Screen.width/2.5f, Screen.height/3, Screen.width/4, Screen.height/10), currentTask);
			StartCoroutine(closeGUI());
		}

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

	bool isObjectiveCleared()
	{
		float distance = Vector3.Distance(derikPosition.transform.position, currentObjective.transform.position);
		if (distance <= 3)
		{
			return true;
		}
		return false;
	}

	IEnumerator closeGUI() {
		yield return new WaitForSeconds(6);
		displayObjectiveTask = false;
		if (currentTask == levelCleared)
		{
			//Application.LoadLevel("Main Menu");
			Application.LoadLevel("Chapter1");
		}
	}
}
