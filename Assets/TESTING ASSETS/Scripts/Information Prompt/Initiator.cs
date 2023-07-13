using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Initiator : MonoBehaviour
{
    public static Initiator Instance { get; private set; }

    [SerializeField] private C_InformationPrompt canvasInformationPrompt;
    [SerializeField] private List<Person_SO> personSOList;

    private string[] mainButtonText;
    private string[] contentTitle;
    private Sprite[] contentImage;

    [TextArea]
    private string[] contentDescription;

    private void Awake()
    {
        Instance = this;

        mainButtonText = new string[personSOList.Count];
        contentTitle = new string[personSOList.Count];
        contentImage = new Sprite[personSOList.Count];
        contentDescription = new string[personSOList.Count];
    }

    public void InitiatePersonInformation()
    {
        for (int i = 0; i < personSOList.Count; i++)
        {
            mainButtonText[i] = personSOList[i].personName;
            contentTitle[i] = personSOList[i].personName;
            contentImage[i] = personSOList[i].personImage;
            contentDescription[i] = personSOList[i].personDescription;
        }
    }

    public void InitiateInformationPrompt()
    {
        InitiatePersonInformation();
        canvasInformationPrompt.Initiate("Notes", mainButtonText, contentTitle, contentImage, contentDescription);
    }
}
