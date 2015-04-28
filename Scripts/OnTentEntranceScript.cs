using UnityEngine;
using System.Collections;

// This Script Forces Bobby to automatically navigate towards
// the first NPC in a given tent, and automatically initiates
// dialogue with it. This happens upon entrance into a tent.

public class OnTentEntranceScript : MonoBehaviour 
{
	private bool hasVisitedTrigger = false;

	// Use this for initialization
	void Start () 
	{
		GameObject.Find("Bobby").GetComponent<SpriteController>().player_controlled = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
	  if (!hasVisitedTrigger)
	  {
	    if (!GameObject.FindGameObjectWithTag("NPCPrimary").GetComponentInChildren<NPCDialogScript>().inTrigger)
	    {
		  transform.Translate(2.5f * Vector2.up * Time.deltaTime);
		}

		else
		{
		  GameObject.Find("Bobby").GetComponent<SpriteController>().xButtonPressed = true;
		  hasVisitedTrigger = true;
		  GameObject.Find("Bobby").GetComponent<SpriteController>().player_controlled = true;
	    }
	  }
	}
}
