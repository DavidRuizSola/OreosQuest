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

    private InstruccionsTrigger instruccionsTrigger;

    //per poder posar el jugador en pausa
    private GameManagerScene gameManagerScene;


    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        gameManagerScene = GameObject.Find("GameManager").GetComponent<GameManagerScene>();
     //   instruccionsTrigger = GameObject.Find("Manual").GetComponent<InstruccionsTrigger>();
    }

    public void StartDialogue (Instructions dialogue)
    {

        //Engeguem l'animació perque aparegui el text
        animator.SetBool("isOpen", true);
        //posem el jugador en pausa
        gameManagerScene.isPaused = true;

        //indiquem que tenim la pantalla plena
        gameManagerScene.busyScreen = true;


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
        animator.SetBool("isOpen", false);
        //treiem la marca que ens indica que tenim un text en pantalla
        gameManagerScene.busyScreen = false;
        //Treiem la pausa del juagdor
        gameManagerScene.isPaused = false;
        
    }
 
}
