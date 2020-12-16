using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {
    public static UIController instance;
    public Animator canvasFlashAnim;
    public Animator canvasMarkerAnim;
    public Animator BottomBarAnim;
    public Button CameraButton;
    public Button HomeButton;
    public Button GalleryButton;
    public Text LocationText;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        SetBottomBarAnim(true);
        LocationText.text = "";
    }
    public void TriggerFlash()
    {
        canvasFlashAnim.SetTrigger("flash");
    }

    public void TriggerMarkerText(bool value)
    {
        canvasMarkerAnim.SetBool("loop", value);
    }
    public void CameraButtonInteractive(bool value)
    {
        CameraButton.interactable = value;
    }
    public void SetBottomBarAnim(bool value)
    {
        BottomBarAnim.SetBool("Appear", value);
        HomeButton.interactable = value;
        CameraButton.interactable = value;
        GalleryButton.interactable = value;
    }
    public void SetLocationText(string value)
    {
        LocationText.text = value;
    }
}
