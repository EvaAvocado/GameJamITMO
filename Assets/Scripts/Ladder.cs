using UnityEngine;

public class Ladder : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Owner _owner;
    [SerializeField] private LadderManager _ladderManager;
    [SerializeField] private Cooldown _cooldown;

    [Header("UI")] [SerializeField] private Animator _hintTextAnimator;

    public bool playerInInteractionZone;
    public bool ownerInInteractionZone;

    private static readonly int IsHint = Animator.StringToHash("is-hint");

    public void DefaultMoveForPlayer(float direction)
    {
        if ((_player.currentFloor == Creature.CurrentFloor.First && direction >= 0.95) ||
            (_player.currentFloor == Creature.CurrentFloor.Third && direction <= -0.95))
        {
            TransferToSecondFloor(_player.gameObject);
        }
        else if (_player.currentFloor == Creature.CurrentFloor.Second && direction >= 0.95)
        {
            TransferToThirdFloor(_player.gameObject);
        }
        else if (_player.currentFloor == Creature.CurrentFloor.Second && direction <= -0.95)
        {
            TransferToFirstFloor(_player.gameObject);
        }
    }

    public void RandomMoveForPlayer(float direction)
    {
        if (_player.currentFloor == Creature.CurrentFloor.First && direction >= 0.95)
        {
            TransferToThirdFloor(_player.gameObject);
        }
        else if (_player.currentFloor == Creature.CurrentFloor.Third && direction <= -0.95)
        {
            TransferToFirstFloor(_player.gameObject);
        }
        else if (_player.currentFloor == Creature.CurrentFloor.Second && direction >= 0.95)
        {
            TransferToFirstFloor(_player.gameObject);
        }
        else if (_player.currentFloor == Creature.CurrentFloor.Second && direction <= -0.95)
        {
            TransferToThirdFloor(_player.gameObject);
        }
    }

    public void MoveToAnotherFloorForPlayer(float direction)
    {
        if (!_player.isCanToRandomFloor)
        {
            DefaultMoveForPlayer(direction);
        }
        else
        {
            int random = Random.Range(0, 2);
            if (random == 0)
            {
                DefaultMoveForPlayer(direction);
            }
            else
            {
                RandomMoveForPlayer(direction);
            }
        }
    }

    public void MoveToAnotherFloorForNpc()
    {
        print("зашел!!!!");
        var randomFloor = Random.Range(0, 2); // => [0;2)

        if ((_owner.currentFloor == Creature.CurrentFloor.First) ||
            (_owner.currentFloor == Creature.CurrentFloor.Third))
        {
            TransferToSecondFloor(_owner.gameObject);
            _cooldown.Reset();
            _owner.isCanMoveToAnotherFloor = false;
            print("зашел");
        }
        else if (randomFloor == 0)
        {
            TransferToThirdFloor(_owner.gameObject);
            _cooldown.Reset();
            _owner.isCanMoveToAnotherFloor = false;
            print("зашел2");
        }
        else
        {
            TransferToFirstFloor(_owner.gameObject);
            _cooldown.Reset();
            _owner.isCanMoveToAnotherFloor = false;
            print("зашел2");
        }
    }

    public void SetPlayerInInteractionZone(bool status)
    {
        playerInInteractionZone = status;

        _hintTextAnimator.SetBool(IsHint, playerInInteractionZone);
    }

    public void SetOwnerInInteractionZone(bool status)
    {
        ownerInInteractionZone = status;
        
        print("SET-OWNER: " + _owner.isCanMoveToAnotherFloor);
        
        if (ownerInInteractionZone && _owner.isCanMoveToAnotherFloor)
        {
            MoveToAnotherFloorForNpc();
        }
    }

    private void TransferToFirstFloor(GameObject creature)
    {
        creature.transform.position = _ladderManager.spawnPoints[0].transform.position;

        if (creature.GetComponent<Player>())
            _player.currentFloor = Creature.CurrentFloor.First;
        else
            _owner.currentFloor = Creature.CurrentFloor.First;
    }

    private void TransferToSecondFloor(GameObject creature)
    {
        creature.transform.position = _ladderManager.spawnPoints[1].transform.position;

        if (creature.GetComponent<Player>())
            _player.currentFloor = Creature.CurrentFloor.Second;
        else
            _owner.currentFloor = Creature.CurrentFloor.Second;
    }

    private void TransferToThirdFloor(GameObject creature)
    {
        creature.transform.position = _ladderManager.spawnPoints[2].transform.position;

        if (creature.GetComponent<Player>())
            _player.currentFloor = Creature.CurrentFloor.Third;
        else
            _owner.currentFloor = Creature.CurrentFloor.Third;
    }
}