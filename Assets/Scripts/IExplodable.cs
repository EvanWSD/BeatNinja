using UnityEngine;
using UnityEngine.Events;

// Allows custom behaviour when caught in a grenade's explosionRadius
public interface IExplodable
{
    [HideInInspector]
    UnityEvent OnExploded { get; }
}
