using UnityEngine;
using System.Collections;

public class NPCEnemyDialogScript : MonoBehaviour 
{
	public string FightinWords;
	public string FightBack;
	private bool DisplayEnemyDialog = false;
	private bool inEnemyTrigger = false;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	  if (inEnemyTrigger)
	    DisplayEnemyDialog = true;

	  else
	    DisplayEnemyDialog = false;
	} 

	void OnGUI()
	{ 
	  GUILayout.BeginArea(new Rect(310, 0, 400, 400)); 
	  //GUI.backgroundColor = Color.black; 

	  if (DisplayEnemyDialog)
	  {
		GUILayout.Label(FightinWords);

		if (GUILayout.Button(FightBack))
		{
		  Application.LoadLevel("TestScene");
		}
	  }

	  GUILayout.EndArea();
	}

	void OnTriggerEnter2D(Collider2D col)
	{
	  inEnemyTrigger = true;
	}

	void OnTriggerExit2D(Collider2D col)
	{
	  inEnemyTrigger = false;
	}
}
