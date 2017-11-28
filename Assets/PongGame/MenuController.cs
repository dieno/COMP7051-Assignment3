using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

    public Text singlePlayerMenuItem;
    public Text multiplayerMenuItem;

    public GameObject bg;

    public int menuItemSelected;
    public bool isOpen;

	// Use this for initialization
	void Start () {
        menuItemSelected = 0;
        OpenMenu();

        CloseMenu();
    }

    // Update is called once per frame
    void Update() {
        if (isOpen)
        {
            if (Input.GetKeyDown("return") || Input.GetButtonDown("Submit"))
            {
                CloseMenu();
            }

            if (Input.GetKeyDown("up") || Input.GetAxis("Vertical1") > 0.25)
            {
                menuItemSelected = 0;
            }
            else if (Input.GetKeyDown("down") || Input.GetAxis("Vertical1") < -0.25)
            {
                menuItemSelected = 1;
            }

            selectMenuItem(menuItemSelected);
        }
    }


    private void selectMenuItem(int selection)
    {
        switch(selection)
        {
            case 0:
                resetMenuItems();
                singlePlayerMenuItem.fontSize = 35;
                singlePlayerMenuItem.color = Color.yellow;
                break;
            case 1:
                resetMenuItems();
                multiplayerMenuItem.fontSize = 35;
                multiplayerMenuItem.color = Color.yellow;
                break;
            default:
                resetMenuItems();
                singlePlayerMenuItem.fontSize = 35;
                singlePlayerMenuItem.color = Color.yellow;
                break;
        }
    }

    private void resetMenuItems()
    {
        singlePlayerMenuItem.fontSize = 30;
        multiplayerMenuItem.fontSize = 30;
        singlePlayerMenuItem.color = Color.white;
        multiplayerMenuItem.color = Color.white;
    }

    public void OpenMenu()
    {
        singlePlayerMenuItem.text = "Single Player";
        multiplayerMenuItem.text = "Multiplayer";
        bg.SetActive(true);
        isOpen = true;
    }

    private void CloseMenu()
    {
        singlePlayerMenuItem.text = "";
        multiplayerMenuItem.text = "";
        bg.SetActive(false);
        isOpen = false;
    }

    private bool controllerAccept()
    {
        if (Application.platform == RuntimePlatform.PS4)
        {
            return Input.GetKeyDown("joystick button 1");
        }
        else
        {
            return Input.GetKeyDown("joystick button 0");
        }
    }
}
