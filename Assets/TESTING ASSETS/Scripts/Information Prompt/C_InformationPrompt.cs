using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class C_InformationPrompt : MonoBehaviour
{
    public List<Button> mainButtonList = new List<Button>();

    public List<Contents> contentList = new List<Contents>();

    [SerializeField] private TextMeshProUGUI promptTitle;

    [Serializable]
    public class Contents
    {
        public GameObject panel;

        public TextMeshProUGUI contentTitle;
        public Image contentImage;
        public TextMeshProUGUI contentText;
        public Button backButton;
    }

    [SerializeField] private GameObject defaultPanel;

    public Button closeButton;
    [SerializeField] private AudioSource buttonSFX;
    [SerializeField] private bool disableOnStart = true;

    public bool closing = false;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        gameObject.SetActive(!disableOnStart);

        closeButton.onClick.AddListener(() =>
        {
            buttonSFX.Play();
            closing = true;
        });
    }

    public virtual void Initiate(string _promptTitle, string[] mainButtonText, string[] contentTitle, Image[] contentImage, string[] contentText)
    {
        foreach (Contents panel in contentList)
        {
            panel.panel.SetActive(false);
        }
        
        foreach (Button mainButton in mainButtonList)
        {
            mainButton.gameObject.SetActive(false);
            mainButton.onClick.RemoveAllListeners();
        }

        for (int i = 0; i < mainButtonText.Length; i++)
        {
            mainButtonList[i].GetComponentInChildren<TextMeshProUGUI>().text = mainButtonText[i];

            contentList[i].contentTitle.text = contentTitle[i];
            contentList[i].contentText.text = contentText[i];

            contentList[i].contentImage = contentImage[i];

            int j = i-1;

            do
            {
                j++;
                mainButtonList[j].onClick.AddListener(() =>
                {
                    foreach (Contents panels in contentList)
                    {
                        panels.panel.SetActive(false);
                        defaultPanel.SetActive(false);
                    }
                    contentList[j].panel.SetActive(true);
                    buttonSFX.Play();
                });

                contentList[j].backButton.onClick.AddListener(() =>
                {
                    defaultPanel.SetActive(true);
                    contentList[j].panel.SetActive(false);
                    buttonSFX.Play();
                });

                ;
            } while (j < i);

            mainButtonList[i].gameObject.SetActive(true);
        }

        promptTitle.text = _promptTitle;
        gameObject.SetActive(true);
    }
    
    public void Terminate()
    {
        closing = false;
        animator.SetTrigger("Hide");
    }
}
