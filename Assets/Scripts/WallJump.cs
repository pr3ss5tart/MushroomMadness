using UnityEngine;
using System.Collections;

public class WallJump : MonoBehaviour {

    PlayerController movement;

    public float distance = 1f;
    public float speed = 2f;
    
	// Use this for initialization
	void Start () {
        movement = GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {

        Physics2D.queriesStartInColliders = false;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, distance);

        if(Input.GetKeyDown(KeyCode.Space) && !movement.isGrounded)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(speed*transform.localScale.x, speed);
        }
	}
}
