using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftPlayerController : MonoBehaviour {

    public float speed;
    private Rigidbody rb;
    private float yMin;
    private float yMax;

    public bool isPlaying = false;
    public bool isSinglePlayer;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        yMin = -7.25f;
        yMax = 7.25f;
       // isPlaying = false;
        isSinglePlayer = true;

       
    }
	
	// Update is called once per frame
	void Update () {
        // float moveVertical = Input.GetAxis("Vertical");

        if(isPlaying)
        {
            if(isSinglePlayer)
            {
                if (Input.GetKey("up"))
                {
                    rb.transform.Translate(0.0f, 1.0f * speed * Time.deltaTime, 0.0f);
                }
                else if (Input.GetKey("down"))
                {
                    rb.transform.Translate(0.0f, -1.0f * speed * Time.deltaTime, 0.0f);
                }

                rb.transform.Translate(0.0f, Input.GetAxis("Vertical") * speed * Time.deltaTime, 0.0f);

            }
            else
            {
                if (Input.GetKey("w"))
                {
                    rb.transform.Translate(0.0f, 1.0f * speed * Time.deltaTime, 0.0f);
                }
                else if (Input.GetKey("s"))
                {
                    rb.transform.Translate(0.0f, -1.0f * speed * Time.deltaTime, 0.0f);
                }

                rb.transform.Translate(0.0f, Input.GetAxis("Vertical") * speed * Time.deltaTime, 0.0f);
            }

            rb.transform.position = new Vector3
            (
                -14.0f,
                Mathf.Clamp(rb.position.y, yMin, yMax),
                -0.5f
            );
        }
    }

    public void Reset()
    {
        rb.transform.position = new Vector3
        (
            -14.0f,
            0.0f,
            -0.5f
        );

        isPlaying = false;
    }

    private void OnCollisionEnter(Collision collision)
    {

    }
}
