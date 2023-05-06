using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Thing : MonoBehaviour
{
    [SerializeField] private int _clickCount;
    [SerializeField] private int _points;
    [SerializeField] private ThingState _state;
    [SerializeField] private bool _isPlayerInZone;
    [SerializeField] private IntChange _action;
    
    [Header("UI")]
    [SerializeField] private TMP_Text _currentClickText;
    [SerializeField] private Animator _textAnimator;

    private PlayerInput _playerInput;
    private int _currentClickCount;
    
    private static readonly int IsShow = Animator.StringToHash("is-show");

    public enum ThingState
    {
        Default,
        Broken
    }

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _currentClickCount = _clickCount;
        _currentClickText.text = _currentClickCount.ToString();
        _playerInput.Player.Intetact.performed += context => Click();
    }

    private void OnEnable()
    {
        _playerInput.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }

    private void Click()
    {
        if (_isPlayerInZone && _currentClickCount > 0)
        {
            _currentClickCount--;
            _currentClickText.text = _currentClickCount.ToString();
            _textAnimator.SetTrigger(IsShow);
            
            if (_currentClickCount == 0)
            {
                _currentClickText.text = "Сломано!";
            }
        }

        if (_currentClickCount <= 0 && _state == ThingState.Default)
        {
            _action?.Invoke(_points);
            _state = ThingState.Broken;
        }
    }

    public void FixThing()
    {
        _state = ThingState.Default;
        _currentClickCount = _clickCount;
        _currentClickText.text = _currentClickCount.ToString();
    }
    
    public void SetIsPlayerInZone(bool status)
    {
        _isPlayerInZone = status;
    }
}

[Serializable]
public class IntChange : UnityEvent<int>
{
        
}
