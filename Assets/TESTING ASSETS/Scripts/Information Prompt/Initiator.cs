using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initiator : MonoBehaviour
{
    [SerializeField] private C_InformationPrompt canvasInformationPrompt;
    [SerializeField] private List<Person_SO> personSOList;

    private string mainButtonText;
    private string contentTitle;
    private Sprite contentImage;

    [TextArea]
    private string contentDescription;

    public void InitiateInformationPrompt()
    {
        
    }
}
