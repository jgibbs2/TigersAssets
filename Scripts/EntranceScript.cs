using UnityEngine;
using System.Collections;

public class EntranceScript : MonoBehaviour {


	public string Scene;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		//Debug.Log (col.gameObject.name);
		if(col.gameObject.name == "Bobby")// && (GameObject.Find("Bobby").GetComponent<SpriteController>().xButtonPressed == true || Input.GetKeyDown(KeyCode.Space)))
		{
			Application.LoadLevel (Scene);
		}
	}
}
