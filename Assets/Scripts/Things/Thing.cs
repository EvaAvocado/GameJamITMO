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
    [SerializeField] private Player _player;
    [SerializeField] private Animator _thingAnimator;
    [SerializeField] private ThingName _name;
    [SerializeField] private ThingState _state;
    [SerializeField] private bool _isPlayerInZone;
    [SerializeField] private IntChange _action;
    
    [Header("UI")]
    [SerializeField] private TMP_Text _currentClickText;
    [SerializeField] private Animator _textAnimator;
    [SerializeField] private Animator _hintTextAnimator;

    private PlayerInput _playerInput;

    private static readonly int IsShow = Animator.StringToHash("is-show");
    private static readonly int IsHint = Animator.StringToHash("is-hint");
    private static readonly int IsBroken = Animator.StringToHash("is-broken");
    private static readonly int IsDefault= Animator.StringToHash("is-default");
    
    public enum ThingName
    {
        Paper,
        PhotoFrame,
        Flower,
        Sofa,
        Glass,
        Bottle,
        Bed,
        Slippers
    }
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
        _hintTextAnimator.SetBool(IsHint, false);
        
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
            _player.SomethingBroken(_name);
            _thingAnimator.SetTrigger(IsBroken);
        }
    }

    public void FixThing()
    {
        _state = ThingState.Default;
        _thingAnimator.SetTrigger(IsDefault);
        
        currentClickCount = clickCount;
        _currentClickText.text = currentClickCount.ToString();
    }

    public void SetDefaultPoints()
    {
        currentClickCount = clickCount;
    }

    public void SetIsPlayerInZone(bool status)
    {
        _isPlayerInZone = status;
        
        if (_state == ThingState.Default)
        {
           _hintTextAnimator.SetBool(IsHint, _isPlayerInZone); 
        }
    }
}

[Serializable]
public class IntChange : UnityEvent<int>
{
        
}
