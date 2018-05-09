using UnityEngine;
using System.Collections;

public class GunPicker : MonoBehaviour {

	GameObject myCam;

	public float pickGunDistance = 4f;

	Animator anim;
	//GunController gcont;

	// Use this for initialization
	void Start () {
		if (myCam == null)
			myCam = Camera.main.gameObject;

		anim = GetComponent<Animator> ();
		isPickedGun = HintScript.isPickedGun;
		//gcont = GetComponent<GunController> ();
	}

	bool isPickedGun;

    // Update is called once per frame
    void Update()
    {
        //shoot a ray when key e pressed
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray r = new Ray(myCam.transform.position, myCam.transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(r, out hit, pickGunDistance, 256))
            {
                GunBuilder myGunBuilder = hit.collider.gameObject.GetComponentInParent<GunBuilder>(); //get the gun builder
				PickupBender pikup = hit.collider.gameObject.GetComponentInParent<PickupBender>();

				if (pikup) {
					GameBender (pikup.gameObject);
					return;
				}

                if (!myGunBuilder)
                    return;

				if(!isPickedGun)
					HintScript.isPickedGun = true;

                /*float distance = Vector3.Distance(myCam.transform.position, myGunBuilder.transform.position);
                //if it exists and close enough
                if (distance < pickGunDistance)
                {*/
                    GunBuilder myRealGunBuilder = GunController.myGunCont.gameObject.GetComponent<GunBuilder>(); //get our gun builder


                    GunBuilder.Gun temp = myRealGunBuilder.myGun;
                    myRealGunBuilder.myGun = myGunBuilder.myGun;
                    myGunBuilder.myGun = temp;


                    myRealGunBuilder.BuildGun();
                    myGunBuilder.BuildGun();
                    myGunBuilder.PickEffect();
                    anim.SetTrigger("isPicked");

                    //delete pickup
                    //Destroy(myGunBuilder.gameObject);
                //}
            }
        }

        Ray r2 = new Ray(myCam.transform.position, myCam.transform.forward);
        RaycastHit hit2;
        if (Physics.Raycast(r2, out hit2, pickGunDistance, 256))
        {
            Debug.DrawLine(myCam.transform.position, hit2.point);

			AppearOnCanPickUp pick = hit2.collider.gameObject.GetComponentInParent<AppearOnCanPickUp>();

            if (!pick)
                return;

            pick.CancelInvoke();
            pick.HighLight();
            

        }
    }

	public MonoBehaviour[] shitToDisable;
	public GameObject[] moreShitToDisable;

	public Animator gey;

	public MonoBehaviour[] stuffToActivate;
	public GameObject[] moreStuffToActivate;

	void GameBender (GameObject pick) {

		//gey.enabled = false;

		foreach (GameObject obj in moreShitToDisable)
			obj.SetActive(false);
		foreach (MonoBehaviour mono in shitToDisable)
			mono.enabled = false;

		foreach (GameObject obj in moreStuffToActivate)
			obj.SetActive(true);
		foreach (MonoBehaviour mono in stuffToActivate)
			mono.enabled = true;

		anim.SetBool ("isRunnning", false);
		anim.SetTrigger("isPicked");

		Destroy (pick);

		foreach (Transform child in transform) {
			Destroy (child.gameObject);
		}
		Invoke ("geyDis", 1f);
		Invoke ("frameLater", 0.1f);

		this.enabled = false;

	}
	void frameLater () {
		anim.SetBool ("isRunnning", false);
		//anim.SetTrigger("isPicked");
	}

	void geyDis () {
		gey.enabled = false;
	}
}
