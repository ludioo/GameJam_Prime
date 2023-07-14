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
                        break;
                    case MainCanvasUI.PersonChosen.Second:
                        GameplayManager.Instance.SetString(bQuestionSequence[_index].name);
                        QuestionManager.Instance.RemoveQuestionsFromList(bQuestionSequence, _index);
                        break;
                    case MainCanvasUI.PersonChosen.Third:
                        GameplayManager.Instance.SetString(cQuestionSequence[_index].name);
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

        if (aQuestionSequence.Count == 0)
        {
            for (int i = 0; i < 3; i++)
            {
                if (QuestionManager.Instance.totalQuestionList.Count > i)
                {
                    Question_SO Aquestion_SO = QuestionManager.Instance.totalQuestionList[i];
                    QuestionManager.Instance.AddQuestionToList(aQuestionSequence, Aquestion_SO);
                    QuestionManager.Instance.RemoveQuestionsFromList(QuestionManager.Instance.aTotalQuestionList, i);

                    questionButtonList[i].gameObject.GetComponentInChildren<TMP_Text>().text = aQuestionSequence[i].questionText;
                }
                else
                {
                    break;
                }
            }
        }
        else if (bQuestionSequence.Count == 0)
        {
            for (int i = 0; i < 3; i++)
            {
                if (QuestionManager.Instance.totalQuestionList.Count > i)
                {
                    Question_SO Bquestion_SO = QuestionManager.Instance.totalQuestionList[i];
                    QuestionManager.Instance.AddQuestionToList(aQuestionSequence, Bquestion_SO);
                    QuestionManager.Instance.RemoveQuestionsFromList(QuestionManager.Instance.bTotalQuestionList, i);

                    questionButtonList[i].gameObject.GetComponentInChildren<TMP_Text>().text = bQuestionSequence[i].questionText;
                }
                else
                {
                    break;
                }
            }
        }
        else if(cQuestionSequence.Count == 0)
        {
            for (int i = 0; i < 3; i++)
            {
                if (QuestionManager.Instance.totalQuestionList.Count > i)
                {
                    Question_SO Cquestion_SO = QuestionManager.Instance.totalQuestionList[i];
                    QuestionManager.Instance.AddQuestionToList(aQuestionSequence, Cquestion_SO);
                    QuestionManager.Instance.RemoveQuestionsFromList(QuestionManager.Instance.cTotalQuestionList, i);

                    questionButtonList[i].gameObject.GetComponentInChildren<TMP_Text>().text = cQuestionSequence[i].questionText;
                }
                else
                {
                    break;
                }
            }
        }

        
        

        

        yield return new WaitForSeconds(1f);

        gameObject.SetActive(true);
    }
}
