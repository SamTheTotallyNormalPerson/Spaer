using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiaolougeManager : MonoBehaviour {

    private Queue<string> sentences;

    public Text nameText;
    public Text dialougeText;
    private Animator anim;
    // Use this for initialization

    _Spearvin userScript;
    void Start() {
        sentences = new Queue<string>();
        anim = GetComponent<Animator>();
        userScript = GameObject.FindWithTag("Player").GetComponent<_Spearvin>();
    }

    public void StartTalk(TalkingKid dialouge)
    {
        anim.SetBool("IsOpen", true);

        Debug.Log("Start conversation with" + dialouge.name);

        nameText.text = dialouge.name;

        sentences.Clear();

        foreach (string sentence in dialouge.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence() {
        if (sentences.Count == 0)
        {
            EndDiaolouge();
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence (string sentence)
    {
        dialougeText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialougeText.text += letter;
            yield return null;
        }
    }

    void EndDiaolouge()
    {
        anim.SetBool("IsOpen", false);
        userScript.IsTalking = true;
    }


   public  void NextBoy()
    {
        FindObjectOfType<AudioManager>().Play("Be Sure");
    }
   }

