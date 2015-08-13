using UnityEngine;
using System.Collections;

public class PlatformScript : MonoBehaviour {

	public bool on;
	public Sprite clear;

	// Use this for initialization
	void Start () {
		GetComponent<Animator> ().enabled = false;
		on = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(on == true && GetComponent<Animator>().enabled == false)
		{
			GetComponent<Animator> ().enabled = true;
		}
		else if(on == false)
		{
			GetComponent<Animator> ().enabled = false;
			GetComponent<SpriteRenderer>().sprite = clear;
		}
	}
}
