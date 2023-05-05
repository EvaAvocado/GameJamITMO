using UnityEngine;

public class Ladder : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Owner _owner;
    [SerializeField] private LadderManager _ladderManager;
    
    public bool playerInInteractionZone;
    public bool ownerInInteractionZone;
    

    public void MoveToAnotherFloorForPlayer(float direction)
    {
        if ((_player.currentFloor == Creature.CurrentFloor.First && direction >= 0.95) || (_player.currentFloor == Creature.CurrentFloor.Third && direction <= -0.95))
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
    
    public void MoveToAnotherFloorForNpc()
    {
        var randomFloor = Random.Range(0, 2); // => [0;2)
        
        if ((_owner.currentFloor == Creature.CurrentFloor.First) || (_owner.currentFloor == Creature.CurrentFloor.Third))
        {
            TransferToSecondFloor(_owner.gameObject);
        }
        else if (randomFloor == 0)
        {
            TransferToThirdFloor(_player.gameObject);
        }
        else
        {
            TransferToFirstFloor(_player.gameObject);
        }
    }

    public void SetPlayerInInteractionZone(bool status)
    {
        playerInInteractionZone = status;
    }
    
    public void SetOwnerInInteractionZone(bool status)
    {
        ownerInInteractionZone = status;
        
        if (ownerInInteractionZone && _owner.isCanMoveToAnotherFloor)
        {
            MoveToAnotherFloorForNpc();
        }
    }

    private void TransferToFirstFloor(GameObject creature)
    {
        creature.transform.position = _ladderManager.spawnPoints[0].transform.position;
        _player.currentFloor = Creature.CurrentFloor.First;
    }

    private void TransferToSecondFloor(GameObject creature)
    {
        creature.transform.position = _ladderManager.spawnPoints[1].transform.position;
        _player.currentFloor = Creature.CurrentFloor.Second;
    }

    private void TransferToThirdFloor(GameObject creature)
    {
        creature.transform.position = _ladderManager.spawnPoints[2].transform.position;
        _player.currentFloor = Creature.CurrentFloor.Third;
    }
}
