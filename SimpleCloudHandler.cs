using UnityEngine;
using Vuforia;

public class SimpleCloudHandler : MonoBehaviour, ICloudRecoEventHandler {
    private CloudRecoBehaviour mCloudRecoBehaviour;
    private bool mIsScanning = false;
    private string mTargetMetadata = "";
    private GameObject shows;
    private GameObject map;
    private GameObject countdown; 

    public ImageTargetBehaviour ImageTargetTemplate;
    // Use this for initialization
    void Start () 
    {
        // register this event handler at the cloud reco behaviour
        mCloudRecoBehaviour = GetComponent<CloudRecoBehaviour>();
        if (mCloudRecoBehaviour)
        {
            mCloudRecoBehaviour.RegisterEventHandler(this);
        }

        //grabbing all of the game objects
        shows = GameObject.Find("Shows");
        map = GameObject.Find("Map");
        countdown = GameObject.Find("Countdown");
    }

    void Update () 
    {   
        //We just want to make sure that the function is called once, when the first tap is registered.
        // if (Input.GetMouseButtonDown(0))
        // {
        //     Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //     RaycastHit hit; 
        //     if (Physics.Raycast(ray, out hit))
        //     {
        //         Debug.Log(hit.transform.name);
        //          //This is where we want the application to open in another url (if they tap on the correct game object)
        //          if (hit.transform.name == "Map") //will most likely need to rename 
        //         {
        //             Application.OpenURL("http://arisefestival.com/");
        //         }
        //     }
        // }
    }

    public void OnInitialized() {
        Debug.Log ("Cloud Reco initialized");
    }
    
    public void OnInitError(TargetFinder.InitState initError) {
        Debug.Log ("Cloud Reco init error " + initError.ToString());
    }
    
    public void OnUpdateError(TargetFinder.UpdateState updateError) {
        Debug.Log ("Cloud Reco update error " + updateError.ToString());
    }

    public void OnStateChanged(bool scanning) {
        mIsScanning = scanning;
        if (scanning)
        {
            // clear all known trackables
            var tracker = TrackerManager.Instance.GetTracker<ObjectTracker>();
            tracker.TargetFinder.ClearTrackables(false);
        }
    }

    // Here we handle a cloud target recognition event
    public void OnNewSearchResult(TargetFinder.TargetSearchResult targetSearchResult) {
        // do something with the target metadata
        mTargetMetadata = targetSearchResult.MetaData;
        // stop the target finder (i.e. stop scanning the cloud)
        mCloudRecoBehaviour.CloudRecoEnabled = false;

        Debug.Log("Metadata for target: " + mTargetMetadata);

        if (mTargetMetadata == "courtney")
        {
            //show their versions of the AR Content
            shows.SetActive(true);
            map.SetActive(true);
            countdown.SetActive(false);
        } else if (mTargetMetadata == "magazine")
        {
            //show the countdown elements
            shows.SetActive(false);
            map.SetActive(false);
            countdown.SetActive(true);
        }

        // Build augmentation based on target
        if (ImageTargetTemplate) 
        {
            // enable the new result with the same ImageTargetBehaviour:
            ObjectTracker tracker = TrackerManager.Instance.GetTracker<ObjectTracker>();
            ImageTargetBehaviour imageTargetBehaviour =
            (ImageTargetBehaviour)tracker.TargetFinder.EnableTracking(
            targetSearchResult, ImageTargetTemplate.gameObject);
        }
    }

    void OnGUI() {
        // Display current scanning status
        // GUI.Box (new Rect(100,100,200,50), mIsScanning ? "Scanning" : "Not scanning");
        // Display metadata of latest detected cloud-target
        // GUI.Box (new Rect(100,200,200,50), "Metadata: " + mTargetMetadata);
        // If not scanning, show button
        // so that user can restart cloud scanning
        if (!mIsScanning) 
        {
            if (GUI.Button(new Rect(100,1000,200,50), "Scan Again")) 
            {
                // Restart TargetFinder
                mCloudRecoBehaviour.CloudRecoEnabled = true;
                shows.SetActive(false);
                map.SetActive(false);
                countdown.SetActive(false);
            }
        }
    }
}