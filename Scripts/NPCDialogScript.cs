using UnityEngine;
using System.Collections;

public class NPCDialogScript : MonoBehaviour
{
	private GUIStyle myStyle;
	private GUIStyle yourStyle;

	public string[] Questions;
	public string[] answerButtons;
	public bool[] DisplayDialog;
	public bool inTrigger;
	public int questNum;
	public Texture2D texture;
	public Item questItemReturn;


	// Use this for initialization
	void Start () 
	{
	  myStyle = new GUIStyle();
	  yourStyle = new GUIStyle();

	  for (int i = 0; i < DisplayDialog.Length; i++)
	    DisplayDialog[i] = false; 

	  inTrigger = false;
	}
	
	// Update is called once per frame 
	void Update () 
	{
	  if (inTrigger && (GameObject.Find("Bobby").GetComponent<SpriteController>().xButtonPressed || Input.GetKeyDown(KeyCode.Space)))
	  {
		if(GameData.access.EnteredTriggerForFirstTime[questNum] && !GameData.access.activeQuests[questNum])  
		{
		  DisplayDialog[0] = true;
		  GameObject.Find("Bobby").GetComponent<SpriteController>().player_controlled = false;
		  GameData.access.EnteredTriggerForFirstTime[questNum] = false;
		}

		if(GameData.access.activeQuests[questNum])
		{
				if (GameData.access.checkInventoryFor(GameData.access.nameOf(questItemReturn)))
			{
					GameData.access.turnIn(questItemReturn);
				
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
		myStyle.normal.textColor = Color.white;
		myStyle.fontStyle = FontStyle.Bold;
		myStyle.normal.background = texture; 
		
		yourStyle.fontSize = 80;
		yourStyle.normal.textColor = Color.red; 
		yourStyle.normal.background = texture;

		//For loop runs through the characters' lines
      for (int i = 0; i < Questions.Length - 1; i++)
	  {

		if(DisplayDialog[i] && !GameData.access.activeQuests[questNum])
	    {
	      GUILayout.Label(Questions[i], myStyle);

		  if (GUILayout.Button(answerButtons[i], yourStyle))
	      {
				//When quest starts
			if (i == Questions.Length - 2)
			{
			  Debug.Log("The quest has started!");
			  GameData.access.displayedItems[questNum] = true;
			  // Put bool to activate items
			  DisplayDialog[i] = false;
			  GameData.access.activeQuests[questNum] = true;
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

	  if (DisplayDialog[DisplayDialog.Length - 1] && GameData.access.activeQuests[questNum])
	  {
	    GUILayout.Label(Questions[Questions.Length - 1], myStyle);

		if (GUILayout.Button(answerButtons[answerButtons.Length - 1], yourStyle))
		{
          for (int i = 0; i < DisplayDialog.Length; i++)
		    DisplayDialog[i] = false;

		  GameData.access.activeQuests[questNum] = false;
		  GameObject.Find("Bobby").GetComponent<SpriteController>().player_controlled = true;
	    }
	  }

	  GUILayout.EndArea();
	}

	void OnTriggerEnter2D(Collider2D col)
	{
	  if (col.gameObject.name == "Bobby")
	  {
	    inTrigger = true; 

		if (GameData.access.numTimesEntered[questNum] == 0)
		  GameData.access.EnteredTriggerForFirstTime[questNum] = true;

	    else
		  GameData.access.EnteredTriggerForFirstTime[questNum] = false;

		GameData.access.numTimesEntered[questNum]++;
	  }
	}

	void OnTriggerExit2D(Collider2D col)
	{
		if (col.gameObject.name == "Bobby")
		  inTrigger = false; 
	    GameObject.Find("Bobby").GetComponent<SpriteController>().xButtonPressed = false;
	}
}
