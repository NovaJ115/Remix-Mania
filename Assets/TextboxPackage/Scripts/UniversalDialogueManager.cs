using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UniversalDialogueManager : MonoBehaviour
{
    [Header("Dialogue Input")]
    [SerializeField]
    public TMP_Text nameText;
    [SerializeField]
    public TMP_Text dialogueText;

    private Queue<string> sentences;
    [SerializeField]
    private float typeSpeed;
    
    private bool canContinue;
    public bool dialogueOver;

    void Start()
    {
        sentences = new Queue<string>();
        canContinue = true;
        typeSpeed = 0.02f;
    }

    void Update()
    {
        if (canContinue == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                DisplayNextSentence();
                //buttonClick.Play();
                Debug.Log("DisplayNextSentanceNow");
            }
        }


    }
    public void StartDialogue(Dialogue dialogue)
    {
        dialogueOver = false;
        if(nameText != null)
        {
            nameText.text = dialogue.name;
        }
        
        sentences.Clear();
        canContinue = true;
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            dialogueOver = true;

            
            Debug.Log("sentences equal 0");
            canContinue = false;
            return;

        }

        string sentence = sentences.Dequeue();

        StopAllCoroutines();

        StartCoroutine(TypeSentence(sentence));
        
        Debug.Log(sentence);
        Debug.Log(sentences.Count);
    }

    public IEnumerator TypeSentence(string sentence)
    {
        
            dialogueText.text = "";
            foreach (char letter in sentence.ToCharArray())
            {
                
                dialogueText.text += letter;

                yield return new WaitForSeconds(typeSpeed);

            }
 
    }





}
