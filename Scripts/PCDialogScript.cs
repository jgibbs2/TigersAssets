using UnityEngine;
using System.Collections;

public class PCDialogScript : MonoBehaviour 
{
	private bool DisplayPCDialog;
	private GUIStyle myStyle;
	private GUIStyle yourStyle;

	public bool inClanMemberTrigger;
	public string RequestToJoin;
	public string Acceptance;
	public Texture2D texture;

	public string name;
	public int num;
	
	// Use this for initialization
	void Start () 
	{
		DisplayPCDialog = false;
		myStyle = new GUIStyle();
		yourStyle = new GUIStyle();
		inClanMemberTrigger = false;
	}
	
	// Update is called once per frame 
	void Update () 
	{
		if (inClanMemberTrigger)
		{
		  if (GameObject.Find("Bobby").GetComponent<SpriteController>().xButtonPressed)
		  {
			DisplayPCDialog = true;
			GameObject.Find("Bobby").GetComponent<SpriteController>().player_controlled = false;
		  }
		}
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
		yourStyle.normal.textColor = Color.white; 
		yourStyle.normal.background = texture;
		
		if (DisplayPCDialog)
		{
			GUILayout.Label(RequestToJoin, myStyle);
			
			if (GUILayout.Button(Acceptance, yourStyle))
			{
				Destroy(GameObject.Find("Sprite PC"));
				GameObject.Find("Bobby").GetComponent<SpriteController>().xButtonPressed = false;
				GameObject.Find("Bobby").GetComponent<SpriteController>().player_controlled = true;
				GameObject.Find ("GameData").GetComponent<GameData>().characters[num] = true;
			}
		}
		
		GUILayout.EndArea();
	}
	
	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Player")
			inClanMemberTrigger = true;
	}
	
	/*void OnTriggerExit2D(Collider2D col)
	{
		if (col.gameObject.tag == "Player")
			inClanMemberTrigger = false; 
	}*/
}
