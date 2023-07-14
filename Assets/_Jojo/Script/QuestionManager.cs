using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionManager : MonoBehaviour
{
    public static QuestionManager Instance { get; private set; }

    public List<Question_SO> totalQuestionList;
    public List<Question_SO> aTotalQuestionList;
    public List<Question_SO> bTotalQuestionList;
    public List<Question_SO> cTotalQuestionList;

    private void Awake()
    {
        Instance = this;

        aTotalQuestionList = new List<Question_SO>(totalQuestionList);
        bTotalQuestionList = new List<Question_SO>(totalQuestionList);
        cTotalQuestionList = new List<Question_SO>(totalQuestionList);
    }

    public void RemoveQuestionsFromList(List<Question_SO> questionList, int index)
    {
        questionList.RemoveAt(index);
    }
    public void _RemoveQuestionsFromList(List<Button> questionList, int index)
    {
        questionList.RemoveAt(index);
    }

    public void AddQuestionToList(List<Question_SO> questionList, Question_SO question)
    {
        questionList.Add(question);
    }
}
