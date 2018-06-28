using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Dormou : MonoBehaviour {


    public float lookRadius = 10f;

    Transform target;
    NavMeshAgent agent;

    private Animator anim;

    public int Health = 1;

    public bool isAlive;

    public BoxCollider collision;

	// Use this for initialization
	void Start () {

        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

       

    }

    // Update is called once per frame
    void Update()
    {

        if (isAlive == true)
        {
            float distance = Vector3.Distance(target.position, transform.position);

            if (distance <= lookRadius)
            {
                agent.SetDestination(target.position);
                anim.SetBool("Run", true);
            }

            else
            {
                anim.SetBool("Run", false);
            }
        }

            if (Health == 0)
            {
                anim.SetTrigger("Rip");
            isAlive = false;
            Destroy(collision);
            }
        }
    

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    void OnTriggerEnter (Collider col)
    {
        if (col.tag == "Punch")
        {
            Health -= 1;
        }
    }

    void MyBabyBoyIsDead()
    {
        isAlive = false;
    }
}
