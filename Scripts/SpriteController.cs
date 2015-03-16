using UnityEngine;
using System.Collections;

public class SpriteController : MonoBehaviour 
{
	public float speed;
	public Texture2D up;
	public Texture2D down;
	public Texture2D left;
	public Texture2D right;

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
		if (GUI.RepeatButton(new Rect (100, 800, 100, 100), up, GUIStyle.none)) {
			v = 1.0f * speed;
		}
		if (GUI.RepeatButton(new Rect (100, 1000, 100, 100), down, GUIStyle.none)) {
			v = -1.0f * speed;
		}
		if (GUI.RepeatButton(new Rect (0, 900, 100, 100), left, GUIStyle.none)) {
			h = -1.0f * speed;
		}
		if (GUI.RepeatButton(new Rect (200, 900, 100, 100), right, GUIStyle.none)) {
			h = 1.0f * speed;
		}
		transform.Translate(Vector2.right * h * Time.deltaTime);
		transform.Translate(Vector2.up * v * Time.deltaTime);
	}

	void OnCollisionEnter2D(Collision2D col) 
	{
		if (col.gameObject.tag == "Enemy")
		  Application.LoadLevel("TestScene");

		if (col.gameObject.tag == "House")
			Application.LoadLevel("Overworld");
		
		if (col.gameObject.tag == "Back")
			Application.LoadLevel("Parade");

		if (col.gameObject.tag == "Orange")
			Application.LoadLevel("Orange");

		if (col.gameObject.tag == "Yellow")
			Application.LoadLevel("Yellow");

		if (col.gameObject.tag == "Green")
			Application.LoadLevel("Green");

		if (col.gameObject.tag == "Blue")
			Application.LoadLevel("Blue");

		if (col.gameObject.tag == "Indigo")
			Application.LoadLevel("Indigo");
	}
}
