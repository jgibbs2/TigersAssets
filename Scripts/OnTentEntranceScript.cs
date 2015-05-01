﻿using UnityEngine;
using System.Collections;

// This Script Forces Bobby to automatically navigate towards
// the first NPC in a given tent, and automatically initiates
// dialogue with it. This happens upon entrance into a tent.

public class OnTentEntranceScript : MonoBehaviour 
{
	private bool hasVisitedTrigger = false;
	public string name_of_party_member;
	private bool secondTimeInTent;

	// Use this for initialization
	void Start () 
	{
		if(GameObject.Find("GameData").GetComponent<GameData>().characters[GameObject.Find("Sprite PC").GetComponent<PCDialogScript>().num] == true)
		{
			secondTimeInTent = true;
			Destroy(GameObject.Find("Sprite PC"));
		}
		else{
			GetComponent<SpriteController>().player_controlled = false;
		}

	}
	
	// Update is called once per frame
	void Update () 
	{
		if (secondTimeInTent == false) {
						if (!hasVisitedTrigger && GetComponent<SpriteController> ().player_controlled == false) {
								if (!GameObject.Find (name_of_party_member).GetComponentInChildren<PCDialogScript> ().inClanMemberTrigger) {
										transform.Translate (2.5f * Vector2.up * Time.deltaTime);
								} else {
										hasVisitedTrigger = true;
										GameObject.Find ("Bobby").GetComponent<SpriteController> ().xButtonPressed = true;
										GameObject.Find ("Bobby").GetComponent<SpriteController> ().player_controlled = true;
								}
						}
				}
	}
}
