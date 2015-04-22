﻿using UnityEngine;
using System.Collections;

public class NPCDialogScript : MonoBehaviour
{

	public string[] answerButtons;
	public string[] Questions;
	public bool[] DisplayDialog;
	bool ActivateQuest = false;
	public bool inTrigger = false;

	// Use this for initialization
	void Start () 
	{
	  for (int i = 0; i < DisplayDialog.Length; i++)
			DisplayDialog[i] = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
	  if (inTrigger && (GameObject.Find("Bobby").GetComponent<SpriteController>().xButtonPressed || Input.GetKeyDown(KeyCode.Space))) 
	  {
	    if (!ActivateQuest)
	      DisplayDialog[0] = true;

		else
		  DisplayDialog[DisplayDialog.Length - 1] = true;
	  }
	}

	void OnGUI()
	{
	  GUILayout.BeginArea(new Rect(310, 0, 400, 400));

      for (int i = 0; i < Questions.Length - 1; i++)
	  {
	    if(DisplayDialog[i] && !ActivateQuest)
	    {
	      GUILayout.Label(Questions[i]);
		
		  if (GUILayout.Button(answerButtons[i]))
	      {
			if (i == Questions.Length - 2)
			{
			  DisplayDialog[i] = false;
			  ActivateQuest = true;
			  GameObject.Find("Bobby").GetComponent<SpriteController>().xButtonPressed = false;
			}

			else
			{
			  DisplayDialog[i]     = false;
			  DisplayDialog[i + 1] = true;
			}
		  } 
		}	  
	  }

	    if (DisplayDialog[DisplayDialog.Length - 1] && ActivateQuest)
	    {
	      GUILayout.Label(Questions[Questions.Length - 1]);

		  if (GUILayout.Button(answerButtons[answerButtons.Length - 1]))
		  {
		    for (int i = 0; i < DisplayDialog.Length; i++)
			  DisplayDialog[i] = false;

			ActivateQuest = false;
		  }
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
		//DisplayDialog = false; 
	    GameObject.Find("Bobby").GetComponent<SpriteController>().xButtonPressed = false;
	}
}
