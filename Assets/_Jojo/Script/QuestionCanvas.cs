using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuestionCanvas : MonoBehaviour
{
    [SerializeField] private List<Button> questionList;
    [SerializeField] private List<Question_SO> questionSequence;

    private int removedIndex;

    private void Awake()
    {
        questionSequence = new List<Question_SO>();

        for (int i = 0; i < questionList.Count; i++)
        {
            questionList[i].onClick.AddListener(() =>
            {
                QuestionManager.Instance.RemoveQuestionsFromList(questionSequence, i);
                removedIndex = i;
            });
        }
    }

    private void OnEnable()
    {
        if (questionSequence == null)
        {
            for (int i = 0; i < 3; i++)
            {
                QuestionManager.Instance.AddQuestionToList(questionSequence, QuestionManager.Instance.aQuestionList[i]);
                QuestionManager.Instance.AddQuestionToList(questionSequence, QuestionManager.Instance.bQuestionList[i]);
                QuestionManager.Instance.AddQuestionToList(questionSequence, QuestionManager.Instance.cQuestionList[i]);
            }
        }
        else
        {
            
        }
    }
}
