using UnityEngine;
using System.Collections;

public class objectPickUp : MonoBehaviour {
	public string objectName;
	public Item item;

	bool inTrigger;
	// Use this for initialization
	void Start () {
		inTrigger = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(inTrigger == true && (Input.GetKeyDown(KeyCode.Space)||Input.GetKeyDown(KeyCode.Space)))
		{
			GameData.access.pickUpItem(item);

			DestroyImmediate(GameObject.Find(objectName));

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
