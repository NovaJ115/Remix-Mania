using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBoxDialogue : MonoBehaviour
{
    [Header("Dialogue and Style")]
    [SerializeField]
    private TextBoxType textBoxType;
    public Color nameColor;
    public Color dialogueColor;
    [SerializeField]
    private Dialogue dialogue;
    [SerializeField]
    private UniversalDialogueManager dialogueManager;
    [Header("Portrait Settings")]
    public Image portrait;
    public Image portraitBG;
    public Color portaitBGColor;
    [Header("TextBox Inputs")]
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private GameObject textBox;

    [Header("Text Box Variants")]
    [SerializeField]
    private StyleVariants[] styleVariants;

    
    
    public void Start()
    {
        textBox.SetActive(false);
        portaitBGColor.a = 1;
        nameColor.a = 1;
        dialogueColor.a = 1;
    }

    public void StartDialogue()
    {
        textBox.SetActive(true);
        dialogueManager.StartDialogue(dialogue);
        animator.SetBool("Start", true);
    }

    public void Update()
    {
        if(dialogueManager.dialogueOver == true)
        {
            animator.SetBool("End", true);
        }
        if(portraitBG != null)
        {
            portraitBG.color = portaitBGColor;
        }
        if(dialogueManager.dialogueText != null)
        {
            dialogueManager.dialogueText.color = dialogueColor;
        }
        if(dialogueManager.nameText != null)
        {
            dialogueManager.nameText.color = nameColor;
        }
        
        //ELEMENT 0
        if (textBoxType == TextBoxType.NameAndPortrait)
        {
            styleVariants[0].gameObject.SetActive(true);
            dialogueManager.nameText = styleVariants[0].nameTextObject;
            dialogueManager.dialogueText = styleVariants[0].dialogueTextObject;
            portrait = styleVariants[0].portraitImage;
            portraitBG = styleVariants[0].portraitBGImage;
        }
        else
        {
            styleVariants[0].gameObject.SetActive(false);
        }

        //ELEMENT 1
        if (textBoxType == TextBoxType.JustName)
        {
            styleVariants[1].gameObject.SetActive(true);
            dialogueManager.nameText = styleVariants[1].nameTextObject;
            dialogueManager.dialogueText = styleVariants[1].dialogueTextObject;
        }
        else
        {
            styleVariants[1].gameObject.SetActive(false);
        }

        //ELEMENT 2
        if (textBoxType == TextBoxType.JustPortrait)
        {
            styleVariants[2].gameObject.SetActive(true);
            dialogueManager.nameText = styleVariants[2].nameTextObject;
            dialogueManager.dialogueText = styleVariants[2].dialogueTextObject;
            portrait = styleVariants[2].portraitImage;
            portraitBG = styleVariants[2].portraitBGImage;
        }
        else
        {
            styleVariants[2].gameObject.SetActive(false);
        }

        //ELEMENT 3
        if (textBoxType == TextBoxType.JustDialogue)
        {
            styleVariants[3].gameObject.SetActive(true);
            dialogueManager.nameText = styleVariants[3].nameTextObject;
            dialogueManager.dialogueText = styleVariants[3].dialogueTextObject;
        }
        else
        {
            styleVariants[3].gameObject.SetActive(false);
        }


    }

}
