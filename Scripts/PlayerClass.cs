
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerClass : MonoBehaviour {
	#region Character Classes
	class Combat_Character
	{
		#region Global Character Variables
		public string Name;
		public int Health;

		public int HP;
		public int MP;
		public string Element;
		
		public int Attack;
		public int Defense;
		public int Magic;
		public int Attack_Boost;
		public int Magic_Boost;
		public float Speed;

		#endregion

		public bool down;
		public bool defending;
		public bool Ready;
		public float time_passed;
		public float startTime = 0.0f;

		public void setHP(int h)
		{
			if((HP - h) < 0)
			{
				HP = 0;
			}
			else
			{
				HP -= h;
			}
		}

		public float getSpeed()
		{
			if(down == true)
			{
				return Speed * 1.4f;
			}
			else if(defending == true)
			{
				return Speed * .7f;
			}
			else
			{
				return Speed;
			}
		}
		public int getDefense()
		{
			if(down == true)
			{
				return (int)(Defense*.7);
			}
			else if(defending == true)
			{
				return (int)(Defense*1.4);
			}
			else
			{
				return Defense;
			}
		}
	}

	class Enemy_Character
	{
		public string Name;
		public int Health;
		
		public int HP;
		public int MP;
		public string Element;
		
		public int Attack;
		public int Defense;
		public int Magic;
		public int Attack_Boost;
		public int Magic_Boost;
		public float Speed;

		
		public bool down;
		public bool defending;
		public bool Ready;
		public float time_passed;
		public float startTime = 0.0f;
		
		public void setHP(int h)
		{
			if((HP - h) < 0)
			{
				HP = 0;
			}
			else
			{
				HP -= h;
			}
		}
		
		public float getSpeed()
		{
			if(down == true)
			{
				return Speed * 1.4f;
			}
			else if(defending == true)
			{
				return Speed * .7f;
			}
			else
			{
				return Speed;
			}
		}
		public int getDefense()
		{
			if(down == true)
			{
				return (int)(Defense*.7);
			}
			else if(defending == true)
			{
				return (int)(Defense*1.4);
			}
			else
			{
				return Defense;
			}
		}
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
	string temp_var;



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
	public Transform Red_Health;
	public Transform Orange_Health;
	public Transform Yellow_Health;
	public Transform Green_Health;
	public Transform Blue_Health;
	public Transform Pink_Health;

	public Transform red_enemy;
	public Transform green_enemy;
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
			foreach (Combat_Character c in Characters)
			{
				if(c.Name+ " Character(Clone)" == name)
				{
					//GameObject.Find(c.Name + " Character(Clone)").GetComponent<CharacterAnimationScript>().action = "Pause";
					temp_var = c.Name;
				}
			}
			if(Input.GetMouseButtonDown(0)&&(name=="Attack"||name=="Magic"))
			{
				selected=true;
				currentAttack.target_group = "enemies";
			}
			else if(Input.GetMouseButtonDown(0)&&name=="Defend")
			{
				state = 1;
				currentAttack.type_of_move = "Defend";
				currentAttack.target_group = "allies";
				currentAttack.attacker = readyClicked;
				CombatBuffer.Add(currentAttack);
				Destroy(GameObject.Find("Attack Select(Clone)"));
				GameObject.Find(temp_var + " Character(Clone)").GetComponent<CharacterAnimationScript>().action = "Defend";
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
						//GameObject.Find(c.Name + " Character(Clone)").GetComponent<CharacterAnimationScript>().action = "Resume";

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
		Enemy_Character Red_Enemy = new Enemy_Character ();
		Red_Enemy.Name = "Red_Enemy";
		Red_Enemy.Defense = 35;
		Red_Enemy.Magic = 50;
		Red_Enemy.defending = false;
		Red_Enemy.Attack = 40;
		Red_Enemy.Speed = 5.0f;
		Red_Enemy.Ready = false;
		Red_Enemy.Health = 100;
		Red_Enemy.Element = "Water";
		Red_Enemy.down = false;

		Enemies.Add (Red_Enemy);

		//Instantiate(john, new Vector3(5, 3, 0), Quaternion.identity);
		//Instantiate(small_john, new Vector3 (5, 0, 0), Quaternion.identity);
		Enemy_Character Green_Enemy = new Enemy_Character ();
		Green_Enemy.Name = "Green_Enemy";
		Green_Enemy.Defense = 35;
		Green_Enemy.Attack = 50;
		Green_Enemy.Magic = 50;
		Green_Enemy.defending = false;
		Green_Enemy.Speed = 3.0f;
		Green_Enemy.Ready = false;
		Green_Enemy.Health = 100;
		Green_Enemy.Element = "Wind";
		Green_Enemy.down = false;

		Enemies.Add (Green_Enemy);

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
					Red.Attack = 50;//40-50-60 base
					Red.Magic = 60;//50-60-70 base
					Red.Defense = 30;//25-30-35 bases
					Red.Speed = 4.0f;
					Red.Health = 100;
					Red.HP = Red.Health;
					Red.Ready = false;
					Red.down = false;
					Red.Element = "Fire";
					Characters.Add (Red);
					break;
				case "Orange":
					Combat_Character Orange = new Combat_Character ();
					Orange.Name = "Orange";
					Orange.Attack = 60;//40-50-60 base
					Orange.Magic = 45;//45?-60-70 base
					Orange.Defense = 35;//25-30-35 bases
					Orange.Speed = 5.0f;
					Orange.Health = 100;
					Orange.HP = Orange.Health;
					Orange.Ready = false;
					Orange.down = false;
					Orange.Element = "None";
					Characters.Add (Orange);
					break;
				case "Yellow":
					Combat_Character Yellow = new Combat_Character ();
					Yellow.Name = "Yellow";
					Yellow.Attack = 50;//40-50-60 base
					Yellow.Magic = 70;//45?-60-70 base
					Yellow.Defense = 35;//25-30-35 bases
					Yellow.Speed = 3.0f;
					Yellow.Health = 100;
					Yellow.HP = Yellow.Health;
					Yellow.Ready = false;
					Yellow.down = false;
					Yellow.Element = "Water";
					Characters.Add (Yellow);
					break;
				case "Green":
					Combat_Character Green = new Combat_Character ();
					Green.Name = "Green";
					Green.Attack = 50;//40-50-60 base
					Green.Magic = 70;//45?-60-70 base
					Green.Defense = 30;//25-30-35 bases
					Green.Speed = 3.0f;
					Green.Health = 100;
					Green.HP = Green.Health;
					Green.Ready = false;
					Green.down = false;
					Green.Element = "Wind";
					Characters.Add (Green);
					break;
				case "Blue":
					Combat_Character Blue = new Combat_Character ();
					Blue.Name = "Blue";
					Blue.Attack = 50;//40-50-60 base
					Blue.Magic = 70;//45?-60-70 base
					Blue.Defense = 30;//25-30-35 bases
					Blue.Speed = 3.0f;
					Blue.Health = 100;
					Blue.HP = Blue.Health;
					Blue.Ready = false;
					Blue.down = false;
					Blue.Element = "Elec";
					Characters.Add (Blue);
					break;
				case "Pink":
					Combat_Character Pink = new Combat_Character ();
					Pink.Name = "Pink";
					Pink.Attack = 50;//40-50-60 base
					Pink.Magic = 70;//45?-60-70 base
					Pink.Defense = 30;//25-30-35 bases
					Pink.Speed = 3.0f;
					Pink.Health = 100;
					Pink.HP = Pink.Health;
					Pink.Ready = false;
					Pink.down = false;
					Pink.Element = "None";
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
			case "Green_Enemy":
				Instantiate(green_enemy, location, Quaternion.identity);
				Instantiate(small_john, location, Quaternion.identity);
				break;
			case "Red_Enemy":
				Instantiate(red_enemy, location, Quaternion.identity);
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
				Instantiate(Red_Health, new Vector2(location.x - .85f, location.y - 1.3f), Quaternion.identity);
				break;
			case "Orange":
				Instantiate(orange, location, Quaternion.identity);
				Instantiate(small_orange, location, Quaternion.identity);
				Instantiate(Orange_Health, new Vector2(location.x - .85f, location.y - 1.3f), Quaternion.identity);
				break;
			case "Yellow":
				Instantiate(yellow, location, Quaternion.identity);
				Instantiate(small_yellow, location, Quaternion.identity);
				Instantiate(Yellow_Health, new Vector2(location.x - .85f, location.y - 1.3f), Quaternion.identity);
				break;
			case "Green":
				Instantiate(green, location, Quaternion.identity);
				Instantiate(small_green, location, Quaternion.identity);
				Instantiate(Green_Health, new Vector2(location.x - .85f, location.y - 1.3f), Quaternion.identity);
				break;
			case "Blue":
				Instantiate(blue, location, Quaternion.identity);
				Instantiate(small_blue, location, Quaternion.identity);
				Instantiate(Blue_Health, new Vector2(location.x - .85f, location.y - 1.3f), Quaternion.identity);
				break;
			case "Pink":
				Instantiate(pink, location, Quaternion.identity);
				Instantiate(small_pink, location, Quaternion.identity);
				Instantiate(Pink_Health, new Vector2(location.x - .85f, location.y - 1.3f), Quaternion.identity);
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
			int thing = Random.Range(0, Characters.Count);
			currentAttack.attacker = enemyName;
			currentAttack.defender = Characters[thing].Name;
			currentAttack.target_group = "allies";
			currentAttack.type_of_move = "Magic";
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
		float t = timeElapsed;
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
				if(Mathf.Floor(i.time_passed)>=i.getSpeed())
				{
					GameObject.Find ("Small " + i.Name + " Character(Clone)").transform.position = new Vector3(0.8f,3.5f, 0.0f);
					i.Ready = true;
				}
				else
				{
					GameObject.Find("Small " + i.Name + " Character(Clone)").transform.position = new Vector3(0.8f, (-3.5f+((i.time_passed*7)/i.getSpeed())), 0.0f);
					//Debug.Log("poopy");
					if(Mathf.Floor(i.time_passed)>= i.getSpeed())
					{
						if(i.down == true)
						{
							i.down = false;
							GameObject.Find(i.Name + "(Clone)").GetComponent<CharacterAnimationScript>().action = "Up";
						}
					}
				}
			}
			else if(i.Ready == true)
			{
				//Debug.Log(i.down);
				if(i.down == true)
				{
					//Debug.Log("poasdfh");
					i.down = false;
					GameObject.Find(i.Name + "(Clone)").GetComponent<CharacterAnimationScript>().action = "Up";
				}
				EnemyAttackSelect(i.Name);//  i.select
				i.time_passed = 0.0f;
				i.startTime = timeElapsed;
				i.Ready = false;


			}


		}

		foreach (Combat_Character i in Characters)
		{
			//GameObject.Find(i.Name + " Character(Clone)").GetComponent<CharacterAnimationScript>().action = "Up";
			if(i.Ready == false) //if//(i.Ready==false)
			{
				i.time_passed=(t-i.startTime);
				if(Mathf.Floor(i.time_passed)>=i.getSpeed())
				{
					GameObject.Find("Small " + i.Name + " Character(Clone)").transform.position = new Vector3(-0.8f, 3.5f, 0.0f);
					//message = "Ready";
					i.Ready = true;
					i.down = false;
					//GameObject.Find(i.Name + " Character(Clone)").GetComponent<CharacterAnimationScript>().action = "Up";
					
				}
				else
				{
					GameObject.Find("Small " + i.Name + " Character(Clone)").transform.position = new Vector3(-0.8f, (-3.5f+((i.time_passed*7)/i.getSpeed())), 0.0f);
					//message = Mathf.Floor(i.time_passed).ToString();
				}
				//GameObject.Find (i.Name + " Text").GetComponent<TextMesh>().text= message;
			}
			else if(current_name == i.Name+" Character(Clone)" && Input.GetMouseButtonDown(0)&& i.Ready == true)
			{
				GameObject.Find(i.Name + " Character(Clone)").GetComponent<CharacterAnimationScript>().action = "Up";
				i.defending = false;

				state = 2;
				//temp_var = i.Name;
				readyClicked = i.Name;
				i.time_passed = 0.0f;
				i.startTime = timeElapsed;
				i.Ready = false;

				Instantiate(attack_select, new Vector3(0, 0, 0), Quaternion.identity);
				currentAttack.attacker = i.Name;
				currentAttack.team = "pro";

				//if this happens, switch to state number two ( attack select );
			}
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
		/*else if (rand >7)
		{
			return 2;
		}*/
		else
			return 1;
	}
	float Effective(string alpha_E, string beta_E, bool defending)//water -> fire -> wind -> elec
	{
		if (alpha_E == "None" || beta_E == "None") {
						return 1.0f;
				} else if (alpha_E == "Fire") {
						if (beta_E == "Wind") {
								if(defending == true)
								{
									return 1.0f;
								}
								else
								{
									return 1.4f;
								}
						} else if (beta_E == "Water") {
								if(defending == true)
								{
									return .5f;
								}
								else
								{
									return .75f;
								}
						} else {
								return 1.0f;
						}
				} else if (alpha_E == "Water") {
						if (beta_E == "Fire") {//if effective
								if(defending == true)
								{
									return 1.0f;
								}
								else
								{
									return 1.4f;
								}
						} else if (beta_E == "Elec") {//if not
								if(defending == true)
								{
									return .5f;
								}
								else
								{
									return .75f;
								}
						} else {
								return 1.0f;
						}
				} else if (alpha_E == "Elec") {
						if (beta_E == "Water") {//if effective
								if(defending == true)
								{
									return 1.0f;
								}
								else
								{
									return 1.4f;
								}
						} else if (beta_E == "Wind") {//if not
								if(defending == true)
								{
									return .5f;
								}
								else
								{
									return .75f;
								}
						} else {
								return 1.0f;
						}
				} else if (alpha_E == "Wind") {
						if (beta_E == "Elec") {//if effective
								if(defending == true)
								{
									return 1.0f;
								}
								else
								{
									return 1.4f;
								}
						} else if (beta_E == "Fire") {//if not
								if(defending == true)
								{
									return .5f;
								}
								else
								{
									return .75f;
								}
						} else {
								return 1.0f;
						}
				} else
						return 1.0f;
	}

	int CalculateMagicDamage ()
	{
		int A = 0, A_boost = 0, D = 0, D_boost = 0;
		string alpha_E = "", beta_E = "";//elements of each character.
		bool defending = false;
		float E = 1.0f;

		if(current_data.target_group == "enemies")
		{
			Combat_Character temp = null;
			Enemy_Character temp2 = null;
			foreach (Combat_Character c in Characters)
			{
				if (c.Name == current_data.attacker)
				{
					A = c.Magic;
					A_boost = c.Attack_Boost;
					alpha_E = c.Element;
					foreach (Enemy_Character e in Enemies)
					{
						if (e.Name == current_data.defender)
						{
							D = e.getDefense();
							beta_E = e.Element;
							defending = e.defending;

							E = Effective(alpha_E, beta_E, defending);
							if(E>1.0f)
							{
								e.down = true;
								//Debug.Log(e.down);
								GameObject.Find(e.Name + "(Clone)").GetComponent<CharacterAnimationScript>().action = "Down";
							}
						}
					}
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
					D = c.getDefense();
					beta_E = c.Element;
					defending = c.defending;
					//Debug.Log(defending);
					//temp2 = c;

					foreach (Enemy_Character e in Enemies)
					{
						if (e.Name == current_data.attacker)
						{
							A = e.Magic;
							alpha_E = e.Element;


							E = Effective(alpha_E, beta_E, defending);
							if(E>1.0f)
							{
								c.down = true;
								//Debug.Log ("down");
								GameObject.Find(c.Name + " Character(Clone)").GetComponent<CharacterAnimationScript>().action = "Down";
							}

						}
					}
				}
			}
		}
		int Z = Random.Range(204, 255);
		//int mod = getMod();
		//Debug.Log ("A = " + A + ", E = " + E);

		float damage = (A*E*Z)/255 - D;
		if(damage <0)
		{
			damage = 0;
		}
		//if the magic type is effective or ineffective, we want to multiply the A.
		//int damage = A - 
		return (int)damage;
	}

	int CalculateAttackDamage()
	{
		int damage = 0;
		if(current_data.type_of_move == "Attack")
		{
			int A = 0;// = temp.Attack;
			int D = 0;// = temp2.Defense;
			int mod = 1;
			if(current_data.target_group == "enemies")
			{
				//Combat_Character temp = null;
				//Enemy_Character temp2 = null;
				foreach (Combat_Character c in Characters)
				{
					if (c.Name == current_data.attacker)
					{
						A = c.Attack;


						foreach (Enemy_Character e in Enemies)
						{
							if (e.Name == current_data.defender)
							{
								D = e.getDefense();

								mod = getMod();
								//temp2 = c;
							}
						}
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
						D = c.getDefense();

						foreach (Enemy_Character e in Enemies)
						{
							if (e.Name == current_data.attacker)
							{
								A = e.Attack;

								mod = getMod();

								if(mod>1)
								{
									c.down = true;
									//Debug.Log("down");
									GameObject.Find(c.Name + " Character(Clone)").GetComponent<CharacterAnimationScript>().action = "Down";
								}
							}
						}
					}
				}
				
				
				//Enemy_Character temp = null;

			}


			int Z = Random.Range(204, 255);
			if ( D ==0)
				D=1;


			//Debug.Log ("A * A = " + A + " & D = " + D + " & mod = " + mod);
			damage  = ((A*mod*Z)/255) - D;//= (((((A * A) / D) * Z) / 255) * mod);
			if(damage<0)
			{
				damage = 0;
			}
		}
		return damage;
	}


	void CreateAttackSprite()
	{

	}

	void PerformAttack()
	{
		//CombatData data = CombatBuffer [0]; absolete, look at current_data
		//attacking animation ( need carter here )

		/*if(perform_state == 1)
		{
			perform_state = 2;
		}
		else if(perform_state == 2)
		{

		}
		else if(perform_state == 3)
		{

		}*/


			//wait till done

		//actual animation of the attack (not too hard, but the thing should last only as long as the animation of the attack)



		//calculate damage here
		int damage = 0;
		if(current_data.type_of_move == "Attack")
		{
			damage = CalculateAttackDamage ();
			if(current_data.target_group == "enemies")
			{
				GameObject.Find(current_data.attacker + " Character(Clone)").GetComponent<CharacterAnimationScript>().action = "Attack";
				//yield 
			}
			else
			{
				Debug.Log(current_data.attacker);
				GameObject.Find(current_data.attacker + "(Clone)").GetComponent<CharacterAnimationScript>().action = "Attack";
			}
		}
		else if(current_data.type_of_move == "Magic")
		{
			damage = CalculateMagicDamage();
			if(current_data.target_group == "enemies")
			{
				GameObject.Find(current_data.attacker + " Character(Clone)").GetComponent<CharacterAnimationScript>().action = "Magic";
			}
			else
			{
				GameObject.Find(current_data.attacker + "(Clone)").GetComponent<CharacterAnimationScript>().action = "Magic";
			}
		}
		else if(current_data.type_of_move == "Defend")
		{
			//Debug.Log("hitting");
			foreach(Combat_Character c in Characters)
			{
				//Debug.Log(c.Name + " == " + current_data.attacker);
				if(c.Name == current_data.attacker)
				{
					//Debug.Log("things");
					c.defending = true;
					return;
				}
			}
		}
		

		//display of damage dealt
		if( current_data.target_group== "allies")
			{
				foreach(Combat_Character c in Characters)
				{
					if(current_data.defender==c.Name)
					{
						GameObject go = GameObject.Find(current_data.defender + " Character(Clone)");
						Instantiate (DamageDisplay, new Vector3(go.transform.position.x+0.5f, go.transform.position.y+1.0f, 0f), Quaternion.identity);
						GameObject.Find("DamageDisplay(Clone)").GetComponent<DamageDisplayScript>().text = damage.ToString();
						//Debug.Log("health = " + c.Health + " & damage = " + damage);
						GameObject.Find (c.Name + "_Health(Clone)").GetComponentInChildren<HealthSprite>().HP = c.Health;
						GameObject.Find (c.Name + "_Health(Clone)").GetComponentInChildren<HealthSprite>().LoseHealth(damage);
						c.setHP(damage);
						//Debug.Log("c.health = " + c.Health);
						if(c.HP<=0)
						{
							Characters.Remove(c);
							Destroy(GameObject.Find(c.Name + " Character(Clone)"));
							Destroy(GameObject.Find("Small " + c.Name + " Character(Clone)"));
							Destroy(GameObject.Find (c.Name + "_Health(Clone)"));
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
				Application.LoadLevel("Parade");
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
