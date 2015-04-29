using UnityEngine;
using System.Collections;

public class NPCDialogScript : MonoBehaviour
{

	public string[] answerButtons;
	public string[] Questions;
	public bool[] DisplayDialog;
	private bool ActivateQuest = false;
	public bool inTrigger = false;
	private bool EnteredTriggerForFirstTime = false;
	private static int numTimesEntered;

	// Use this for initialization
	void Start () 
	{
	  for (int i = 0; i < DisplayDialog.Length; i++)
			DisplayDialog[i] = false;		
	}
	
	// Update is called once per frame
	void Update () 
	{
	  if (GameObject.Find("Bobby").GetComponent<SpriteController>().xButtonPressed || Input.GetKeyDown(KeyCode.Space))
	  {
	    if(EnteredTriggerForFirstTime && !ActivateQuest) 
		{
		  DisplayDialog[0] = true;
		  GameObject.Find("Bobby").GetComponent<SpriteController>().player_controlled = false;
		  EnteredTriggerForFirstTime = false;
		}

	    if(ActivateQuest)
		{
			if (GameData.access.checkInventoryFor(GameData.access.nameOf(Item.Apple)))
			{
				GameData.access.turnIn(Item.Apple);
				
				// Goes to the quest done dialog
		  		DisplayDialog[DisplayDialog.Length - 1] = true;
		 	 	GameObject.Find("Bobby").GetComponent<SpriteController>().player_controlled = false;
			}
		} 
	  }	
	}

	//Updates every frame
	void OnGUI()
	{
		//Creates text box
	  GUILayout.BeginArea(new Rect(310, 0, 400, 400));

		//For loop runs through the characters' lines
      for (int i = 0; i < Questions.Length - 1; i++)
	  {
	    if(DisplayDialog[i] && !ActivateQuest)
	    {
	      GUILayout.Label(Questions[i]);

		  if (GUILayout.Button(answerButtons[i]))
	      {
				//When quest starts
			if (i == Questions.Length - 2)
			{
						Debug.Log("The quest has started!");
			  // Put bool to activate items
			  DisplayDialog[i] = false;
			  ActivateQuest = true;
			  GameObject.Find("Bobby").GetComponent<SpriteController>().xButtonPressed = false;
			  GameObject.Find("Bobby").GetComponent<SpriteController>().player_controlled = true;
			}

			else
			{
			  DisplayDialog[i] = false;
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
		  GameObject.Find("Bobby").GetComponent<SpriteController>().player_controlled = true;
	    }
	  }

	  GUILayout.EndArea();
	}

	void OnTriggerEnter2D(Collider2D col)
	{
	  inTrigger = true;

	  if(numTimesEntered == 0)
	    EnteredTriggerForFirstTime = true;

	  else
		EnteredTriggerForFirstTime = false;

	  numTimesEntered++;
	}

	void OnTriggerExit2D(Collider2D col)
	{
		inTrigger = false; 
	    GameObject.Find("Bobby").GetComponent<SpriteController>().xButtonPressed = false;
	}
}
