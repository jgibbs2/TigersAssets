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
	  if (inEnemyTrigger && (GameObject.Find("Bobby").GetComponent<SpriteController>().xButtonPressed || Input.GetKeyDown(KeyCode.Space)))
	  {
	    DisplayEnemyDialog = true;
		GameObject.Find("Bobby").GetComponent<SpriteController>().player_controlled = false;
		GetComponentInParent<NPCMovement>().talking = true;
	  }

	  //else
	    //DisplayEnemyDialog = false;
	} 

	void OnGUI()
	{ 
		GUILayout.BeginArea(new Rect(0, 0, 2000, 400)); //Computer
		//GUILayout.BeginArea(new Rect(0, 900, 2000, 400));  //Phone
	  myStyle.fontSize = 80;
	  myStyle.normal.textColor = Color.red;
	  myStyle.fontStyle = FontStyle.Bold;
	  myStyle.normal.background = texture; 

	  yourStyle.fontSize = 80;
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
