using UnityEngine;
using System.Collections;

public class DeleteThis : MonoBehaviour {
	float startTime;
	public float endTime;
	// Use this for initialization
	void Start () {
		startTime = Time.time;
		//endTime = 2 * 0.417f;
	}
	
	// Update is called once per frame
	void Update () {

		if(Time.time - startTime >= endTime)
		{
			GameObject.Find("Combat").GetComponent<Combat>().substate = 2;
			Destroy(gameObject);
		}
	}
}
