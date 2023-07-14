using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionManager : MonoBehaviour
{
    public static QuestionManager Instance { get; private set; }

    public List<Question_SO> totalQuestionList;

    private void Awake()
    {
        Instance = this;
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
