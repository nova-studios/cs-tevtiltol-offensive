using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Hp : MonoBehaviour {

	[HideInInspector]
	public int hpi = 20;
	public int maxhp = 20;
	public Slider healthbar;
    public Slider healthbar2;
    [HideInInspector]
	public Transform player;

	public bool shouldDistanceSelfDestruct = true;


	// Use this for initialization
	void Start () {
		hpi = maxhp;
		if(healthbar != null)
			healthbar.maxValue = maxhp;
        if (healthbar2 != null)
            healthbar2.maxValue = maxhp;
		GameObject _p = GameObject.FindGameObjectWithTag ("Player");
		if (_p) {
			player = _p.transform;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(healthbar != null){
			healthbar.value = hpi;
		}
        if (healthbar2 != null)
        {
            healthbar2.value = hpi;
        }

        if (!player)
            return;
		if (Vector3.Distance (player.transform.position, transform.position) > 100f && shouldDistanceSelfDestruct) {
			EnemyCounter.count--;
			Destroy (gameObject);
		}
	}
	public void Damage (int damage){
		
		hpi -= damage;
	}

}