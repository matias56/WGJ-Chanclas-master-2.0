using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeTitle : MonoBehaviour
{

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

    public AnimSprite ihneS;

    public GameObject[] lv;

    public bool lvs;


    public Rigidbody2D rb;   // Reference to the player's Rigidbody2D

    private Vector2 savedVelocity;         // Store the player's velocity before the dialogue
    // Start is called before the first frame update
    void Start()
    {
        ihne = GetComponent<Ihne>();

        ihne.enabled = false;


        ihneS = GetComponent<AnimSprite>();

        rb = GetComponent<Rigidbody2D>();


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

        for (var i = 0; i < lv.Length; i++)
        {
            if (lv[i] == null)
                return;
        }


        // Ensure all dialogue cards are inactive at the start
        DeactivateAllDialogues(lv);
        DeactivateAllDialogues(death);
        DeactivateAllDialogues(tor);
        DeactivateAllDialogues(stone);
        DeactivateAllDialogues(das);
        DeactivateAllDialogues(end);




    }

    // Update is called once per frame
    void Update()
    {

       


        if (Input.GetKeyDown(KeyCode.Return) && currentDialogue != null && currentDialogue[currentCardIndex].activeSelf)
        {
            ShowNextDialogueCard();
        }









    }

    public void TriggerDialogue(string type)
    {


        if (rb != null)
        {
            savedVelocity = rb.velocity;
            rb.velocity = Vector2.zero; // Stop any ongoing movement
        }

        if (ihne != null)
        {
            ihne.enabled = false;
        }

        if (ihneS != null)
        {
            ihneS.dis = true;
            ihneS.OnDisable();
        }



        // Set the current dialogue array based on the trigger type
        switch (type)
        {
            case "lv":
                currentDialogue = lv;
                break;
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
        if (currentDialogue == end)
        {
            // Scene transition
            SceneManager.LoadScene("3"); // Replace "3" with your actual scene name or index
        }
        ihne.enabled = true;
        rb.velocity = savedVelocity;
        ihneS.OnEnable();
        ihneS.dis = false;
        // Optional: perform any additional actions when all dialogues in the current section are complete
        Debug.Log("Dialogue section complete.");
        currentDialogue = null; // Reset to indicate no active dialogue
    }


}
