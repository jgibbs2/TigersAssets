using UnityEngine;
using System.Collections;

public class NPCDialogScript : MonoBehaviour
{
	private GUIStyle myStyle;
	private GUIStyle yourStyle;
	private bool EnteredTriggerForFirstTime;
	private bool ActivateQuest;
	//private bool[] questCompleted;
	private bool questCompleted;
	private static int numTimesEntered;

	public string[] Questions;
	public string[] answerButtons;
	public bool[] DisplayDialog;
	public bool inTrigger;
	//public int questNum;
	public Texture2D texture;

	// Use this for initialization
	void Start () 
	{
	  myStyle = new GUIStyle();
	  yourStyle = new GUIStyle();

	  for (int i = 0; i < DisplayDialog.Length; i++)
	    DisplayDialog[i] = false; 

	  EnteredTriggerForFirstTime = false;
	  ActivateQuest = false;
	  inTrigger = false;
	  questCompleted = false;
	}
	
	// Update is called once per frame 
	void Update () 
	{
	  if (inTrigger && (GameObject.Find("Bobby").GetComponent<SpriteController>().xButtonPressed || Input.GetKeyDown(KeyCode.Space)))
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
	    GUILayout.BeginArea(new Rect(0, 0, 2000, 400)); //Computer
		//GUILayout.BeginArea(new Rect(0, 900, 2000, 400));  //Phone

		myStyle.fontSize = 80;
		myStyle.normal.textColor = Color.red;
		myStyle.fontStyle = FontStyle.Bold;
		myStyle.normal.background = texture; 
		
		yourStyle.fontSize = 80;
		yourStyle.normal.textColor = Color.white; 
		yourStyle.normal.background = texture;

		//For loop runs through the characters' lines
      for (int i = 0; i < Questions.Length - 1; i++)
	  {

	    if(DisplayDialog[i] && !ActivateQuest)
	    {
	      GUILayout.Label(Questions[i], myStyle);

		  if (GUILayout.Button(answerButtons[i], yourStyle))
	      {
				//When quest starts
			if (i == Questions.Length - 2)
			{
			  Debug.Log("The quest has started!");
			  GameObject.Find("GameData").GetComponent<GameData>().appleQuest = true;
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
	    GUILayout.Label(Questions[Questions.Length - 1], myStyle);

		if (GUILayout.Button(answerButtons[answerButtons.Length - 1], yourStyle))
		{
          for (int i = 0; i < DisplayDialog.Length; i++)
		    DisplayDialog[i] = false;

		  ActivateQuest = false;
		  GameObject.Find("Bobby").GetComponent<SpriteController>().player_controlled = true;
		  questCompleted = true; // Quest is resolved here (To be scaled via an array once we have multiple quests)
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
