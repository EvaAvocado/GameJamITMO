using System.Runtime.InteropServices;
using UnityEngine;


public class UnityScript : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void UnityPluginRequestJs();

    private void Start()
    {
        RequestJs();
    }

    private void RequestJs()
    {
        UnityPluginRequestJs();
    }
    
}