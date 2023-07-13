using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


[RequireComponent(typeof(AudioSource))]
public class C_CustomGesture : MonoBehaviour
{
    [Header("Gesture Bar Options")]
    public TextMeshProUGUI gesture_prompt;
    public Image gesture_bg;
    public Image[] gesture_timebars;

    [Header("Timebar Options")]
    public TextMeshProUGUI time_display;
    public Image[] timebars;

    AudioSource audioSource { get => GetComponent<AudioSource>(); }
    [Header("Sound Options")]
    public AudioClip sound_appear;
    public AudioClip sound_correct;
    public AudioClip sound_wrong;

    [Header("Color Options")]
    public Color color_correct = Color.green;
    public Color color_wrong = Color.red;
    public Color color_standard = Color.magenta;

    [Header("Misc Options")]
    public bool disable_on_start = true;

    // EDIT JONATHAN WILLIAM
    private Animator animator;
    [SerializeField] private float startFlashingTimer;

    private void Start()
    {
        // EDIT JONATHAN WILLIAM
        animator = GetComponent<Animator>();

        gameObject.SetActive(!disable_on_start);
    }

    public void Initiate(string prompt, float max_time)
    {
        //reset disable on start
        disable_on_start = false;

        // display gesture prompt and reset all values
        gesture_prompt.text = prompt;
        UpdateTimeBar(max_time, 0f);
        UpdateGestureBar(0f);

        // set bg color to default
        gesture_bg.color = color_standard;

        // activate the canvas
        gameObject.SetActive(true);

        // play appear sound
        audioSource.clip = sound_appear;
        audioSource.Play();
    }

    public void Terminate()
    {
        // EDIT JONATHAN WILLIAM
        animator.SetTrigger("Hide");

        gameObject.SetActive(false);
    }

    public void UpdateTimeBar(float time_display, float time_fraction)
    {
        // update the timebar depending on time fraction given (0 to 1);
        foreach (var timebar in timebars)
        {
            timebar.fillAmount = Mathf.Clamp01(time_fraction);

            if (time_fraction <= startFlashingTimer)
            {
                timebar.GetComponent<Animator>().SetBool("Flashing", true);
            }
            else
            {
                timebar.GetComponent<Animator>().SetBool("Flashing", false);
            }
        }
        Debug.Log("Time Bar Time Fraction: " + time_fraction.ToString());


        // update time display text ceiling to corresponding int
        this.time_display.text = string.Format("{0:0}", Mathf.Ceil(Mathf.Clamp(time_display, 0f, float.MaxValue)));
    }

    public void UpdateGestureBar(float time_fraction)
    {
        // update the gesture bar depending on time fraction given (0 to 1);
        foreach (var timebar in gesture_timebars)
        {
            timebar.fillAmount = Mathf.Clamp01(time_fraction);
        }
        Debug.Log("Gesture Bar Time Fraction: " + time_fraction.ToString());
    }

    public void SetCanvasCorrect()
    {
        // remove the gesture bar
        UpdateGestureBar(0f);

        // play correct sound
        audioSource.clip = sound_correct;
        audioSource.Play();

        // change display color
        gesture_bg.color = color_correct;
    }

    public void SetCanvasWrong()
    {
        // remove the gesture bar
        UpdateGestureBar(0f);

        // play correct sound
        audioSource.clip = sound_wrong;
        audioSource.Play();

        // change display color
        gesture_bg.color = color_wrong;
    }
}
