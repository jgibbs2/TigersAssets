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
	public Transform fire;

	//public IList characters = new IList<string>();
	// Use this for initialization

	int numSelected = 0;
	GameObject[] character_list = new GameObject[3];
	bool done = false;
	int place;

	void Start () {
		place = 0;
		//Instantiate(charSelect, new Vector3(0,0,-1), Quaternion.identity);
		//bool[] arr = GameObject.Find (GameData).GetComponent<GameData> ().characters;
		if(GameObject.Find ("GameData").GetComponent<GameData> ().characters[0] == false)
		{
			Destroy(GameObject.Find("Orange Character"));
		}
		if(GameObject.Find ("GameData").GetComponent<GameData> ().characters[1] == false)
		{
			Destroy(GameObject.Find("Yellow Character"));
		}
		if(GameObject.Find ("GameData").GetComponent<GameData> ().characters[2] == false)
		{
			Destroy(GameObject.Find("Green Character"));
		}
		if(GameObject.Find ("GameData").GetComponent<GameData> ().characters[3] == false)
		{
			Destroy(GameObject.Find("Blue Character"));
		}
		if(GameObject.Find ("GameData").GetComponent<GameData> ().characters[4] == false)
		{
			Destroy(GameObject.Find("Pink Character"));
		}

		//Instantiate (fire, new Vector3 (0, 0, 0), Quaternion.identity);// these initialize each character.
	}

	void RemoveCharacter(GameObject selected_avatar)
	{
		place--;
		character_list [place] = null;

		GameObject parent_character = selected_avatar.transform.parent.gameObject;//.position = location;
		selected_avatar.GetComponent<SpriteRenderer> ().enabled = false;
		selected_avatar.GetComponent<BoxCollider2D> ().enabled = false;
		parent_character.GetComponent<SpriteRenderer> ().enabled = true;
		parent_character.GetComponent<BoxCollider2D> ().enabled = true;
	}

	void GoToCombat()
	{
		if(place>0)
		{
			GameObject.Find ("Combat").GetComponent<Combat>().Initialize(character_list);
		}
	}

	void AddCharacter(GameObject selected_character)
	{
		Vector2 location;
		if(place == 1)
		{
			location = new Vector2(5.3f,-3.75f);
		}
		else if(place == 2)
		{
			location = new Vector2(6.3f,-3.75f);
		}
		else{
			location = new Vector2(7.3f,-3.75f);
		}

		GameObject av = selected_character.transform.FindChild ("Avatar").gameObject;//.position = location;
		av.transform.position = location;
		av.GetComponent<SpriteRenderer> ().enabled = true;
		av.GetComponent<BoxCollider2D> ().enabled = true;
		selected_character.GetComponent<SpriteRenderer> ().enabled = false;
		selected_character.GetComponent<BoxCollider2D> ().enabled = false;
		character_list [place] = selected_character;

		place++;
	}

	// Update is called once per frame
	void Update () {
			GameObject mouse_over_character = null;

			if(Input.GetMouseButtonDown(0))
			{
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				if (Physics2D.Raycast(ray.origin, ray.direction))
				{					
					mouse_over_character = Physics2D.Raycast (ray.origin, ray.direction).collider.gameObject;
					if(mouse_over_character.name == null)
					{
						mouse_over_character = null;
					}
				}
			}
			if(mouse_over_character!=null)
			{
				if(mouse_over_character.name.Contains("Arrow"))
			  	{
					GoToCombat();
				}
				else if(mouse_over_character.name.Contains("Avatar"))
				{
					RemoveCharacter(mouse_over_character);
				}
				else if(place < 2)
				{
					AddCharacter(mouse_over_character);
				}
			}
	}
}
