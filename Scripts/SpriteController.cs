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

	// Use this for initialization
	void Start () 
	{
	}

	// Update is called once per frame
	void Update () 
	{
		//float hor = Input.GetAxis ("Horizontal");
		//float ver = Input.GetAxis ("Vertical");
		//Debug.Log ("hor = " + hor + ", ver = " + ver);
		float h = Input.GetAxis("Horizontal") * speed;
		float v = Input.GetAxis("Vertical") * speed;
			transform.Translate(Vector2.right * h * Time.deltaTime);
			transform.Translate(Vector2.up * v * Time.deltaTime);


	}

	void OnGUI()
	{
		float h = 0;
		float v = 0;
		if (GUI.RepeatButton(new Rect (150, 575, 200, 200), up, GUIStyle.none)) {
			v = 1.0f * speed;
		}
		if (GUI.RepeatButton(new Rect (150, 875, 200, 200), down, GUIStyle.none)) {
			v = -1.0f * speed;
		}
		if (GUI.RepeatButton(new Rect (0, 725, 200, 200), left, GUIStyle.none)) {
			h = -1.0f * speed;
		}
		if (GUI.RepeatButton(new Rect (300, 725, 200, 200), right, GUIStyle.none)) {
			h = 1.0f * speed;
		}
		if (GUI.RepeatButton(new Rect (1000, 725, 200, 200), x, GUIStyle.none)) {
			xButtonPressed = true;
		}
		transform.Translate(Vector2.right * h * Time.deltaTime);
		transform.Translate(Vector2.up * v * Time.deltaTime);
	}

	void OnCollisionEnter2D(Collision2D col) 
	{
		string t = col.gameObject.name;

		if (t.Contains("Enemy"))
		  Application.LoadLevel("TestScene");

		if (t == "House")
			Application.LoadLevel("Overworld");

		if (t.Contains("Exit"))
			Application.LoadLevel("Parade");

		if (t == "Orange_Tent_Collider")
			Application.LoadLevel("Orange");

		if (t == "Yellow_Tent_Collider")
			Application.LoadLevel("Yellow");

		if (t == "Green_Tent_Collider")
			Application.LoadLevel("Green");

		if (t == "Blue_Tent_Collider")
			Application.LoadLevel("Blue");

		if (t == "Indigo_Tent_Collider")
			Application.LoadLevel("Indigo");
	}
}
