using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffDevSprites : MonoBehaviour
{
    [SerializeField] private SpriteRenderer[] devSprites = new SpriteRenderer[1];
    [SerializeField] private Color alphaZero = new Color(0, 0, 0, 0);

    void Awake()
    {
        foreach (var spriteRenderer in devSprites)
        {
            spriteRenderer.color = alphaZero;
        }
    }
}
