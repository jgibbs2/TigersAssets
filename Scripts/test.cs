using UnityEngine;
using System.Collections;

public class test : MonoBehaviour {
	public Animator anim;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.W))
		{
			anim.SetInteger("State", 3);
		}
		else if(Input.GetKeyDown(KeyCode.A))
		{
			anim.SetInteger("State", 1);
		}
		else if(Input.GetKeyDown(KeyCode.S))
		{
			anim.SetInteger("State", 0);
		}
		else if(Input.GetKeyDown(KeyCode.D))
		{
			anim.SetInteger("State", 2);
		}
	}
}
