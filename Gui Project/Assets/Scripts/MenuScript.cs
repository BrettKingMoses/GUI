using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    #region Variables
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

    [Header("Keys")]
    public KeyCode forward;
    public KeyCode backward;
    public KeyCode left;
    public KeyCode right;
    public KeyCode jump;
    public KeyCode crouch;
    public KeyCode sprint;
    public KeyCode interact;
    public KeyCode holdingKey;
    public KeyCode fire;

    [Header("References")]
    public Light dirLight;
    public AudioSource music;
    public GUISkin menuFont;
    public GUIStyle menuStyle;
    #endregion Variables
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
    private void Update()
    {
        if (music != null)
        {
            if (muteToggle == false)
            {
                if (music.volume != volumeSlider)
                {
                    holdingVolume = volumeSlider;
                    music.volume = volumeSlider;
                }
            }
            else
            {
                volumeSlider = 0;
                music.volume = 0;
            }
        }
    }
    private void OnGUI()
    {
        if (!showOptions)
        {
            GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "", menuStyle);//background

            GUI.skin = menuFont;
            GUI.Box(new Rect(4 * scrW, 0.25f * scrH, 8 * scrW, 2 * scrH), "Easycoded Main Menu");
            //buttons
            if (GUI.Button(new Rect(1.25f * scrW, 4 * scrH, 4 * scrW, scrH), "Play"))
            {
                SceneManager.LoadScene(1);
            }
            if (GUI.Button(new Rect(1.25f * scrW, 5 * scrH, 4 * scrW, scrH), "Options"))
            {
                showOptions = (true);
            }
            if (GUI.Button(new Rect(1.25f * scrW, 6 * scrH, 4 * scrW, scrH), "Exit"))
            {
                Application.Quit();
            }
        }
        else if (showOptions)
        {
            GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "", menuStyle);//background
            GUI.skin = menuFont;
          
            GUI.Box(new Rect(4 * scrW, 0.25f * scrH, 8 * scrW, 2 * scrH), "Easycoded Options");
            //set our aspect ratio if screen size changes
            if (scrW != Screen.width / 16)
            {
                scrW = Screen.width / 16;
                scrH = Screen.height / 9;
            }
            if (GUI.Button(new Rect(14.875f * scrW, 8.375f * scrH, scrW, 0.5f * scrH), "Back"))
            {
                showOptions = false;
            }

            #region KeyBinding
            //set up buttons like in other script but less tedious and sad
            GUI.Box(new Rect(12 * scrW, scrH, scrW, scrH), "Forward");
            GUI.Box(new Rect(12 * scrW, 2 * scrH, scrW, scrH), "Backwards");
            GUI.Box(new Rect(12 * scrW, 3 * scrH, scrW, scrH), "Interact");
            GUI.Box(new Rect(12 * scrW, 4 * scrH, scrW, scrH), "Sprint");
            GUI.Box(new Rect(12 * scrW, 5 * scrH, scrW, scrH), "Fire");
            GUI.Box(new Rect(12 * scrW, 6 * scrH, scrW, scrH), "Right");
            GUI.Box(new Rect(12 * scrW, 7 * scrH, scrW, scrH), "Left");
            GUI.Button(new Rect(13 * scrW, scrH, scrW, scrH), forward.ToString());
            GUI.Button(new Rect(13 * scrW, 2 * scrH, scrW, scrH), backward.ToString());
            GUI.Button(new Rect(13 * scrW, 3 * scrH, scrW, scrH), interact.ToString());
            GUI.Button(new Rect(13 * scrW, 4 * scrH, scrW, scrH), sprint.ToString());
            GUI.Button(new Rect(13 * scrW, 5 * scrH, scrW, scrH), fire.ToString());
            GUI.Button(new Rect(13 * scrW, 6 * scrH, scrW, scrH), right.ToString());
            GUI.Button(new Rect(13 * scrW, 7 * scrH, scrW, scrH), left.ToString());
            #endregion

            #region Sliders
            int lightSoundIndex = 0;
            GUI.Box(new Rect(0.25f * scrW, 3f * scrH + (lightSoundIndex * (scrH * 0.5f)), 1.75f * scrW, 0.5f * scrH), "Volume");
            volumeSlider = GUI.HorizontalSlider(new Rect(2f * scrW, 3f * scrH + (lightSoundIndex * (scrH * 0.5f)), 1.75f * scrW, 0.5f * scrH), volumeSlider, 0, 1);
            lightSoundIndex++;
            brightnessSlider = GUI.HorizontalSlider(new Rect(2f * scrW, 3.125f * scrH + (lightSoundIndex * (scrH * 0.5f)), 1.75f * scrW, 0.25f * scrH), brightnessSlider, 0, 1);
            GUI.Box(new Rect(0.25f * scrW, 3f * scrH + (lightSoundIndex * (scrH * 0.5f)), 1.75f * scrW, 0.5f * scrH), "Brightness");
            if (GUI.Button(new Rect(03.75f * scrW, 3f * scrH + (lightSoundIndex * (scrH * 0.5f)), 1.75f * scrW, 0.5f * scrH), "Mute"))
            {
                ToggleVolume();
            }
        }
        #endregion

        #region Resolutions and Screen Size

        #endregion
    }
    bool ToggleVolume()
    {
        if (muteToggle == true)
        {
            volumeSlider = holdingVolume;
            muteToggle = false;
            return false;
        }

        else
        {
            holdingVolume = volumeSlider;
            muteToggle = true;
            volumeSlider = 0;
            music.volume = 0;
            return false;
        }
    }
    public void Forward()
    {
        if (!(backward == KeyCode.None || left == KeyCode.None || right == KeyCode.None || crouch == KeyCode.None || jump == KeyCode.None || interact == KeyCode.None || sprint == KeyCode.None))
        {
            holdingKey = forward;
            forward = KeyCode.None;
        }
    }
    public void Backward()
    {
        if (!(forward == KeyCode.None || left == KeyCode.None || right == KeyCode.None || crouch == KeyCode.None || jump == KeyCode.None || interact == KeyCode.None || sprint == KeyCode.None))
        {
            holdingKey = backward;
            backward = KeyCode.None;
        }
    }
    public void Left()
    {
        if (!(backward == KeyCode.None || forward == KeyCode.None || right == KeyCode.None || crouch == KeyCode.None || jump == KeyCode.None || interact == KeyCode.None || sprint == KeyCode.None))
        {
            holdingKey = left;
            left = KeyCode.None;
        }
    }
    public void Right()
    {
        if (!(backward == KeyCode.None || left == KeyCode.None || forward == KeyCode.None || crouch == KeyCode.None || jump == KeyCode.None || interact == KeyCode.None || sprint == KeyCode.None))
        {
            holdingKey = right;
            right = KeyCode.None;
        }
    }
    public void Jump()
    {
        if (!(backward == KeyCode.None || left == KeyCode.None || right == KeyCode.None || crouch == KeyCode.None || forward == KeyCode.None || interact == KeyCode.None || sprint == KeyCode.None))
        {
            holdingKey = jump;
            jump = KeyCode.None;
        }
    }
    public void Fire()
    {
        if (!(backward == KeyCode.None || left == KeyCode.None || right == KeyCode.None || forward == KeyCode.None || jump == KeyCode.None || interact == KeyCode.None || sprint == KeyCode.None))
        {
            holdingKey = crouch;
            fire = KeyCode.None;
        }
    }
    public void Sprint()
    {
        if (!(backward == KeyCode.None || left == KeyCode.None || right == KeyCode.None || crouch == KeyCode.None || jump == KeyCode.None || interact == KeyCode.None || forward == KeyCode.None))
        {
            holdingKey = sprint;
            sprint = KeyCode.None;
        }
    }
    public void Interact()
    {
        if (!(backward == KeyCode.None || left == KeyCode.None || right == KeyCode.None || crouch == KeyCode.None || jump == KeyCode.None || forward == KeyCode.None || sprint == KeyCode.None))
        {
            holdingKey = interact;
            interact = KeyCode.None;
        }
    }
}