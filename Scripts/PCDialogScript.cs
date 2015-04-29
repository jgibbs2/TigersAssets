using UnityEngine;
using System.Collections;

public class PCDialogScript : MonoBehaviour 
{
	private bool DisplayPCDialog = false;
	private GUIStyle myStyle;
	private GUIStyle yourStyle;

	public bool inClanMemberTrigger = false;
	public string RequestToJoin;
	public string Acceptance;
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
		if (inClanMemberTrigger)
		{
		  if (GameObject.Find("Bobby").GetComponent<SpriteController>().xButtonPressed)
			DisplayPCDialog = true;
		}
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
		
		if (DisplayPCDialog)
		{
			GUILayout.Label(RequestToJoin, myStyle);
			
			if (GUILayout.Button(Acceptance, yourStyle))
			{
				Destroy(GameObject.FindWithTag("ClanMember"));
				GameObject.Find("Bobby").GetComponent<SpriteController>().xButtonPressed = false;
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
