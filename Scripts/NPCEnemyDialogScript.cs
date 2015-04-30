using UnityEngine;
using System.Collections;

public class NPCEnemyDialogScript : MonoBehaviour 
{
	private bool DisplayEnemyDialog = false;
	private bool inEnemyTrigger = false;
	private GUIStyle myStyle;
	private GUIStyle yourStyle;

	public string FightinWords;
	public string FightBack;
	public Texture2D texture;

	// Use this for initialization 
	void Start () 
	{
	  myStyle = new GUIStyle();
	  yourStyle = new GUIStyle(); 
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
	  GUILayout.BeginArea(new Rect(0, 0, 1200, 400)); 
		
	  myStyle.fontSize = 72;
	  myStyle.normal.textColor = Color.white;
	  myStyle.fontStyle = FontStyle.Bold;
	  myStyle.normal.background = texture; 

	  yourStyle.fontSize = 72;
	  yourStyle.normal.textColor = Color.white; 
	  yourStyle.normal.background = texture;

	  if (DisplayEnemyDialog)
	  {
		GUILayout.Label(FightinWords, myStyle);

		if (GUILayout.Button(FightBack, yourStyle))
		{
		  Application.LoadLevel("TestScene");
		}
	  }

	  GUILayout.EndArea();
	}

	void OnTriggerEnter2D(Collider2D col)
	{
	  if (col.gameObject.name == "Bobby")
	    inEnemyTrigger = true;
	}

	void OnTriggerExit2D(Collider2D col)
	{
      if (col.gameObject.name == "Bobby")
	    inEnemyTrigger = false; 
	}
}
