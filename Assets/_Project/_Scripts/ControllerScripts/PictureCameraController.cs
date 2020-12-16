using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class PictureCameraController : MonoBehaviour {

    public static PictureCameraController instance;
    public string ScreenShotFilePath;
    public GameObject picturePanel;
    //AudioSource snapAudio;

    private void Awake()
    {
        instance = this;
        if (!PlayerPrefs.HasKey("ScreenShotFilePath"))
        {
            PlayerPrefs.SetString("ScreenShotFilePath", Application.persistentDataPath + "/ScreenShot");
        }
    }
    private void Start()
    {
        //snapAudio = GetComponent<AudioSource>();
        ScreenShotFilePath = PlayerPrefs.GetString("ScreenShotFilePath");
        if (!Directory.Exists(ScreenShotFilePath))
        {
            Directory.CreateDirectory(ScreenShotFilePath);
            //Debug.Log("File created");
        }
        else
        {
            //Debug.Log("FileExists");
        }
    }

    [Header("Public Variable")]
    //public Text CountDownText;
    //public Animator Flash;
    //public Image Viewer;
    [Header("Flash Variables")]
    //public Sprite Flash_Image;
    //public Sprite No_FlashImage;
    //public Button FlashButton;

    string DataPathImageName = "";
    string galleryImageName = "";

    public void PictureButtonPressed()
    {
        //Invoke("SaveInDataPath", 0.1f);
        UIController.instance.SetBottomBarAnim(false);
        Invoke("TakeSS", 1f);
    }

    #region NewPictureSaveMethod

    void TakeSS()
    {
        StartCoroutine(TakeScreenshotAndSave());
    }

    private IEnumerator TakeScreenshotAndSave()
    {
        yield return new WaitForEndOfFrame();

        Texture2D ss = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        ss.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        ss.Apply();

        // Save the screenshot to Gallery/Photos
        Debug.Log("Permission result: " + NativeGallery.SaveImageToGallery(ss, "BProperties", "BProperties {0}.png"));

        // To avoid memory leaks
        Destroy(ss);
        picturePanel.SetActive(true);
        Invoke("DestroyPictureText", 1f);
        //snapAudio.Stop();
        //snapAudio.Play();
        AudioController.instance.PlayCameraAudio();
        UIController.instance.SetBottomBarAnim(true);
    }

    void DestroyPictureText()
    {
        picturePanel.SetActive(false);
    }

    #endregion


    #region SavePicture Functions
    void SaveInDataPath() // Step 1: Save in DataPath
    {
        try
        {
            string prependNumber = "" + System.DateTime.Now.Year + System.DateTime.Now.Month + System.DateTime.Now.Day + System.DateTime.Now.TimeOfDay.Hours + System.DateTime.Now.TimeOfDay.Minutes + System.DateTime.Now.TimeOfDay.Seconds;
            //Debug.Log("PrependNumber: " + prependNumber);

            galleryImageName = "ScreenShot" + prependNumber + ".jpg";
            DataPathImageName = ScreenShotFilePath + "/ScreenShot" + prependNumber + ".jpg";


#if UNITY_EDITOR
            ScreenCapture.CaptureScreenshot(DataPathImageName);
            //Invoke("SavePictureToGallery", 1);// if YES Button Pressed
#else
            Texture2D ScreenShotT2D = ScreenCapture.CaptureScreenshotAsTexture(1);
            SaveToPhone(ScreenShotT2D); // Step2 : Save to Phone
#endif

        }
        catch (System.Exception e)
        {
            //Debug.Log(e.ToString());
        }
    }

    public Texture2D getLastImage()
    {
        byte[] byteArray = File.ReadAllBytes(DataPathImageName);
        Texture2D texture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        texture.LoadImage(byteArray);
        return texture;
    }
    public void SaveToPhone(Texture2D image) // Only called in IOS and Android // Step2 : Save to Phone
    {
        byte[] bytes = image.EncodeToJPG();

        File.WriteAllBytes(DataPathImageName, bytes);
        //Invoke("SavePictureToGallery", 1); // will do it if YES Button presed
    }

    public void SavePictureToGallery()
    {
        //Debug.Log("Saving it now");
        StartCoroutine(SaveToGallery());
    }

    Texture2D texture;
    IEnumerator SaveToGallery()
    {
        yield return new WaitForEndOfFrame();

        byte[] byteArray = File.ReadAllBytes(DataPathImageName);

        texture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        texture.LoadImage(byteArray);
        //GalleryController.instance.GatherAllImageFromFile();
        //GalleryController.instance.AddImageTolist(texture);
        //Debug.Log(texture.name + " " + texture.width);
        //Debug.Log("Saving to gallery() " + galleryImageName);
#if UNITY_EDITOR

        NativeGallery.SaveImageToGallery(texture, "BProperties", galleryImageName);
        //Debug.Log("Saving to gallery(Editor) " + galleryImageName);


#elif UNITY_ANDROID

        NativeGallery.SaveImageToGallery(texture, "BProperties", galleryImageName);
        Debug.Log("Saving to gallery(Android) " + galleryImageName);
#elif UNITY_IOS

           NativeGallery.SaveImageToGallery(texture, "AR Book", "galleryImageName {0}" + ".jpg");
#endif
    }
    #endregion


    public void Delete_Picture(string FileName, string GalleryImageName)
    {
        File.Delete(FileName);
        //#if UNITY_ANDROID
        //        NativeGallery.DeleteFromGallery("AR Book/" + galleryImageName, true);
        //#endif
    }

    #region Flash Functions
    //public bool flashOn = false;
    //public void FlashButtonPressed()
    //{
    //    flashOn = !flashOn;
    //    if (flashOn)
    //    {
    //        FlashButton.image.sprite = Flash_Image;
    //        Vuforia.CameraDevice.Instance.SetFlashTorchMode(true);
    //        //Audio_SFX_Controller.instance.PlayPositive();
    //    }
    //    else
    //    {
    //        FlashButton.image.sprite = No_FlashImage;
    //        Vuforia.CameraDevice.Instance.SetFlashTorchMode(false);
    //        //Audio_SFX_Controller.instance.PlayNegative();
    //    }

    //}
    #endregion

    //public void ViewerPanel_No_ButtonPressed()
    //{

    //    UI_Controller_Viewer.instance.Show_PictureDelete();
    //    File.Delete(DataPathImageName);
    //    Audio_SFX_Controller.instance.PlayNegative();
    //    if (SceneLoadingManager.instance.CurrentScene == SceneLoadingManager.SceneType.Selfi)
    //    {
    //        Invoke("ButtonPressedYes_AppearCameraSwapPanel", 2f);
    //    }
    //}
    //public void ViewerPanel_Yes_ButtonPressed()
    //{
    //    SavePictureToGallery();

    //    //UI_Controller_Viewer.instance.Save_HideViewer();
    //    UI_Controller_Viewer.instance.Show_PictureSaved();
    //    Audio_SFX_Controller.instance.PlayPositive();
    //    if (SceneLoadingManager.instance.CurrentScene == SceneLoadingManager.SceneType.Selfi)
    //    {
    //        Invoke("ButtonPressedYes_AppearCameraSwapPanel", 2f);
    //    }
    //}
    //void ButtonPressedYes_AppearCameraSwapPanel()
    //{
    //    UI_Controller_Selfie.instance.ShowCameraSwapPanelAnimation();
    //}
    //public void ButtonPressed_BackToPreviousScene()
    //{
    //    if (SceneLoadingManager.instance.CurrentScene == SceneLoadingManager.SceneType.AR)
    //    {
    //        SceneLoadingManager.instance.LoadScene("MenuScene");
    //    }
    //    else if (SceneLoadingManager.instance.CurrentScene == SceneLoadingManager.SceneType.Selfi)
    //    {
    //        SceneLoadingManager.instance.LoadScene("AR_Scene");
    //    }
    //}
}
