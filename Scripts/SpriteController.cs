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
	public bool xButtonPressed = false;
	public bool player_controlled = true;
	private Animator animator;
	public string enemies;
	public bool something_up = false;
	public bool something_down = false;
	public bool something_left = false;
	public bool something_right = false;

	// Use this for initialization
	void Start () 
	{
		animator = this.GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update () 
	{
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
					Debug.Log("should not be moving right");
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
	}

	void OnGUI()
	{
		animator.speed = .01f;

		/*if (GUI.RepeatButton(new Rect (0, 0, 200, 200), up, GUIStyle.none)) {
			Debug.Log("GUI BUTTON");
		}*/ //dont need this anymore
		if(player_controlled)
		{
			xButtonPressed = false;
			float h = 0;
			float v = 0;
			if (GUI.RepeatButton(new Rect (150, 575, 200, 200), up, GUIStyle.none)) {
				if(!something_up)
				{
					v = 1.0f * speed;
					animator.speed = Mathf.Abs(v)/25;
					animator.SetInteger("State", 3);
				}
			}
			if (GUI.RepeatButton(new Rect (150, 875, 200, 200), down, GUIStyle.none)) {
				if(!something_down)
				{
					v = -1.0f * speed;
					animator.speed = Mathf.Abs(v)/25;
					animator.SetInteger("State", 0);
				}
			}
			if (GUI.RepeatButton(new Rect (0, 725, 200, 200), left, GUIStyle.none)) {
				if(!something_left)
				{
					h = -1.0f * speed;
					animator.speed = Mathf.Abs(h)/25;
					animator.SetInteger("State", 1);
				}
			}
			if (GUI.RepeatButton(new Rect (300, 725, 200, 200), right, GUIStyle.none)) {
				if(!something_right)
				{
					h = 1.0f * speed;
					animator.speed = Mathf.Abs(h)/25;
					animator.SetInteger("State", 2);
				}
			}
			if (GUI.RepeatButton(new Rect (1550, 725, 200, 200), x, GUIStyle.none)) {
				xButtonPressed = true;
			}
			transform.Translate(Vector2.right * h * Time.deltaTime);
			transform.Translate(Vector2.up * v * Time.deltaTime);
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
