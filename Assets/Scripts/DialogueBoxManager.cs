using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DialogueBoxManager : MonoBehaviour {

    public GameObject textBox;

    public Text theText;

    public TextAsset textFile;
    public string[] textLines;

    public int currentLine;
    public int endAtLine;

    public PlayerController player;

    public bool isActive;

    public bool stopPlayerMovement;

	// Use this for initialization
	void Start () {

        //player = GetComponent<PlayerController>();

        player = GameObject.FindObjectOfType<PlayerController>();

        if(textFile != null)
        {
            textLines = (textFile.text.Split('\n'));
        }

        if(endAtLine == 0)
        {
            endAtLine = textLines.Length - 1;
        }

        if (isActive)
        {
            EnableTextBox();
        } else
        {
            DisableTextBox();
        }
	}
	
	// Update is called once per frame
	void Update () {

        //If textbox is not active, then don't run code below return.
        if (!isActive)
        {
            return; 
        }

        theText.text = textLines[currentLine]; //Start at 0, then move on.

        if (Input.GetKeyDown(KeyCode.Return))
        {
            currentLine += 1;
        }

        if(currentLine > endAtLine)
        {
            DisableTextBox();
        }
	}

    public void EnableTextBox()
    {
        textBox.SetActive(true); //Activates text box
        isActive = true;

        if (stopPlayerMovement)
        {
            player.canMove = false;
        }
    }

    public void DisableTextBox()
    {
        textBox.SetActive(false); //Kills box if reaches end of text.
        isActive = false;
    }

    public void ReloadScript(TextAsset theText)
    {
        if(theText != null)
        {
            textLines = new string[1]; //pops in new script
            textLines = (theText.text.Split('\n'));
        }
    }
}
