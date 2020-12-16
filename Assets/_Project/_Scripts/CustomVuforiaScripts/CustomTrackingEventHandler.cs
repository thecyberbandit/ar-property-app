using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
public class CustomTrackingEventHandler : MonoBehaviour, ITrackableEventHandler
{

    #region PROTECTED_MEMBER_VARIABLES

    protected TrackableBehaviour mTrackableBehaviour;
    protected TrackableBehaviour.Status m_PreviousStatus;
    protected TrackableBehaviour.Status m_NewStatus;
    public List<Renderer> Meshes;
    #endregion // PROTECTED_MEMBER_VARIABLES

    #region UNITY_MONOBEHAVIOUR_METHODS

    protected virtual void Start()
    {
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
        Transform[] allTransforms = GetComponentsInChildren<Transform>();
        foreach(Transform t in allTransforms)
        {
            if(t.CompareTag("Mesh"))
            {
                t.GetComponent<Renderer>().enabled = false;
                Meshes.Add(t.GetComponent<Renderer>());
            }
        }
    }

    protected virtual void OnDestroy()
    {
        if (mTrackableBehaviour)
            mTrackableBehaviour.UnregisterTrackableEventHandler(this);
    }

    #endregion // UNITY_MONOBEHAVIOUR_METHODS

    #region PUBLIC_METHODS

    /// <summary>
    ///     Implementation of the ITrackableEventHandler function called when the
    ///     tracking state changes.
    /// </summary>
    public void OnTrackableStateChanged(
        TrackableBehaviour.Status previousStatus,
        TrackableBehaviour.Status newStatus)
    {
        m_PreviousStatus = previousStatus;
        m_NewStatus = newStatus;

        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " found");
            OnTrackingFound();
        }
        else if (previousStatus == TrackableBehaviour.Status.TRACKED &&
                 newStatus == TrackableBehaviour.Status.NO_POSE)
        {
            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");
            OnTrackingLost();
        }
        else
        {
            // For combo of previousStatus=UNKNOWN + newStatus=UNKNOWN|NOT_FOUND
            // Vuforia is starting, but tracking has not been lost or found yet
            // Call OnTrackingLost() to hide the augmentations
            OnTrackingLost();
        }
    }

    #endregion // PUBLIC_METHODS

    #region PROTECTED_METHODS

    protected virtual void OnTrackingFound()
    {
        //var rendererComponents = GetComponentsInChildren<Renderer>(true);
        //var colliderComponents = GetComponentsInChildren<Collider>(true);
        //var canvasComponents = GetComponentsInChildren<Canvas>(true);

        //// Enable rendering:
        //foreach (var component in rendererComponents)
        //    component.enabled = true;

        //// Enable colliders:
        //foreach (var component in colliderComponents)
        //    component.enabled = true;

        //// Enable canvas':
        //foreach (var component in canvasComponents)
        //    component.enabled = true;
        if(transform.name=="Type_A")
        {
            UIController.instance.SetLocationText("Rightway Airport Plaza");
        }
        else if(transform.name=="Type_B")
        {
            UIController.instance.SetLocationText("Doreen Vincita");
        }
        UIController.instance.TriggerMarkerText(false);
        UIController.instance.CameraButtonInteractive(true);
        //set the canvas flash triggered
        UIController.instance.TriggerFlash();
        GameController.instance.trackerFound = true;
        AudioController.instance.PlayScanAudio();
        //On tracking found, make all renderers with the tag "Mesh" visible
        //MeshVisibility(true);
        Invoke("MeshVisibility_Invoke", 0.5f);
    }
    void MeshVisibility_Invoke()
    {
        MeshVisibility(true);
    }
    void MeshVisibility(bool value)
    {
        if(Meshes != null)
        {
            int count = Meshes.Count;

            for(int i = 0; i<count; i++)
            {
                Meshes[i].enabled = value;
            }
        }
    }


    protected virtual void OnTrackingLost()
    {
        //var rendererComponents = GetComponentsInChildren<Renderer>(true);
        //var colliderComponents = GetComponentsInChildren<Collider>(true);
        //var canvasComponents = GetComponentsInChildren<Canvas>(true);

        //// Disable rendering:
        //foreach (var component in rendererComponents)
        //    component.enabled = false;

        //// Disable colliders:
        //foreach (var component in colliderComponents)
        //    component.enabled = false;

        //// Disable canvas':
        //foreach (var component in canvasComponents)
        //    component.enabled = false;
        MeshVisibility(false);
        if(GameController.instance!=null)
        {
            GameController.instance.trackerFound = false;
            GameController.instance.StartTimer();
        }
        if(UIController.instance!=null)
        {
            UIController.instance.SetLocationText("");
        }
    }

    #endregion // PROTECTED_METHODS
}
