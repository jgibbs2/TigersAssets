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
		if(col.gameObject.name == "up")
		{
			Application.LoadLevel (Scene);
		}
	}
}
