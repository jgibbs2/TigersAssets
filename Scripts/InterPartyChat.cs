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
