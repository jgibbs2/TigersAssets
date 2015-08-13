using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CombatActions : MonoBehaviour {

	public Action[] CombatActionList = new Action[4];
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public Action[] getCombatActionList()
	{
		return CombatActionList;
	}

}

/*public class Action
{
	public string name;
	public string element;
	public GameObject sprite;
	public GameObject button;
	public string type_of_move;//Magic, Attack, Defense, Heal
	public bool self_targeting;  // true means it does self target, false is it doesn't
	public bool targets_teammate;
	
	//public void makeButton(Vector3 position) {Instantiate(button, position, Quaternion.identity);}
	
	//public void makeSprite(Vector3 position) {Instantiate(sprite, position, Quaternion.identity);}
	
	public Action(string n, string e, GameObject s, GameObject b, string t, bool st, bool tt)
	{
		name = n;
		element = e;
		sprite = s;
		button = b; 
		type_of_move = t;
		self_targeting = st;
		targets_teammate = tt;
	}
	
}*/
