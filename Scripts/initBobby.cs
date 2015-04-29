using UnityEngine;
using System.Collections;

public class initBobby : MonoBehaviour {

	void Start()
	{
		Debug.Log (GameData.access.getBobbyX_parade ());
		Debug.Log (GameData.access.getBobbyY_parade ());
		var player = GameObject.FindWithTag ("Player");
		if(player != null)
			player.transform.position.Set(GameData.access.getBobbyX_parade(), GameData.access.getBobbyY_parade(),0);
		else
			Debug.Log ("Cannot find bobby");
	}
	

}
