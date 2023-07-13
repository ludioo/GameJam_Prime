using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoSlider : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private VideoPlayer videoPlayer;

    private bool dragging;

    public void Drag()
    {
        dragging = true;
    }

    public void ReleaseDrag()
    {
        dragging = false;
        float frame = slider.value * videoPlayer.frameCount;
        videoPlayer.frame = (long)frame;
    }

    public void OnPointerDown()
    {
        if (videoPlayer.isPlaying)
        {
            videoPlayer.Pause();
        }
        else if(!videoPlayer.isPlaying)
        {
            videoPlayer.Play();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        slider = slider.GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (videoPlayer.isPlaying && !dragging)
        {
            slider.value = (float)videoPlayer.frame / (float)videoPlayer.frameCount;
        }
    }
}
