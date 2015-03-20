using UnityEngine;
using System.Collections;

public class NPCMovement : MonoBehaviour {


	//distance_travelled
	//direction travelling
	private Vector2 direction_travelling;
	private float initial_pause_time;
	private float total_pause_time;
	private float distance_travelled;
	private float total_distance;
	private bool moving;
	private bool pause;
	private int speed = 3;

	// Use this for initialization
	void Start () {
		moving = false;
	}
	
	// Update is called once per frame
	void Update () {

		if(moving)
		{
			transform.Translate(direction_travelling * speed * Time.deltaTime);
			if(direction_travelling == Vector2.up || direction_travelling == -Vector2.up)
			{
				distance_travelled = distance_travelled + Mathf.Abs(direction_travelling.y * speed * Time.deltaTime);
			}
			else{
				distance_travelled = distance_travelled + Mathf.Abs(direction_travelling.x * speed * Time.deltaTime);
			}

			if(distance_travelled>= total_distance)
			{
				moving = false;
				distance_travelled = 0.0f;
				total_distance = 0.0f;
				pause = true;
				initial_pause_time = Time.time;
				total_pause_time = Random.Range (1.0f, 3.0f);
			}
		}
		else if(pause)
		{
			if(Time.time - initial_pause_time >= total_pause_time)
			{
				pause = false;
			}
		}
		else
		{
			total_distance = Random.Range(0.5f, 3.0f);
			int num = Random.Range (0, 4);
			if(num == 0)
			{
				direction_travelling = Vector2.up;
			}
			else if(num == 1)
			{
				direction_travelling = -Vector2.up;
			}
			else if(num == 2)
			{
				direction_travelling = Vector2.right;
			}
			else if(num == 3)
			{
				direction_travelling = -Vector2.right;
			}
			moving = true;
		}
		//if moving
			//continue moving
			//if reached the end
				//moving is false
		//else
			//pick a distance
			//pick a direction
			//moving to true
	}

	void OnCollisionEnter2D(Collision2D col) 
	{
		//Debug.Log ("FRAT");
		//moving = false;
		direction_travelling = -direction_travelling;
	}
}


