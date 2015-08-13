using UnityEngine;
using System.Collections;

public class HealthSprite : MonoBehaviour {

	public int HP;
	float x;
	float y;
	Vector2 scale;
	// Use this for initialization
	void Start () {

	}

	public void HealthInit(int h) //called after characters are selected and moved to their positions
	{
		//setHP
		HP = h;
		x = this.transform.position.x;
		y = this.transform.position.y;
		scale = new Vector2 (this.transform.localScale.x, this.transform.localScale.y);
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
		GetComponentInParent<Stats> ().setHealth (damage);//make this negative in order to 
	}

	public void GainHealth(int health)
	{
		float heal = health / (float)HP;
		float dif = this.transform.localScale.x + heal;
		if(dif < 0)
		{
			this.transform.localScale += new Vector3(this.transform.localScale.x, 0, 0);
		}
		else{
			this.transform.localScale = scale;
		}
		this.transform.position = new Vector2(x, y);
		GetComponentInParent<Stats> ().setHealth (health);//update the health in stats.cs for the character.
	}
	
	// Update is called once per frame
	void Update () {
	}
}
