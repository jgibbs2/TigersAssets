using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterSelect : MonoBehaviour {
	public Transform charSelect;
	public Transform small_red;
	public Transform small_orange;
	public Transform small_yellow;
	public Transform small_green;
	public Transform small_blue;
	public Transform small_pink;

	//public IList characters = new IList<string>();
	// Use this for initialization

	int numSelected = 0;
	List<string> characterList = new List<string>();
	bool done = false;

	void Start () {
		Instantiate(charSelect, new Vector3(0,0,-1), Quaternion.identity);// these initialize each character.
	}

	void AddCharacter(string color)
	{
		int place;
		if (numSelected < 3) // ensure that we haven't already selected our three characters.
		{
			GameObject.Find(color + " Character").GetComponent<SpriteRenderer>().enabled = false;
			GameObject.Find(color + " Character").transform.collider2D.enabled = false;//.GetComponent<t>().enabled = false;

			if(characterList.Contains("Empty"))
			{
				place = characterList.IndexOf("Empty");
				characterList[place] = color;
			}
			else
			{
				characterList.Add(color); // add it to the list
				place = characterList.IndexOf(color);
			}



			//below we are actually creating the prefab of the smaller version before we move onto the next screen.
			switch(place){
				case 0:
					switch(color){
						case "Red":
							Instantiate(small_red, new Vector3(0.6f,3.7f,0f), Quaternion.identity);
							break;
						case "Orange":
							Instantiate(small_orange, new Vector3(0.6f,3.7f,0f), Quaternion.identity);
							break;
						case "Yellow":
							Instantiate(small_yellow, new Vector3(0.6f,3.7f,0f), Quaternion.identity);
							break;
						case "Green":
							Instantiate(small_green, new Vector3(0.6f,3.7f,0f), Quaternion.identity);
							break;
						case "Blue":
							Instantiate(small_blue, new Vector3(0.6f,3.7f,0f), Quaternion.identity);
							break;
						case "Pink":
							Instantiate(small_pink, new Vector3(0.6f,3.7f,0f), Quaternion.identity);
							break;
					}
					break;
				case 1:
					switch(color){
					case "Red":
						Instantiate(small_red, new Vector3(1.6f,3.7f,0f), Quaternion.identity);
						break;
					case "Orange":
						Instantiate(small_orange, new Vector3(1.6f,3.7f,0f), Quaternion.identity);
						break;
					case "Yellow":
						Instantiate(small_yellow, new Vector3(1.6f,3.7f,0f), Quaternion.identity);
						break;
					case "Green":
						Instantiate(small_green, new Vector3(1.6f,3.7f,0f), Quaternion.identity);
						break;
					case "Blue":
						Instantiate(small_blue, new Vector3(1.6f,3.7f,0f), Quaternion.identity);
						break;
					case "Pink":
						Instantiate(small_pink, new Vector3(1.6f,3.7f,0f), Quaternion.identity);
						break;
					}
					break;
				case 2:
					switch(color){
					case "Red":
						Instantiate(small_red, new Vector3(2.6f,3.7f,0f), Quaternion.identity);
						break;
					case "Orange":
						Instantiate(small_orange, new Vector3(2.6f,3.7f,0f), Quaternion.identity);
						break;
					case "Yellow":
						Instantiate(small_yellow, new Vector3(2.6f,3.7f,0f), Quaternion.identity);
						break;
					case "Green":
						Instantiate(small_green, new Vector3(2.6f,3.7f,0f), Quaternion.identity);
						break;
					case "Blue":
						Instantiate(small_blue, new Vector3(2.6f,3.7f,0f), Quaternion.identity);
						break;
					case "Pink":
						Instantiate(small_pink, new Vector3(2.6f,3.7f,0f), Quaternion.identity);
						break;
					}
					break;
				}
			numSelected++;
		}
	}

	void RemoveCharacter(string color)
	{
		int place;
		if (numSelected > 0)
		{
			place = characterList.IndexOf(color);
			characterList[place] = "Empty";

			Destroy(GameObject.Find("Small " + color + " Character(Clone)"));
			GameObject.Find(color + " Character").GetComponent<SpriteRenderer>().enabled = true;
			GameObject.Find(color + " Character").transform.collider2D.enabled = true;
			numSelected--;
		}
	}

	void GoToCombat()
	{
		while (numSelected<3) {
			characterList.Add("Empty");
			numSelected++;
				}

		GameObject.Find ("Home").GetComponent<PlayerClass> ().initialize (characterList);
		done = true;
	}
	
	// Update is called once per frame
	void Update () {
		if ( done == false)
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			if(Physics2D.Raycast(ray.origin, ray.direction))
			{
			RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

			//	Debug.Log (hit.collider.name);

			string objectName = hit.collider.gameObject.name;
			if (Input.GetMouseButtonDown (0) && objectName != null) {
				//objectName = hit.collider.gameObject.name;
			switch(objectName){
				case "Red Character":
					AddCharacter("Red");
					break;
				case "Small Red Character(Clone)":
					RemoveCharacter("Red");
					break;
				case "Orange Character":
					AddCharacter("Orange");
					break;
				case "Small Orange Character(Clone)":
					RemoveCharacter("Orange");
					break;
				case "Yellow Character":
					AddCharacter("Yellow");
					break;
				case "Small Yellow Character(Clone)":
					RemoveCharacter("Yellow");
					break;
				case "Green Character":
					AddCharacter("Green");
					break;
				case "Small Green Character(Clone)":
					RemoveCharacter("Green");
					break;
				case "Blue Character":
					AddCharacter("Blue");
					break;
				case "Small Blue Character(Clone)":
					RemoveCharacter("Blue");
					break;
				case "Pink Character":
					AddCharacter("Pink");
					break;
				case "Small Pink Character(Clone)":
					RemoveCharacter("Pink");
					break;
				case "Arrow":
					GoToCombat();
					break;
				}
			}
			}

		}
		   
	}
}
