using UnityEngine;
using System.Collections;

public class objectPickUp : MonoBehaviour {
	public string objectName;
	public Item item;

	bool inTrigger;
	// Use this for initialization
	void Start () {
		inTrigger = false;
		var thisObject = GameObject.Find(objectName);
		thisObject.SetActive(GameData.access.appleQuest);
		Debug.Log(thisObject.activeSelf.ToString());

	}
	
	// Update is called once per frame
	void Update () {
		if(inTrigger == true && (Input.GetKeyDown(KeyCode.Space)||GameObject.Find("Bobby").GetComponent<SpriteController>().xButtonPressed))
		{
			GameData.access.pickUpItem(item);
			GameData.access.appleQuest = false;

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
