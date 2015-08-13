using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Combat : MonoBehaviour {

	#region Variables
	//public variables that need to be accessed by other scripts

	//Variables we need to keep track of through all steps
	GameObject[] Characters;
	GameObject[] Enemies;
	List<CombatData> CombatBuffer = new List<CombatData> ();
	int state;
	public int substate;
	public string[] enemies_to_instantiate;
	bool first; //used to determine whether this is the first time through a given state

	GameObject selected_character;

	CombatData combatData = new CombatData();

	//time variables, relevant for the PassTime function so we can move our characters on the timeline.
	float lastTime;
	float timeElapsed;

	GUIStyle myStyle;
	Rect display_position_a;
	Rect display_position_b;
	Rect display_box;
	string info_string;
	bool text_displayed;
	Texture2D text_background;

	#endregion

	// Use this for initialization
	void Start () {
		transform.name = "Combat";

		text_background = (Texture2D)Resources.Load ("Combat/Walls") as Texture2D;

		info_string = "";
		text_displayed = false;
		myStyle = new GUIStyle();
		if(Application.platform == RuntimePlatform.Android)
		{
			display_position_a = new Rect(1300, 370, 400, 400);
			display_position_b = new Rect(200, 400, 400, 400);
			display_box = new Rect(0,750,Screen.width,Screen.height);
			myStyle.fontSize = 64;
		}
		else
		{
			display_position_a = new Rect(450, 100, 300, 300);
			display_position_b = new Rect(100, 100, 300, 300);
			display_box = new Rect(0,350,Screen.width,Screen.height);
			myStyle.fontSize = 30;
		}
		myStyle.normal.textColor = Color.white;
		myStyle.fontStyle = FontStyle.Bold;
		myStyle.normal.background = text_background;

		first = true;
		state = 0;

		Instantiate (Resources.Load ("Combat/Character Select"), new Vector2 (0, 0), Quaternion.identity);

		//Characters = new GameObject[1];
		//Characters [0] = GameObject.Find ("Bobby");
		//Characters [1] = GameObject.Find ("Matty");*/
		Enemies = new GameObject[1];
		Enemies [0] = GameObject.Find("Bobby 1");
	}

	void OnGUI()
	{
		//Debug.Log (text_displayed);
		if(text_displayed)
		{
			//Debug.Log(info_string);
			GUI.Label(display_box, info_string, myStyle);
		}
	}

	// Update is called once per frame
	void Update () {
		if(CombatBuffer.Count>0)
		{
			state=3;
		}

		if (state != 0) {   //If we're in combat and one side has elimated everyone from the other side.
			/*if(Enemies.Count == 0 || Characters.Length == 0)
			{
				Application.LoadLevel("Matty");
			}*/
		}

		if (state == 1) //If state is 1, then we're advancing everyone on the timeline.  Time passes.  
		{
			PassTime();
		}
		else if (state == 2)
		{
			PlayerAttackSelect();
		}
		else if(state == 3)
		{
			PerformAttack();			
		}

		lastTime = Time.time; //This gives us the last time that this script ran. This is relevant for the countdown function.
	}

	public void Initialize(GameObject[] PlayerCharacters)
	{
		Vector2 location = new Vector2(0, 0);
		int character_number = 1;
		int num_characters = 0;
		//create characters
		foreach(GameObject s in PlayerCharacters)
		{
			if(s!=null)
			{
				num_characters++;
			}
		}

		Enemies = new GameObject[enemies_to_instantiate.Length];
		int enemy_number = 1;
		foreach(string s in enemies_to_instantiate)
		{
			if(enemy_number ==1)
			{
				location = new Vector3 (3.77f, -1.17f, 0);
			}
			else if(enemy_number == 2)
			{
				location = new Vector3(6,1,0);
			}
			else{
				location = new Vector3 (5, -3, 0);
			}

			GameObject temp = (GameObject)Instantiate (Resources.Load ("Combat/"+ s)as GameObject, location, Quaternion.identity);
			temp.transform.parent = GameObject.Find("Combat").transform;
			temp.GetComponentInChildren<HealthSprite>().HealthInit(temp.GetComponent<Stats>().health);
			temp.transform.Find("Avatar").position = new Vector3(0.8f, Random.Range(-3.5f, 3.5f));
			Enemies[enemy_number-1] = temp;
			enemy_number++;
		}

		Characters = new GameObject[num_characters];
		foreach (GameObject s in PlayerCharacters)
		{
			if(s!=null)
			{
				if(character_number == 1)
				{
					location = new Vector2(-3.08f, -0.72f);
				}
				else if(character_number == 2)
				{
					location = new Vector2(-5.58f,1.32f);
				}
				else if(character_number == 3)
				{
					location = new Vector2(-5.35f, -2.79f);
				}

				s.transform.parent = GameObject.Find("Combat").transform;
				s.transform.position = location;
				s.GetComponentInChildren<HealthSprite>().HealthInit(s.GetComponent<Stats>().health);
				s.transform.Find("Avatar").position = new Vector3(-0.8f, Random.Range(-3.5f, 3.5f));
				Characters[character_number-1] = s;
				s.GetComponent<SpriteRenderer>().enabled = true;
				s.GetComponent<BoxCollider2D>().enabled = true;
				s.transform.Find("Health").GetComponent<SpriteRenderer>().enabled = true;
				s.transform.Find("platform").GetComponent<SpriteRenderer>().enabled = true;
				//GameObject tigers = (GameObject)Instantiate(Resources.Load("Combat/" + s) as GameObject, location, Quaternion.identity);
				//tigers.name = s;

				character_number++;
			}
		}

		state = 1;
		first = true;
		//now that we have removed the characters from the Character Select object we can destroy it.
		Destroy(GameObject.Find("Character Select(Clone)"));
	}

	#region Damage Calculation

	private void CalculateDamage()
	{
		int damage = 0;


		if(CombatBuffer[0].action.type_of_move == "Attack")
		{
			int Z = Random.Range(204, 255);
			damage = (int)(CombatBuffer[0].attacker.GetComponent<Stats>().attack * Z)/255 - CombatBuffer[0].target.GetComponent<Stats>().getDefense();
			if(damage < 0)
			{
				damage = 0;
			}
			CombatBuffer[0].target.GetComponentInChildren<HealthSprite>().LoseHealth(damage);
		}

		else if(CombatBuffer[0].action.type_of_move == "Magic")
		{
			float effective = Effective(CombatBuffer[0].action.element, CombatBuffer[0].target.GetComponent<Stats>().element, CombatBuffer[0].target.GetComponent<Stats>().defending);
			if(effective>1.0f)
			{
				CombatBuffer[0].target.GetComponent<Stats>().down = true;
				CombatBuffer[0].target.GetComponent<CharacterAnimationScript>().action = "Down";
			}

			float Z = Random.Range(204, 255);
			damage = (int)(CombatBuffer[0].attacker.GetComponent<Stats>().magic * effective * Z)/255 - CombatBuffer[0].target.GetComponent<Stats>().getDefense();
			if(damage < 0)
			{
				damage = 0;
			}
			CombatBuffer[0].target.GetComponentInChildren<HealthSprite>().LoseHealth(damage);
		}

		else if(CombatBuffer[0].action.type_of_move == "Heal")
		{
			int Z = Random.Range(204, 255);
			damage = (int)(CombatBuffer[0].attacker.GetComponent<Stats>().attack * Z)/255 - CombatBuffer[0].attacker.GetComponent<Stats>().getDefense();
			CombatBuffer[0].target.GetComponentInChildren<HealthSprite>().GainHealth(damage);
		}

		//return (int)damage;
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

	#endregion

	#region PassTime
	void PassTime()
	{
		timeElapsed += (Time.time - lastTime); // CHANGE: This was right before PassTime gets called.  Moved into function for easier reading.
	
		//this if statement prevents a division by zero error on the first iteration by giving lastTime a non-zero value;
		if(lastTime == 0.0f)
		{
			lastTime = Time.time;
		}

		//Delta T is the change in time since the last time the avatars(characters on timeline) moved
		float deltaT = Time.time - lastTime;


		//This little block is super neat.  We define a blank gameobject first. Then, if the mouse button is down
		// we draw a ray through the location of the mouse click (this works too with tapping the screen).  If
		// that character has a "Stats" component, which they will if they're a combat character, then we load a
		// pointer to that object into our blank gameobject.  Not specifically a pointer, but C# in unity works
		// using pass by reference.
		GameObject mouse_over_character = null;
		if(Input.GetMouseButtonDown(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics2D.Raycast(ray.origin, ray.direction))
			{					
				mouse_over_character = Physics2D.Raycast (ray.origin, ray.direction).collider.gameObject;
				if(mouse_over_character.GetComponent<Stats>()==null)
				{
					mouse_over_character = null;
				}
			}
		}

		//This loop is to advance each character along the timeline.
		foreach (GameObject i in Characters)
		{
			Stats temp_stats = i.GetComponent<Stats>();
			Transform avatar = i.transform.FindChild("Avatar");
			if(temp_stats.ready == false)
			{
				//first we move the object as a function of the amount of time that has passed and the current speed of the character
				avatar.position += new Vector3(0.0f, deltaT * temp_stats.getSpeed()/2, 0.0f);

				//Then we check and see if it is past our threshold that means the character is ready to attack
				if(avatar.position.y >= 3.5f)
				{
					//if the avatar would go above the threshold we set it to a fixed position, and then set that character ready to attack
					avatar.position = new Vector3(-0.8f, 3.5f, 0.0f);
					temp_stats.ready = true;
					i.GetComponentInChildren<PlatformScript>().on = true;

					//Not sure if this needs to go here yet.
					if(temp_stats.down == true)
					{
						temp_stats.down = false;
						i.GetComponent<CharacterAnimationScript>().action = "Up";
					}
				}
			}

			//Comment
			else if(temp_stats.ready == true)
			{
				if(mouse_over_character!=null)
				{
					if(i == mouse_over_character)
					{
						//if the character is ready to attack and the mouse is clicking on it, then go to attack select.
						state = 2;
						//then load the clicked character into our selected character variable
						selected_character = mouse_over_character;
					}
				}
			}
		}

		#region Enemy PassTime
		//Same thing to move the enemies along the timeline
		foreach (GameObject i in Enemies)
		{
			Stats temp_stats = i.GetComponent<Stats>();
			Transform avatar = i.transform.FindChild("Avatar");
			if(temp_stats.ready == false)
			{
				//first we move the object as a function of the amount of time that has passed and the current speed of the character
				avatar.position += new Vector3(0.0f, deltaT * temp_stats.getSpeed()/2, 0.0f);
				
				//Then we check and see if it is past our threshold that means the character is ready to attack
				if(avatar.position.y >= 3.5f)
				{
					//if the avatar would go above the threshold we set it to a fixed position, and then set that character ready to attack
					avatar.position = new Vector3(0.8f, 3.5f, 0.0f);
					temp_stats.ready = true;
					
					//Not sure if this needs to go here yet.
					if(temp_stats.down == true)
					{
						temp_stats.down = false;
						i.GetComponent<CharacterAnimationScript>().action = "Up";
					}
				}
			}
			if(temp_stats.ready == true)
			{
				ComputerAttackSelect(i, temp_stats);
			}
		}
		#endregion
	}
	#endregion

	#region Attack Select
	void ComputerAttackSelect(GameObject enemy, Stats enemy_stats)
	{

		enemy_stats.defending = false;
		//this random number between 0 and 3 gives us the choice of action from the character.
		int attack_choice = Random.Range(0, 3);

		//Now we need to take this and Load all of the information into our CombatData Class
		//CombatData combatData = new CombatData();

		combatData.action = enemy_stats.Moves [attack_choice];

		combatData.attacker = enemy;
		
		if(combatData.action.targets_teammate == true)
		{
			if(combatData.action.self_targeting == true)
			{
				combatData.target = enemy;
			}
			else
			{
				int thing = Random.Range(0, Enemies.Length);
				combatData.target = Enemies[thing];
			}
		}
		else
		{
			int thing = Random.Range(0, Characters.Length);
			combatData.target = Characters[thing];
		}

		CombatBuffer.Add (combatData);
	}

	void PlayerAttackSelect()
	{
		/*
		 * this works like a state machine.  state 1 is pulling up the menu and waiting for the player to select an option. state 2 is selcting an enemy
		 */
		GameObject mouse_over_object = null;
		if(Input.GetMouseButtonDown(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics2D.Raycast(ray.origin, ray.direction))
			{					
				mouse_over_object = Physics2D.Raycast (ray.origin, ray.direction).collider.gameObject;
				Debug.Log(mouse_over_object.name);
			}
		}

		if(first)
		{
			GameObject menu = (GameObject)Instantiate(Resources.Load ("Combat/AttackMenu") as GameObject, new Vector3(-7f, 0, 0), Quaternion.identity);

			int temp = 0;
			Vector3 location;
			foreach(Action a in selected_character.GetComponent<Stats>().Moves)
			{
				if(temp == 0)
				{
					location = new Vector3(-6.77f, 2.5f, 0);
				}
				else if(temp == 1)
				{
					location = new Vector3(-6.77f, 0, 0);	
				}
				else
				{
					location = new Vector3(-6.77f, -2.5f, 0);
				}

				GameObject go = (GameObject)Instantiate(a.button as GameObject, location, Quaternion.identity);
				go.transform.parent = menu.transform;

				temp++;
			}
			first = false;
		}

		if(substate == 0) //we're selecting an attack
		{

			if(mouse_over_object != null)
			{
				if (mouse_over_object.name.Contains("Back_Button"))
				{
					first = true;
					state = 1;
					Destroy(GameObject.Find("AttackMenu(Clone)"));
				}
				else if(mouse_over_object.name.Contains("Button"))
				{
					foreach (Action a in selected_character.GetComponent<Stats>().Moves)
					{
						if(a.name == mouse_over_object.GetComponent<AttackButtonScript>().a)
						{
							combatData.attacker = selected_character;
							combatData.action = a;
							info_string = "";
							substate = 1;
							Destroy(GameObject.Find("AttackMenu(Clone)"));
						}
					}
				}
			}
		}
		else if (substate == 1) //we're selecting a target
		{
			bool done = false;
			if(combatData.action.self_targeting)
			{
				combatData.target = selected_character;
				done = true;
			}
			else if(combatData.action.targets_teammate)
			{
				//we need to select a teammate
				info_string = "Select a teammate";
				text_displayed = true;
				if(mouse_over_object!= null && mouse_over_object.GetComponent<Stats>()!=null)
				{
					if(mouse_over_object.GetComponent<Stats>().team == "ally")
					{
						combatData.target = mouse_over_object;
						done = true;
					}
				}
			}
			else
			{
				//we need to select an enemy
				info_string = "Select an enemy";
				text_displayed = true;
				if(mouse_over_object!= null && mouse_over_object.GetComponent<Stats>()!=null)
				{
					if(mouse_over_object.GetComponent<Stats>().team == "enemy")
					{
						combatData.target = mouse_over_object;
						done = true;
					}
				}
			}

			if(done)
			{
				state = 1;
				substate = 0;
				//info_string = "";
				first = true;
				CombatBuffer.Add(combatData);
				selected_character.GetComponent<Stats>().defending = false;
			}
		}
	}

	#endregion

	#region PerformAttack
	void PerformAttack()
	{
		if(substate == 0)
		{
			//here we make the attacker use his animation.
			if(first)
			{
				info_string = CombatBuffer[0].action.name;
				text_displayed = true;

				if(CombatBuffer[0].attacker.GetComponent<Stats>().team == "ally")
				{
					CombatBuffer[0].attacker.GetComponentInChildren<PlatformScript>().on = false;
				}
				if(CombatBuffer[0].action.name == "Attack")
				{
					CombatBuffer[0].attacker.GetComponent<CharacterAnimationScript>().action = "Attack";
				}
				else if (CombatBuffer[0].action.name == "Defend")
				{
					CombatBuffer[0].attacker.GetComponent<CharacterAnimationScript>().action = "Defend";
					CombatBuffer[0].attacker.GetComponent<Stats>().defending = true;
				}
				else
				{
					CombatBuffer[0].attacker.GetComponent<CharacterAnimationScript>().action = "Magic";
				}
				first = false;
			}
			else
			{
				if(!CombatBuffer[0].attacker.GetComponent<CharacterAnimationScript>().playing || CombatBuffer[0].attacker.GetComponent<Stats>().defending)
				{
					substate = 1;
					first = true;
				}
			}
		}
		else if (substate == 1)
		{
			//here have the damage sprite run and update the health if necessary.  Thinking of not using the damage display
			//run for a certain amount of time
			if(first)
			{
				if(!CombatBuffer[0].attacker.GetComponent<Stats>().defending)
				{
					Instantiate(CombatBuffer[0].action.sprite, CombatBuffer[0].target.transform.position, Quaternion.identity);
				}
				else
				{
					//defend for several seconds
					substate = 2;
				}
				first = false;
			}

		}
		else if(substate == 2)
		{
			info_string = "";
			text_displayed = false;

			first = true;
			substate = 0;
			state = 1;
			CombatBuffer[0].attacker.GetComponent<Stats>().ready = false;

			CalculateDamage();

			CombatBuffer[0].attacker.transform.FindChild("Avatar").position = new Vector2(CombatBuffer[0].attacker.transform.FindChild("Avatar").position.x, -3.5f);

			CombatBuffer.Remove(CombatBuffer[0]);
			//update health



			//run once.  Move avatar down, remove glow(allies only), reset things
			//also remove character if needed.  Perhaps we can just remove them from combat.  Have them pass out animation, but remove from the update function.
		}
	}
	#endregion

	#region CombatData Class
	/*
	 * This is an object that gets created that hold all of the input data we need to execute an attack.
	 * The Public game objects attacket and target are pass by reference so they actually affect the character that's assigned to them
	 */

	class CombatData
	{
		//public bool targeting_protagonist;// true is targeting protagonist.  false means that the target is an opponent.
		public GameObject attacker;
		public GameObject target;
		//public string type_of_move; //attack defense heal magic
		//public string element;

		public Action action; // This holds the information of the individual attack.

	}

	#endregion
}
