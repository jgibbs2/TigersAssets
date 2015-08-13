using UnityEngine;
using System.Collections;

public class ChaseScript : MonoBehaviour {


	public bool chase;
	private bool found_bobby;
	private float destination_x;
	private float destination_y;

	private float delta_x;
	private float delta_y;

	// Use this for initialization
	void Start () {
		chase = false;
		found_bobby = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(GetComponentInParent<MinotaurCutScene>().moving== true)
		{
			chase = true;
		}
		if(chase == true)
		{
			if(!found_bobby)
			{
				destination_x = GameObject.Find("Bobby").transform.position.x;
				destination_y = GameObject.Find("Bobby").transform.position.y;

				delta_x = -(gameObject.transform.position.x - destination_x)/100;
				delta_y = -(gameObject.transform.position.y - destination_y)/100;
			}

			if((gameObject.transform.position.x <= destination_x - .3 || gameObject.transform.position.x >= destination_x + .3))
			{
				gameObject.transform.position = new Vector2(gameObject.transform.position.x + delta_x, gameObject.transform.position.y);
			}
			
			if((gameObject.transform.position.y <= destination_y - .3 || gameObject.transform.position.y >= destination_y + .3))
			{
				gameObject.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + delta_y);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.name == "Bobby")
		{
			Destroy(transform.parent.gameObject);
			//Application.LoadLevel("TestScene");
		}
	}
}
