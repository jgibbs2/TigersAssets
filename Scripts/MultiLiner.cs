﻿using UnityEngine;
using System.Collections;

public class MultiLiner : MonoBehaviour {

	public string[] Questions;
	public Texture2D BobbyImage;
	public Texture2D OtherImage;
	public Texture2D background;

	public string dialogueType; //partyJoin, storyAdvance
	public int state_to_change;
	public string quest_to_give;

	/*
	 * possibly add in other characters?
	 * 
	 */

	private GUIStyle myStyle;
	private int num;
	private bool talking;
	private string sentence;
	private bool inTrigger;

	private Rect display_position_a;
	private Rect display_position_b;
	private Rect display_box;

	
	// Use this for initialization
	void Start () {

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


		num = 0;
		talking = false;

		myStyle.normal.textColor = Color.white;
		myStyle.fontStyle = FontStyle.Bold;
		myStyle.normal.background = background;
	}
	
	// Update is called once per frame
	void Update () 
	{  

		if(inTrigger && (GameObject.Find("Bobby").GetComponent<SpriteController>().xButtonPressed == true || Input.GetKeyDown(KeyCode.Space)))
		{
			GameObject.Find ("Bobby").GetComponent<SpriteController> ().player_controlled = false;
			gameObject.GetComponentInParent<NPCMovement>().talking = true;
			talking = true;
		}


		if(Input.GetMouseButtonDown(0) && talking) 
		{
			//We'll increment num to go to the next thing in the list
			num++;
			if(num == Questions.Length)
			{


				num = 0;
				GameObject.Find ("Bobby").GetComponent<SpriteController> ().player_controlled = true;
				talking = false;
				if(dialogueType == "partyJoin")
				{
					int characterNumber;
					if(transform.parent.name == "Matty")
					{
						characterNumber = 0;
					}
					else{
						characterNumber = 1;
					}

					//GameObject.Find ("GameData").GetComponent<GameData>().characters[characterNumber] = true;
					Destroy(transform.parent.gameObject);
					GameObject.Find("OWorld").GetComponent<OWorld>().state++;
				}
				else if(dialogueType == "storyAdvance")
				{
					GameObject.Find("OWorld").GetComponent<OWorld>().state++;
					gameObject.GetComponent<OneLiner>().enabled = true;
					gameObject.GetComponent<MultiLiner>().enabled = false;
				}
				else if(dialogueType == "questGive")
				{
					gameObject.GetComponent<OneLiner>().enabled = true;
					gameObject.GetComponent<MultiLiner>().enabled = false;
				}
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
	
	void OnGUI()
	{
		if (talking)
		{
			sentence = Questions[num];
			if(sentence.Contains("Bobby:"))
			{
				GUI.DrawTexture(display_position_a, BobbyImage);
			}
			else
			{
				GUI.DrawTexture(display_position_b, OtherImage);
			}
			GUI.Label(display_box, sentence, myStyle);
			
			//If we're in dialogue and the player taps the screen to advance the dialogue
			
		}
	}
}