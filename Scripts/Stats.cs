using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Stats : MonoBehaviour {

	public string name;
	public int health;
	public float speed;
	public int attack;
	public int defense;
	public int magic;
	public bool defending;
	public string element;
	public string team; // ally or enemy

	public string first_action;
	public string second_action;
	public string third_action;
	
	public bool ready;
	public bool down;
	public Action[] Moves;

	private bool once = false;
	private int HP; //this is used to track the health of the character.  the variable " health " is just a max number;

	private Action[] CombatActionList = new Action[4];

	// Use this for initialization
	void Start () {

		CombatActionList[0] = (new Action ("Attack", "None", Resources.Load ("Combat/slashAttack") as GameObject, Resources.Load ("Combat/AttackButton") as GameObject, "Attack", false, false));
		CombatActionList[1] = (new Action ("FireBall", "Fire", Resources.Load ("Combat/fireAttack") as GameObject, Resources.Load ("Combat/FireBallButton") as GameObject, "Magic", false, false));
		CombatActionList[2] = (new Action ("Defend", "None", Resources.Load ("Combat/fireAttack") as GameObject, Resources.Load ("Combat/DefendButton") as GameObject, "Defend", true, false));
		CombatActionList[3] = (new Action ("Heal", "None", Resources.Load ("Combat/fireAttack") as GameObject, Resources.Load ("Combat/HealButton") as GameObject, "Heal", false, true));

		ready = false;

		//Action[] temp = GameObject.Find("Bar").GetComponent<CombatActions>().CombatActionList;

		Moves = new Action[3];

		foreach (Action a in CombatActionList)
		{
			if(a.name == first_action)
			{
				Moves[0] = a;
			}
			else if(a.name == second_action)
			{
				Moves[1] = a;
			}
			else if(a.name == third_action)
			{
				Moves[2] = a;
			}
		}
		GetComponentInChildren<HealthSprite> ().HP = health;
		//Debug.Log(name + ", " + Moves[0].name + ", " + Moves[1].name + ", " + Moves[2].name);
	}
	
	// Update is called once per frame
	void Update () {
	}

	public float getSpeed()
	{
		if(down == true)
		{
			return speed * 0.5f;
		}
		else if(defending == true)
		{
			return speed * 2.0f;
		}
		else
		{
			return speed;
		}
	}

	public int getDefense()
	{
		if(down == true)
		{
			return (int)(defense*.7);
		}
		else if(defending == true)
		{
			return (int)(defense*1.4);
		}
		else
		{
			return defense;
		}
	}
	public void setHealth(int change)
	{
		//Debug.Log (HP);
		//Debug.Log (change);
		HP = HP - change;
		if(HP > health)
		{
			HP = health;
		}
		else if(HP < 0)
		{
			HP = 0;
		}
		//Debug.Log (HP);
	}
}

public class Action
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
	
}
