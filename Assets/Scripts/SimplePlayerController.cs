using UnityEngine;
using System.Collections;

public class SimplePlayerController : MonoBehaviour {

    //Base version of code
    public float moveSpeed;
    public float jumpHeight;
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    public bool canDoubleJump;

    private bool grounded;
    private bool doubleJumped;

    //Added on later (Size adjustment mechanic)
    public float jumpHeightSmall;
    public float jumpHeightBig;

    public bool isNormal;
    public bool isSmall;
    public bool isBig;

	// Use this for initialization
	void Start () {
        isNormal = true;
        isBig = false;
        isSmall = false;
        canDoubleJump = false;
	}

    void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }
	
	// Update is called once per frame
	void Update () {
        if (grounded && canDoubleJump)
            doubleJumped = false;

        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            Jump();
            //GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpHeight);
        }

        //This code is for the double jump
        if (Input.GetKeyDown(KeyCode.Space) &&!doubleJumped && !grounded)
        {
            Jump();
            //GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpHeight);
            doubleJumped = true;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
        }

        //Size adjustment mechanic
        if (Input.GetKeyDown(KeyCode.Z)) //This shrinks the player
        {
            isSmall = true;
            isNormal = false;
            isBig = false;
            StartCoroutine(PlayerSmall());
        }

        if (Input.GetKeyDown(KeyCode.X)) //This returns the player to normal size.
        {
            isSmall = false;
            isNormal = true;
            isBig = false;
           // StartCoroutine(PlayerNormal());
        }

        if (Input.GetKeyDown(KeyCode.C)) //This grows the player
        {
            isSmall = false;
            isNormal = false;
            isBig = true;
            StartCoroutine(PlayerBig());
        }
    }

    public void Jump()
    {
       if(isNormal)
           GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpHeight);

       if(isSmall)
           GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpHeightSmall);

        if (isBig)
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpHeightBig);

    }

    IEnumerator PlayerBig()
    {
        transform.localScale = new Vector3(2, 2, 0); //grows player
        yield return new WaitWhile(() => { return isBig; }); //runs coroutine while isBig = true
        transform.localScale = Vector3.one;
    }

    IEnumerator PlayerSmall()
    {
        transform.localScale = new Vector3(0.5f, 0.5f, 0); //shrinks player
        yield return new WaitWhile(() => { return isSmall; }); //runs coroutine while isSmall = true;
        transform.localScale = Vector3.one;
    }

   /* IEnumerator PlayerNormal()
    {
        transform.localScale = new Vector3(2f, 1f, 0); //normalizes player
        yield return new WaitWhile(() => { return isNormal ; }); //runs coroutine while isNormal = true;
        transform.localScale = Vector3.one;
    } */
}
