using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Elements", menuName = "ScriptableObjects/Element", order = 1)]
public class ScriptableCollectable : ScriptableObject
{
    public string elementName;
    public CollectableType type;
    public Sprite sprite;
    public float colliderX;
    public float colliderY;
    public Quaternion rotation;
}