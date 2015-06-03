﻿using UnityEngine;
using System.Collections;

public class OneLiner : MonoBehaviour {
	private GUIStyle myStyle;
	public Texture2D texture;
	public Texture2D characterImage;

	private bool speech = false;
	// Use this for initialization
	void Start () {
		myStyle = new GUIStyle();
		myStyle.fontSize = 30;
		myStyle.normal.textColor = Color.white;
		myStyle.fontStyle = FontStyle.Bold;
		myStyle.normal.background = texture;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space))
		{
			if(speech)
			{
				speech = false;
			}
			else
			{
				speech = true;
			}
		}
	}

	void OnGUI(){
		if(speech)
		{
			GUI.DrawTexture(new Rect(450, 100, 300, 300), characterImage);
			GUI.Label(new Rect(0,350,Screen.width,Screen.height),"Bobby: All your base are belong to us!", myStyle);

		}

	}
}
