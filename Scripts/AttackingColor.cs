using UnityEngine;
using System.Collections;

public class AttackingColor : MonoBehaviour {

	public Sprite red;
	public Sprite orange;
	public Sprite yellow;
	public Sprite green;
	public Sprite blue;
	public Sprite pink;


	// Use this for initialization
	void Start () {
		string v = GameObject.Find ("Home").GetComponent<PlayerClass> ().clicked;
		if(v.Contains("Red"))
		{
			GetComponent<SpriteRenderer>().sprite = red;
		}
		else if(v.Contains("Orange"))
		{
			GetComponent<SpriteRenderer>().sprite = orange;
		}
		else if(v.Contains("Yellow"))
		{
			GetComponent<SpriteRenderer>().sprite = yellow;
		}
		else if(v.Contains("Green"))
		{
			GetComponent<SpriteRenderer>().sprite = green;
		}
		else if(v.Contains("Blue"))
		{
			GetComponent<SpriteRenderer>().sprite = blue;
		}
		else if(v.Contains("Pink"))
		{
			GetComponent<SpriteRenderer>().sprite = pink;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
