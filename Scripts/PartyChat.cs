using UnityEngine;
using System.Collections;

public class PartyChat : MonoBehaviour {

	public GameObject party_member;
	private bool talking;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.name == "Bobby")
		{
			GameObject.Find (col.name).GetComponent<SpriteController> ().player_controlled = false;
			talking = true;
			Instantiate (party_member, col.gameObject.transform.position, Quaternion.identity);
		}
	}
}
