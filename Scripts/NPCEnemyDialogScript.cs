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
	public string members;
	

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
	  myStyle.normal.textColor = Color.white;
	  myStyle.fontStyle = FontStyle.Bold;
	  myStyle.normal.background = texture; 

	  yourStyle.fontSize = 80;
	  yourStyle.normal.textColor = Color.red; 
	  yourStyle.normal.background = texture;

	  if (DisplayEnemyDialog)
	  {
		GUILayout.Label(FightinWords, myStyle);

		if (GUILayout.Button(FightBack, yourStyle))
		{
				GameObject.Find("GameData").GetComponent<GameData>().enemies[0] = false;
				GameObject.Find("GameData").GetComponent<GameData>().enemies[1] = false;
				GameObject.Find("GameData").GetComponent<GameData>().enemies[2] = false;
				GameObject.Find("GameData").GetComponent<GameData>().enemies[3] = false;
				GameObject.Find("GameData").GetComponent<GameData>().enemies[4] = false;
				GameObject.Find("GameData").GetComponent<GameData>().enemies[5] = false;

			foreach(char c in members)
			{
					Debug.Log(c);
					if(c == 'r')
					{
						GameObject.Find("GameData").GetComponent<GameData>().enemies[0] = true;
					}
					else if (c== 'o')
					{
						GameObject.Find("GameData").GetComponent<GameData>().enemies[1] = true;
					}
					else if(c == 'y')
					{
						GameObject.Find("GameData").GetComponent<GameData>().enemies[2] = true;
					}
					else if (c == 'g')
					{
						GameObject.Find("GameData").GetComponent<GameData>().enemies[3] = true;
					}
					else if(c == 'b')
					{
						GameObject.Find("GameData").GetComponent<GameData>().enemies[4] = true;
					}
					else if(c == 'p')
					{
						GameObject.Find("GameData").GetComponent<GameData>().enemies[5] = true;
					}
			}
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
