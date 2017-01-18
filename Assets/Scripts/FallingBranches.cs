using UnityEngine;
using System.Collections;

public class FallingBranches : MonoBehaviour {

    private Rigidbody2D rb2d;

    public float fallDelay;

    

	// Use this for initialization
	void Start () {
        rb2d = GetComponent<Rigidbody2D>(); //Calls RigidBody2D
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Player"))
        {
            PlayerController player = col.gameObject.GetComponent<PlayerController>();
           if (!player.isSmall)
            {
                StartCoroutine(Fall());
            }
            

        }
    }

    IEnumerator Fall()
    {
        yield return new WaitForSeconds(fallDelay);

        rb2d.isKinematic = false;

        GetComponent<Collider2D>().isTrigger = true; //Allows platform to pass through floor
    }
	
	// Update is called once per frame
	void Update () {
	    
           
	}
}
