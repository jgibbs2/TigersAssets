using UnityEngine;
using System.Collections;

public class NPCDialogScript : MonoBehaviour
{

	public string answerButton;
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
		for (int i = 0; i < Questions.Length - 1; i++)
		{
	      GUILayout.Label(Questions[i]);

	      if (GUILayout.Button(answerButton))
		  {
		    if (i == Questions.Length - 2)
			{
	          ActivateQuest = true;
		      DisplayDialog = false;
			}
	      }
		}

	    /*if (GUILayout.Button(answerButtons[1]))
	      DisplayDialog = false;*/
      }

	  if (DisplayDialog && ActivateQuest)
	  {
	    GUILayout.Label(Questions[Questions.Length - 1]);

		if (GUILayout.Button(answerButton))
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
