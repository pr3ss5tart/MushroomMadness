using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    public float speed = 10;

    private Rigidbody2D rb2d;

    public Vector2 jumpForce;

    public Vector2 jumpForceBig = new Vector2(0, 200);

    public Vector2 jumpForceSmall = new Vector2(0, 10);

    Animator anim;

    public int lives; //lives

    public bool isDead; //Checks to see if player is dead.

    public bool isImmune; //This allows player to be immune to damage for a certain time aftert taking damage.

    public float immuneCounter;

    public float immuneTime;

    public bool isBig;

    public bool isNormal;

    public bool isSmall;

    SpriteRenderer playerSprite;

    public bool canMove;

    public bool isGrounded;

    public Transform grounder;
    public float radiuss;
    public LayerMask ground;
    public bool outsideForce;

    public float BaseSpeed;

    // Use this for initialization
    void Start () {

        rb2d = GetComponent<Rigidbody2D>();

        anim = GetComponent<Animator>();

        playerSprite = GetComponent<SpriteRenderer>();

        canMove = true;

        isNormal = true;
    
	}

    // Update is called once per frame
    void Update()
    {

        if (!canMove)
            return;

        if (isImmune)
        {
            immuneCounter -= Time.deltaTime;
        }

        if(immuneCounter <= 0)
        {
            isImmune = false;
            immuneCounter = immuneTime;
        }

        if (Input.GetKey(KeyCode.A)) //Moves player left
        {
            anim.SetBool("Moving", true); //This plays walkcycle whenever player moves

            rb2d.velocity = new Vector2(-speed, rb2d.velocity.y);

            playerSprite.flipX = true;
            
        } else if (Input.GetKey(KeyCode.D)) //Moves player right
        {
            anim.SetBool("Moving", true); //This plays walkcycle whenever player moves

            rb2d.velocity = new Vector2(speed, rb2d.velocity.y);
            playerSprite.flipX = false;
        } else
        {
            anim.SetBool("Moving", false); //This plays walkcycle whenever player stops moving

            rb2d.velocity = new Vector2(0, rb2d.velocity.y);
        }

        isGrounded = Physics2D.OverlapCircle(grounder.transform.position, radiuss, ground);

        //Here is jumping mechanics
        Debug.Log(isGrounded);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            //Big jump
            if(isBig)
            jumpForce = new Vector2(0, 500);
            

            //Normal jump
            if(isNormal)
            jumpForce = new Vector2(0, 400);
            

            //Small Jump
            if(isSmall)
            jumpForce = new Vector2(0, 200);

            Debug.Log(jumpForce);

            rb2d.AddForce(jumpForce, ForceMode2D.Force);
        }

        //Grow Big
        if (Input.GetKeyDown(KeyCode.C) && isNormal)
        {
            isSmall = false;
            isBig = true;
            isNormal = false;
            StartCoroutine(PlayerBig()); //Starts big coroutine
        }

        //Shrink small
        if (Input.GetKeyDown(KeyCode.Z) && isNormal)
        {
            isSmall = true;
            isBig = false;
            isNormal = false;
            StartCoroutine(PlayerSmall()); //Starts small coroutine
        }

        //Stay normal
        if (Input.GetKeyDown(KeyCode.X))
        {
            isBig = false;
            isSmall = false;
            isNormal = true;
        }

        if (isDead)
        {
            Debug.Log("You have died."); //Kills player
            SceneManager.LoadScene(0);
        }
        

    }

    void OnTriggerEnter2D(Collider2D col)
    {
       if(col.tag == "Danger" && !isDead)
        {

            rb2d.velocity = Vector2.zero; // This stops player movement during damage
            Debug.Log("You hit a spike! Oh teh noes!!!");
            lives--;
            rb2d.AddForce(new Vector2(0, 250)); //This applies 500u of force in y axis.

            if (lives > 1 && !isImmune)
            {
                //starts immuneTime
                lives--;
                isImmune = true;
            }

        }
        

        if (lives <= 0)
            isDead = true; 
    }

    void OnGUI()
    {
        GUILayout.Label("  lives:  " + lives);
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
}
