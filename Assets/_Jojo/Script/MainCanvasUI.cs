using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainCanvasUI : MonoBehaviour
{
    [SerializeField] private Button hintPanelUI;
    [SerializeField] private Button notePanelUI;

    private void Awake()
    {
        notePanelUI.onClick.AddListener(() =>
        {
            Initiator.Instance.InitiateInformationPrompt();
            gameObject.SetActive(false);
        });
    }
}