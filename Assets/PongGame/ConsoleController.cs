using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsoleController : MonoBehaviour {

    //public GameObject background;
   // public InputField inputField;
    public Text consoleOutput;
    public Text consoleCursor;

    public Text consoleInputField;

    private bool isOpen;
    private bool isOpening;

    private string[] consoleText;
    private int numLines;

    public GameObject backgroundQuad;
    public GameObject backgroundPanel;
    public GameObject ball;

    private BallController bc;

    private Material backgroundMaterial;

	// Use this for initialization
	void Start () {
        //background.SetActive(false);
        consoleOutput.gameObject.SetActive(false);
        consoleCursor.gameObject.SetActive(false);
        backgroundPanel.gameObject.SetActive(false);
        //inputField.gameObject.SetActive(false);
        //inputField.DeactivateInputField();

        isOpening = false;


        consoleInputField.gameObject.SetActive(false);
        consoleInputField.text = "";

        isOpen = false;
        consoleOutput.text = "";
        numLines = 0;
        consoleText = new string[8];

        backgroundMaterial = backgroundQuad.GetComponent<Renderer>().material;
        bc = ball.GetComponent<BallController>();
    }
	
	// Update is called once per frame
	void Update () {
        if(isOpen)
        {
            if(!Input.anyKeyDown)
            {
                isOpening = false;
            }


            if(Input.GetKeyDown("return"))
            {
                
                if (numLines == 7)
                {
                    ScrollText();
                    numLines--;
                } else {
                   // numLines++;
                }

                //ProcessCommand(inputField.text);
                ProcessCommand(consoleInputField.text);

                numLines++;


                //inputField.text = "";
                //inputField.ActivateInputField();
                consoleInputField.text = "";
            }

            if(!Input.GetKeyDown("return") && Input.anyKeyDown && !isOpening)
            {
                consoleInputField.text += Input.inputString;
            }

            if(Input.GetKeyDown(KeyCode.Delete) || Input.GetKeyDown(KeyCode.Backspace))
            {
                if(consoleInputField.text.Length > 1)
                {
                    string temp = consoleInputField.text.Substring(0, consoleInputField.text.Length - 2);
                   
                    consoleInputField.text = temp;
                }
                
            }


            consoleOutput.text = "";

            foreach (string s in consoleText)
            {
                consoleOutput.text += s;
                consoleOutput.text += "\n";
            }
        }
    }

    private void ProcessCommand(string command)
    {
        string[] parts = command.Split(' ');

        
        switch (parts[0])
        {
            case "background":
                if(parts.Length > 1)
                {
                    switch (parts[1])
                    {
                        case "red":
                            consoleText[numLines] = "Changing background color to red.";
                            backgroundMaterial.color = Color.red;
                            break;
                        case "blue":
                            consoleText[numLines] = "Changing background color to blue.";
                            backgroundMaterial.color = Color.blue;
                            break;
                        case "green":
                            consoleText[numLines] = "Changing background color to green.";
                            backgroundMaterial.color = Color.green;
                            break;
                        case "":
                            consoleText[numLines] = "Color not specified: " + parts[1];
                            break;
                        default:
                            consoleText[numLines] = "Color not supported: " + parts[1];
                            break;
                    }
                }
                else
                {
                    consoleText[numLines] = "Color not specified.";
                }
                
                break;
            case "speed":
                if (parts.Length > 1)
                {
                    switch (parts[1])
                    {
                        case "slow":
                            consoleText[numLines] = "Changing speed to slow.";
                            bc.speed = 5;
                            bc.recalculateVelocity();
                            break;
                        case "normal":
                            consoleText[numLines] = "Changing speed to normal.";
                            bc.speed = 10;
                            bc.recalculateVelocity();
                            break;
                        case "fast":
                            consoleText[numLines] = "Changing speed to fast.";
                            bc.speed = 15;
                            bc.recalculateVelocity();
                            break;
                        case "":
                            consoleText[numLines] = "Speed not specified: " + parts[1];
                            break;
                        default:
                            consoleText[numLines] = "Speed not supported: " + parts[1];
                            break;
                    }
                }
                else
                {
                    consoleText[numLines] = "Speed not specified.";
                }
                break;
            case "":
            default:
                consoleText[numLines] = "Command not found: " + command;
                break;
        }


    }

    private void ScrollText()
    {
        for(int i = 0; i < numLines; i++)
        {
            consoleText[i] = consoleText[i + 1];
        }
    }

    public void OpenConsole()
    {
        //inputField.gameObject.SetActive(true);
        //inputField.Select();
        //inputField.ActivateInputField();
        //background.SetActive(true);
        consoleInputField.gameObject.SetActive(true);
        backgroundPanel.gameObject.SetActive(true);
        consoleOutput.gameObject.SetActive(true);
        consoleCursor.gameObject.SetActive(true);
        isOpening = true;
        isOpen = true;
    }

    public void CloseConsole()
    {
        //inputField.DeactivateInputField();
        //inputField.gameObject.SetActive(false);
        consoleInputField.gameObject.SetActive(false);
        //background.SetActive(false);
        backgroundPanel.gameObject.SetActive(false);
        consoleOutput.gameObject.SetActive(false);
        consoleCursor.gameObject.SetActive(false);
        isOpen = false;
    }
}
