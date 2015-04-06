using UnityEngine;
using System.Collections;

public class Fire : MonoBehaviour {
	public Animation anim;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if(!anim.isPlaying)
		{
			Destroy(this);
		}
	}
}
