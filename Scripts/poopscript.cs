using UnityEngine;
using System.Collections;

public class poopscript : MonoBehaviour {

	private Animator animator;
	//private int set = 0;
	public string action = "";
	// Use this for initialization
	void Start () {
		animator = this.GetComponent<Animator>();
		action = "";
	}
	
	// Update is called once per frame
	void Update () {
		animator.SetInteger("State", 0);
		//animator.speed = 1;
		//Debug.Log (animator.speed);
		//set = 0;


		if (Input.GetKeyDown (KeyCode.A)) {
			action = "Pause";
				}
		else if (Input.GetKeyDown (KeyCode.S)) {
			action = "Attack";
		}
		else if (Input.GetKeyDown (KeyCode.D)) {
			action = "Magic";
		}
		else if (Input.GetKeyDown (KeyCode.F)) {
			action = "Defend";
		}
		
		//Debug.Log (action);
		if(action == "Attack")
		{
			//animator.speed = 1;
			animator.SetInteger("State", 1);
			action = "";
		}
		else if(action == "Magic")
		{
			animator.SetInteger("State", 2);
			action = "";
		}
		else if(action == "Defend")
		{
			animator.SetInteger("State", 3);
			action = "";
		}
		else if(action == "Down")
		{
			animator.SetInteger("State", 4);
			action = "";
		}
		else if(action == "Up")
		{
			animator.SetInteger("State", 5);
			action = "";
		}
		else if(action == "Pause")
		{
			animator.speed = 0.001f;
			//animator.SetInteger("State", -1);
		}
		else if(action == "Resume")
		{
			animator.SetInteger("State", 0);
			//animator.speed = 1;
		}
	}
}
