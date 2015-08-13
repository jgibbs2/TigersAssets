using UnityEngine;
using System.Collections;

public class ExitScript : MonoBehaviour {

	public string Scene;
	public int state_to_exit = -1;  // if this is unchanged, then there is no action required to exit	
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
			if(GameObject.Find("OWorld").GetComponent<OWorld>().state >= state_to_exit)
			{
				Application.LoadLevel (Scene);
			}
			else{
				//fill with dialogue
			}


		}
	}
}
