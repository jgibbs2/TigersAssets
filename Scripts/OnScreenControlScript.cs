using UnityEngine;
using System.Collections;

public class OnScreenControlScript : MonoBehaviour {

	// Use this for initialization
	public Texture tex;
	void Start () {
	
	}
	void OnGUI () {
		if (GUI.Button (new Rect (0, 0, 100, 100), tex, GUIStyle.none)) {
			Debug.Log("Pressed");
				}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
