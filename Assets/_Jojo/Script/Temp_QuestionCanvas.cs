using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Temp_QuestionCanvas : MonoBehaviour
{
    public MainCanvasUI mainCanvasUI;

    [Header("NPC A Section")]
    public List<Button> aQuestionButtonList;
    public List<Question_SO> aAvailableQuestionList;

    [Header("NPC B Section")]
    public List<Button> bQuestionButtonList;
    public List<Question_SO> bAvailableQuestionList;

    [Header("NPC C Section")]
    public List<Button> cQuestionButtonList;
    public List<Question_SO> cAvailableQuestionList;

    private int index;

    private void Start()
    {
        for (int i = 0; i < aQuestionButtonList.Count; i++)
        {
            QuestionManager.Instance.AddQuestionToList(aAvailableQuestionList, QuestionManager.Instance.aTotalQuestionList[i]);
            QuestionManager.Instance.AddQuestionToList(bAvailableQuestionList, QuestionManager.Instance.bTotalQuestionList[i]);
            QuestionManager.Instance.AddQuestionToList(cAvailableQuestionList, QuestionManager.Instance.cTotalQuestionList[i]);
        }

        for (int i = 0; i < aQuestionButtonList.Count; i++)
        {
            QuestionManager.Instance.RemoveQuestionsFromList(QuestionManager.Instance.aTotalQuestionList, 0);
            QuestionManager.Instance.RemoveQuestionsFromList(QuestionManager.Instance.bTotalQuestionList, 0);
            QuestionManager.Instance.RemoveQuestionsFromList(QuestionManager.Instance.cTotalQuestionList, 0);
        }
    }

    public IEnumerator InitializeQuestionCanvas(int personChosen)
    {
        switch (personChosen)
        {
            case 0:

                Debug.Log("First Person Chosen!");
                Debug.Log(aAvailableQuestionList.Count);

                if(aAvailableQuestionList.Count < 3 && QuestionManager.Instance.aTotalQuestionList.Count != 0)
                {
                    QuestionManager.Instance.AddQuestionToList(aAvailableQuestionList, QuestionManager.Instance.aTotalQuestionList[0]);
                    QuestionManager.Instance.RemoveQuestionsFromList(QuestionManager.Instance.aTotalQuestionList, 0);
                }

                for (int i = 0; i < aAvailableQuestionList.Count; i++)
                {
                    aQuestionButtonList[i].GetComponentInChildren<TextMeshProUGUI>().text = aAvailableQuestionList[i].questionText;

                    aQuestionButtonList[i].gameObject.SetActive(true);
                }

                break;
            case 1:

                Debug.Log("Second Person Chosen!");

                if (bAvailableQuestionList.Count < 3 && QuestionManager.Instance.bTotalQuestionList.Count != 0)
                {
                    QuestionManager.Instance.AddQuestionToList(bAvailableQuestionList, QuestionManager.Instance.bTotalQuestionList[0]);
                    QuestionManager.Instance.RemoveQuestionsFromList(QuestionManager.Instance.bTotalQuestionList, 0);
                }

                for (int i = 0; i < bAvailableQuestionList.Count; i++)
                {
                    bQuestionButtonList[i].GetComponentInChildren<TextMeshProUGUI>().text = bAvailableQuestionList[i].questionText;

                    bQuestionButtonList[i].gameObject.SetActive(true);
                }

                break;
            case 2:

                Debug.Log("Third Person Chosen!");

                if (cAvailableQuestionList.Count < 3 && QuestionManager.Instance.cTotalQuestionList.Count != 0)
                {
                    QuestionManager.Instance.AddQuestionToList(cAvailableQuestionList, QuestionManager.Instance.cTotalQuestionList[0]);
                    QuestionManager.Instance.RemoveQuestionsFromList(QuestionManager.Instance.cTotalQuestionList, 0);
                }

                for (int i = 0; i < cAvailableQuestionList.Count; i++)
                {
                    cQuestionButtonList[i].GetComponentInChildren<TextMeshProUGUI>().text = cAvailableQuestionList[i].questionText;

                    cQuestionButtonList[i].gameObject.SetActive(true);
                }

                break;
        }

        yield return new WaitForSeconds(1f);

        gameObject.SetActive(true);
    }

    public void AButtonPress(int index)
    {
        GameplayManager.Instance.SetString(aAvailableQuestionList[index].name);
        QuestionManager.Instance.RemoveQuestionsFromList(aAvailableQuestionList, index);
    }

    public void BButtonPress(int index)
    {
        GameplayManager.Instance.SetString(bAvailableQuestionList[index].name);
        QuestionManager.Instance.RemoveQuestionsFromList(bAvailableQuestionList, index);
    }

    public void CButtonPress(int index)
    {
        GameplayManager.Instance.SetString(cAvailableQuestionList[index].name);
        QuestionManager.Instance.RemoveQuestionsFromList(cAvailableQuestionList, index);
    }
}
