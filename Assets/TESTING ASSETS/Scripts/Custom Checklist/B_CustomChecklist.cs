using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace PrimeExpress
{
    public class B_CustomChecklist : B_BasicEvent
    {
        [Header("Canvas Ref")]
        public C_Checklist canvas_checklist;

        [Header("Content List")]
        public string QTA_Prompt = "";
        private int selectCount = 0;
        public List<C_Checklist.QTA_ChecklistSet> CheckListSet = new List<C_Checklist.QTA_ChecklistSet>();

        [Header("Continue Method")]
        public ContinueMethod continueMethod = ContinueMethod.SelectionLimit;
        public int selectLimit = 2;

        [Header("Shuffle Method")]
        public SHUFFLE shuffle = SHUFFLE.NO_SHUFFLE;

        [Header("Internal Counter")]
        public bool counter_initialized = false;
        public int total_options = 0;
        public int total_selections = 0;
        public int total_correct_options = 0;
        public int total_correct_selected = 0;

        public enum ContinueMethod
        {
            SelectionLimit,
            AllCorrect
        }

        public enum SHUFFLE
        {
            NO_SHUFFLE,
            SHUFFLE_FIRST_TIME
        }

        private void Start()
        {
            counter_initialized = false;
            if (canvas_checklist == null)
            {
                canvas_checklist = C_Checklist.Instance;
                if (canvas_checklist == null) Debug.LogError("Missing Checklist Canvas!", this);
            }
            if (CheckListSet.Count == 0) Debug.LogError("No Checklist Set!", this);
        }

        public override IEnumerator Flow_Initiate()
        {
            if (!counter_initialized)
            {
                if (shuffle == SHUFFLE.SHUFFLE_FIRST_TIME) CheckListSet = ShuffleList(CheckListSet);
                InitializeCounter();
                foreach (var x in CheckListSet)
                {
                    total_correct_options += (x.isCorrect) ? 1 : 0;
                    x.disable = false;
                }
            }
            yield return null;
        }

        private List<T> ShuffleList<T>(List<T> list_to_shuffle)
        {
            List<int> index_shuffler = new List<int>();
            List<int> index_shuffled = new List<int>();

            for (int i = 0; i < list_to_shuffle.Count; i++)
            {
                int x = i;
                index_shuffler.Add(x);
            }

            for (int i = 0; i < list_to_shuffle.Count; i++)
            {
                int index = Random.Range(0, index_shuffler.Count);
                index_shuffled.Add(index_shuffler[index]);
                index_shuffler.RemoveAt(index);
            }

            List<T> newList = new List<T>(list_to_shuffle);
            for (int i = 0; i < list_to_shuffle.Count; i++)
            {
                newList[i] = list_to_shuffle[index_shuffled[i]];
            }

            return newList;
        }

        [ContextMenu("Try Shuffle")]
        private void Shuffle()
        {
            foreach (var shuf in ShuffleList(CheckListSet))
            {
                Debug.Log(shuf.content);
            }
        }

        private void InitializeCounter()
        {
            branch = 0;
            total_options = 0;
            total_selections = 0;
            total_correct_options = 0;
            total_correct_selected = 0;
            counter_initialized = true;
        }

        public override IEnumerator Flow_EventSequence()
        {
            switch (continueMethod)
            {
                case ContinueMethod.AllCorrect:
                    Debug.Log(string.Format("Correct/Total Correct: {0}/{1}", total_correct_selected, total_correct_options));
                    if (total_correct_selected == total_correct_options)
                    {
                        Debug.Log("Loading Next Sequence");
                        //counter_initialized = false;
                        branch = 0;

                        // just clear list in Extra_Support_CheackList if only support_Cheacklist in QTA_Checklist_Canvas not null
                        if (canvas_checklist.support_CheackList != null)
                        {
                            Debug.Log("Clear List");
                            canvas_checklist.support_CheackList.clearList();
                        }

                        yield break;
                    }
                    break;
                case ContinueMethod.SelectionLimit:
                    Debug.Log(string.Format("Selected/Total Options: {0}/{1}", total_selections, total_options));
                    if (selectCount >= selectLimit)
                    {
                        Debug.Log("Loading Next Sequence");
                        //counter_initialized = false;
                        branch = 0;

                        // just clear list in Extra_Support_CheackList if only support_Cheacklist in QTA_Checklist_Canvas not null
                        if (canvas_checklist.support_CheackList != null)
                        {
                            Debug.Log("Clear List");
                            canvas_checklist.support_CheackList.clearList();
                        }

                        yield break;
                    }
                    break;
            }

            Debug.Log("initiating in coroutine");
            canvas_checklist.Initiate(QTA_Prompt, CheckListSet);

            yield return new WaitUntil(() => canvas_checklist.isSelected);
            C_Checklist.QTA_ChecklistSet selectedSet = canvas_checklist.selectedChecklist;
            branch = selectedSet.branch_id;
            total_correct_selected += selectedSet.isCorrect ? 1 : 0;
            Debug.Log("Option Selected");

            // delay x seconds for animation purpose
            yield return new WaitForSeconds(2f);

            // EDIT JONATHAN WILLIAM
            canvas_checklist.GetComponent<Animator>().SetTrigger("Hide");
            yield return new WaitForSeconds(1f);

            canvas_checklist.gameObject.SetActive(false);

            selectCount++;
        }

        public override IEnumerator Flow_Terminate()
        {
            return base.Flow_Terminate();
        }
    }
}

