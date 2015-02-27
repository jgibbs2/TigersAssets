using UnityEngine;
using System.Collections;

public class SpriteController : MonoBehaviour 
{
	public float speed;

	// Use this for initialization
	void Start () 
	{
	}

	// Update is called once per frame
	void Update () 
	{
		float h = Input.GetAxis("Horizontal") * speed;
		float v = Input.GetAxis("Vertical") * speed;
		transform.Translate(Vector2.right * h * Time.deltaTime);
		transform.Translate(Vector2.up * v * Time.deltaTime);
	}

	void OnCollisionEnter2D(Collision2D col) 
	{
		if (col.gameObject.tag == "Enemy")
		  Application.LoadLevel(0);
	}
}
