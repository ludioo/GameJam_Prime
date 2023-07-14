using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuestionCanvas : MonoBehaviour
{
    [SerializeField] private List<Button> questionButtonList;
    [SerializeField] private List<Question_SO> questionSequence;

    private void Awake()
    {
        for (int i = 0; i < questionButtonList.Count; i++)
        {
            int index = i;
            questionButtonList[i].onClick.AddListener(() =>
            {
                GameplayManager.Instance.SetString(questionSequence[index].name);
                QuestionManager.Instance.RemoveQuestionsFromList(questionSequence, index);
            });
        }

        gameObject.SetActive(false);
    }

    public IEnumerator InitializeQuestionCanvas()
    {
        if (questionSequence.Count == 0)
        {
            for (int i = 0; i < 3; i++)
            {
                if (QuestionManager.Instance.totalQuestionList.Count > i)
                {
                    Question_SO question_SO = QuestionManager.Instance.totalQuestionList[0];
                    QuestionManager.Instance.AddQuestionToList(questionSequence, question_SO);
                    QuestionManager.Instance.RemoveQuestionsFromList(QuestionManager.Instance.totalQuestionList, 0);

                    questionButtonList[i].gameObject.GetComponentInChildren<TMP_Text>().text = questionSequence[i].questionText;
                }
                else
                {
                    break;
                }
            }
        }
        else if (questionSequence.Count < 3)
        {
            if (QuestionManager.Instance.totalQuestionList.Count == 0)
            {
                QuestionManager.Instance.AddQuestionToList(questionSequence, QuestionManager.Instance.totalQuestionList[0]);
            }
            else
            {
                QuestionManager.Instance.AddQuestionToList(questionSequence, QuestionManager.Instance.totalQuestionList[0]);
                QuestionManager.Instance.RemoveQuestionsFromList(QuestionManager.Instance.totalQuestionList, 0);
            }

            questionButtonList[2].gameObject.GetComponentInChildren<TMP_Text>().text = questionSequence[2].questionText;
        }

        

        yield return new WaitForSeconds(1f);

        gameObject.SetActive(true);
    }

    public void BubbleSort()
    {
        bool swapped;

        for (int i = 0; i < questionSequence.Count - 1; i++)
        {
            swapped = false;

            for (int j = 0; j < questionSequence.Count - i - 1; j++)
            {
                if (questionSequence[j].questionNumber > questionSequence[j + 1].questionNumber)
                {
                    Question_SO temp = questionSequence[j];
                    questionSequence[j] = questionSequence[j + 1];
                    questionSequence[j + 1] = temp;
                    swapped = true;
                }
            }

            if (!swapped)
            {
                break;
            }
        }
    }
}
