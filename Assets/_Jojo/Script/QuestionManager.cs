using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionManager : MonoBehaviour
{
    public static QuestionManager Instance { get; private set; }

    public List<Question_SO> aQuestionList;
    public List<Question_SO> bQuestionList;
    public List<Question_SO> cQuestionList;

    private void Awake()
    {
        Instance = this;
    }

    public void RemoveQuestionsFromList(List<Question_SO> questionList, int index)
    {
        questionList.RemoveAt(index);
    }

    public void AddQuestionToList(List<Question_SO> questionList, Question_SO question)
    {
        questionList.Add(question);
    }
}
