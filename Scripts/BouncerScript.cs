using UnityEngine;
using System.Collections;

public class BouncerScript : MonoBehaviour {


	public float new_x;
	public float new_y;
	public int when_to_move;

	public float delta_x;
	private float delta_y;
	
	private bool moved;
	// Use this for initialization
	void Start () {

		delta_x = -(gameObject.transform.position.x - new_x)/100;
		delta_y = -(gameObject.transform.position.y - new_y)/100;

		if(GameObject.Find ("OWorld").GetComponent<OWorld> ().state >= when_to_move)
		{
			gameObject.transform.position = new Vector2(new_x, new_y);
		}
	}

	// Update is called once per frame
	void Update () {
		if (GameObject.Find ("OWorld").GetComponent<OWorld> ().state >= when_to_move)
		{

			if((gameObject.transform.position.x <= new_x - .3 || gameObject.transform.position.x >= new_x + .3))
			{
				gameObject.transform.position = new Vector2(gameObject.transform.position.x + delta_x, gameObject.transform.position.y);
			}

			if((gameObject.transform.position.y <= new_y - .3 || gameObject.transform.position.y >= new_y + .3))
			{
				gameObject.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + delta_y);
			}

			/*if(gameObject.transform.position != new Vector3(new_x, new_y, 0))
			{
				gameObject.transform.position = new Vector2(gameObject.transform.position.x + delta_x, gameObject.transform.position.y + delta_y);
			}*/
		}
	}
}
