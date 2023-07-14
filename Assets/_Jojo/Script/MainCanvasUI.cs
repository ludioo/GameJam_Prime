using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainCanvasUI : MonoBehaviour
{
    public enum PersonChosen
    {
        First,
        Second,
        Third
    }

    private PersonChosen personChosen;

    public Button submitButton;
    public GameObject confirmationUI;
    public List<GameObject> dialogueBox;

    private void Awake()
    {
        submitButton.onClick.AddListener(() =>
        {
            GameplayManager.Instance.ChangeState();
        });
    }

    public void InitializeDialogueBox(string text)
    {
        foreach (GameObject box in dialogueBox)
        {
            box.GetComponentInChildren<TMP_Text>().text = text;
        }
    }

    public void ChangeState(int index)
    {
        switch (index)
        {
            case 0:
                personChosen = PersonChosen.First;
                break;

            case 1:
                personChosen = PersonChosen.Second;
                break;

            case 2:
                personChosen = PersonChosen.Third;
                break;
        }
    }

    public PersonChosen GetPersonChosen()
    {
        return personChosen;
    }
}