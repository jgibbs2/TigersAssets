using UnityEngine;
using System.Collections;

public class MultiLiner : MonoBehaviour {

	public string[] Questions;
	public Texture2D BobbyImage;
	public Texture2D OtherImage;
	public Texture2D background;
	public string dialogueType;

	/*
	 * possibly add in other characters?
	 * 
	 */

	private GUIStyle myStyle;
	private int num;
	private bool talking;
	private string sentence;
	private bool inTrigger;

	
	// Use this for initialization
	void Start () {
		num = 0;
		talking = false;
		myStyle = new GUIStyle();
		myStyle.fontSize = 30;
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
						Debug.Log("Nailed it");
					}
					else{
						characterNumber = 1;
					}

					//GameObject.Find ("GameData").GetComponent<GameData>().characters[characterNumber] = true;
					Destroy(transform.parent.gameObject);
					GameObject.Find("OWorld").GetComponent<OWorld>().state++;
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
				GUI.DrawTexture(new Rect(450, 100, 300, 300), BobbyImage);
			}
			else
			{
				GUI.DrawTexture(new Rect(100, 100, 300, 300), OtherImage);
			}
			GUI.Label(new Rect(0,350,Screen.width,Screen.height), sentence, myStyle);
			
			//If we're in dialogue and the player taps the screen to advance the dialogue
			
		}
	}
}
