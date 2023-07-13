using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using PrimeTools;
using TMPro;


public class C_CustomRecording2 : MonoBehaviour
{
    [Header("Self Reference")]
    public Button _stopButton;
    public GameObject _greenFrame;
    public TextMeshProUGUI _titleText;
    public TextMeshProUGUI _subText;
    public TextMeshProUGUI _countdownText;
    public TextMeshProUGUI _timerText;
    public Animator animator;

    //add Stop Record Button
    public GameObject _stopRecord;
    public Button _stopRecordButton;

    [Header("MicWeelco Reference")]
    public Transform _voiceWave;
    public Transform _lookAtTarget;
    public MicInput_Weelco micInput;

    [Header("Value Modifier")]
    public float _recordTimer = 5f;
    public string titleText;
    public string subtitleText;

    private bool _startCountdownTimer = false;
    public bool _startRecordTimer = false;
    public bool _recordStopImm = false;
    [SerializeField] private float _countdownTimer = 4f;

    [SerializeField] public float countdownTimer;
    [SerializeField] private float recordTimer;

    public RawImage RecTimer_Red_RImg;
    private Animator timeBarAnimator;

    void Start()
    {
        _stopButton.onClick.AddListener(() => StopRecordImmed());
        _stopButton.interactable = false;
        _stopRecordButton.onClick.AddListener(() => StopRecordImmed());
        _stopRecordButton.interactable = false;
        _greenFrame.SetActive(false);
        _stopRecord.SetActive(false);
        gameObject.SetActive(false); //activate this if done!

        // EDIT JONATHAN WILLIAM
        animator = GetComponent<Animator>();
        timeBarAnimator = RecTimer_Red_RImg.GetComponent<Animator>();
    }

    private void OnEnable()
    {
        countdownTimer = _countdownTimer;
        recordTimer = _recordTimer;
        DisplayTime(-1f);
        _titleText.text = titleText;
        _subText.text = subtitleText;
        //if (_countdownTimer > 0 || _countdownTimer == 0)
        //{
        //    _startCountdownTimer = true;
        //}
        _startCountdownTimer = true;
        //micInput = FindObjectOfType<MicInput_Weelco>();
        micInput.StopMicrophone();
        if (animator) animator.SetBool("B_Recording", false);
    }

    private void OnDisable()
    {
        _countdownTimer = countdownTimer;
        _recordTimer = recordTimer;

        _startCountdownTimer = false;
        _startRecordTimer = false;
        _recordStopImm = false;
        _stopButton.interactable = false;
        _stopRecordButton.interactable = false;
    }

    private void Update()
    {
        float value = 0.5f;
        if (micInput != null)
        {
            value = Mathf.Clamp01(micInput._soundLevel * 5);
        }
        else
        {
            Debug.LogWarning("Mic Input is Null!");
        }
        if (value < 0.001f) Debug.Log("Mic Volume Too Low, is it detecting??");

        _voiceWave.localScale = new Vector3(1f, value, 1f);
        if (_lookAtTarget)
        {
            transform.LookAt(_lookAtTarget);
            Vector3 rotationCurrent = transform.eulerAngles;
            rotationCurrent.x = 0f;
            rotationCurrent.z = 0f;
            transform.eulerAngles = rotationCurrent;
        }

        if (_startCountdownTimer)
        {
            StartCoroutine(CR_countdown());
        }

        if (_startRecordTimer)
        {
            RecordTimer();
        }

        if (_recordTimer == 0)
        {
            stopRecord();
        }
    }

    private void RecordTimer()
    {
        if (_recordTimer > 0)
        {
            _recordTimer -= Time.deltaTime;
            DisplayTime(_recordTimer);

            // EDIT JONATHAN WILLIAM
            if (_recordTimer <= recordTimer / 2)
            {
                timeBarAnimator.SetBool("Flashing", true);
            }
            else
            {
                timeBarAnimator.SetBool("Flashing", false);
            }
        }
        else
        {
            Debug.Log("Times run out!");
            _recordTimer = 0f;
            _startRecordTimer = false;
        }
    }

    private void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        _timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    IEnumerator CR_countdown()
    {
        _greenFrame.SetActive(false);
        _stopRecord.SetActive(false);
        _startCountdownTimer = false;
        yield return new WaitUntil(() => CountDown_CoroutineTask());
    }

    private bool CountDown_CoroutineTask()
    {
        if (_countdownTimer > 0)
        {
            _countdownTimer -= Time.deltaTime;
            //Debug.Log("DeltaTime : " + Time.deltaTime);
            //Debug.Log("Countdown called, current : " + _countdownTimer);
            float seconds = Mathf.Clamp(Mathf.FloorToInt(_countdownTimer % 60), 0, int.MaxValue);
            _countdownText.text = "Mulai merekam dalam... " + string.Format("{0:0}", seconds);
        }
        else
        {
            if (animator) animator.SetBool("B_Recording", true);
            //_countdownText.text = "Sedang merekam...";
            _countdownText.text = "   ";
            Debug.Log("RECORDING!");
            _countdownTimer = 0f;
            _startRecordTimer = true;
            micInput.StartRecording();
            _stopButton.interactable = true;
            _stopRecordButton.interactable = true;
            _greenFrame.SetActive(true);
            _stopRecord.SetActive(true);
        }

        return _countdownTimer == 0f;
    }

    public void StopRecordImmed()
    {
        _greenFrame.SetActive(false);
        _stopRecord.SetActive(false);
        _recordStopImm = true;
        if (_recordStopImm)
        {
            stopRecord();
        }
    }

    public void stopRecord()
    {
        _greenFrame.SetActive(false);
        _stopRecord.SetActive(false);
        _startRecordTimer = false;
        _titleText.text = "Rekaman selesai";
        _subText.text = "Menyimpan hasil rekaman...";
        _countdownText.text = "Mohon tunggu";
        micInput.StopMicrophone();
        _stopButton.interactable = false;
        _stopRecordButton.interactable = false;
        if (animator) animator.SetBool("B_Recording", false);
    }
}