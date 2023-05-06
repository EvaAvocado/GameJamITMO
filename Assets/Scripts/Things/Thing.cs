using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Thing : MonoBehaviour
{
    [Header("Click count")]
    public int clickCount;
    public int currentClickCount;
    
    [Header("Points")]
    public int points;
    public int currentPoints;
    
    [Header("Other")]
    [SerializeField] private ThingState _state;
    [SerializeField] private bool _isPlayerInZone;
    [SerializeField] private IntChange _action;
    
    [Header("UI")]
    [SerializeField] private TMP_Text _currentClickText;
    [SerializeField] private Animator _textAnimator;

    private PlayerInput _playerInput;
   
    
    private static readonly int IsShow = Animator.StringToHash("is-show");

    public enum ThingState
    {
        Default,
        Broken
    }

    private void Awake()
    {
        _playerInput = new PlayerInput();
        currentClickCount = clickCount;
        currentPoints = points;
        _currentClickText.text = currentClickCount.ToString();
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
        if (_isPlayerInZone && currentClickCount > 0 && _state != ThingState.Broken)
        {
            currentClickCount--;
            _currentClickText.text = currentClickCount.ToString();
            _textAnimator.SetTrigger(IsShow);
            
            if (currentClickCount == 0)
            {
                _currentClickText.text = "Сломано!";
            }
        }

        if (currentClickCount <= 0 && _state == ThingState.Default)
        {
            _action?.Invoke(currentPoints);
            _state = ThingState.Broken;
        }
    }

    public void FixThing()
    {
        _state = ThingState.Default;
        currentClickCount = clickCount;
        _currentClickText.text = currentClickCount.ToString();
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
