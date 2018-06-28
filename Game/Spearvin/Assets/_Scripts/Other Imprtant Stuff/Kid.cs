using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kid : MonoBehaviour
{

    public TalkingKid dialogue;
    public GameObject poop;
    private Animator anim;
    public GameObject Radius;


    void Start()
    {
        poop.SetActive(true);
        anim = GetComponent<Animator>();
        Radius.SetActive(true);
    }

    void Update()
    {

    }

    void OnTriggerStay(Collider col)
    {
        if (col.tag == "Player" && Input.GetButtonDown("Fire4"))
        {
            meh();
            anim.SetBool("Talk", true);
            FindObjectOfType<AudioManager>().Play("Welcome");
            Radius.SetActive(false);
        }

        else
        {
            anim.SetBool("Talk", false);
        }


       
    }

    void meh()
    {
        FindObjectOfType<DiaolougeManager>().StartTalk(dialogue);
    }
}
