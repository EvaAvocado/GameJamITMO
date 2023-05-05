using UnityEngine;

public class Creature : MonoBehaviour
{
    [SerializeField] protected float Speed;
    
    public CurrentFloor currentFloor;
    protected float CurrentSpeed;
    
    
    public enum CurrentFloor
    {
        First,
        Second,
        Third
    }
}
