using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using PrimeTools;

public class C_CustomSorting : MonoBehaviour
{
    /* Conversion to Unity XR Notes:
     * Nothing changed. 
     * - Yoshua 2021-11-16
     */

    public TextMeshProUGUI _prompText;

    [Header("Options Components")]
    public RectTransform _optionParent;
    public GameObject _buttonPrefab;

    [Header("Timebar Components")]
    public RectTransform _timeBarContainer;
    public Image[] _timeBars;
    public TextMeshProUGUI _timeBarSeconds;

    [Header("Debug Option")]
    public bool _showDebug = true;


    private List<Priority_Button> PriorityButtons = new List<Priority_Button>();
    private List<Priority_Button> PrioritySelections = new List<Priority_Button>();
    public bool isInitiated { get; private set; } = false;
    private float _setTime = 0f;
    private bool _commitAnswer = false;
    public int MissCount { get; private set; } = 0;
    public int Score { get; private set; } = 0;

    public static C_CustomSorting Instance { get; set; }

    private RectTransform mainCanvas;

    // EDIT JONATHAN WILLIAM
    private Animator animator;
    [SerializeField] private float startFlashingTime;

    private void Awake()
    {
        // EDIT JONATHAN WILLIAM
        animator = GetComponent<Animator>();

        // Ensure singleton
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;
        // Ensure this gameobject is enabled during game start/level start!
        // check all components, if it doesn't exist, error flag
        mainCanvas = GetComponent<RectTransform>();
        if (_prompText == null) Debug.LogError("Component Missing", this);
        if (_optionParent == null) Debug.LogError("Component Missing", this);
        if (_timeBarContainer == null) Debug.LogError("Component Missing", this);
        if (_timeBarSeconds == null) Debug.LogError("Component Missing", this);
    }

    private void Start()
    {
        //Disable this during start up
        gameObject.SetActive(false);

        // EDIT JONATHAN WILLIAM
        foreach (var tb in _timeBars)
        {
            tb.GetComponent<Animator>().SetBool("Flashing", false);
        }
    }

    public void Initiate(string prompt, string[] options, float time)
    {
        Deb("Initiating Random");
        // reorder the items by shuffling
        //  create a new int list filled with [0, 1, 2 ... n] where n = options.Length - 1
        List<int> indexes = new List<int>();
        for (int i = 0; i < options.Length; i++) indexes.Add(i);
        //  shuffle the indexes list
        //Debug.Log(indexes.Count);
        P_ListShuffle.Shuffle(indexes);
        //Debug.Log(indexes.Count);

        // throw to main Initiate
        Initiate(prompt, options, indexes.ToArray(), time);
    }

    public void Initiate(string prompt, string[] options, int[] optionIndexes, float time)
    {
        if (isInitiated) return;
        else isInitiated = true;
        Deb("Initiating Defined");

        // reset time
        _setTime = time;

        // reset submit flag
        _commitAnswer = false;

        // reset score and misscount
        Score = 0;
        MissCount = 0;

        // clear all options
        PriorityButtons.Clear();
        foreach (Transform child in _optionParent)
            Destroy(child.gameObject);

        // clear previously selected options
        PrioritySelections.Clear();

        // rewrite the prompt
        _prompText.text = prompt;

        //Debug the content and priority
        for (int i = 0; i < options.Length; i++) Deb(string.Format("{0}, {1}", options[i], optionIndexes[i]));

        Deb(options.Length.ToString());
        // Generate buttons and it's content
        for (int i = 0; i < options.Length; i++)
        {
            C_CustomButtonSorting newButtonController = Instantiate(_buttonPrefab, _optionParent).GetComponent<C_CustomButtonSorting>();
            PriorityButtons.Add(new Priority_Button(newButtonController, optionIndexes[i], options[optionIndexes[i]], this));
        }
        foreach (var pb in PriorityButtons) pb.Generate();

        //sort based on priority sequence
        Deb("Before Sort");
        foreach (var pb in PriorityButtons) Debug.Log(pb.priority);
        SortSequence(ref PriorityButtons);
        Deb("After Sort");
        foreach (var pb in PriorityButtons) Debug.Log(pb.priority);
        Deb("Initiation Done, Proceeding with countdown");

        // activate game object
        gameObject.SetActive(true);

        StartCoroutine(CR_CountDown());
    }

    private void SortSequence(ref List<Priority_Button> priorityButtons)
    {

        List<Priority_Button> tempList = new List<Priority_Button>(priorityButtons);
        List<Priority_Button> newList = new List<Priority_Button>();

        Priority_Button min_item = null;
        for (int i = 0; i < priorityButtons.Count; i++)
        {
            int lastValue = int.MaxValue;
            foreach (var x in tempList)
            {
                if (x.priority < lastValue)
                {
                    lastValue = x.priority;
                    min_item = x;
                }
            }
            Debug.Log(string.Format("min item: {0}", min_item.priority));
            newList.Add(min_item);
            tempList.Remove(min_item);
        }

        priorityButtons = newList;
    }

    private IEnumerator CR_CountDown()
    {
        float alpha = 0f;
        float startTime = Time.time;

        while (alpha < 1f && !_commitAnswer)
        {
            Deb(alpha.ToString(), false);
            alpha = Mathf.Clamp01((Time.time - startTime) / _setTime);

            foreach (var tb in _timeBars)
            {
                tb.fillAmount = 1 - alpha;

                // EDIT JONATHAN WILLIAM
                if (tb.fillAmount <= startFlashingTime || tb.fillAmount == 0f)
                {
                    tb.GetComponent<Animator>().SetBool("Flashing", true);
                }
                else
                {
                    tb.GetComponent<Animator>().SetBool("Flashing", false);
                }
            }

            _timeBarSeconds.text = string.Format("{0:0}", _setTime * (1 - alpha));
            yield return null;
        }

        StartCoroutine(CR_CheckScoreAndFinish());
    }

    private IEnumerator CR_CheckScoreAndFinish()
    {
        // set all buttons interactable off
        foreach (var pb in PriorityButtons) pb.buttonController.ButtonOption.interactable = false;

        // assign depending on correct or wrong and extract priority index then sort as per correct 
        var TempList_Sort = new Priority_Button[PriorityButtons.Count];

        foreach (var x in PriorityButtons)
        {
            x.buttonController.SetPriorityText(x.priority + 1);
            TempList_Sort[x.priority] = x;
            x.buttonController.SetState(C_CustomButtonSorting.ButtonState.Wrong);
        }

        // sort sibling
        for (int i = 0; i < TempList_Sort.Length; i++)
        {
            TempList_Sort[i].buttonController.transform.SetAsLastSibling();
        }

        for (int i = 0; i < PrioritySelections.Count; i++)
        {
            if (PriorityButtons[i] == PrioritySelections[i])
            {
                PrioritySelections[i].buttonController.SetState(C_CustomButtonSorting.ButtonState.Correct);
                Score++;
            }
            else
            {
                PrioritySelections[i].buttonController.SetState(C_CustomButtonSorting.ButtonState.Wrong);
            }
        }

        // check as per score only
        //for(int i = 0; i < Score; i++)
        //{
        //    PrioritySelections[i].buttonController.SetState(QTA_Priority_Option_Controller.ButtonState.Correct);
        //}


        yield return new WaitForSeconds(5f);
        isInitiated = false;
    }

    public void Terminate()
    {
        //gameObject.SetActive(false);

        // EDIT JONATHAN WILLIAM
        animator.SetTrigger("Hide");
    }

    public int GetScoreOffsetMissCount()
    {
        return Mathf.Clamp(Score - MissCount, 0, int.MaxValue);
    }

    [Serializable]
    public class Priority_Button
    {
        public C_CustomButtonSorting buttonController;
        public string optionContent;
        public int priority;
        public C_CustomSorting parentCanvasController;

        public Priority_Button(C_CustomButtonSorting buttonController, int priority, string optionContent, C_CustomSorting parentCanvasController)
        {
            this.buttonController = buttonController;
            this.optionContent = optionContent;
            this.priority = priority;
            this.parentCanvasController = parentCanvasController;
        }

        internal void Generate()
        {
            buttonController.SetContentText(optionContent);
            buttonController.SetPriorityText(0);
            buttonController.SetState(C_CustomButtonSorting.ButtonState.Normal);
            buttonController.ButtonOption.onClick.AddListener(() => parentCanvasController.OptionOnSelect(this));
        }

        public void ChangeState(C_CustomButtonSorting.ButtonState newState, int priority = 0)
        {
            buttonController.SetState(newState);
            buttonController.SetPriorityText(priority);
        }
    }

    public void OptionOnSelect(Priority_Button priorityButton)
    {
        if (_commitAnswer) return;
        Deb("On Select");
        if (PrioritySelections.Contains(priorityButton))
            PrioritySelections.Remove(priorityButton);
        else
            PrioritySelections.Add(priorityButton);

        foreach (var pb in PriorityButtons) pb.ChangeState(C_CustomButtonSorting.ButtonState.Normal);
        for (int i = 0; i < PrioritySelections.Count; i++)
        {
            C_CustomButtonSorting.ButtonState buttonState = (PrioritySelections[i] == PriorityButtons[i]) ? C_CustomButtonSorting.ButtonState.Correct : C_CustomButtonSorting.ButtonState.Wrong;
            //PrioritySelections[i].ChangeState(QTA_Priority_Option_Controller.ButtonState.Selected, i + 1);
            PrioritySelections[i].ChangeState(buttonState, i + 1);
        }

        // check score here
        if (PrioritySelections.Count > 0)
        {
            bool isCorrect = PrioritySelections[PrioritySelections.Count - 1].buttonController.GetState() == C_CustomButtonSorting.ButtonState.Correct;
            if (!isCorrect)
            {
                MissCount += 1;
                Debug.Log("Miss Count: " + MissCount);
            }
        }

        if (PrioritySelections.Count == PriorityButtons.Count) _commitAnswer = true;
    }

    private void OnDestroy()
    {
        Instance = null;
    }

    private void Deb(string in_string, bool toConsole = true)
    {
        if (toConsole && _showDebug) Debug.Log(in_string, this);
    }

    [Serializable]
    public class QTA_Priority_Content
    {
        public string content;
        [Tooltip("This will be ignored if QTA System is set to AsArranged")]
        public int priority;
    }

    public List<Priority_Button> GetPriorityButtons() { return PriorityButtons; }
    public List<Priority_Button> GetPriotySelections() { return PrioritySelections; }
}