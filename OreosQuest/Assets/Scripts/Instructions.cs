using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Fem una classe nova per poder guardar les instruccions

[System.Serializable]
public class Instructions
{
    public string name;

    [TextArea(3,10)]
    public string[] sentences;

}
