using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InstructionsManager : MonoBehaviour
{

    public Text nameText;
    public Text dialogueText;

    public Animator animator;

    private Queue <string> sentences;

    //per poder posar el jugador en pausa
    public PlayerController playerController;


    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue (Instructions dialogue)
    {

        //Engeguem l'animació perque aparegui el text
        animator.SetBool("isOpen", true);
        //posem el jugador en pausa
        playerController.isPaused = true;


        //Modifiquem el titol del text per mostrar el titol de la instrucció
        nameText.text = dialogue.name;

        sentences.Clear();


        //anem mostrant el text que hem introduït
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }


        //comprobem si hem arribta al ultim text
        DisplayNextSentence();
    }

    public void DisplayNextSentence ()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
    }


    void EndDialogue()
    {
        // si estem al ultim text apliquem l'animació que elimina el text
        Debug.Log("End of conversation.");
        animator.SetBool("isOpen", false);
        //Treiem la pausa del juagdor
        playerController.isPaused = false;
        
    }
 
}
