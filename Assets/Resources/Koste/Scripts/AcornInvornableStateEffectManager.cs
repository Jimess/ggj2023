using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcornInvornableStateEffectManager : MonoBehaviour
{
    public List<Material> materials;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        LevelController.OnInvurnableStateChange += InvurnableStateChange;
    }
    private void OnDisable()
    {
        LevelController.OnInvurnableStateChange -= InvurnableStateChange;
    }

    private void InvurnableStateChange(bool invurnable)
    {
        spriteRenderer.material = invurnable ? materials[1] : materials[0];
    }
}
