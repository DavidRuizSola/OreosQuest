using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstruccionsTrigger : MonoBehaviour
{

    public Instructions dialogue1;
    public Instructions dialogue2;
    public Instructions dialogue3;
    public Instructions dialogue4;
    public Instructions dialogue5;
    public Instructions dialogue6;

    public bool textOnScreen;
    public int manualNumber;
    

    void Start()
    {

        textOnScreen = false;
        manualNumber = 0;
        
    }

    void Update()
    {

        if(textOnScreen)
        {
            switch (manualNumber)
            {
                case 1:
                    FindObjectOfType<InstructionsManager>().StartDialogue(dialogue1);
                   textOnScreen = false;
                    break;

                case 2:
                    FindObjectOfType<InstructionsManager>().StartDialogue(dialogue2);
                    textOnScreen = false;
                    break;

                case 3:
                    FindObjectOfType<InstructionsManager>().StartDialogue(dialogue3);
                    textOnScreen = false;
                    break;

                case 4:
                    FindObjectOfType<InstructionsManager>().StartDialogue(dialogue4);
                    textOnScreen = false;
                    break;

                case 5:
                    FindObjectOfType<InstructionsManager>().StartDialogue(dialogue5);
                    textOnScreen = false;
                    break;

                case 6:
                    FindObjectOfType<InstructionsManager>().StartDialogue(dialogue6);
                    textOnScreen = false;
                    break;

                default:
                    break;

            }
        }

        
    }


    public void TriggerDialogue()
    {
        FindObjectOfType<InstructionsManager>().StartDialogue(dialogue1);
    }

}
