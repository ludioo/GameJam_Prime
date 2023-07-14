using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainCanvasUI : MonoBehaviour
{
    public List<GameObject> dialogueBox;

    public void InitializeDialogueBox(string text)
    {
        foreach (GameObject box in dialogueBox)
        {
            box.GetComponentInChildren<TMP_Text>().text = text;
        }
    }
}