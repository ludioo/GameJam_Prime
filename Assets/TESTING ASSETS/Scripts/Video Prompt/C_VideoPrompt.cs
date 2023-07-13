using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Video;

public class C_VideoPrompt : MonoBehaviour
{
    public List<Contents> contentList = new List<Contents>();

    [Serializable]
    public class Contents
    {
        public GameObject content;
        public RawImage rawImage;

        [Header("Please fill if you want to use the timebar!")]
        public GameObject videoTimeBar;
    }

    [SerializeField] private bool disableOnStart = true;

    public TextMeshProUGUI promptTitle;

    [SerializeField] private Button closeButton;
    [SerializeField] private AudioSource buttonSFX;

    public bool closing = false;

    [SerializeField] private bool useTimebar = true;

    [Header("Please fill if there is more than 1 video!")]
    [SerializeField] private ContentSlider contentSlider;

    [SerializeField] private Button nextButton;
    [SerializeField] private Button previousButton;
    [SerializeField] private Scrollbar scrollbar;


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

    public virtual void InitiateWithoutDetailsPrompt(string _promptTitle, Texture[] rawImageTexture, RenderTexture[] renderTexture)
    {
        foreach (Contents contents in contentList)
        {
            if (!useTimebar)
            {
                contents.content.GetComponent<VideoSlider>().enabled = false;
                contents.videoTimeBar.SetActive(false);
            }

            contents.content.SetActive(false);
        }

        for (int i = 0; i < rawImageTexture.Length; i++)
        {
            contentList[i].rawImage.texture = rawImageTexture[i];
            contentList[i].rawImage.GetComponent<VideoPlayer>().targetTexture = renderTexture[i];
            contentList[i].content.gameObject.SetActive(true);
        }

        if (rawImageTexture.Length > 1)
        {
            scrollbar = scrollbar.GetComponent<Scrollbar>();

            nextButton.onClick.AddListener(() =>
            {
                if (contentSlider.posisi < contentSlider.contentPosition.Length - 1)
                {
                    previousButton.gameObject.SetActive(true);
                    contentSlider.posisi += 1;
                    contentSlider.scrollPosition = contentSlider.contentPosition[contentSlider.posisi];
                    buttonSFX.Play();
                }

                for (int i = 0; i < contentList.Count; i++)
                {
                    contentList[i].rawImage.GetComponent<VideoPlayer>().Stop();
                }
            });

            previousButton.onClick.AddListener(() =>
            {
                if (contentSlider.posisi > 0)
                {
                    nextButton.gameObject.SetActive(true);
                    contentSlider.posisi -= 1;
                    contentSlider.scrollPosition = contentSlider.contentPosition[contentSlider.posisi];
                    buttonSFX.Play();
                }

                for (int i = 0; i < contentList.Count; i++)
                {
                    contentList[i].rawImage.GetComponent<VideoPlayer>().Stop();
                }
            });
        }

        promptTitle.text = _promptTitle;
        gameObject.SetActive(true);
    }

    public void Terminate()
    {
        for (int i = 0; i < contentList.Count; i++)
        {
            contentList[i].rawImage.GetComponent<VideoPlayer>().Stop();
        }
        closing = false;
        animator.SetTrigger("Hide");
    }

    private void Update()
    {
        if (contentSlider != null)
        {
            if (contentSlider.posisi == 0)
            {
                previousButton.gameObject.SetActive(false);
            }

            if (contentSlider.posisi == contentList.Count - 1)
            {
                nextButton.gameObject.SetActive(false);
            }
        }
    }
}
