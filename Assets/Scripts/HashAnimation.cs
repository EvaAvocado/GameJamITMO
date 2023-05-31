using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class HashAnimation : MonoBehaviour
{
    public static readonly int IsIdle = Animator.StringToHash("IsIdle");
    public static readonly int Speed = Animator.StringToHash("Speed");
}