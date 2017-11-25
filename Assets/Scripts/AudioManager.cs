using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public AudioClip bgm1;
    public AudioClip bgm2;
    private AudioSource noise;
    private GameObject enemy;
    private bool bgm1Playing = true;
    private bool fogOn = false;
    private bool mute = false;
	// Use this for initialization
	void Start () {
        noise = GetComponent<AudioSource>();
        noise.clip = bgm1;
        noise.Play();
    }
	
	// Update is called once per frame
	void Update () {
        enemy = GameObject.Find("Enemy(Clone)");
        transform.position = enemy.transform.position;
	}

    public void SwitchTrack()
    {
        noise.Stop();
        if (bgm1Playing)
        {
            noise.clip = bgm2;
            bgm1Playing = false;
        } else {
            noise.clip = bgm1;
            bgm1Playing = true;
        }
        if (!mute)
        {
            noise.Play();
        }
    }

    public void SwitchVolume()
    {
        if (!fogOn)
        {
            noise.volume = 0.5f;
            fogOn = true;
        } else {
            noise.volume = 1;
            fogOn = false;
        }
        
    }

    public void MuteSounds()
    {
        switch (mute)
        {
            case true:
                mute = false;
                noise.Play();
                break;
            case false:
                mute = true;
                noise.Stop();
                break;
        }
    }
}
