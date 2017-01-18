using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class SwitchScript : MonoBehaviour {

    public GameObject[] gates;
    IEnumerator currentGate;
    Vector3 maxUp;

    GameManager manager;
	// Use this for initialization

//This code is done by Christain Basiga v
    void getGates()
    {
        Debug.Log(GameObject.FindGameObjectsWithTag("gate").Length);
        string baseName = "Gate_";
        for (int i = 1; i < GameObject.FindGameObjectsWithTag("gate").Length+1; i++)
        {
            gates[i - 1] = GameObject.Find(baseName + i);
        }
    }
    void Awake()
    {
        manager = GameObject.Find("GameManager").GetComponent<GameManager>(); //Finds game manager
       
    }
	void Start () {

       
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        
        if (col.collider.CompareTag("Player"))
        {
            PlayerController player = col.gameObject.GetComponent<PlayerController>();
            if (player.isBig)
            {
                Debug.Log("You pressed the switch");
                if (!manager.gateMoving && this.name[this.name.Length - 1] == manager.gate.Current.name[manager.gate.Current.name.Length - 1]) 
                    StartCoroutine(manager.MoveGate());
            }

        
        }
    }
  
}
