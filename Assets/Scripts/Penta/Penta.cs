using UnityEngine;

public class Penta : MonoBehaviour
{
    [SerializeField] private bool _isPlayerInZone;
    [SerializeField] private AmuletManager _amuletManager;
    private PlayerInput _playerInput;
    
    private void Awake()
    {
        _playerInput = new PlayerInput();
        _playerInput.Player.Intetact.performed += context => SpawnRandomAmuletInPenta();
    }

    private void OnEnable()
    {
        _playerInput.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }

    private void SpawnRandomAmuletInPenta()
    {
        if (_isPlayerInZone)
        {
            _amuletManager.SpawnNewRandomAmulet();
        }
    }

    public void SetIsPlayerInZone(bool status)
    {
        _isPlayerInZone = status;
    }
}
