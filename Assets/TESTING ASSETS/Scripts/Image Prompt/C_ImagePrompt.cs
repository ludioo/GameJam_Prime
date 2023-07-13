using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class C_ImagePrompt : MonoBehaviour
{
    [SerializeField] private C_DetailsPrompt canvasDetailsPrompt;
    public List<Contents> contentList = new List<Contents>();

    [Serializable]
    public class Contents
    {
        public GameObject content;

        public Image image;
        public Button descriptionButton;
    }

    [SerializeField] private bool disableOnStart = true;

    public TextMeshProUGUI promptTitle;

    [SerializeField] private Button closeButton;
    [SerializeField] private AudioSource buttonSFX;

    [Header("Please fill if there is more than 1 image!")]
    [SerializeField] private ContentSlider contentSlider;

    public Button nextButton;
    public Button previousButton;
    [SerializeField] private Scrollbar scrollbar;

    public bool closing = false;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        if (contentList.Count == 0)
        {
            Debug.LogError("Please insert at least 1 image in the inspector!"); ;
        }
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

    public virtual void InitiateWithoutDetailsPrompt(string _promptTitle, Sprite[] images)
    {
        foreach (Contents contents in contentList)
        {
            contents.content.SetActive(false);

            Destroy(contents.content.GetComponent<EventTrigger>());
        }

        for (int i = 0; i < images.Length; i++)
        {
            contentList[i].image.sprite = images[i];
            contentList[i].content.gameObject.SetActive(true);
        }

        if (images.Length > 1)
        {
            scrollbar = scrollbar.GetComponent<Scrollbar>();

            nextButton.onClick.AddListener(() =>
            {

                if (contentSlider.posisi < contentSlider.contentPosition.Length - 1)
                {
                    contentSlider.posisi += 1;
                    contentSlider.scrollPosition = contentSlider.contentPosition[contentSlider.posisi];
                    buttonSFX.Play();
                }
            });

            previousButton.onClick.AddListener(() =>
            {
                if (contentSlider.posisi > 0)
                {
                    contentSlider.posisi -= 1;
                    contentSlider.scrollPosition = contentSlider.contentPosition[contentSlider.posisi];
                    buttonSFX.Play();
                }
            });
        }

        promptTitle.text = _promptTitle;
        canvasDetailsPrompt.promptTitle.text = _promptTitle;
        gameObject.SetActive(true);
    }
    public virtual void InitiateWithDetailsPrompt(string _promptTitle, Sprite[] images, string[] descriptionText)
    {
        foreach (Contents contents in contentList)
        {
            contents.content.SetActive(false);

            contents.descriptionButton.gameObject.SetActive(false);
            contents.descriptionButton.onClick.RemoveAllListeners();
        }

        for (int i = 0; i < images.Length; i++)
        {
            contentList[i].image.sprite = images[i];
            contentList[i].content.gameObject.SetActive(true);

            int j = i - 1;

            do
            {
                j++;

                contentList[j].descriptionButton.onClick.AddListener(() =>
                {
                    canvasDetailsPrompt.promptTitle = promptTitle;
                    canvasDetailsPrompt.descriptionImage.sprite = contentList[j].image.sprite;
                    canvasDetailsPrompt.descriptionText.text = descriptionText[j];

                    canvasDetailsPrompt.animator.SetTrigger("Show");
                    buttonSFX.Play();
                });

            } while (j < i);
        }

        if (images.Length > 1)
        {
            scrollbar = scrollbar.GetComponent<Scrollbar>();

            nextButton.onClick.AddListener(() =>
            {

                if (contentSlider.posisi < contentSlider.contentPosition.Length - 1)
                {
                    previousButton.gameObject.SetActive(true);
                    previousButton.GetComponent<Animator>().SetBool("Active", true);
                    contentSlider.posisi += 1;
                    contentSlider.scrollPosition = contentSlider.contentPosition[contentSlider.posisi];
                    buttonSFX.Play();
                }
            });

            previousButton.onClick.AddListener(() =>
            {
                if (contentSlider.posisi > 0)
                {
                    nextButton.gameObject.SetActive(true);
                    nextButton.GetComponent<Animator>().SetBool("Active", true);
                    contentSlider.posisi -= 1;
                    contentSlider.scrollPosition = contentSlider.contentPosition[contentSlider.posisi];
                    buttonSFX.Play();
                }
            });
        }

        promptTitle.text = _promptTitle;
        canvasDetailsPrompt.promptTitle.text = _promptTitle;
        gameObject.SetActive(true);
    }
    public void Terminate()
    {
        closing = false;
        animator.SetTrigger("Hide");
    }

    private void Update()
    {
        if (contentSlider != null)
        {
            if (contentSlider.posisi == 0)
            {
                previousButton.GetComponent<Animator>().SetBool("Active", false);

                float timer = 1f;

                timer -= Time.deltaTime;
                if (timer <= 0f)
                {
                    timer = 1f;
                    previousButton.gameObject.SetActive(false);
                }
            }

            if (contentSlider.posisi == contentList.Count - 1)
            {
                nextButton.GetComponent<Animator>().SetBool("Active", false);

                float timer = 1f;

                timer -= Time.deltaTime;
                if (timer <= 0f)
                {
                    timer = 1f;
                    nextButton.gameObject.SetActive(false);
                }
            }
        }
    }
}
