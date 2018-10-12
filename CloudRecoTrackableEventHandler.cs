/*===============================================================================
THIS IS THE ONE WE WANT TO BE EDITING AND CHANGING TO WORK LIKE THE EXAMPLE
===============================================================================*/
using UnityEngine;
using Vuforia;

public class CloudRecoTrackableEventHandler : DefaultTrackableEventHandler
{
    #region PUBLIC_MEMBERS

    CloudContentManager m_CloudContentManager;
    #endregion // PUBLIC_MEMBERS


    #region PROTECTED_METHODS

    protected override void Start()
    {

        base.Start();

        m_CloudContentManager = FindObjectOfType<CloudContentManager>();
    }

    protected override void OnTrackingFound()
    {
        Debug.Log("<color=blue>OnTrackingFound()</color>");

        base.OnTrackingFound();

        if (m_CloudContentManager)
        {
            m_CloudContentManager.ShowTargetInfo(true); 
        }

        // Stop finder since we have now a result, finder will be restarted again when we lose track of the result
        ObjectTracker objectTracker = TrackerManager.Instance.GetTracker<ObjectTracker>();

        if (objectTracker != null)
        {
            objectTracker.TargetFinder.Stop();
        }
    }

    protected override void OnTrackingLost()
    {
        Debug.Log("<color=blue>OnTrackingLost()</color>");

        base.OnTrackingLost();

        if (m_CloudContentManager)
        {
            m_CloudContentManager.ShowTargetInfo(false);
        }

        // Start finder again if we lost the current trackable
        ObjectTracker objectTracker = TrackerManager.Instance.GetTracker<ObjectTracker>();

        if (objectTracker != null)
        {
            objectTracker.TargetFinder.ClearTrackables(false);
            objectTracker.TargetFinder.StartRecognition();
        }
    }

    #endregion //PROTECTED_METHODS
}
