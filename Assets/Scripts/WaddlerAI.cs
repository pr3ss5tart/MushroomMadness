using UnityEngine;
using System.Collections;

public class WaddlerAI : MonoBehaviour {

    //I am a change. I need to be committed and pushed to the dev branch.

    public float velocity;

    //I am a conflicting change with the MainDevelop branch

    //I am another conflict

    public Transform sightStart;
    public Transform sightEnd;

    public bool isColliding;
    //jknkmkmkmkm
    Rigidbody2D waddlerBody;

	// Use this for initialization
	void Start () {
        waddlerBody = this.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {

        waddlerBody.velocity = new Vector2(velocity, waddlerBody.velocity.y); //This keeps the enemy walking to the right, unless it collides w/object
        isColliding = Physics2D.Linecast(sightStart.position, sightEnd.position);

        if(isColliding)
        {
            transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
            velocity *= -1;
        }
	}
}
