using UnityEngine;
using System.Collections;

public class downTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.GetComponent<Collider2D>().isTrigger == false)
		{
			GetComponentInParent<SpriteController> ().something_down = true;
		}
	}
	
	void OnTriggerExit2D(Collider2D col)
	{
		GetComponentInParent<SpriteController> ().something_down = false;
	}
}
