using UnityEngine;

public class Penta : MonoBehaviour
{
    [SerializeField] private bool _isPlayerInZone;
    [SerializeField] private ScoreUI _scoreUI;
    [SerializeField] private int _costToSpawn;
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
        if (_isPlayerInZone && _scoreUI.Score >= _costToSpawn)
        {
            _scoreUI.ChangeScore(-_costToSpawn);
            _amuletManager.SpawnNewRandomAmulet();
        }
    }

    public void SetIsPlayerInZone(bool status)
    {
        _isPlayerInZone = status;
    }
}
