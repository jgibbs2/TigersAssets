
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
		public int Health;
		/*public int Health
		{
			/*get{ return _health;}
			set{
				_health = _health - value;
				if (_health < 0) {_health = 0;}
			}
		}*/

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
		public string target_group;//allies, enemies
	}

	CombatData current_data;

	List<Combat_Character> Characters = new List<Combat_Character> ();
	List<Enemy_Character> Enemies = new List<Enemy_Character> ();
	List<string> readyCharacters = new List<string>();

	List<CombatData> CombatBuffer = new List<CombatData> ();
	Random rnd = new Random();

	float timeElapsed = 1.0f;
	float timer = 0.0f;//Time.time;

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
	public Transform john;
	public Transform small_john;
	public Transform small_enemy;
	public Transform attack_select;
	public Transform background;
	public Transform bar;
	public Transform DamageDisplay;

	int attackStep = 1;
	CombatData currentAttack = new CombatData();

	#region offensive attack select
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

		//Debug.Log (name);
		//current_data.attacker = name;

		if (attackStep == 1)
		{
			if(Input.GetMouseButtonDown(0)&&(name=="Attack"||name=="Magic"))
			{
				selected=true;
				currentAttack.target_group = "enemies";
			}
			else if(Input.GetMouseButtonDown(0)&&name=="Defend")
			{
				selected = true;
				currentAttack.target_group = "allies";
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
			if(currentAttack.target_group == "enemies")//if(Input.GetMouseButtonDown(0))
			{
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
			}
			else
			{
				foreach (Combat_Character c in Characters)
				{
					if(c.Name+ " Character(Clone)" == name&&Input.GetMouseButtonDown(0))
					{
						currentAttack.defender = c.Name;
						CombatBuffer.Add(currentAttack);
						state = 1;
						attackStep = 1;
					}
				}
			}

		}
	}
	#endregion

	#region initialize and start
	public void initialize(List<string> list_of_characters)
	{
		//This is where we need to instantiate the enemies
		//Instantiate(enemy, new Vector3(5, 0, 0), Quaternion.identity);
		//Instantiate (small_enemy, new Vector3(5, 0, 0), Quaternion.identity);
		Enemy_Character Enemy = new Enemy_Character ();
		Enemy.Name = "Enemy";
		Enemy.Defense = 1;
		Enemy.Power = 5;
		Enemy.Speed = 4;
		Enemy.Ready = false;
		Enemy.Health = 10;
		Enemies.Add (Enemy);

		//Instantiate(john, new Vector3(5, 3, 0), Quaternion.identity);
		//Instantiate(small_john, new Vector3 (5, 0, 0), Quaternion.identity);
		Enemy_Character John = new Enemy_Character ();
		John.Name = "John";
		John.Defense = 2;
		John.Power = 10;
		John.Speed = 3;
		John.Ready = false;
		John.Health = 10;
		Enemies.Add (John);

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
					Red.Power = 3;
					Red.Speed = 2;
					Red.Health = 20;
					Red.Ready = false;
					Characters.Add (Red);
					break;
				case "Orange":
					Combat_Character Orange = new Combat_Character ();
					Orange.Name = "Orange";
					Orange.Power = 3;
					Orange.Speed = 3;
					Orange.Health = 20;
					Orange.Magic = 2;
					Orange.Ready = false;
					Characters.Add (Orange);
					break;
				case "Yellow":
					Combat_Character Yellow = new Combat_Character ();
					Yellow.Name = "Yellow";
					Yellow.Speed = 4;
					Yellow.Power = 2;
					Yellow.Magic = 4;
					Yellow.Health = 20;
					Yellow.Ready = false;
					Characters.Add (Yellow);
					break;
				case "Green":
					Combat_Character Green = new Combat_Character ();
					Green.Name = "Green";
					Green.Speed = 5;
					Green.Power = 2;
					Green.Magic = 4;
					Green.Health = 20;
					Green.Ready = false;
					Characters.Add (Green);
					break;
				case "Blue":
					Combat_Character Blue = new Combat_Character ();
					Blue.Name = "Blue";
					Blue.Speed = 6;
					Blue.Ready = false;
					Blue.Power = 2;
					Blue.Magic = 4;
					Blue.Health = 20;
					Characters.Add (Blue);
					break;
				case "Pink":
					Combat_Character Pink = new Combat_Character ();
					Pink.Name = "Pink";
					Pink.Speed = 7;
					Pink.Power = 2;
					Pink.Magic = 4;
					Pink.Health = 20;
					Pink.Ready = false;
					Characters.Add (Pink);
					break;
			}
		}

		//disable the other script
		GameObject.Find ("Home").GetComponent<CharacterSelect> ().enabled = false;
		Destroy(GameObject.Find ("Character Select(Clone)"));



		int character_number = 1;
		Vector3 location = new Vector3(0f, 0f, 0f);

		foreach (Enemy_Character E in Enemies)
		{
			
			if(character_number ==1)
			{
				location = new Vector3(5,3,0);
			}
			else if(character_number == 2)
			{
				location = new Vector3 (5, 0, 0);
			}
			else{
				location = new Vector3 (5, -3, 0);
			}


			switch(E.Name)
			{
			case "John":
				Instantiate(john, location, Quaternion.identity);
				Instantiate(small_john, location, Quaternion.identity);
				break;
			case "Enemy":
	            Instantiate(enemy, location, Quaternion.identity);
	            Instantiate(small_enemy, location, Quaternion.identity);
	            break;
			}
            character_number++;
		}
		
    	character_number = 1;

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

	void EnemyAttackSelect(string enemyName)
	{
		int attack_choice = 0;//Random.Range(0, 2);
		if(attack_choice == 0)//if we choose attack
		{
			int thing = Random.Range(0, Characters.Count);
			currentAttack.attacker = enemyName;
			currentAttack.defender = Characters[thing].Name;
			currentAttack.target_group = "allies";
			currentAttack.type_of_move = "Attack";
		}
		else if(attack_choice == 1)// if we choose magic
		{

		}
		else // we have chosen defense
		{

		}
		CombatBuffer.Add(currentAttack);

	}
	#region Countdown
	void countDown()
	{
		//string message = "";
		float t = timeElapsed;//?
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
			//i.startTime = Time.time;
			if(i.Ready == false)
			{
				//t = Time.time;
				i.time_passed=(t-i.startTime);
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
				EnemyAttackSelect(i.Name);//  i.select
				i.time_passed = 0.0f;
				i.startTime = timeElapsed;
				i.Ready = false;


			}


		}

		foreach (Combat_Character i in Characters)
		{

			if(i.Ready == false) //if//(i.Ready==false)
			{
				i.time_passed=(t-i.startTime);
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
				i.startTime = timeElapsed;
				i.Ready = false;

				Instantiate(attack_select, new Vector3(0, 0, 0), Quaternion.identity);
				currentAttack.attacker = i.Name;
				currentAttack.team = "pro";

				//if this happens, switch to state number two ( attack select );
			}
			//Debug.Log(i.time_passed);
		}
		//timeElapsed += Time.time - timer;?
	}
	#endregion

	int getMod()
	{
		int rand = Random.Range(0, 10);
		if(rand <1)
		{
			return 0;
		}
		else if (rand >7)
		{
			return 2;
		}
		else
			return 1;
	}

	int CalculateDamage()
	{
		int damage = 0;
		if(current_data.type_of_move == "Attack")
		{
			int A = 0;// = temp.Power;
			int D = 0;// = temp2.Defense;
			if(current_data.target_group == "enemies")
			{
				Combat_Character temp = null;
				foreach (Combat_Character c in Characters)
				{
					if (c.Name == current_data.attacker)
					{
						A = c.Power;
						//temp = c;
					}
				}

				
				Enemy_Character temp2 = null;
				foreach (Enemy_Character c in Enemies)
				{
					if (c.Name == current_data.defender)
					{
						D = c.Defense;
						//temp2 = c;
					}
				}
			}
			else
			{
				//Combat_Character temp2 = null;
				foreach (Combat_Character c in Characters)
				{
					if (c.Name == current_data.defender)
					{
						D = c.Defense;
						//temp2 = c;
					}
				}
				
				
				//Enemy_Character temp = null;
				foreach (Enemy_Character c in Enemies)
				{
					if (c.Name == current_data.attacker)
					{
						A = c.Power;
							//temp = c;
					}
				}
			}


			int Z = Random.Range(225, 255);
			if ( D ==0)
				D=1;

			int mod = getMod();
			//Debug.Log ("A * A = " + A + " & D = " + D + " & mod = " + mod);
			damage = (((((A * A) / D) * Z) / 255) * mod);
		}
		return damage;
	}
	void PerformAttack()
	{
		//CombatData data = CombatBuffer [0]; absolete, look at current_data
		//attacking animation ( need carter here )

			//wait till done

		//actual animation of the attack (not too hard, but the thing should last only as long as the animation of the attack)

			// wait till done

		//calculate damage here
		int damage = CalculateDamage ();

		//display of damage dealt
		if( current_data.target_group== "allies")
			{
				foreach(Combat_Character c in Characters)
				{
					if(current_data.defender==c.Name)
					{
						//Debug.Log("poopy");
						GameObject go = GameObject.Find(current_data.defender + " Character(Clone)");
						Instantiate (DamageDisplay, new Vector3(go.transform.position.x+0.5f, go.transform.position.y+1.0f, 0f), Quaternion.identity);
						GameObject.Find("DamageDisplay(Clone)").GetComponent<DamageDisplayScript>().text = damage.ToString();
						Debug.Log("health = " + c.Health + " & damage = " + damage);
						c.Health = c.Health - damage;
						Debug.Log("c.health = " + c.Health);
						if(c.Health<=0)
						{
							Characters.Remove(c);
							Destroy(GameObject.Find(c.Name + " Character(Clone)"));
							Destroy(GameObject.Find("Small " + c.Name + " Character(Clone)"));
							break;
						}
					}
				}
			}
			else// attack is directed at the enemies
			{
				foreach(Enemy_Character e in Enemies)
				{
					if(current_data.defender==e.Name)
					{
						
						//Debug.Log("poopy");
						GameObject go = GameObject.Find(current_data.defender + "(Clone)");
						Instantiate (DamageDisplay, new Vector3(go.transform.position.x+0.5f, go.transform.position.y+1.0f, 0f), Quaternion.identity);
						GameObject.Find("DamageDisplay(Clone)").GetComponent<DamageDisplayScript>().text = damage.ToString();
						e.Health = e.Health-damage;
						if(e.Health<=0)
						{
							Enemies.Remove(e);
							Destroy(GameObject.Find(e.Name + "(Clone)"));
							Destroy(GameObject.Find("Small " + e.Name + " Character(Clone)"));
							break;
						}
					}
					//Debug.Log(e.Name);
				}
			}



		//finished with this attack
		//state = 1;
		//CombatBuffer.Remove(CombatBuffer[0]);
	}

	float timerStart = 0.0f;

	// Update is called once per frame
	void Update () {
		if(CombatBuffer.Count>0)
		{
			current_data = CombatBuffer [0];
			state=3;
		}

		if (state == 0) {}
		else if (state == 1)
		{
			if(Enemies.Count==0||Characters.Count==0)
			{
				Application.LoadLevel(2);
			}
			timer = Time.time;
			timeElapsed += (timer-timerStart);
			countDown ();

			//Debug.Log(timeElapsed);
		}
		else if (state == 2)
		{
			ProAttackSelect();
		}
		else if(state == 3)
		{
			PerformAttack();
			//Debug.Log(CombatBuffer[0].defender);
			state = 1;
			//Debug.Log (CombatBuffer.FirstOrDefault().attacker);//);+ " is " + CombatBuffer[1].type_of_move + " at " + CombatBuffer[1].defender);
			CombatBuffer.Remove(CombatBuffer[0]);
		}
		timerStart = Time.time;
		//timerStart = Time.time;
		//timer = Time.time;
	}
}
