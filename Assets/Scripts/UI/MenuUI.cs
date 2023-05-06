using UnityEngine;

public class MenuUI : MonoBehaviour
{
    [SerializeField] private GameObject _menu;
    [SerializeField] private SettingsMenu _settingsMenu;
    private PlayerInput _playerInput;

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _playerInput.Player.Menu.performed += context => ChangeStateMenu();
    }
    
    private void Start()
    {
        _settingsMenu.SetVolume();
    }
    
    private void OnEnable()
    {
        _playerInput.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }

    private void ChangeStateMenu()
    {
        _menu.SetActive(!_menu.activeSelf);
        _settingsMenu.SaveVolume();
    }
    
    
}
