using UnityEngine;
using System.Collections;

public class DamageDisplayScript : MonoBehaviour {

	public string text = "";
	float initial_time = 0.0f;
	// Use this for initialization
	void Start () {
		initial_time = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		//gameObject.Text = text;
		GetComponent<TextMesh> ().text = text;
		if (Time.time - initial_time >= 1) 
		{
			GameObject.Find("Home").GetComponent<PlayerClass>().sub_state = 4;
			Destroy(gameObject);
		}
	}
}
