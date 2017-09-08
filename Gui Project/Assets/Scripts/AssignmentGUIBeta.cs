using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssignmentGUIBeta : MonoBehaviour
{
    [Header("Bools")]
    public bool showOptions;
    public bool fullScreenToggle;
    public bool muteToggle;
    public float volumeSlider, holdingVolume;
    public float brightnessSlider;

    [Header("Resolution and Sreen Elements")]
    public int index;
    public int[] resX, resY;
    public float scrW, scrH;


    [Header("References")]
    public Light dirLight;
    public AudioSource music;
    // Use this for initialization
    void Start()
    {
        scrW = Screen.width / 16;
        scrH = Screen.height / 9;
        dirLight = GameObject.FindGameObjectWithTag("Sun").GetComponent<Light>();
        music = GameObject.Find("MenuMusic").GetComponent<AudioSource>();
        volumeSlider = music.volume;
        brightnessSlider = dirLight.intensity;
    }
    // Update is called once per frame
    void Update()
    {

    }
}
