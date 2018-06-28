using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnmey : MonoBehaviour {

    public float secondstilldeath = 0f;
    public int Health = 1;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Health <= 0)
        {
            Invoke("Wait", secondstilldeath);
        }
	}

    void OnTriggerEnter (Collider col)
    {
        if(col.tag == "Punch")
        {
            Health -= 1;
        }
    }

    void Wait()
    {
        Destroy(gameObject);
    }
}
