using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstPersonController : MonoBehaviour
{

    public float movementSpeed = 5.0f;
    public float mouseSesitivity = 5.0f;
    public float controllerSensitivity = 5.0f;
    public float UpDownRange = 60.0f;

    public Light flashlightLight;

    public GameObject ballPrefab;
    public Transform ballSpawn;

    private CharacterController cc;
    private Camera mCamera;
    private float verticalRotation = 0f;

    private Vector3 originalPos;
    private Vector3 originalrot;
    private bool noClip = false;

    private const float BALL_SPEED = 12f;

    // Use this for initialization
    void Start()
    {
        cc = GetComponent<CharacterController>();
        mCamera = Camera.main;
        originalPos = transform.position;
        originalrot = transform.rotation.eulerAngles;
        Cursor.lockState = CursorLockMode.Locked;

        /*PlayerPrefs.SetFloat("posX", originalPos.x);
        PlayerPrefs.SetFloat("posY", originalPos.y);
        PlayerPrefs.SetFloat("posZ", originalPos.z);
        PlayerPrefs.SetFloat("rotX", originalrot.x);
        PlayerPrefs.SetFloat("rotY", originalrot.y);
        PlayerPrefs.SetFloat("rotZ", originalrot.z);

        LoadPosition();*/
    }

    // Update is called once per frame
    void Update()
    {

        //Commands
        if (Input.GetKeyDown(KeyCode.Home) || Input.GetButtonDown("Options"))
        {
            transform.position = originalPos;
            transform.rotation = Quaternion.Euler(originalrot);
            mCamera.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
        }

        if (Input.GetKeyDown(KeyCode.W) || Input.GetButtonDown("Triangle"))
        {
            noClip = !noClip;
        }
        Physics.IgnoreLayerCollision(8, 9, noClip);

        if (Input.GetKeyDown(KeyCode.R))//|| Input.GetButtonDown("Triangle"))
        {
            flashlightLight.enabled = !flashlightLight.enabled;
        }

        //Rotation
        float rotLeftRight = Input.GetAxis("Mouse X") * mouseSesitivity;
        rotLeftRight += Input.GetAxis("RightJoystickX") * controllerSensitivity;

        transform.Rotate(0f, rotLeftRight, 0f);


        verticalRotation -= Input.GetAxis("Mouse Y") * mouseSesitivity;
        verticalRotation -= Input.GetAxis("RightJoystickY") * controllerSensitivity;

        verticalRotation = Mathf.Clamp(verticalRotation, -UpDownRange, UpDownRange);
        mCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);

        //Movement
        float forwardSpeed = Input.GetAxis("Vertical") * movementSpeed;
        float sideSpeed = Input.GetAxis("Horizontal") * movementSpeed;

        Vector3 speed = new Vector3(sideSpeed, 0f, forwardSpeed);
        speed = transform.rotation * speed;

        cc.SimpleMove(speed);

        if (Input.GetKeyDown(KeyCode.Space))// || Input.GetButtonDown("Options"))
        {
            ShootBall();
        }

    }

    void ShootBall()
    {
        // Create the Bullet from the Bullet Prefab
        var ball = (GameObject)Instantiate(
            ballPrefab,
            ballSpawn.position,
            ballSpawn.rotation);

        // Add velocity to the bullet
        ball.GetComponent<Rigidbody>().velocity = ball.transform.forward * BALL_SPEED;

        // Destroy the bullet after 2 seconds
        Destroy(ball, 2.0f);
    }

    void LoadPosition()
    {

        Vector3 pos = new Vector3(
            PlayerPrefs.GetFloat("posX", originalPos.x),
            PlayerPrefs.GetFloat("posY", originalPos.y),
            PlayerPrefs.GetFloat("posZ", originalPos.z));

        Vector3 rot = new Vector3(
            PlayerPrefs.GetFloat("rotX", originalrot.x),
            PlayerPrefs.GetFloat("rotY", originalrot.y),
            PlayerPrefs.GetFloat("rotZ", originalrot.z));

        transform.position = pos;
        transform.rotation = Quaternion.Euler(rot);
        mCamera.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Door")
        {
            /*PlayerPrefs.SetFloat("posX", transform.position.x);
            PlayerPrefs.SetFloat("posY", transform.position.y);
            PlayerPrefs.SetFloat("posZ", transform.position.z);
            PlayerPrefs.SetFloat("rotX", transform.rotation.x);
            PlayerPrefs.SetFloat("rotY", transform.rotation.y);
            PlayerPrefs.SetFloat("rotZ", transform.rotation.z);*/
            //Cursor.lockState = CursorLockMode.None;
            //MazeManager.mzMngr.SaveScore();
            SceneManager.LoadScene("PongGame");

        }
    }
}
