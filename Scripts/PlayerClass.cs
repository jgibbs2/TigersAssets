
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerClass : MonoBehaviour {
	#region Character Classes
	class Combat_Character
	{
		#region Global Character Variables
		private string _name;
		public string Name
		{
			get{return _name;}
			set{_name = value;}
		}

		private int _health;
		public int Health
		{
			get{ return _health;}
			set{
				_health = _health - value;
				if (_health < 0) {_health = 0;}
			}
		}

		private int _power;
		public int Power
		{
			get{return _power;}
			set{_power = value;}
		}

		private int _defense;
		public int Defense
		{
			get{return _defense;}
			set{_defense = value;}
		}

		private int _magic;
		public int Magic
		{
			get{return _magic;}
			set{_magic = value;}
		}

		private int _speed;
		public int Speed
		{
			get{return _speed;}
			set{_speed = value;}
		}
		#endregion
		//make official the stuff below this.
		//operational things
		public bool Ready;
		public float time_passed;
		public float startTime = 0.0f;
		//bool combat_timer;
		//ToDo: Some form of list of attacks, damages and possible targets

		//make constructor that can get and set our private variables.
	}

	class Enemy_Character
	{
		#region Global Character Variables
		public string Name;
		public int Health;
		public int Power;
		public int Defense;		
		public int Magic;
		public int Speed;
		#endregion

		public bool Ready;
		public float time_passed;
		public float startTime = 0.0f;
		//bool combat_timer;
		//ToDo: Some form of list of attacks, damages and possible targets
		
		//make constructor that can get and set our private variables.
	}

	#endregion


	class CombatData
	{
		public string team;//either "pro" or "ant"
		//List<string> names; // names of attackers
		public string attacker;
		public string defender;
		public string type_of_move;
	}

	List<Combat_Character> Characters = new List<Combat_Character> ();
	List<Enemy_Character> Enemies = new List<Enemy_Character> ();
	List<string> readyCharacters = new List<string>();

	List<CombatData> CombatBuffer = new List<CombatData> ();

	string readyClicked;


	//int speed = 5;
	bool ready = false;
	int state = 0;
	//float startTime = 0.0f;
	public Transform red;
	public Transform green;
	public Transform blue;
	public Transform orange;
	public Transform yellow;
	public Transform pink;
	public Transform small_red;
	public Transform small_green;
	public Transform small_blue;
	public Transform small_orange;
	public Transform small_yellow;
	public Transform small_pink;

	public Transform enemy;
	public Transform small_enemy;
	public Transform attack_select;
	public Transform background;
	public Transform bar;

	int attackStep = 1;
	CombatData currentAttack = new CombatData();

	void ProAttackSelect()
	{
		string name;
		bool selected = false;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if (Physics2D.Raycast (ray.origin, ray.direction))
		{
			name = Physics2D.Raycast (ray.origin, ray.direction).collider.gameObject.name;
		}
		else
		{
			name = "nothing";
		}

		Debug.Log (name);

		if (attackStep == 1)
		{
			if(Input.GetMouseButtonDown(0)&&(name=="Attack"||name=="Magic"||name=="Defend"))
			{
				selected=true;
			}
			if(selected == true)
			{
				currentAttack.type_of_move = name;
				attackStep = 2;
				Destroy(GameObject.Find("Attack Select(Clone)"));//.GetComponent<SpriteRenderer>().enabled = false;
			}

		}
		else if (attackStep == 2)
		{
			//if(Input.GetMouseButtonDown(0))
			//{
				foreach (Enemy_Character e in Enemies)
				{
					if(e.Name+ "(Clone)" == name&&Input.GetMouseButtonDown(0))
					{
						currentAttack.defender = e.Name;
						CombatBuffer.Add(currentAttack);
						state = 1;
						attackStep = 1;
					}
				}
			//}

		}
	}

	#region initialize and start
	public void initialize(List<string> list_of_characters)
	{
		//This is where we need to instantiate the enemies
		Instantiate(enemy, new Vector3(5, 0, 0), Quaternion.identity);
		Instantiate (small_enemy, new Vector3(5, 0, 0), Quaternion.identity);
		Enemy_Character Enemy = new Enemy_Character ();
		Enemy.Name = "Enemy";
		Enemy.Speed = 300;
		Enemy.Ready = false;
		Enemies.Add (Enemy);

		// enemies here ( or after )
		foreach (string character in list_of_characters) {

			Destroy ( GameObject.Find("Small " + character + " Character(Clone)"));
			switch (character)
			{
				case "Empty":
					break;
				case "Red":
					Combat_Character Red = new Combat_Character ();
					Red.Name = "Red";
					Red.Speed = 200;
					Red.Ready = false;
					Characters.Add (Red);
					break;
				case "Orange":
					Combat_Character Orange = new Combat_Character ();
					Orange.Name = "Orange";
					Orange.Speed = 200;
					Orange.Ready = false;
					Characters.Add (Orange);
					break;
				case "Yellow":
					Combat_Character Yellow = new Combat_Character ();
					Yellow.Name = "Yellow";
					Yellow.Speed = 200;
					Yellow.Ready = false;
					Characters.Add (Yellow);
					break;
				case "Green":
					Combat_Character Green = new Combat_Character ();
					Green.Name = "Green";
					Green.Speed = 200;
					Green.Ready = false;
					Characters.Add (Green);
					break;
				case "Blue":
					Combat_Character Blue = new Combat_Character ();
					Blue.Name = "Blue";
					Blue.Speed = 200;
					Blue.Ready = false;
					Characters.Add (Blue);
					break;
				case "Pink":
					Combat_Character Pink = new Combat_Character ();
					Pink.Name = "Pink";
					Pink.Speed = 200;
					Pink.Ready = false;
					Characters.Add (Pink);
					break;
			}
		}
		Debug.Log ("poopy");
		//disable the other script
		GameObject.Find ("Home").GetComponent<CharacterSelect> ().enabled = false;
		Destroy(GameObject.Find ("Character Select(Clone)"));


		int character_number = 1;
		Vector3 location = new Vector3(0f, 0f, 0f);
		foreach (Combat_Character C in Characters)
		{
			if(character_number ==1)
			{
				location = new Vector3(-5,3,0);
			}
			else if(character_number == 2)
			{
				location = new Vector3 (-5, 0, 0);
			}
			else{
				location = new Vector3 (-5, -3, 0);
			}

			switch(C.Name)
			{
			case "Red":
				Instantiate(red, location, Quaternion.identity);
				Instantiate (small_red, location, Quaternion.identity);
				break;
			case "Orange":
				Instantiate(orange, location, Quaternion.identity);
				Instantiate(small_orange, location, Quaternion.identity);
				break;
			case "Yellow":
				Instantiate(yellow, location, Quaternion.identity);
				Instantiate(small_yellow, location, Quaternion.identity);
				break;
			case "Green":
				Instantiate(green, location, Quaternion.identity);
				Instantiate(small_green, location, Quaternion.identity);
				break;
			case "Blue":
				Instantiate(blue, location, Quaternion.identity);
				Instantiate(small_blue, location, Quaternion.identity);
				break;
			case "Pink":
				Instantiate(pink, location, Quaternion.identity);
				Instantiate(small_pink, location, Quaternion.identity);
				break;
			}
			character_number++;
		}
		state = 1;
		Instantiate (bar, new Vector3 (0f, 0f, 0f), Quaternion.identity);
	}

	// Use this for initialization
	void Start () {
		Instantiate(background, new Vector3(0f,0f,0f), Quaternion.identity);// these initialize each character.

		//Instantiate (blue, new Vector3 (-5, -3, 0), Quaternion.identity);
		//GameObject red = GameObject.Instantiate*/
	}

	#endregion

	void EnemyAttackSelect()
	{

	}

	void countDown()
	{
		//string message = "";
		float t = Time.time;
		string current_name;
		
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if (Physics2D.Raycast (ray.origin, ray.direction))
		{
			current_name = Physics2D.Raycast (ray.origin, ray.direction).collider.gameObject.name;
		}
		else
		{
			current_name = "nothing";
		}

		foreach (Enemy_Character i in Enemies)
		{
			if(i.Ready == false)
			{
				i.time_passed+=(t-i.startTime);
				if(Mathf.Floor(i.time_passed)>=i.Speed)
				{
					GameObject.Find ("Small " + i.Name + " Character(Clone)").transform.position = new Vector3(0.8f,3.5f, 0.0f);
					i.Ready = true;
				}
				else
				{
					GameObject.Find("Small " + i.Name + " Character(Clone)").transform.position = new Vector3(0.8f, (-3.5f+((i.time_passed*7)/i.Speed)), 0.0f);
				}
			}
			else if(i.Ready == true)
			{
				//EnemyAttackSelect();  i.select
				i.time_passed = 0.0f;
				i.startTime = Time.time;
				i.Ready = false;


			}

		}

		foreach (Combat_Character i in Characters)
		{
			Debug.Log(i.Name);
			if(i.Ready == false) //if//(i.Ready==false)
			{
				i.time_passed+=(t-i.startTime);
				if(Mathf.Floor(i.time_passed)>=i.Speed)
				{
					GameObject.Find("Small " + i.Name + " Character(Clone)").transform.position = new Vector3(-0.8f, 3.5f, 0.0f);
					//message = "Ready";
					i.Ready = true;
					
				}
				else
				{
					GameObject.Find("Small " + i.Name + " Character(Clone)").transform.position = new Vector3(-0.8f, (-3.5f+((i.time_passed*7)/i.Speed)), 0.0f);
					//message = Mathf.Floor(i.time_passed).ToString();
				}
				//GameObject.Find (i.Name + " Text").GetComponent<TextMesh>().text= message;
			}
			else if(current_name == i.Name+" Character(Clone)" && Input.GetMouseButtonDown(0)&& i.Ready == true)
			{
				state = 2;
				readyClicked = i.Name;
				i.time_passed = 0.0f;
				i.startTime = Time.time;
				i.Ready = false;

				Instantiate(attack_select, new Vector3(0, 0, 0), Quaternion.identity);
				currentAttack.attacker = i.Name;
				currentAttack.team = "pro";

				//if this happens, switch to state number two ( attack select );
			}
		}
	}

	// Update is called once per frame
	void Update () {
		if (state == 0) {}
		else if (state == 1)
		{
			countDown ();
		}
		else if (state == 2)
		{
			ProAttackSelect();
		}


	}
}
