using UnityEngine;
using System.Collections;

public class HealthSprite : MonoBehaviour {

	public int HP;
	float x;
	float y;
	// Use this for initialization
	void Start () {
		//setHP
		x = this.transform.position.x;
		y = this.transform.position.y;
		HP = 100;
	}

	public void LoseHealth(int damage)
	{
		float dam = damage / (float)HP;
		float dif = this.transform.localScale.x - dam;
		if(dif < 0)
		{
			this.transform.localScale -= new Vector3(this.transform.localScale.x, 0, 0);
		}
		else{
			this.transform.localScale -= new Vector3(dam, 0, 0);
		}
		this.transform.position = new Vector2(x, y);
	}
	
	// Update is called once per frame
	void Update () {
	}
}
