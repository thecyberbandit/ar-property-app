using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GalleryOpener : MonoBehaviour {


	public void ButtonPressed_OpenGallary () {

        AndroidJavaClass unityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject unityActivity = unityClass.GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaObject unityContext = unityActivity.Call<AndroidJavaObject>("getApplicationContext");

        AndroidJavaObject plugin = new AndroidJavaObject("ltd.pte.alienide.unityplugin.PluginClass");
        plugin.Call("SetContext", unityContext);
        plugin.Call("OpenFolderInGallery"); 
    }
	
	
}
