using UnityEngine;

public abstract class Amulet : MonoBehaviour
{
    public AmuletManager.AmuletState amuletState;
    public AmuletManager amuletManager;
    
    protected virtual void UpdateTimer()
    {
        print("UpdateTimer");
    }

    public virtual void SetEffect()
    {
        print("SetEffect");
    }

    public virtual void Reset()
    {
        print("Reset");
    }
}