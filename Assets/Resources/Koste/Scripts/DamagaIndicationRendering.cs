using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagaIndicationRendering : MonoBehaviour
{
    [SerializeField]
    private Sprite[] sprites;
    private SpriteRenderer spriteRenderer;
    private int acornLife;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (sprites == null) return;
        int acornLife = LevelController.Instance.GetAcornLife();

        if (acornLife == this.acornLife) return;
        this.acornLife = acornLife;

        if (acornLife < 0 || acornLife > sprites.Length - 1) return;
        spriteRenderer.sprite = sprites[sprites.Length - 1 - acornLife];
    }
}
