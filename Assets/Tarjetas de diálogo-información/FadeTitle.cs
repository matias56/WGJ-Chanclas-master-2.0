using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeTitle : MonoBehaviour
{
    public Image img;

    public Ihne ihne;

    

    public GameObject[] death;

    private int currentCardIndex = 0;   // Track the current active dialogue card

    public GameObject[] tor;

    public GameObject[] stone;

    private GameObject[] currentDialogue; // Reference to the current active dialogue array


    public GameObject[] das;

    public GameObject[] end;

    public bool isOi;

    public bool isD;

    public bool isS;

    public bool dash;


    public bool en;
    // Start is called before the first frame update
    void Start()
    {
        ihne = GetComponent<Ihne>();

        ihne.enabled = false;

       

  

        for (var i = 0; i < death.Length; i++)
        {
            if (death[i] == null)
                return;
        }


        for (var i = 0; i < tor.Length; i++)
        {
            if (tor[i] == null)
                return;
        }


        for (var i = 0; i < stone.Length; i++)
        {
            if (stone[i] == null)
                return;
        }

        for (var i = 0; i < das.Length; i++)
        {
            if (das[i] == null)
                return;
        }


        for (var i = 0; i < end.Length; i++)
        {
            if (end[i] == null)
                return;
        }

        // Ensure all dialogue cards are inactive at the start
        DeactivateAllDialogues(death);
        DeactivateAllDialogues(tor);
        DeactivateAllDialogues(stone);
        DeactivateAllDialogues(das);
        DeactivateAllDialogues(end);




    }

    // Update is called once per frame
    void Update()
    {

       

        if(Input.GetKeyDown(KeyCode.Return))
        {
            Fade();
            ihne.enabled = true;
        }


        if (Input.GetKeyDown(KeyCode.Return) && currentDialogue != null && currentDialogue[currentCardIndex].activeSelf)
        {
            ShowNextDialogueCard();
        }









    }

    public void TriggerDialogue(string type)
    {
        // Set the current dialogue array based on the trigger type
        switch (type)
        {
            case "death":
                currentDialogue = death;
                break;
            case "oi":
                currentDialogue = tor;
                break;
            case "st":
                currentDialogue = stone;
                break;
            case "dash":
                currentDialogue = das;
                break;
            case "end":
                currentDialogue = end;
                break;
        }

        if (currentDialogue != null && currentDialogue.Length > 0)
        {
            currentCardIndex = 0; // Reset the card index
            ActivateCurrentDialogueCard();
        }
    }

    void DeactivateAllDialogues(GameObject[] dialogueCards)
    {
        foreach (GameObject card in dialogueCards)
        {
            card.SetActive(false);
        }
    }


    void ActivateCurrentDialogueCard()
    {
        if (currentCardIndex < currentDialogue.Length)
        {
            currentDialogue[currentCardIndex].SetActive(true);
        }

    }
    void Fade()
    {
        img.CrossFadeAlpha(0, 2, true);
    }


    void ShowNextDialogueCard()
    {

        if (currentDialogue != null && currentCardIndex < currentDialogue.Length)
        {
            // Deactivate the current card
            currentDialogue[currentCardIndex].SetActive(false);

            // Move to the next card
            currentCardIndex++;

            if (currentCardIndex < currentDialogue.Length)
            {
                // Activate the next card
                ActivateCurrentDialogueCard();
            }
            else
            {
                // All dialogues in this section have been shown
                DialogueSequenceComplete();
            }
        }

    }

    void DialogueSequenceComplete()
    {
        // Optional: perform any additional actions when all dialogues in the current section are complete
        Debug.Log("Dialogue section complete.");
        currentDialogue = null; // Reset to indicate no active dialogue
    }


}
