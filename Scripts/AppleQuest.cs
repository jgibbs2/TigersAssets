using UnityEngine;
using System.Collections;

public class AppleQuest : MonoBehaviour {
	private bool complete;
	
	public Texture2D MattyImage;
	public Texture2D OtherImage;
	public Texture2D BobbyImage;
	public Texture2D background;
	public string[] first_strings;
	public string[] second_strings;
	public GameObject Apple;
	
	private Rect display_position_a;
	private Rect display_position_b;
	private Rect display_box;
	private GUIStyle myStyle;
	private int num = 0;
	private bool inTrigger = false;
	private string first;
	private string second;
	private bool speech = false;
	
	// Use this for initialization
	void Start () {
		GetComponent<QuestGiver>().enabled = false;
		
		first = first_strings[0];
		second = second_strings [0];
		
		
		myStyle = new GUIStyle();
		if(Application.platform == RuntimePlatform.Android)
		{
			display_position_a = new Rect(1300, 370, 400, 400);
			display_position_b = new Rect(200, 400, 400, 400);
			display_box = new Rect(0,750,Screen.width,Screen.height);
			myStyle.fontSize = 64;
		}
		else
		{
			display_position_a = new Rect(450, 100, 300, 300);
			display_position_b = new Rect(100, 100, 300, 300);
			display_box = new Rect(0,350,Screen.width,Screen.height);
			myStyle.fontSize = 30;
		}
		
		myStyle.normal.textColor = Color.white;
		myStyle.fontStyle = FontStyle.Bold;
		myStyle.normal.background = background;
		
		//make apples here!
	}
	
	// Update is called once per frame
	void Update () {
		if (inTrigger && (GameObject.Find("Bobby").GetComponent<SpriteController>().xButtonPressed || Input.GetKeyDown(KeyCode.Space)))
		{
			speech = true;
			GameObject.Find("Bobby").GetComponent<SpriteController>().player_controlled = false;
			GetComponentInParent<NPCMovement>().talking = true;
		}
		
		if(speech && Input.GetMouseButtonDown(0))
		{
			if(num == 0)
			{
				num++;
			}
			else{
				//if that's the last thing in the dialogue, give control back to bobby and let the NPC move again
				speech = false;
				GameObject.Find("Bobby").GetComponent<SpriteController>().player_controlled = true;
				GetComponentInParent<NPCMovement>().talking = false;
				//Then reset so we can talk the the NPC again
				num = 0;
			}
		}
		//check bobby's inventory for x apples.  If he has them, quest is complete
		/*
		if(GameObject.Find("Boar(Clone)")==false)
		{
			complete = true;
		}*/
		
	}
	
	void OnGUI()
	{
		if(speech)
		{
			if(complete == false)
			{
				first = first_strings[0];
				second = second_strings[0];
			}
			else{
				first = first_strings[1];
				second = second_strings[1];
			}
			
			
			if(num == 0)
			{
				//display the first text item
				if(first.Contains("Bobby:"))
				{
					GUI.DrawTexture(display_position_a, BobbyImage);
				}
				else if(first.Contains("Matty:"))
				{
					GUI.DrawTexture(display_position_a, MattyImage);
				}
				else{
					GUI.DrawTexture(display_position_b, OtherImage);
				}
				GUI.Label(display_box, first, myStyle);
			}
			else{
				if(second.Contains("Bobby:"))
				{
					GUI.DrawTexture(display_position_a, BobbyImage);
				}
				else if(second.Contains("Matty:"))
				{
					GUI.DrawTexture(display_position_a, OtherImage);
				}
				else{
					GUI.DrawTexture(display_position_b, OtherImage);
				}
				GUI.Label(display_box, second, myStyle);
			}
		}
	}
	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.name == "Bobby")
			inTrigger = true;
	}
	
	void OnTriggerExit2D(Collider2D col)
	{
		if (col.gameObject.name == "Bobby")
			inTrigger = false; 
	}
}
