using UnityEngine;
using System.Collections;

public class drawGUI : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Debug.Log("Drawing inventory");
		GameData.access.DrawInventory();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
