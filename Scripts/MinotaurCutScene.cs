using UnityEngine;
using System.Collections;

public class MinotaurCutScene : MonoBehaviour {

	private bool talking;
	public bool moving;
	private bool inTrigger;

	public string[] Dialog;
	public Texture2D BobbyImage;
	public Texture2D OtherImage;
	public Texture2D background;

	private string sentence;
	public int num;
	private Rect display_position_a;
	private Rect display_position_b;
	private Rect display_box;
	private GUIStyle myStyle;

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

		inTrigger = false;
		talking = false;
		moving = false;
		num = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
		if(inTrigger)
		{
			GameObject.Find("Bobby").GetComponent<SpriteController>().player_controlled = false;
			talking = true;
		}

		if(talking)
		{
			if(Input.GetMouseButtonDown(0))
			{
				num++;
				if(num>=Dialog.Length)
				{
				//end dialog
					talking = false;
					//Destroy(gameObject);
					// make head minotaur disappear
					//GetComponentInChildren
					moving = true;
				}

			}
		}
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.name == "Bobby")
			inTrigger = true;
	}

	void OnGUI()
	{
		if (talking && num<Dialog.Length)
		{
			sentence = Dialog[num];
			if(sentence.Contains("Bobby:"))
			{
				GUI.DrawTexture(display_position_a, BobbyImage);
			}
			else
			{
				GUI.DrawTexture(display_position_b, OtherImage);
			}
			GUI.Label(display_box, sentence, myStyle);		
		}
	}
}
