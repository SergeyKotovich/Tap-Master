using System;
using UnityEngine;

namespace DefaultNamespace.Player
{
    [Serializable]
    public class DefaultData
    {
        [field:SerializeField] public int Money { get; private set; }
        [field:SerializeField] public int BoostersCount { get; private set; }
    }
}