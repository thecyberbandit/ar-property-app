using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Controller_MenuScene : MonoBehaviour {
    public static UI_Controller_MenuScene instance;
    public Animator SettingsAnimator;
    public Animator MenuCanvasAnimator;
    public Toggle soundToggle;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {       
        AppearMenuCanvas(true);
        if(PlayerPrefs.GetInt("SoundKeyValue")==0)
        {
            soundToggle.isOn = false;
        }
        else
        {
            soundToggle.isOn = true;
        }
    }
    public void AppearSettingsPanel(bool value)
    {
        SettingsAnimator.SetBool("Appear", value);
    }
    public void AppearMenuCanvas(bool value)
    {
        MenuCanvasAnimator.SetBool("Appear", value);
    }
    public void ButtonPressed_SettingsButton()
    {
        AppearMenuCanvas(false);
        AppearSettingsPanel(true);
    }
    public void ButtonPressed_OkButton()
    {
        AppearMenuCanvas(true);
        AppearSettingsPanel(false);
    }
}
