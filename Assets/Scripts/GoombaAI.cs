using UnityEngine;
using System.Collections;

public class GoombaAI : MonoBehaviour {

    public Transform[] bossSpots; //This array holds points where the boss moves

    public float speed;

    public Transform Target;

    public GameObject projectile;

	// Use this for initialization
	void Start () {
        StartCoroutine("boss");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator boss()
    {
        //First Attack

        while(transform.position.x != bossSpots[0].position.x)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(bossSpots[0].position.x, transform.position.y), speed);

            yield return null;
        }

        transform.localScale = new Vector2(-1, 1);

        yield return new WaitForSeconds(4);

        int i = 0;
        while (i < 6)
        {
           // Instantiate(projectile, hole, Random.Range);

            i++;

           yield return new WaitForSeconds(.7f);
        }

        yield return null;
    }
}
