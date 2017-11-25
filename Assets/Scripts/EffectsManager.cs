using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsManager : MonoBehaviour {

    public Material nightSkybox;
    private Material daySkybox;
    private bool nightFog;
    private bool night;
    public AudioManager audioManager;
	// Use this for initialization
	void Start () {
        Shader.SetGlobalFloat("_AmbientLighIntensity", 1.0f);
        daySkybox = RenderSettings.skybox;
        nightFog = false;
        night = false;
        RenderSettings.fog = false;
        RenderSettings.fogColor = new Color(0.9f, 0.9f, 0.9f, 1.0f);
        audioManager = audioManager.GetComponent<AudioManager>();
    }

    void OnApplicationQuit()
    {
        Shader.SetGlobalFloat("_AmbientLighIntensity", 1.0f);
        RenderSettings.skybox = daySkybox;
        RenderSettings.fog = false;
    }

    // Update is called once per frame
    void Update () {
        if ((Input.GetKeyDown(KeyCode.Q) || Input.GetButtonDown("Fire2")) && night)
        {
            float ambInt = Shader.GetGlobalFloat("_AmbientLighIntensity");
            //Debug.Log(ambInt);
            ambInt = 1.0f;
            Shader.SetGlobalFloat("_AmbientLighIntensity", ambInt);
            RenderSettings.skybox = daySkybox;
            nightFog = false;
            night = false;
            audioManager.SwitchTrack();
        }
        else if ((Input.GetKeyDown(KeyCode.Q) ||Input.GetButtonDown("Fire2")) && !night)
        {
            float ambInt = Shader.GetGlobalFloat("_AmbientLighIntensity");
            ambInt = 0.25f;
            Shader.SetGlobalFloat("_AmbientLighIntensity", ambInt);
            RenderSettings.skybox = nightSkybox;
            nightFog = true;
            night = true;
            audioManager.SwitchTrack();
        }


        if (Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("Fire1"))
        {
            bool fog = RenderSettings.fog;

            if(fog)
            {
                RenderSettings.fog = false;
                audioManager.SwitchVolume();
                
            }
            else
            {
                RenderSettings.fog = true;
                audioManager.SwitchVolume();
            }
        }

        if (nightFog)
        {
            RenderSettings.fogColor = new Color(0.1f, 0.1f, 0.1f, 1.0f);
        }
        else
        {
            RenderSettings.fogColor = new Color(0.9f, 0.9f, 0.9f, 1.0f);
        }
    }
}
