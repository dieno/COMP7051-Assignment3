using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour {
    
    public float movementSpeed = 5.0f;
    public float mouseSesitivity = 5.0f;
    public float controllerSensitivity = 5.0f;
    public float UpDownRange = 60.0f;

    private CharacterController cc;
    private Camera camera;
    private float verticalRotation = 0f;

    private Vector3 originalPos;
    private Vector3 originalrot;
    private bool noClip = false;

    // Use this for initialization
    void Start () {
        cc = GetComponent<CharacterController>();
        camera = Camera.main;
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
            camera.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
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
        camera.transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
        
        //Movement
        float forwardSpeed = Input.GetAxis("Vertical") * movementSpeed;
        float sideSpeed = Input.GetAxis("Horizontal") * movementSpeed;

        Vector3 speed = new Vector3(sideSpeed, 0f, forwardSpeed);
        speed = transform.rotation * speed;

        cc.SimpleMove(speed);
	}
}
