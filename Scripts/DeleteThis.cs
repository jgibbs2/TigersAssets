using UnityEngine;
using System.Collections;

public class DeleteThis : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(GameObject.Find ("BlueSprites_0").GetComponent<Animator>().isActiveAndEnabled == true)
		{
			Debug.Log ("working");
		}
	}
}
