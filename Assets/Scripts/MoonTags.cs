using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MoonTags : MonoBehaviour
{
    [FormerlySerializedAs("tag")] [SerializeField] private TagList tagList;
    public TagList TagList => tagList;
}

public enum TagList
{
    Player,
    Staff,
    Water,
    Enemy,
    Stage,
    Flood,
    PlainKey,
    BossKey
}