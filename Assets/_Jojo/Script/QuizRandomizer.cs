using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizRandomizer : MonoBehaviour
{
    public List<GameObject> quizFlowList;

    [Header("The amount of question you want to use")]
    public int totalQuestion;

    private GameObject randomizedQuizFlow;
    public GameObject Randomize(int min, int max)
    {
        int randomIndex = Random.Range(min, max);

        randomizedQuizFlow = quizFlowList[randomIndex];
        quizFlowList.RemoveAt(randomIndex);

        return randomizedQuizFlow;
    }

    public int GetQuizFlowListCount()
    {
        return quizFlowList.Count;
    }

    public GameObject GetRandomizedQuizFlow()
    {
        return randomizedQuizFlow;
    }
}
