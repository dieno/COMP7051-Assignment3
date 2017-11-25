using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour {
    
    public float movementSpeed = 5.0f;
    public float mouseSesitivity = 5.0f;
    public float controllerSensitivity = 5.0f;
    public float UpDownRange = 60.0f;

    public Transform flashlightTransform;
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
    void Start () {
        cc = GetComponent<CharacterController>();
        mCamera = Camera.main;
        originalPos = transform.position;
        originalrot = transform.rotation.eulerAngles;
        Cursor.lockState = CursorLockMode.Locked;
    }
	
	// Update is called once per frame
	void Update () {
        //Commands

        if(Input.GetKeyDown(KeyCode.Home) || Input.GetButtonDown("Options"))
        {
            transform.position = originalPos;
            transform.rotation = Quaternion.Euler(originalrot);
            mCamera.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
        }

        if(Input.GetKeyDown(KeyCode.W) || Input.GetButtonDown("Triangle"))
        {
            noClip = !noClip;
        }

        Physics.IgnoreLayerCollision(8, 9, noClip);


        //Rotation

        float rotLeftRight = Input.GetAxis("Mouse X") * mouseSesitivity;
        rotLeftRight += Input.GetAxis("RightJoystickX") * controllerSensitivity;

        transform.Rotate(0f, rotLeftRight, 0f);
        

        verticalRotation -= Input.GetAxis("Mouse Y") * mouseSesitivity;
        verticalRotation -= Input.GetAxis("RightJoystickY") * controllerSensitivity;
        
        verticalRotation = Mathf.Clamp(verticalRotation, -UpDownRange, UpDownRange);
        mCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);

        flashlightTransform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);

        //ballSpawn.transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);

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
        Debug.Log("ball: " + ball.transform.forward);
        Debug.Log("cam: " + mCamera.transform.forward);
        //ball.GetComponent<Rigidbody>().AddForce(ball.transform.forward * 500);//BALL_SPEED);

        // Destroy the bullet after 2 seconds
        Destroy(ball, 2.0f);
    }

}
