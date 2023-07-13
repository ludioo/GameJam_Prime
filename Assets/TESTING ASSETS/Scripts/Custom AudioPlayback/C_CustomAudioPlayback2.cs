using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

namespace PrimeExpress
{
    public class C_CustomAudioPlayback2 : MonoBehaviour
    {
        [Tooltip("If blank will fire a log at start")]
        public AudioSource as_canvas;

        [Header("Canvas Display Items")]
        public Image image_playbackPos;
        public TextMeshProUGUI text_headerText;
        public TextMeshProUGUI text_playtimeLeft;

        public bool is_initiate { get; private set; } = false;
        Coroutine CRC_PlaybackAudio = null;

        private Animator animator;

        private void Start()
        {
            animator = GetComponent<Animator>();
            // please assign audio source via inspector
            if (!as_canvas) Debug.LogError("PLEASE ASSIGN AUDIO SOURCE FOR AUDIO PLAYBACK CANVAS");

            // at the beginning this should turn off
            gameObject.SetActive(false);
        }

        public void Initiate(AudioClip audio_clip, string header_text = "")
        {
            // check if system already initiated
            if (is_initiate)
            {
                // if already initiated, terminate to start fresh
                Terminate();
            }

            // reset some flags
            is_initiate = true;

            // asign audio clip
            as_canvas.Stop();
            as_canvas.clip = audio_clip;

            // assign header text to match input parameters
            text_headerText.text = header_text;

            // enable gameobject to show canvas
            gameObject.SetActive(true);

            // set image filled position at zero
            image_playbackPos.fillAmount = 0f;

            // Initiate playback
            CRC_PlaybackAudio = StartCoroutine(CR_PlaybackAudio());
        }

        private IEnumerator CR_PlaybackAudio()
        {
            // play the canvas and store variables regarding play time
            as_canvas.Play();
            float audio_length = as_canvas.clip.length;
            float playback_pos = 0f;
            float playback_left = audio_length;

            // wait until audio source stop playing while updating the UI
            yield return new WaitUntil(() =>
            {
                // calculate display values
                playback_pos = Mathf.Clamp01(as_canvas.time / audio_length);
                playback_left = audio_length - as_canvas.time;

                // set value displays
                text_playtimeLeft.text = DisplayTime(playback_left);
                image_playbackPos.fillAmount = playback_pos;

                return !as_canvas.isPlaying;
            });

            Terminate();
        }

        // EDIT JONATHAN WILLIAM
        public void Terminate()
        {
            // stop audio playback and nullified
            as_canvas.Stop();
            as_canvas.clip = null;

            // clear coroutine if exist
            if (CRC_PlaybackAudio != null)
                StopCoroutine(CRC_PlaybackAudio);

            // EDIT JONATHAN WILLIAM
            // disable canvas
            //gameObject.SetActive(false);
            animator.SetTrigger("Hide");
            float timer = 2f;
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                gameObject.SetActive(false);
            }

            // reset initiate
            is_initiate = false;
        }

        private string DisplayTime(float timeToDisplay)
        {
            timeToDisplay += 1;

            float minutes = Mathf.FloorToInt(timeToDisplay / 60);
            float seconds = Mathf.FloorToInt(timeToDisplay % 60);

            return string.Format("{0:00}:{1:00}", minutes, seconds);
        }

        private void OnDisable()
        {
            Terminate();
        }
    }
}
