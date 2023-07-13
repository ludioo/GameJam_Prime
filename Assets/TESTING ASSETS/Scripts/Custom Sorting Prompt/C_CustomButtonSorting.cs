using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class C_CustomButtonSorting : MonoBehaviour
{
    public Text _priorityText;
    public Text _contentText;
    public Image _backgroundImage;
    public CanvasGroup cg_priority;
    public Button ButtonOption;

    [Header("Normal Color")]
    public Color _normalBackgroundColor;
    public Color _normalTextColor;
    [Header("Selected Color")]
    public Color _selectedBackgroundColor;
    public Color _selectedTextColor;
    [Header("Correct Color")]
    public Color _correctBackgroundColor;
    public Color _correctTextColor;
    [Header("Wrong Color")]
    public Color _wrongBackgroundColor;
    public Color _wrongTextColor;

    public enum ButtonState
    {
        Normal,
        Selected,
        Correct,
        Wrong
    }

    [Header("Button State")]
    [SerializeField]
    private ButtonState State = ButtonState.Normal;

    public void SetPriorityText(int inInt)
    {
        _priorityText.text = string.Format("#{0}", inInt);
    }

    public void SetContentText(string inText)
    {
        _contentText.text = inText;
    }

    public void SetState(ButtonState state)
    {
        State = state;

        switch (State)
        {
            case ButtonState.Normal:
                _backgroundImage.color = _normalBackgroundColor;
                _priorityText.color = _normalTextColor;
                _contentText.color = _normalTextColor;
                cg_priority.alpha = 0f;
                break;
            case ButtonState.Correct:
                _backgroundImage.color = _correctBackgroundColor;
                _priorityText.color = _correctTextColor;
                _contentText.color = _correctTextColor;
                cg_priority.alpha = 1f;
                break;
            case ButtonState.Selected:
                _backgroundImage.color = _selectedBackgroundColor;
                _priorityText.color = _selectedTextColor;
                _contentText.color = _selectedTextColor;
                cg_priority.alpha = 1f;
                break;
            case ButtonState.Wrong:
                _backgroundImage.color = _wrongBackgroundColor;
                _priorityText.color = _wrongTextColor;
                _contentText.color = _wrongTextColor;
                cg_priority.alpha = 1f;
                break;
        }
    }

    public ButtonState GetState()
    {
        return State;
    }

#if UNITY_EDITOR
    ButtonState lastState = ButtonState.Normal;
#endif

    private void Update()
    {
#if UNITY_EDITOR
        if (lastState != State)
        {
            SetState(State);
            lastState = State;
        }
#endif
    }
}