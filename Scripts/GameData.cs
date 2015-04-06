using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


/*
 * Maintains data across scene transitions
 * 
 * This is a singleton class so only one of them
 * will exist at any given time although every
 * scene will technically "have" one.
 * 
 * It maintains a public static refrence to itself
 * so it can be access from anywhere without having
 * to use some findObject function.
 */
public class GameData : MonoBehaviour {

	public static GameData access;
	private const string saveLocation = "/gameData.dat";

	float bobbyX_parade;
	float bobbyY_parade;

	public float getBobbyX_parade(){
		return bobbyX_parade;
	}

	public float getBobbyY_parade(){
		return bobbyY_parade;
	}

	void Awake()
	{
		// If another GameData object does not exist
		if (access == null) {
			DontDestroyOnLoad (gameObject);
			access = this;
			Load ();
		} else {
			// If a GameData already exists, don't make a new one
			Destroy(gameObject);
		}
	}

	// Save all game data to a file
	public void Save()
	{
		bobbyX_parade = GameObject.Find ("Bobby").transform.localPosition.x;
		bobbyY_parade = GameObject.Find ("Bobby").transform.localPosition.y;

		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + saveLocation);

		GameData_saver save = new GameData_saver ();

		// Copy all of the data to the save class
		save.bobbyX_parade = bobbyX_parade;
		save.bobbyY_parade = bobbyY_parade;

		// Write save object to the file in binary
		bf.Serialize (file, save);

		file.Close ();
	}

	// Load game data from a file
	public void Load()
	{
		if (File.Exists (Application.persistentDataPath + saveLocation)) {
		
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + saveLocation, FileMode.Open);

			GameData_saver saved = (GameData_saver)bf.Deserialize (file);

			bobbyX_parade = saved.bobbyX_parade;
			bobbyY_parade = saved.bobbyY_parade;

			file.Close ();
		} else {
			bobbyX_parade = 0;
			bobbyY_parade = 0;
		}
	}
}
/*
 * This is a data packing class that will be written to the save file 
 * It may as well be a struct. Only used in GameData::Save() and GameData::Load()
 */
[Serializable]
class GameData_saver
{
	public float bobbyX_parade;
	public float bobbyY_parade;
}