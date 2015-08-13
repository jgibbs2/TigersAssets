using UnityEngine;
using System.Collections;

public class CharacterAnimationScript : MonoBehaviour {

	private Animator animator;
	//private int set = 0;
	public string action = "";
	public bool playing;

	private float start_time;
	private float end_time;

	// Use this for initialization
	void Start () {
		animator = this.GetComponent<Animator>();
		action = "";
	}
	
	// Update is called once per frame
	void Update () {
		animator.SetInteger("State", 0);

		if(Time.time - start_time >= end_time)
		{
			playing = false;
		}

		if(action != "")
		{
			if(action == "Attack")
			{
				animator.SetInteger("State", 1);
			}
			else if(action == "Magic")
			{
				animator.SetInteger("State", 2);
			}
			else if(action == "Defend")
			{
				animator.SetInteger("State", 3);
			}
			else if(action == "Down")
			{
				animator.SetInteger("State", 4);
			}
			else if(action == "Up")
			{
				animator.SetInteger("State", 5);
			}
			action = "";
			playing = true; // playing something other than the idle animation
			end_time = animator.GetCurrentAnimatorStateInfo(0).length;
			start_time = Time.time;
		}
	}
}
