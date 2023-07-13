using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PrimeExpress
{
    public class B_CustomSorting : B_BasicEvent
    {
        /* Conversion to Unity XR Notes:
         * Nothing changed. 
         * - Yoshua 2021-11-16
         */

        [Header("QTA Priority Settings")]
        public C_CustomSorting sorting_canvas;
        private float _setTime = 20f;
        public float _timePerOption = 15f;

        [Header("QTA Contents")]
        public string _QTA_Prompt;
        public string[] QTA_Priority_Contents;

        [Header("Result")]
        public int max_score;
        public int miss_count;
        public int final_score;
        public int raw_score;
        public bool sorted_correct { get => final_score == max_score; }
        public bool sorted_correct_with_RawScore { get => raw_score == max_score; }

        public override IEnumerator Flow_EventSequence()
        {
            // reset value
            final_score = 0;
            raw_score = 0;
            this.max_score = QTA_Priority_Contents.Length;

            // Initiate the canvas and wait until user finish sorting
            _setTime = _timePerOption * QTA_Priority_Contents.Length;
            sorting_canvas.Initiate(_QTA_Prompt, QTA_Priority_Contents, _setTime);
            yield return new WaitUntil(() => !sorting_canvas.isInitiated);

            // store score here
            this.miss_count = sorting_canvas.MissCount;
            final_score = Mathf.Clamp(this.max_score - this.miss_count, 0, this.max_score);
            raw_score = sorting_canvas.Score;

            // Debug Output
            Debug.Log(string.Format("Sorting QTA {0}, Correct: {1}, Misscount {2}, Final Score: {3}", gameObject.name, max_score, miss_count, final_score), gameObject);

            // Terminate Canvas
            // EDIT JONATHAN WILLIAM
            sorting_canvas.Terminate();
            yield return new WaitForSeconds(1);
            sorting_canvas.gameObject.SetActive(false);
        }

        public bool sorted_correct_PriorityOnCanvas()
        {
            for (int i = 0; i < QTA_Priority_Contents.Length; i++)
            {
                if (sorting_canvas.GetPriorityButtons()[i].priority != sorting_canvas.GetPriotySelections()[i].priority) return false;
            }
            return true;
        }

    }
}
