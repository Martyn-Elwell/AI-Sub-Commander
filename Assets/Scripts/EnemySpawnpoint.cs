using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnpoint : MonoBehaviour
{
    public animationEnum animationType;
    public GameObject prefabOverride;
}

public enum animationEnum
{
    Idel,
    Sitting,
    Leaning,
    Drinking,
    Lying,
    PushUp,
    Reading,
    Typing,
    Toilet,
    SittingGround,
    Dancing
}
