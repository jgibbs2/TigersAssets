using UnityEngine;
using System.Collections;

public class NPCEnemyDialogScript : MonoBehaviour 
{
	public string FightinWords;
	public string FightBack;
	private bool DisplayEnemyDialog = false;
	private bool inEnemyTrigger = false;
	public Texture2D texture = new Texture2D(400, 400);
	private GUIStyle myStyle;
	private GUIStyle yourStyle;

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
	  GUILayout.BeginArea(new Rect(310, 0, 400, 400)); 
		
	  myStyle.fontSize = 24;
	  myStyle.normal.textColor = Color.white;
	  myStyle.fontStyle = FontStyle.Bold;
	  myStyle.normal.background = texture; 

	  yourStyle.fontSize = 24;
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
	  if (col.gameObject.tag == "Player")
	    inEnemyTrigger = true;
	}

	void OnTriggerExit2D(Collider2D col)
	{
      if (col.gameObject.tag == "Player")
	    inEnemyTrigger = false; 
	}
}
