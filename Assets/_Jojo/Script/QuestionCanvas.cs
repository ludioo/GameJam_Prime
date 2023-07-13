using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuestionCanvas : MonoBehaviour
{
    [SerializeField] private List<Button> questionList;
    [SerializeField] private List<Question_SO> questionSequence;

    [Space]
    [SerializeField] private MainCanvasUI mainCanvasUI;

    private void Awake()
    {
        switch (mainCanvasUI.GetChosenPerson())
        {
            case MainCanvasUI.PersonChosen.First:
                for (int i = 0; i < questionList.Count; i++)
                {
                    questionList[i].onClick.AddListener(() =>
                    {
                        QuestionManager.Instance.RemoveQuestionsFromList(questionSequence, i);
                    });
                }
                break;

            case MainCanvasUI.PersonChosen.Second:
                for (int i = 0; i < questionList.Count; i++)
                {
                    questionList[i].onClick.AddListener(() =>
                    {
                        QuestionManager.Instance.RemoveQuestionsFromList(questionSequence, i);
                    });
                }
                break;

            case MainCanvasUI.PersonChosen.Third:
                for (int i = 0; i < questionList.Count; i++)
                {
                    questionList[i].onClick.AddListener(() =>
                    {
                        QuestionManager.Instance.RemoveQuestionsFromList(questionSequence, i);
                    });
                }
                break;
        }
        gameObject.SetActive(false);
    }

    public void InitializeQuestionCanvas()
    {
        gameObject.SetActive(true);
        if (questionSequence == null)
        {
            switch (mainCanvasUI.GetChosenPerson())
            {
                case MainCanvasUI.PersonChosen.First:
                    for (int i = 0; i < 3; i++)
                    {
                        QuestionManager.Instance.AddQuestionToList(questionSequence, QuestionManager.Instance.aQuestionList[i]);
                        QuestionManager.Instance.RemoveQuestionsFromList(QuestionManager.Instance.aQuestionList, i);
                    }
                    break;

                case MainCanvasUI.PersonChosen.Second:
                    for (int i = 0; i < 3; i++)
                    {
                        QuestionManager.Instance.AddQuestionToList(questionSequence, QuestionManager.Instance.bQuestionList[i]);
                        QuestionManager.Instance.RemoveQuestionsFromList(QuestionManager.Instance.bQuestionList, i);
                    }
                    break;

                case MainCanvasUI.PersonChosen.Third:
                    for(int i = 0; i < 3; i++)
                    {
                        QuestionManager.Instance.AddQuestionToList(questionSequence, QuestionManager.Instance.cQuestionList[i]);
                        QuestionManager.Instance.RemoveQuestionsFromList(QuestionManager.Instance.cQuestionList, i);
                    }
                    break;
            }
        }
        else
        {
            if (questionSequence.Count < 3)
            {
                // cek questionNumber terbesar di sequence
                BubbleSort();

                int largestIndexInList = questionSequence[questionSequence.Count - 1].questionNumber;

                // misal di questionSequence ada question 1, 2, 3
                // dan dipilih question 1
                // maka akan add question 4 dari list question A di QuestionManager
                // dan question tersebut akan dihapus dari list question A

                switch (mainCanvasUI.GetChosenPerson())
                {
                    case MainCanvasUI.PersonChosen.First:
                        QuestionManager.Instance.AddQuestionToList(questionSequence, QuestionManager.Instance.aQuestionList[largestIndexInList + 1]);
                        QuestionManager.Instance.RemoveQuestionsFromList(QuestionManager.Instance.aQuestionList, largestIndexInList + 1);
                        break;

                    case MainCanvasUI.PersonChosen.Second:
                        QuestionManager.Instance.AddQuestionToList(questionSequence, QuestionManager.Instance.bQuestionList[largestIndexInList + 1]);
                        QuestionManager.Instance.RemoveQuestionsFromList(QuestionManager.Instance.bQuestionList, largestIndexInList + 1);
                        break;

                    case MainCanvasUI.PersonChosen.Third:
                        QuestionManager.Instance.AddQuestionToList(questionSequence, QuestionManager.Instance.cQuestionList[largestIndexInList + 1]);
                        QuestionManager.Instance.RemoveQuestionsFromList(QuestionManager.Instance.cQuestionList, largestIndexInList + 1);
                        break;
                }
            }
        }
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
