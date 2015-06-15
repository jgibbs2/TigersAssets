using UnityEngine;
using System.Collections;

public class SpriteController : MonoBehaviour 
{
	public float speed;
	public Texture2D up;
	public Texture2D down;
	public Texture2D left;
	public Texture2D right;
	public Texture2D x;
	public bool xButtonPressed;
	public bool player_controlled = true;
	private Animator animator;
	public string enemies;
	public bool something_up = false;
	public bool something_down = false;
	public bool something_left = false;
	public bool something_right = false;
	private Vector2 touch_one = new Vector2(0, 0);
	private Vector2 touch_two = new Vector2(0, 0);
	private bool wasPressed = false;
	private bool just_spoke;

	// Use this for initialization
	void Start () 
	{
		xButtonPressed = false;
		animator = this.GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update () 
	{
		int nbTouches = Input.touchCount;
		if(nbTouches > 0)
		{

			for (int i = 0; i < nbTouches; i++)
			{

				if(i==0)
				{
					touch_one = Input.GetTouch(0).position;
					touch_one.y = Screen.height - touch_one.y;
				}
				else if(i==1)
				{
					touch_two = Input.GetTouch(1).position;
					touch_two.y = Screen.height - touch_two.y;
				}
				Touch touch = Input.GetTouch(i);
			}

		}
		else
		{
			touch_one = new Vector2(0, 0);
			touch_two = new Vector2(0, 0);
		}

		//float hor = Input.GetAxis ("Horizontal");
		//float ver = Input.GetAxis ("Vertical");
		//Debug.Log ("hor = " + hor + ", ver = " + ver);
		if(player_controlled == true)
		{
			float h = Input.GetAxis("Horizontal") * speed;

			float v = Input.GetAxis("Vertical") * speed;

			//transform.Translate(Vector2.right * h * Time.deltaTime);

			if(h>0)
			{

				if(!something_right)
				{
					transform.Translate(Vector2.right * h * Time.deltaTime);
				}
			}
			else if(h<0)
			{
				if(!something_left)
				{
					transform.Translate(Vector2.right * h * Time.deltaTime);
				}
			}

			else if(v>0)
			{
				if(!something_up)
				{
					transform.Translate(Vector2.up * v * Time.deltaTime);
				}
			}
			else if(v<0)
			{
				if(!something_down)
				{
					transform.Translate(Vector2.up * v * Time.deltaTime);
				}
			}

		}

		if(player_controlled)
		{
			float v = 0;
			float h = 0;
			if (new Rect (150, 575, 200, 200).Contains(touch_one)|| new Rect (150, 575, 200, 200).Contains(touch_two)) {
				if(!something_up)
				{
					v = 1.0f * speed;
					animator.speed = Mathf.Abs(v)/25;
					animator.SetInteger("State", 3);
				}
			}
			
			if (new Rect (150, 875, 200, 200).Contains(touch_one)|| new Rect (150, 875, 200, 200).Contains(touch_two)) {
				if(!something_down)
				{
					v = -1.0f * speed;
					animator.speed = Mathf.Abs(v)/25;
					animator.SetInteger("State", 0);
				}
			}
			
			if (new Rect (0, 725, 200, 200).Contains(touch_one)|| new Rect (0, 725, 200, 200).Contains(touch_two)) {
				if(!something_left)
				{
					h = -1.0f * speed;
					animator.speed = Mathf.Abs(h)/25;
					animator.SetInteger("State", 1);
				}
			}
			
			if (new Rect (300, 725, 200, 200).Contains(touch_one)|| new Rect (300, 725, 200, 200).Contains(touch_two)) {
				if(!something_right)
				{
					h = 1.0f * speed;
					animator.speed = Mathf.Abs(h)/25;
					animator.SetInteger("State", 2);
				}
			}

			if(just_spoke)
			{
				xButtonPressed = false;
				if(Input.touchCount>0)
				{
					if(Input.GetTouch(0).phase == TouchPhase.Ended)
					{
						just_spoke = false;
					}
				}
			}
			else if((new Rect (1550, 725, 200, 200).Contains(touch_one) && Input.GetTouch(0).phase == TouchPhase.Began))//|| new Rect (1550, 725, 200, 200).Contains(touch_two)&& wasPressed)
			{
				wasPressed = true;
			}
			else if((new Rect (1550, 725, 200, 200).Contains(touch_one) && wasPressed && Input.GetTouch(0).phase == TouchPhase.Ended))// || (new Rect (1550, 725, 200, 200).Contains(touch_two)&& wasPressed
			{
				xButtonPressed = true;

			}
			else{
				xButtonPressed = false;
			}


			transform.Translate(Vector2.right * h * Time.deltaTime);
			transform.Translate(Vector2.up * v * Time.deltaTime);
		}
		else
		{
			just_spoke = true;
		}
	}

	void OnGUI()
	{
		animator.speed = .01f;

		/*if (GUI.RepeatButton(new Rect (0, 0, 200, 200), up, GUIStyle.none)) {
			Debug.Log("GUI BUTTON");
		}*/ //dont need this anymore
		if(player_controlled)
		{
			//xButtonPressed = false;
			float h = 0;
			float v = 0;

			//GUI.RepeatButton(new Rect (150, 575, 200, 200), up, GUIStyle.none);

			GUI.DrawTexture(new Rect (150, 575, 200, 200), up);
			GUI.DrawTexture(new Rect (150, 875, 200, 200), down);
			GUI.DrawTexture(new Rect (0, 725, 200, 200), left);
			GUI.DrawTexture(new Rect (300, 725, 200, 200), right);
			GUI.DrawTexture(new Rect (1550, 725, 200, 200), x);

			/*if (GUI.RepeatButton(new Rect (1550, 725, 200, 200), x, GUIStyle.none)){//.Contains(touch_one)|| new Rect (1550, 725, 200, 200).Contains(touch_two)) {
				xButtonPressed = true;
			}
			else
			{
				xButtonPressed = false;
			}*/
		


		}
	}

	void OnCollisionEnter2D(Collision2D col) 
	{
		string t = col.gameObject.name;

		if (t.Contains("Enemy"))
		  Application.LoadLevel("TestScene");
		

		if (t == "House")
			Application.LoadLevel ("Overworld");
		

		if (t.Contains ("Exit")) {
			Application.LoadLevel ("Parade");
		}

		if (t == "Orange_Tent_Collider"){
			//Application.LoadLevel("TestScene");
			Application.LoadLevel ("Orange");
			GameData.access.Save();
		}

		if (t == "Yellow_Tent_Collider"){
			Application.LoadLevel("Yellow");
			GameData.access.Save();
		}

		if (t == "Green_Tent_Collider"){
			Application.LoadLevel("Green");
			GameData.access.Save();
		}

		if (t == "Blue_Tent_Collider"){
			Application.LoadLevel("Blue");
			GameData.access.Save();
		}

		if (t == "Indigo_Tent_Collider"){
			Application.LoadLevel("Indigo");
			GameData.access.Save();
		}
	}
}
