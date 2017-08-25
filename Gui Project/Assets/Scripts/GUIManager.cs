using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour
{
    #region Variables
    [Header("Bools")]
    public bool gameScene;
    public bool showOptions;
    public bool pause;
    public bool fullScreenToggle;
    public bool paused;
    [Header("Resolution")]
    public int index;
    public int[] resX, resY;
    public Dropdown resolutionDropdown;
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
    //tries to remember key we try to change
    [Header("GUI Text")]
    public Text forwardText;
    public Text backwardsText;
    public Text leftText;
    public Text rightText;
    public Text jumpText;
    public Text crouchText;
    public Text sprintText;
    public Text interactText;
    [Header("References")]
    public Slider volumeSlider, BrightnessSlider;
    public GameObject menu;
    public GameObject options;
    public Light dirLight;
    public AudioSource music;
    [Header("GUI Elements")]
    public GameObject Menu;
    public GameObject Options;
    public Toggle fullWindowToggle;
    public GameObject Pause;
    #endregion Variables
    void Start()
    {
        Time.timeScale = 1;
        fullScreenToggle = true;//change when you load in the info
        if (PlayerPrefs.HasKey("Volume"))
        {
            Load();
        }
        if (volumeSlider != null && music != null)
        {
            volumeSlider.value = music.volume;
        }
        if (BrightnessSlider != null && dirLight != null)
        {
            BrightnessSlider.value = dirLight.intensity;
        }
        if (gameScene)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = true;
        }
        #region SetUp Keys
        //Set out keys to the preset keys we may have saved, else set keys to default
        forward = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Forward", "W"));
        backward = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Backwards", "S"));
        left = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Left", "A"));
        right = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Right", "D"));
        jump = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Jump", "Space"));
        crouch = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Crouch", "LeftControl"));
        sprint = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Sprint", "LeftShift"));
        interact = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Interact", "E"));



        backwardsText.text = backward.ToString();
        leftText.text = left.ToString();
        rightText.text = right.ToString();
        jumpText.text = jump.ToString();
        crouchText.text = crouch.ToString();
        sprintText.text = sprint.ToString();
        interactText.text = interact.ToString();
        #endregion
    }
    void Update()

    {
        if (gameScene)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                TogglePause();
            }
        }
        if (showOptions)
        {
            music.volume = volumeSlider.GetComponent<Slider>().value = music.volume;
        }
    }
    public bool TogglePause()
    {
        if (paused)
        {
            if (!showOptions)
            {
                Time.timeScale = 1;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                paused = false;
            }
            else
            {
                showOptions = false;
                Options.SetActive(false);
                Pause.SetActive(true);
            }

        }
        return false;
    }
    public void Play()
    {
        SceneManager.LoadScene(1);
    }
    public void Load()
    {
        music.volume = PlayerPrefs.GetFloat("Volume");
        dirLight.intensity = PlayerPrefs.GetFloat("Brightness");
    }
    public void Save()
    {
        PlayerPrefs.SetFloat("volume", music.volume);
        PlayerPrefs.SetFloat("Brightness", dirLight.intensity);
        PlayerPrefs.SetString("Forward", forward.ToString());
        PlayerPrefs.SetString("Backward", backward.ToString());
        PlayerPrefs.SetString("Left", left.ToString());
        PlayerPrefs.SetString("Right", right.ToString());
        PlayerPrefs.SetString("Jump", jump.ToString());
        PlayerPrefs.SetString("Crouch", crouch.ToString());
        PlayerPrefs.SetString("Sprint", sprint.ToString());
        PlayerPrefs.SetString("Interact", interact.ToString());
    }
    public void Exit()
    {
        Application.Quit();
        Debug.Log("quit");
    }
    #region Controls
    public void Forward()
    {
        if (!(backward == KeyCode.None || left == KeyCode.None || right == KeyCode.None || crouch == KeyCode.None || jump == KeyCode.None || interact == KeyCode.None || sprint == KeyCode.None))
        {
            holdingKey = forward;
            forward = KeyCode.None;
            forwardText.text = forward.ToString();
        }
    }
    public void Backward()
    {
        if (!(forward == KeyCode.None || left == KeyCode.None || right == KeyCode.None || crouch == KeyCode.None || jump == KeyCode.None || interact == KeyCode.None || sprint == KeyCode.None))
        {
            holdingKey = backward;
            backward = KeyCode.None;
            backwardsText.text = backward.ToString();
        }
    }
    public void Left()
    {
        if (!(backward == KeyCode.None || forward == KeyCode.None || right == KeyCode.None || crouch == KeyCode.None || jump == KeyCode.None || interact == KeyCode.None || sprint == KeyCode.None))
        {
            holdingKey = left;
            left = KeyCode.None;
            leftText.text = left.ToString();
        }
    }
    public void Right()
    {
        if (!(backward == KeyCode.None || left == KeyCode.None || forward == KeyCode.None || crouch == KeyCode.None || jump == KeyCode.None || interact == KeyCode.None || sprint == KeyCode.None))
        {
            holdingKey = right;
            right = KeyCode.None;
            rightText.text = right.ToString();
        }
    }
    public void Jump()
    {
        if (!(backward == KeyCode.None || left == KeyCode.None || right == KeyCode.None || crouch == KeyCode.None || forward == KeyCode.None || interact == KeyCode.None || sprint == KeyCode.None))
        {
            holdingKey = jump;
            jump = KeyCode.None;
            jumpText.text = jump.ToString();
        }
    }
    public void Crouch()
    {
        if (!(backward == KeyCode.None || left == KeyCode.None || right == KeyCode.None || forward == KeyCode.None || jump == KeyCode.None || interact == KeyCode.None || sprint == KeyCode.None))
        {
            holdingKey = crouch;
            crouch = KeyCode.None;
            crouchText.text = crouch.ToString();
        }
    }
    public void Sprint()
    {
        if (!(backward == KeyCode.None || left == KeyCode.None || right == KeyCode.None || crouch == KeyCode.None || jump == KeyCode.None || interact == KeyCode.None || forward == KeyCode.None))
        {
            holdingKey = sprint;
            sprint = KeyCode.None;
            sprintText.text = sprint.ToString();
        }
    }
    public void Interact()
    {
        if (!(backward == KeyCode.None || left == KeyCode.None || right == KeyCode.None || crouch == KeyCode.None || jump == KeyCode.None || forward == KeyCode.None || sprint == KeyCode.None))
        {
            holdingKey = interact;
            interact = KeyCode.None;
            interactText.text = interact.ToString();
        }
    }
    #endregion
    public void ShowOptions()
    {
        ToggleOptions();
    }
    public bool ToggleOptions()
    {
        if (showOptions)
        {
            showOptions = false;
            menu.SetActive(false);
            options.SetActive(true);
            return false;
        }
        else
        {
            showOptions = true;
            menu.SetActive(true);
            options.SetActive(false);
            return true;
        }
    }
    #region Key Rigistry
    private void OnGUI()
    {
        Event e = Event.current;
        //if forward is set to none
        if (forward == KeyCode.None)
        {
            //if an event is triggered by key press
            if (e.isKey)
            {
                //if that key is not assigned to any other key
                Debug.Log("Key Pressed: " + e.keyCode);
                if (!(e.keyCode == backward || e.keyCode == right || e.keyCode == left || e.keyCode == jump || e.keyCode == crouch || e.keyCode == sprint || e.keyCode == interact))
                {
                    //set forward key that was pressed
                    forward = e.keyCode;
                    //set key to none
                    holdingKey = KeyCode.None;
                    //set GUI to new key
                    forwardText.text = forward.ToString();
                }
                else
                {
                    forward = holdingKey;
                    holdingKey = KeyCode.None;
                    forwardText.text = forward.ToString();

                }
            }

        }
        if (backward == KeyCode.None)
        {
            //if an event is triggered by key press
            if (e.isKey)
            {
                //if that key is not assigned to any other key
                Debug.Log("Key Pressed: " + e.keyCode);
                if (!(e.keyCode == forward || e.keyCode == right || e.keyCode == left || e.keyCode == jump || e.keyCode == crouch || e.keyCode == sprint || e.keyCode == interact))
                {//set forward key that was pressed
                    backward = e.keyCode;
                    //set key to none
                    holdingKey = KeyCode.None;
                    //set GUI to new key
                    backwardsText.text = backward.ToString();
                }
                else
                {
                    backward = holdingKey;
                    holdingKey = KeyCode.None;
                    backwardsText.text = backward.ToString();

                }
            }

        }
        if (right == KeyCode.None)
        {
            //if an event is triggered by key press
            if (e.isKey)
            {
                //if that key is not assigned to any other key
                Debug.Log("Key Pressed: " + e.keyCode);
                if (!(e.keyCode == backward || e.keyCode == forward || e.keyCode == left || e.keyCode == jump || e.keyCode == crouch || e.keyCode == sprint || e.keyCode == interact))
                {//set forward key that was pressed
                    right = e.keyCode;
                    //set key to none
                    holdingKey = KeyCode.None;
                    //set GUI to new key
                    rightText.text = right.ToString();
                }
                else
                {
                    right = holdingKey;
                    holdingKey = KeyCode.None;
                    rightText.text = right.ToString();

                }
            }

        }
        if (left == KeyCode.None)
        {
            //if an event is triggered by key press
            if (e.isKey)
            {
                //if that key is not assigned to any other key
                Debug.Log("Key Pressed: " + e.keyCode);
                if (!(e.keyCode == backward || e.keyCode == right || e.keyCode == forward || e.keyCode == jump || e.keyCode == crouch || e.keyCode == sprint || e.keyCode == interact))
                {//set forward key that was pressed
                    left = e.keyCode;
                    //set key to none
                    holdingKey = KeyCode.None;
                    //set GUI to new key
                    leftText.text = left.ToString();
                }
                else
                {
                    left = holdingKey;
                    holdingKey = KeyCode.None;
                    leftText.text = left.ToString();

                }
            }

        }
        if (jump == KeyCode.None)
        {
            //if an event is triggered by key press
            if (e.isKey)
            {
                //if that key is not assigned to any other key
                Debug.Log("Key Pressed: " + e.keyCode);
                if (!(e.keyCode == backward || e.keyCode == right || e.keyCode == left || e.keyCode == forward || e.keyCode == crouch || e.keyCode == sprint || e.keyCode == interact))
                {//set forward key that was pressed
                    jump = e.keyCode;
                    //set key to none
                    holdingKey = KeyCode.None;
                    //set GUI to new key
                    jumpText.text = jump.ToString();
                }
                else
                {
                    jump = holdingKey;
                    holdingKey = KeyCode.None;
                    jumpText.text = jump.ToString();

                }
            }

        }
        if (crouch == KeyCode.None)
        {
            //if an event is triggered by key press
            if (e.isKey)
            {
                //if that key is not assigned to any other key
                Debug.Log("Key Pressed: " + e.keyCode);
                if (!(e.keyCode == backward || e.keyCode == right || e.keyCode == left || e.keyCode == jump || e.keyCode == forward || e.keyCode == sprint || e.keyCode == interact))
                {   //set forward key that was pressed
                    crouch = e.keyCode;
                    //set key to none
                    holdingKey = KeyCode.None;
                    //set GUI to new key
                    crouchText.text = crouch.ToString();
                }
                else
                {
                    crouch = holdingKey;
                    holdingKey = KeyCode.None;
                    crouchText.text = crouch.ToString();

                }
            }

        }
        if (sprint == KeyCode.None)
        {
            //if an event is triggered by key press
            if (e.isKey)
            {
                //if that key is not assigned to any other key
                Debug.Log("Key Pressed: " + e.keyCode);
                if (!(e.keyCode == backward || e.keyCode == right || e.keyCode == left || e.keyCode == jump || e.keyCode == crouch || e.keyCode == forward || e.keyCode == interact))
                {//set forward key that was pressed
                    sprint = e.keyCode;
                    //set key to none
                    holdingKey = KeyCode.None;
                    //set GUI to new key
                    sprintText.text = sprint.ToString();
                }
                else
                {
                    sprint = holdingKey;
                    holdingKey = KeyCode.None;
                    sprintText.text = sprint.ToString();

                }
            }

        }
        if (interact == KeyCode.None)
        {
            //if an event is triggered by key press
            if (e.isKey)
            {
                //if that key is not assigned to any other key
                Debug.Log("Key Pressed: " + e.keyCode);
                if (!(e.keyCode == backward || e.keyCode == right || e.keyCode == left || e.keyCode == jump || e.keyCode == crouch || e.keyCode == sprint || e.keyCode == forward))
                {//set forward key that was pressed
                    interact = e.keyCode;
                    //set key to none
                    holdingKey = KeyCode.None;
                    //set GUI to new key
                    interactText.text = interact.ToString();
                }
                else
                {
                    interact = holdingKey;
                    holdingKey = KeyCode.None;
                    interactText.text = interact.ToString();

                }
            }

        }
    }
    #endregion
    #region FullScreen toggle and Resolution
    public void FullScreenToggle()
    {
        fullScreenToggle = !fullScreenToggle;
        Screen.fullScreen = !Screen.fullScreen;
    }
    /*
     resolutions
     3840 * 2160
     1920 * 1080
     1280 * 720
     2560 * 1440
     1680 * 960
     1600 * 900
     1024 * 576

        Screen.SetResolution(x,y,fullscreen(bool));
     */
    public void ResolutionChange()
    {
        index = resolutionDropdown.value;
        Screen.SetResolution(resX[index], resY[index], fullScreenToggle);
    }
    #endregion
}