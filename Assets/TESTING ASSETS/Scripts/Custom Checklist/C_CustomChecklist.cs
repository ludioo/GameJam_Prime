using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace PrimeExpress
{
    public class C_CustomChecklist : MonoBehaviour
    {
        [Header("Support Script its okay if you leave it Empty and its only work in all Correct")]
        public C_SortingText_Checklist support_CheackList;

        [Header("Prompt Items")]
        public TextMeshProUGUI _promptTextArea;

        [Header("Button Items")]
        public GameObject _optionPrefab;
        public Transform _optionParent;
        public CanvasGroup _optionCanvasGroup;

        [Header("Color Options")]
        public Sprite sprite_normal;
        public Color _normalTextColor;
        public Sprite sprite_incorrect;
        public Color _selectedTextColor_incorrect;
        public Sprite sprite_correct;
        public Color _selectedTextColor_correct;
        public Sprite sprite_greyedOut;
        public Color _greyedOutTextColor;

        [Header("SFX")]
        public AudioSource _audioSource;
        public AudioClip clip_appear;
        public AudioClip clip_correct;
        public AudioClip clip_wrong;


        private List<GameObject> buttonList;
        public bool isSelected { get; private set; } = false;
        public QTA_ChecklistSet selectedChecklist { get; private set; } = null;

        public static C_CustomChecklist Instance { get; private set; } = null;

        private void Awake()
        {
            if (Instance != null)
                Destroy(gameObject);
            else
                Instance = this;

            if (!_promptTextArea) Debug.LogError("Core Item Missing!", this);
            if (!_optionPrefab) Debug.LogError("Core Item Missing!", this);
            if (!_optionParent) Debug.LogError("Core Item Missing!", this);
            if (!_optionCanvasGroup) Debug.LogError("Core Item Missing!", this);
        }

        private void Start()
        {
            gameObject.SetActive(false);
        }

        public void Initiate(string prompt, List<QTA_ChecklistSet> optionList, bool greyOutSelection = true)
        {
            // Activate gameobject
            gameObject.SetActive(true);

            // play appear audio
            _audioSource.Stop();
            _audioSource.clip = clip_appear;
            _audioSource.Play();

            // reset bool flag
            isSelected = false;
            selectedChecklist = null;

            // clear all previous children
            foreach (Transform child in _optionParent)
            {
                Destroy(child.gameObject);
            }

            // update prompt
            _promptTextArea.text = prompt;

            // clear previous childrens

            // spawn buttons
            buttonList = new List<GameObject>();
            int optNumber = 0;
            foreach (var opt in optionList)
            {
                GameObject newButton = Instantiate(_optionPrefab, _optionParent);
                newButton.name = string.Format("Qta Checklist Option {0}", (++optNumber).ToString());
                newButton.GetComponentInChildren<TextMeshProUGUI>().text = opt.content;

                // standardized spawned buttons
                newButton.GetComponent<Button>().image.sprite = sprite_normal;
                newButton.GetComponentInChildren<TextMeshProUGUI>().color = _normalTextColor;
                newButton.GetComponent<Button>().onClick.AddListener(() => ButtonOnSelect(newButton, opt));

                // greyout selected buttons
                if (opt.disable)
                {
                    newButton.GetComponent<Button>().interactable = false;
                    if (greyOutSelection)
                    {
                        Debug.Log(string.Format("option: {0}, greyed out", opt.content));
                        newButton.GetComponent<Button>().image.sprite = sprite_greyedOut;
                        newButton.GetComponentInChildren<TextMeshProUGUI>().color = _greyedOutTextColor;

                        //  add text for order After select, but only if support_CheackList != null
                        if (support_CheackList != null)
                        {
                            newButton.transform.GetChild(1).gameObject.SetActive(true);
                            support_CheackList.custom_After_Selected_Button(newButton);
                        }
                    }
                    else
                    {
                        Debug.Log(string.Format("option: {0}, not greyed out", opt.content));
                        if (opt.isCorrect)
                        {
                            newButton.GetComponent<Button>().image.sprite = sprite_correct;
                            newButton.GetComponentInChildren<TextMeshProUGUI>().color = _selectedTextColor_correct;
                        }
                        else if (!opt.isCorrect)
                        {
                            newButton.GetComponent<Button>().image.sprite = sprite_incorrect;
                            newButton.GetComponentInChildren<TextMeshProUGUI>().color = _selectedTextColor_incorrect;
                        }
                    }

                }

                // add new button reference to list
                buttonList.Add(newButton);
            }



            //// update main canvas and collision
            //var col = GetComponent<BoxCollider>();
            //RectTransform mainCanvas = GetComponent<RectTransform>();
            //mainCanvas.sizeDelta = new Vector2(mainCanvas.sizeDelta.x, (160f + 70f * buttonList.Count));
            //if (col) col.size = new Vector3(mainCanvas.sizeDelta.x, mainCanvas.sizeDelta.y, col.size.z);
        }

        public void ButtonOnSelect(GameObject button, QTA_ChecklistSet set)
        {
            if (isSelected) return;
            else isSelected = true;

            Debug.Log("Selected: " + button.name, this);

            if (set.isCorrect)
            {
                _audioSource.clip = clip_correct;
                _audioSource.Play();
                button.GetComponent<Button>().image.sprite = sprite_correct;
                button.GetComponentInChildren<TextMeshProUGUI>().color = _selectedTextColor_correct;

                // add text for order select, but only if support_CheackList != null
                if (support_CheackList != null)
                {
                    button.transform.GetChild(1).gameObject.SetActive(true);
                    support_CheackList.custom_First_Selected_Button(button);
                }


            }
            else if (!set.isCorrect)
            {
                _audioSource.clip = clip_wrong;
                _audioSource.Play();
                button.GetComponent<Button>().image.sprite = sprite_incorrect;
                button.GetComponentInChildren<TextMeshProUGUI>().color = _selectedTextColor_incorrect;
            }

            set.disable = true;
            selectedChecklist = set;
        }

        public void ClearAllListeners()
        {

        }

        [System.Serializable]
        public class QTA_ChecklistSet
        {
            public string content = "";
            public bool disable { get; set; } = false;
            public bool isCorrect = false;
            public int branch_id;
        }
    }
}

