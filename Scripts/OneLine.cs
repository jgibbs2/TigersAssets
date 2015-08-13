using UnityEngine;
using System.Collections;

public class OneLine : MonoBehaviour {

	public string[] enemies;


	bool once;
	void Start () 
	{
		once = false;
	}
	
	// Update is called once per frame 
	void Update () 
	{
		if(!once)
		{
			Debug.Log("getting in?");
			if(Input.GetMouseButtonDown(0))
			{
				GameObject c = (GameObject)Instantiate (Resources.Load ("Combat/Combat")as GameObject, new Vector2 (0, 0), Quaternion.identity);
				c.GetComponent<Combat>().enemies_to_instantiate = enemies;
				once = true;
			}

		}
	} 

}
