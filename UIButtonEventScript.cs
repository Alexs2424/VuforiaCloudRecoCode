using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtonEventScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void eventOneButton() {
		Application.OpenURL("http://www.myanimalhome.net/#/tour");
	}

	public void eventTwoButton() {
		Application.OpenURL("https://www.axs.com/events/351025/beach-house-sold-out-tickets");
	}

	public void eventThreeButton() {
		Application.OpenURL("https://www.axs.com/events/351831/courtney-barnett-tickets");
	}

	public void toMaps() {
		Application.OpenURL("https://www.google.com/maps/place/Ogden+Theatre/@39.7401786,-104.9758075,19z/data=!4m5!3m4!1s0x876c792d49eb5773:0xa7f6227aa8c340bc!8m2!3d39.7401704!4d-104.9752697");
	}

	public void toArise() {
		Application.OpenURL("http://arisefestival.com/tickets/");
	}

	public void toPlace() {
		Application.OpenURL("http://www.ogdentheatre.com/");
	}

    public void toSaltMag() {
        Application.OpenURL("https://saltmag.online/");
    }
}
