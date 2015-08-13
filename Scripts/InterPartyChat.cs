using UnityEngine;
using System.Collections;

public class InterPartyChat : MonoBehaviour {

	private GUIStyle myStyle;
	public string[] Questions;
	private int num;
	private bool talking;
	private string sentence;
	public Texture2D BobbyImage;
	public Texture2D OtherImage;
	public Texture2D background;

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
	  if(Input.GetMouseButtonDown(0) && talking) 
	  {
	    //We'll increment num to go to the next thing in the list
		num++;
		if(num == Questions.Length)
		{
		  Destroy(gameObject);
		  GameObject.Find ("Bobby").GetComponent<SpriteController> ().player_controlled = true;
		  talking = false;
		}
	  }
	}  

	void OnTriggerEnter2D(Collider2D col)
	{
		GameObject.Find ("Bobby").GetComponent<SpriteController> ().player_controlled = false;
		talking = true;
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
