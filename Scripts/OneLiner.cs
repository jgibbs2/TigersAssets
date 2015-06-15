using UnityEngine;
using System.Collections;

public class OneLiner : MonoBehaviour 
{

	public Texture2D MattyImage;
	public Texture2D OtherImage;
	public Texture2D characterImage;
	public Texture2D background;


	public string[] first_strings;
	public string[] second_strings;
	public int[] state_change_numbers;

	public bool change_state;
	public int what_state_do_we_change;

	private GUIStyle myStyle;
	private GUIStyle yourStyle;

	private int num = 0;
	private bool inTrigger = false;
	private string first;
	private string second;

	private bool speech = false;
	// Use this for initialization
	void Start () 
	{
		first = first_strings[0];
		second = second_strings [0];

		myStyle = new GUIStyle();
		myStyle.fontSize = 30;
		myStyle.normal.textColor = Color.white;
		myStyle.fontStyle = FontStyle.Bold;
		myStyle.normal.background = background;

		yourStyle = new GUIStyle();
		yourStyle.fontSize = 30;
		yourStyle.normal.textColor = Color.red;
		//yourStyle.fontStyle = FontStyle.Bold;  
		yourStyle.normal.background = background;
	}
	
	// Update is called once per frame 
	void Update () 
	{
		if (inTrigger && (GameObject.Find("Bobby").GetComponent<SpriteController>().xButtonPressed || Input.GetKeyDown(KeyCode.Space)))
		{
			speech = true;
			GameObject.Find("Bobby").GetComponent<SpriteController>().player_controlled = false;
			GetComponentInParent<NPCMovement>().talking = true;
		}

		/*if(Input.GetKeyDown(KeyCode.Space))
			speech = !speech;*/

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

				if(change_state == true && GameObject.Find("OWorld").GetComponent<OWorld>().state == what_state_do_we_change)
				{
					GameObject.Find("OWorld").GetComponent<OWorld>().state++;
				}
				//Then reset so we can talk the the NPC again
				num = 0;
			}
		}
	}

	void OnGUI()
	{
		if(speech)
		{
			int temp = GameObject.Find("OWorld").GetComponent<OWorld>().state;
			if(temp == state_change_numbers[0])
			{
				first = first_strings[0];
				second = second_strings[0];
			}
			else if(temp == state_change_numbers[1])
			{
				first = first_strings[1];
				second = second_strings[1];
			}
			else{
				first = first_strings[2];
				second = second_strings[2];
			}


			if(num == 0)
			{
				//display the first text item
				if(first.Contains("Bobby:"))
				{
					GUI.DrawTexture(new Rect(450, 100, 300, 300), characterImage);
				}
				else{
					GUI.DrawTexture(new Rect(100, 100, 300, 300), OtherImage);
				}
				GUI.Label(new Rect(0,700,Screen.width,Screen.height),first, myStyle);
			}
			else{
				if(second.Contains("Bobby:"))
				{
					GUI.DrawTexture(new Rect(450, 100, 300, 300), characterImage);
				}
				else{
					GUI.DrawTexture(new Rect(100, 100, 300, 300), OtherImage);
				}
				GUI.Label(new Rect(0,700,Screen.width,Screen.height),second, myStyle);
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
