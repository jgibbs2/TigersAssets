﻿using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.Generic;
using UnityEngine.UI;

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

	List<QuestItem> playerInventory;
	Canvas inventory_Display;


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

			// Set Up
			DontDestroyOnLoad (gameObject);
			access = this;
			Load ();

			// Set player inventory to be the Default config
			inventory_Display = GameObject.Find("Inventory_Display").GetComponent<Canvas>();

			playerInventory = defaultQuestItems();
			int row = 0; int col = 0;
			foreach(QuestItem item in playerInventory){
				makeNewImage(item, row, col);

				if (col == 2)
				{
					row++;
					col = 0;
				}else{
					col++;
				}
			}

		} else {
			// If a GameData already exists, don't make a new one
			Destroy(gameObject);
		}
	}

	private void makeNewImage(QuestItem item, int row, int col){

		// Create a new object from the prefab questItemPic
		GameObject newItem = (GameObject)Instantiate(Resources.Load("UI/questItemPic"));
		newItem.name = item.name;
		var image = newItem.GetComponent<Image>();

		// Set the properties of the image
		image.sprite = item.image;
		image.transform.SetParent(inventory_Display.transform);
		image.transform.localScale = new Vector3(1,1,1);

		image.transform.localPosition = new Vector3(col * 100, row * 100, 1);
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

	private List<QuestItem> defaultQuestItems(){

		List<QuestItem> defaultInventory = new List<QuestItem>();

		for(int i = 0; i < 9; i++){
			defaultInventory.Add(new QuestItem(
				"Test" + i, 																// Name
				"This is a test item", 													// Description
				Resources.Load<Sprite>("UI/url"),									    // Image 
				false));																// Picked Up?
		}
		return defaultInventory;
	}


}

class QuestItem{

	public string name;
	public string description;
	public bool pickedUp;
	public Sprite image;

	public QuestItem(string name, string description, Sprite image, bool pickedUp){
		this.name = name;
		this.description = description;
		this.image = image;
		this.pickedUp = pickedUp;
	}

	public string ToString(){
		return name + " " + description;
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