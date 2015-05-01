using UnityEngine;
using System.Collections;

public class initBobby : MonoBehaviour {

	void Start()
	{
		Debug.Log (GameData.access.getBobbyX_parade ());
		Debug.Log (GameData.access.getBobbyY_parade ());
		var player = GameObject.Find ("Bobby");
		if(player != null)
			player.transform.position = new Vector2(GameData.access.getBobbyX_parade(),GameData.access.getBobbyY_parade()-0.25f);
		else
			Debug.Log ("Cannot find bobby");
	}
	

}
