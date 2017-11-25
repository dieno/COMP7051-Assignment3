using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour {

    private CharacterController cc;
    private AudioSource noise;
	// Use this for initialization
	void Start () {
        cc = GetComponent<CharacterController>();
        noise = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        if (cc.isGrounded && cc.velocity.magnitude > 2f && !noise.isPlaying)
        {
            noise.volume = Random.Range(0.8f, 1);
            noise.pitch = Random.Range(0.8f,1.1f);
            noise.Play();
            
        }
	}
}
