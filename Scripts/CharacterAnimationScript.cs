using UnityEngine;
using System.Collections;

public class CharacterAnimationScript : MonoBehaviour {

	private Animator animator;
	//private int set = 0;
	public string action;
	// Use this for initialization
	void Start () {
		animator = this.GetComponent<Animator>();
		action = "";
	}
	
	// Update is called once per frame
	void Update () {
		animator.SetInteger("State", 0);
		//animator.speed = 1;
		Debug.Log (animator.speed);
		//set = 0;


		if(action == "Attack")
		{
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
			animator.SetInteger("State", -1);
		}
		else if(action == "Resume")
		{
			animator.SetInteger("State", 0);
			animator.speed = 1;
		}



		/*var vertical = Input.GetAxis("Vertical");
		var horizontal = Input.GetAxis("Horizontal");

		/*if(set !=0)
		{
			set = 0;
		}
		else if (vertical > 0)
		{
			animator.SetInteger("State", 2);
			set = 1;
		}
		else if (vertical < 0)
		{
			animator.SetInteger("State", 3);
		}
		else if (horizontal > 0)
		{
			animator.SetInteger("State", 1);
			set = 1;
		}
		else if (horizontal < 0)
		{
			animator.SetInteger("State", 4);
			set = 1;
		}

		if(*/

	}
}
