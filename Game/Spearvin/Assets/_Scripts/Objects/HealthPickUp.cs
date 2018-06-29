using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp : MonoBehaviour {

    public GameObject pickupEffect;

    _Spearvin userScript;

    void Start()
    {
        userScript = GameObject.FindWithTag("Player").GetComponent<_Spearvin>();
    }

	void OnTriggerEnter(Collider col)

    {
       if (col.CompareTag("Player"))
        {
            PickUp();
        }
    }

    void PickUp()
    {
        Instantiate(pickupEffect, transform.position, transform.rotation);

        //Apply Effect
        userScript.Health += 1;

        //Remove Power
        Destroy(gameObject);
    }
}
