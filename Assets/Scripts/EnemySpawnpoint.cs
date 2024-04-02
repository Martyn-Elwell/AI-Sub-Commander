using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnpoint : MonoBehaviour
{
    public animationEnum animationType;
    public GameObject prefabOverride;
    public GameObject room;

    private void Start()
    {
        room = transform.parent.parent.gameObject;
    }
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
