using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuestionCanvas : MonoBehaviour
{
    [SerializeField] private List<Button> questionButtonList;
    [SerializeField] private List<Question_SO> questionSequence;

    [SerializeField] private List<Question_SO> aQuestionSequence;
    [SerializeField] private List<Question_SO> bQuestionSequence;
    [SerializeField] private List<Question_SO> cQuestionSequence;

    private int index;

    public MainCanvasUI mainCanvasUI;

    private void Awake()
    {
        for (int i = 0; i < questionButtonList.Count; i++)
        {
            int _index = i;
            questionButtonList[_index].onClick.AddListener(() =>
            {
                GameplayManager.Instance.SetString(questionSequence[_index].name);

                switch (mainCanvasUI.GetPersonChosen())
                {
                    case MainCanvasUI.PersonChosen.First:
                        QuestionManager.Instance.RemoveQuestionsFromList(aQuestionSequence, _index);
                        break;
                    case MainCanvasUI.PersonChosen.Second:
                        QuestionManager.Instance.RemoveQuestionsFromList(bQuestionSequence, _index);
                        break;
                    case MainCanvasUI.PersonChosen.Third:
                        QuestionManager.Instance.RemoveQuestionsFromList(cQuestionSequence, _index);
                        break;
                }

                index = _index;
            });
        }
    }

    public IEnumerator InitializeQuestionCanvas()
    {
        if (QuestionManager.Instance.totalQuestionList.Count == 0)
        {
            questionButtonList[index].gameObject.SetActive(false);
            QuestionManager.Instance._RemoveQuestionsFromList(questionButtonList, index);
        }

        if (questionSequence.Count == 0 && aQuestionSequence.Count == 0 && bQuestionSequence.Count == 0 && cQuestionSequence.Count == 0)
        {
            for (int i = 0; i < 3; i++)
            {
                if (QuestionManager.Instance.totalQuestionList.Count > i)
                {
                    Question_SO question_SO = QuestionManager.Instance.totalQuestionList[0];
                    QuestionManager.Instance.AddQuestionToList(questionSequence, question_SO);
                    QuestionManager.Instance.RemoveQuestionsFromList(QuestionManager.Instance.totalQuestionList, 0);

                    Question_SO Aquestion_SO = QuestionManager.Instance.totalQuestionList[0];
                    QuestionManager.Instance.AddQuestionToList(aQuestionSequence, Aquestion_SO);
                    QuestionManager.Instance.RemoveQuestionsFromList(QuestionManager.Instance.aTotalQuestionList, 0);

                    Question_SO Bquestion_SO = QuestionManager.Instance.totalQuestionList[0];
                    QuestionManager.Instance.AddQuestionToList(bQuestionSequence, Bquestion_SO);
                    QuestionManager.Instance.RemoveQuestionsFromList(QuestionManager.Instance.bTotalQuestionList, 0);

                    Question_SO Cquestion_SO = QuestionManager.Instance.totalQuestionList[0];
                    QuestionManager.Instance.AddQuestionToList(cQuestionSequence, Cquestion_SO);
                    QuestionManager.Instance.RemoveQuestionsFromList(QuestionManager.Instance.cTotalQuestionList, 0);

                    questionButtonList[i].gameObject.GetComponentInChildren<TMP_Text>().text = questionSequence[i].questionText;
                }
                else
                {
                    break;
                }
            }
        }
        else if (aQuestionSequence.Count < 3)
        {
            if (QuestionManager.Instance.aTotalQuestionList.Count != 0)
            {
                QuestionManager.Instance.AddQuestionToList(aQuestionSequence, QuestionManager.Instance.aTotalQuestionList[0]);
                QuestionManager.Instance.RemoveQuestionsFromList(QuestionManager.Instance.aTotalQuestionList, 0);

                for (int i = 0; i < 3; i++)
                {
                    questionButtonList[i].gameObject.GetComponentInChildren<TMP_Text>().text = aQuestionSequence[i].questionText;
                }
            }
            else if (QuestionManager.Instance.bTotalQuestionList.Count != 0)
            {
                QuestionManager.Instance.AddQuestionToList(bQuestionSequence, QuestionManager.Instance.bTotalQuestionList[0]);
                QuestionManager.Instance.RemoveQuestionsFromList(QuestionManager.Instance.bTotalQuestionList, 0);

                for (int i = 0; i < 3; i++)
                {
                    questionButtonList[i].gameObject.GetComponentInChildren<TMP_Text>().text = bQuestionSequence[i].questionText;
                }
            }
            else if (QuestionManager.Instance.cTotalQuestionList.Count != 0)
            {
                QuestionManager.Instance.AddQuestionToList(cQuestionSequence, QuestionManager.Instance.cTotalQuestionList[0]);
                QuestionManager.Instance.RemoveQuestionsFromList(QuestionManager.Instance.cTotalQuestionList, 0);

                for (int i = 0; i < 3; i++)
                {
                    questionButtonList[i].gameObject.GetComponentInChildren<TMP_Text>().text = cQuestionSequence[i].questionText;
                }
            }
        }

        

        yield return new WaitForSeconds(1f);

        gameObject.SetActive(true);
    }
}
