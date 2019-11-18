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

    public bool manualIsOn;
    public int manualNumber;

    void Start()
    {

        manualIsOn = false;
        manualNumber = 0;
        
    }

    void Update()
    {

        if(manualIsOn)
        {
            switch (manualNumber)
            {
                case 1:
                    FindObjectOfType<InstructionsManager>().StartDialogue(dialogue1);
                   manualIsOn = false;
                    break;

                case 2:
                    FindObjectOfType<InstructionsManager>().StartDialogue(dialogue2);
                    manualIsOn = false;
                    break;

                case 3:
                    FindObjectOfType<InstructionsManager>().StartDialogue(dialogue3);
                    manualIsOn = false;
                    break;

                case 4:
                    FindObjectOfType<InstructionsManager>().StartDialogue(dialogue4);
                    manualIsOn = false;
                    break;

                case 5:
                    FindObjectOfType<InstructionsManager>().StartDialogue(dialogue5);
                    manualIsOn = false;
                    break;

                case 6:
                    FindObjectOfType<InstructionsManager>().StartDialogue(dialogue6);
                    manualIsOn = false;
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
