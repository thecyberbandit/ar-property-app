using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour {
    public static SettingsController instance;
    public Toggle soundToggle;
    private void Awake()
    {
            instance = this;
    }
    public void ToggleSound()
    {
        if(soundToggle!=null)
        {
            if(soundToggle.isOn)
            {
                PlayerPrefs.SetInt("SoundKeyValue", 1);
                //Debug.Log("Sound is On");
            }
            else
            {
                PlayerPrefs.SetInt("SoundKeyValue", 0);
                //Debug.Log("Sound is Off");
            }
        }
    }
    
}
