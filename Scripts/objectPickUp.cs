using UnityEngine;
using System.Collections;

public class objectPickUp : MonoBehaviour {
	public string word;
	bool inTrigger;
	// Use this for initialization
	void Start () {
		inTrigger = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(inTrigger == true && Input.GetKeyDown(KeyCode.Space))
		{
			GameData.access.pickUpItem(Item.Apple);
			Destroy(GameObject.Find(word));

		}
	}
	
	void OnTriggerEnter2D(Collider2D col)
	{
		string s = col.gameObject.name;
		if (s == "Bobby")
		{
			inTrigger = true;
		}

	}
}
