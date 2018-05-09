﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DPS_Text : MonoBehaviour {


    float myStatVal = 0f;

    public Text statValue;

    GunController myGunCont;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {


        if (myGunCont == null)
            myGunCont = GunController.myGunCont;


        int lel = (int)((float)myGunCont.damage * myGunCont.fireRate / 60f);
        statValue.text = lel.ToString() + " DPS";
    }
}
