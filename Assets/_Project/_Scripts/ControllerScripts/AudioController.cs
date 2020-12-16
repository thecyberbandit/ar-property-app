using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {
    public static AudioController instance;
    public AudioSource cameraAudio;
    public AudioSource ScanAudio;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        if(PlayerPrefs.GetInt("SoundKeyValue")==0)
        {
            SetAudioPlayValue(0);
        }
        else
        {
            SetAudioPlayValue(1);
        }
    }
    public void PlayCameraAudio()
    {
        if(cameraAudio.isPlaying)
        {
            cameraAudio.Stop();
            cameraAudio.Play();
        }
        else
        {
            cameraAudio.Play();
        }
    }
    public void PlayScanAudio()
    {
        if (ScanAudio.isPlaying)
        {
            ScanAudio.Stop();
            ScanAudio.Play();
        }
        else
        {
            ScanAudio.Play();
        }
    }
    public void SetAudioPlayValue(float value)
    {
        cameraAudio.volume = value;
        ScanAudio.volume = value;
    }
}
