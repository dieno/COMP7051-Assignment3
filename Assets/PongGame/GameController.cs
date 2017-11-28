using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public Text player1ScoreText;
    public Text player2ScoreText;
    public Text GameOverText;
    public Text WinnerText;
    public int player1Score;
    public int player2Score;

    public bool isPlaying;
    public bool isPaused;
    private bool gameOver;
    //private bool restart;

    public GameObject ball;
    private BallController bc;

    public GameObject leftPlayer;
    private LeftPlayerController lpc;

    public GameObject rightPlayer;
    private RightPlayerController rpc;

    private MenuController mc;

    private ConsoleController cc;

    // Use this for initialization
    void Start() {
        gameOver = false;
        isPlaying = false;
        isPaused = false;

        GameOverText.text = "";
        WinnerText.text = "";
        player1ScoreText.text = "";
        player2ScoreText.text = "";

        bc = ball.GetComponent<BallController>();
        lpc = leftPlayer.GetComponent<LeftPlayerController>();
        rpc = rightPlayer.GetComponent<RightPlayerController>();

        mc = GetComponent<MenuController>();
        cc = GetComponent<ConsoleController>();

        StartGame();
        isPlaying = true;
    }

    // Update is called once per frame
    void Update() {
        if (isPlaying == false)
        {
            if (gameOver == false  && mc.isOpen == false)
            {
                StartGame();
                isPlaying = true;
            }
            if (gameOver == true && mc.isOpen == false)
            {
                if(Input.GetKeyDown("return") || Input.GetButtonDown("Submit"))
                {
                    StartGame();
                    isPlaying = true;
                }
                else if(Input.GetKeyDown("escape") || Input.GetButtonDown("Cancel"))
                {
                    EndGame();
                }
            }
        }
        else
        {
            if (Input.GetKeyDown("escape") || Input.GetButtonDown("Cancel"))
            {
                if (isPaused)
                {

                    ResumeGame();
                    cc.CloseConsole();
                    
                }
                else
                {
                    EndGame();
                }
            }
            else if ((Input.GetKeyDown("c")) && !isPaused)
            {
                PauseGame();
                cc.OpenConsole();
                
            }
            else if (player1Score > 4)
            {
                WinnerText.text = "Player 1 Wins!";
                GameOver();
            }
            else if (player2Score > 4)
            {
                WinnerText.text = "Player 2 Wins!";
                GameOver();
            }
        }
    }

    public void GameOver()
    {
        /*
        GameOverText.text = "GAME OVER";
        bc.GameOver();
        isPlaying = false;
        lpc.isPlaying = false;
        lpc.Reset();
        rpc.isPlaying = false;
        rpc.Reset();
        gameOver = true;
        */

        SceneManager.LoadScene("RyanScene");
    }

    public void EndGame()
    {
        /*
        gameOver = false;
        bc.GameOver();
        isPlaying = false;
        lpc.isPlaying = false;
        lpc.Reset();
        rpc.isPlaying = false;
        rpc.Reset();
        GameOverText.text = "";
        WinnerText.text = "";
        player1ScoreText.text = "";
        player2ScoreText.text = "";
        mc.OpenMenu();
        */

        SceneManager.LoadScene("RyanScene");
    }

    public void StartGame()
    {
        gameOver = false;
        GameOverText.text = "";
        WinnerText.text = "";
        player1Score = 0;
        UpdatePlayer1Score();
        player2Score = 0;
        UpdatePlayer2Score();
        bc.speed = 10;

        lpc.isPlaying = true;
        rpc.isPlaying = true;

        if (mc.menuItemSelected == 0)
        {
            lpc.isSinglePlayer = true;
            rpc.isHumanPlayer = false;
        }
        else
        {
            lpc.isSinglePlayer = false;
            rpc.isHumanPlayer = true;
        }

        bc.Serve();
    }

    private void PauseGame()
    {
        isPaused = true;
        lpc.isPlaying = false;
        rpc.isPlaying = false;
        bc.PauseBall();
    }

    private void ResumeGame()
    {
        isPaused = false;
        lpc.isPlaying = true;
        rpc.isPlaying = true;
        bc.ResumeBall();
    }

    public void IncrementPlayer1Score()
    {
        player1Score++;
        UpdatePlayer1Score();
    }

    void UpdatePlayer1Score() {
        player1ScoreText.text = "Player 1 Score: " + player1Score;
    }

    public void IncrementPlayer2Score()
    {
        player2Score++;
        UpdatePlayer2Score();
    }

    void UpdatePlayer2Score()
    {
        player2ScoreText.text = "Player 2 Score: " + player2Score;
    }

    private bool controllerAccept()
    {
        if(Application.platform == RuntimePlatform.PS4)
        {
            return Input.GetKeyDown("joystick button 1");
        }
        else
        {
            return Input.GetKeyDown("joystick button 0");
        }
    }
}
