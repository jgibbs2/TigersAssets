using UnityEngine;
using System.Collections;

public class NPCDialogScript : MonoBehaviour
{

	public string[] answerButtons;
	public string[] Questions;
	bool DisplayDialog = false;
	bool ActivateQuest = false;
	public bool inTrigger = false;

	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
	  if (inTrigger && (GameObject.Find("Bobby").GetComponent<SpriteController>().xButtonPressed || Input.GetKeyDown(KeyCode.Space))) 
	    DisplayDialog = true;
	}

	void OnGUI()
	{
	  GUILayout.BeginArea(new Rect(310, 0, 400, 400));

	  if(DisplayDialog && !ActivateQuest)
	  {
	    GUILayout.Label(Questions[0]);
	    //GUILayout.Label(Questions[1]);

	    if (GUILayout.Button(answerButtons[0]))
	    {
	      ActivateQuest = true;
		  DisplayDialog = false;
	    }

	    /*if (GUILayout.Button(answerButtons[1]))
	      DisplayDialog = false;*/
      }

	  if (DisplayDialog && ActivateQuest)
	  {
	    GUILayout.Label(Questions[2]);

		if (GUILayout.Button(answerButtons[2]))
		  DisplayDialog = false;
	  }

	  GUILayout.EndArea();
	}

	void OnTriggerEnter2D(Collider2D col)
	{
	  inTrigger = true;
	}

	void OnTriggerExit2D(Collider2D col)
	{
		inTrigger = false;
		DisplayDialog = false;
	    GameObject.Find("Bobby").GetComponent<SpriteController>().xButtonPressed = false;
	}
}
