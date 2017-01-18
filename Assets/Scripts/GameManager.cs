using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class GameManager : MonoBehaviour {


    public List<GameObject> gates;
    public IEnumerator<GameObject> gate;
    float maxUp;
    // Use this for initialization

    //This code is done by Christain Basiga v
    void getGates()
    {
        string baseName = "Gate_";
        for (int i = 1; i < GameObject.FindGameObjectsWithTag("gate").Length + 1; i++)
        {
            gates.Add(GameObject.Find(baseName + i));
        }
    }
    
    public bool gateMoving
    { get; set; }
  public  IEnumerator MoveGate()
    {
        Vector3 initPos = gate.Current.transform.position;
        gateMoving = true;
        do
        {
            
            gate.Current.transform.Translate(Vector3.up * 4 * Time.deltaTime);
            yield return new WaitForEndOfFrame();

        } while (gate.Current.transform.position.y <= initPos.y + maxUp);

        gate.MoveNext();
        gateMoving = false;
    }

    void Awake()
    {
        gates = new List<GameObject>();

    }

    // Use this for initialization
    void Start () {
        maxUp = 5.0f;
        getGates();
        gate = gates.GetEnumerator();
        gate.MoveNext();
    }

    // Update is called once per frame
    void Update () {
	
	}


}
