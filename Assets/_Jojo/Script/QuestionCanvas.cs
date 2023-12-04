using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuestionCanvas : MonoBehaviour
{
    [SerializeField] private List<Button> questionButtonList;

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

                switch (mainCanvasUI.GetPersonChosen())
                {
                    case MainCanvasUI.PersonChosen.First:
                        GameplayManager.Instance.SetString(aQuestionSequence[_index].name);
                        QuestionManager.Instance.RemoveQuestionsFromList(aQuestionSequence, _index);


                        index = _index;
                        break;
                    case MainCanvasUI.PersonChosen.Second:
                        GameplayManager.Instance.SetString(bQuestionSequence[_index].name);
                        QuestionManager.Instance.RemoveQuestionsFromList(bQuestionSequence, _index);


                        index = _index;
                        break;
                    case MainCanvasUI.PersonChosen.Third:
                        GameplayManager.Instance.SetString(cQuestionSequence[_index].name);
                        QuestionManager.Instance.RemoveQuestionsFromList(cQuestionSequence, _index);


                        index = _index;
                        break;
                }
            });
        }
    }

    public IEnumerator InitializeQuestionCanvas()
    { 
        switch (mainCanvasUI.GetPersonChosen())
        {
            case MainCanvasUI.PersonChosen.First:
                if (QuestionManager.Instance.aTotalQuestionList.Count == 0)
                {
                    questionButtonList[index].gameObject.SetActive(false);

                    for (int i = 0; i < QuestionManager.Instance.aTotalQuestionList.Count; i++)
                    {
                        questionButtonList[i].gameObject.SetActive(true);
                    }
                }
                break;
            case MainCanvasUI.PersonChosen.Second:
                if (QuestionManager.Instance.bTotalQuestionList.Count == 0)
                {
                    questionButtonList[index].gameObject.SetActive(false);

                    for (int i = 0; i < QuestionManager.Instance.bTotalQuestionList.Count; i++)
                    {
                        questionButtonList[i].gameObject.SetActive(true);
                    }
                }
                break;
            case MainCanvasUI.PersonChosen.Third:
                if (QuestionManager.Instance.cTotalQuestionList.Count == 0)
                {
                    questionButtonList[index].gameObject.SetActive(false);

                    for (int i = 0; i < QuestionManager.Instance.cTotalQuestionList.Count; i++)
                    {
                        questionButtonList[i].gameObject.SetActive(true);
                    }
                }
                break;
        }

        if (aQuestionSequence.Count == 0)
        {
            for (int i = 0; i < 3; i++)
            {
                if (QuestionManager.Instance.totalQuestionList.Count > i)
                {
                    Question_SO Aquestion_SO = QuestionManager.Instance.aTotalQuestionList[0];
                    QuestionManager.Instance.AddQuestionToList(aQuestionSequence, Aquestion_SO);
                    QuestionManager.Instance.RemoveQuestionsFromList(QuestionManager.Instance.aTotalQuestionList, 0);

                    questionButtonList[i].gameObject.GetComponentInChildren<TMP_Text>().text = aQuestionSequence[i].questionText;
                }
                else
                {
                    break;
                }
            }
        }
        if (bQuestionSequence.Count == 0)
        {
            for (int i = 0; i < 3; i++)
            {
                if (QuestionManager.Instance.totalQuestionList.Count > i)
                {
                    Question_SO Bquestion_SO = QuestionManager.Instance.bTotalQuestionList[0];
                    QuestionManager.Instance.AddQuestionToList(bQuestionSequence, Bquestion_SO);
                    QuestionManager.Instance.RemoveQuestionsFromList(QuestionManager.Instance.bTotalQuestionList, 0);

                    questionButtonList[i].gameObject.GetComponentInChildren<TMP_Text>().text = bQuestionSequence[i].questionText;
                }
                else
                {
                    break;
                }
            }
        }
        if(cQuestionSequence.Count == 0)
        {
            for (int i = 0; i < 3; i++)
            {
                if (QuestionManager.Instance.totalQuestionList.Count > i)
                {
                    Question_SO Cquestion_SO = QuestionManager.Instance.cTotalQuestionList[0];
                    QuestionManager.Instance.AddQuestionToList(cQuestionSequence, Cquestion_SO);
                    QuestionManager.Instance.RemoveQuestionsFromList(QuestionManager.Instance.cTotalQuestionList, 0);

                    questionButtonList[i].gameObject.GetComponentInChildren<TMP_Text>().text = cQuestionSequence[i].questionText;
                }
                else
                {
                    break;
                }
            }
        }

        if (aQuestionSequence.Count < 3 && aQuestionSequence.Count != 0 && QuestionManager.Instance.aTotalQuestionList.Count != 0)
        {
            Question_SO Aquestion_SO = QuestionManager.Instance.aTotalQuestionList[0];
            QuestionManager.Instance.AddQuestionToList(aQuestionSequence, Aquestion_SO);
            QuestionManager.Instance.RemoveQuestionsFromList(QuestionManager.Instance.aTotalQuestionList, 0);

            for (int i = 0; i < 3; i++)
            {
                questionButtonList[i].gameObject.GetComponentInChildren<TMP_Text>().text = aQuestionSequence[i].questionText;
            }
        }
        else if (bQuestionSequence.Count < 3 && bQuestionSequence.Count != 0 && QuestionManager.Instance.bTotalQuestionList.Count != 0)
        {
            Question_SO Bquestion_SO = QuestionManager.Instance.bTotalQuestionList[0];
            QuestionManager.Instance.AddQuestionToList(bQuestionSequence, Bquestion_SO);
            QuestionManager.Instance.RemoveQuestionsFromList(QuestionManager.Instance.bTotalQuestionList, 0);

            for (int i = 0; i < 3; i++)
            {
                questionButtonList[i].gameObject.GetComponentInChildren<TMP_Text>().text = bQuestionSequence[i].questionText;
            }
        }
        else if (cQuestionSequence.Count < 3 && cQuestionSequence.Count != 0 && QuestionManager.Instance.cTotalQuestionList.Count != 0)
        {
            Question_SO Cquestion_SO = QuestionManager.Instance.cTotalQuestionList[0];
            QuestionManager.Instance.AddQuestionToList(cQuestionSequence, Cquestion_SO);
            QuestionManager.Instance.RemoveQuestionsFromList(QuestionManager.Instance.cTotalQuestionList, 0);

            for (int i = 0; i < 3; i++)
            {
                questionButtonList[i].gameObject.GetComponentInChildren<TMP_Text>().text = cQuestionSequence[i].questionText;
            }
        }

        yield return new WaitForSeconds(1f);

        gameObject.SetActive(true);
    }
}
