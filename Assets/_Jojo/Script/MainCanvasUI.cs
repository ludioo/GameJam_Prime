using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainCanvasUI : MonoBehaviour
{
    public enum PersonChosen
    {
        First,
        Second,
        Third
    }

    private PersonChosen personChosen;

    [SerializeField] private Button hintPanelUI;
    [SerializeField] private Button notePanelUI;

    [Header("Choices of People")]
    [SerializeField] private Button firstPersonButton;
    [SerializeField] private Button secondPersonButton;
    [SerializeField] private Button thirdPersonButton;

    private void Awake()
    {
        notePanelUI.onClick.AddListener(() =>
        {
            Initiator.Instance.InitiateInformationPrompt();
            gameObject.SetActive(false);
        });

        firstPersonButton.onClick.AddListener(() =>
        {
            personChosen = PersonChosen.First;
        });

        secondPersonButton.onClick.AddListener(() =>
        {
            personChosen = PersonChosen.Second;
        });

        thirdPersonButton.onClick.AddListener(() =>
        {
            personChosen = PersonChosen.Third;
        });
    }

    public PersonChosen GetChosenPerson()
    {
        return personChosen;
    }
}