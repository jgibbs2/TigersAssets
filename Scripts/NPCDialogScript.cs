using UnityEngine;
using System.Collections;

public class NPCDialogScript : MonoBehaviour {

	public string[] answerButtons;
	public string[] Questions;
	bool DisplayDialog = false;
	bool ActivateQuest = false;

	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
	}

	void OnGUI()
	{
	  GUILayout.BeginArea(new Rect(310, 0, 400, 400));

	  if(DisplayDialog && !ActivateQuest)
	  {
	    GUILayout.Label(Questions[0]);
	    GUILayout.Label(Questions[1]);

	    if (GUILayout.Button(answerButtons[0]))
	    {
	      ActivateQuest = true;
		  DisplayDialog = false;
	    }

	    if (GUILayout.Button(answerButtons[1]))
	      DisplayDialog = false;
      }

	  if (DisplayDialog && ActivateQuest)
	  {
	    GUILayout.Label(Questions[2]);

		if (GUILayout.Button(answerButtons[2]))
				DisplayDialog = false;
	  }

	  GUILayout.EndArea();
	}

	void OnTriggerEnter2D()
	{
      DisplayDialog = true;
	}

	void OnTriggerExit2D()
	{
	  DisplayDialog = false;
	}
}
