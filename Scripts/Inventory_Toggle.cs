using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

public class Inventory_Toggle : MonoBehaviour, IPointerClickHandler {

	Button inventoryButton;
	UnityEngine.Canvas inventory_Display;

	// Use this for initialization
	void Start () {
		inventoryButton = GameObject.Find("Inventory_Toggle").GetComponent<Button>();
		inventory_Display = GameObject.Find("Inventory_Display").GetComponent<Canvas>();

		inventory_Display.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// Implementing the pointerClick interface
	public void OnPointerClick (PointerEventData e){
		var curState = inventory_Display.enabled;
		inventory_Display.enabled = !curState;
		Debug.Log("Button Clicked! Previous state was now " + curState.ToString());
	}

}