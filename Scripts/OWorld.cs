using UnityEngine;
using System.Collections;
using System.IO;

public class OWorld : MonoBehaviour {
	public GameObject Matty;
	public GameObject HeadMino;
	public GameObject Boar;
	public static OWorld access;


	public GameObject TeamSpeak1;
	public GameObject TeamSpeak2;
	public GameObject TeamSpeak3;

	
	public int state;
	private int last_state;
	private bool created_state_objects_yet;


	// Use this for initialization
	void Start () {
		state = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if(last_state != state)  // checks if state has been updated so that we can instantiate objects.
		{
			created_state_objects_yet = false;
			Debug.Log ("state change");
		}
		last_state = state;

		if(state == 0) // just arrived
		{
			if(created_state_objects_yet == false)
			{
				Instantiate(Matty, new Vector2(-6, -45), Quaternion.identity);
				created_state_objects_yet = true;
			}
		}
		else if(state == 1) // Matty has joined your party
		{

		}

		else if(state == 2) // entered the village
		{
			if(created_state_objects_yet == false)
			{
				Instantiate(TeamSpeak1, new Vector2(-0.10f, -27.99f), Quaternion.identity);
				Instantiate(TeamSpeak2, new Vector2(-38.89f, -3.15f), Quaternion.identity);
				Instantiate(TeamSpeak3, new Vector2(-50.3f, -2.3f), Quaternion.identity);
				created_state_objects_yet = true;
			}
		}

		else if(state == 3) // Talked to the dying minotaur
		{
			//prepare for the thug fight
			//instantiate big bad minotaur
			if(created_state_objects_yet == false && GameObject.Find("Home")!= null)
			{
				Instantiate(HeadMino, new Vector2(-40, -3), Quaternion.identity);
				created_state_objects_yet = true;
			}
		}
		else if(state == 4) // finished the fight
		{
			GameObject.Find("Elder").GetComponentInChildren<OneLiner>().enabled = false;
			GameObject.Find("Elder").GetComponentInChildren<MultiLiner>().enabled = true;
		}
		else if (state == 5) // talked to elder by the lake, and initiated quests.
		{
			GameObject.Find("Green Leader").GetComponentInChildren<OneLiner>().enabled = false;
			GameObject.Find("Green Leader").GetComponentInChildren<QuestGiver>().enabled = true;
			//create boars outside the gates
			/*if(created_state_objects_yet == false)
			{
				Instantiate(Boar, new Vector2(6.67f, -46.78f), Quaternion.identity);
				Instantiate(Boar, new Vector2(-6.75f, -41.68f), Quaternion.identity);
				Instantiate(Boar, new Vector2(5.97f, -39.3f), Quaternion.identity);
				created_state_objects_yet = true;
			}*/
		}
	}

	void Awake()
	{
		// If another GameData object does not exist
		if (access == null) {
			
			// Set Up
			DontDestroyOnLoad (gameObject);
			access = this;
			
		} else {
			// If a GameData already exists, don't make a new one
			Destroy(gameObject);
		}
	}
}
