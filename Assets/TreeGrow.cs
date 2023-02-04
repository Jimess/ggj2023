using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TreeGrow : MonoBehaviour
{
    [SerializeField] float str;
    [SerializeField] int vibrato;
    [SerializeField] float randomness;

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.B))
        //{
        //    Grow(transform.position);
        //}
    }

    public void Grow(Vector2 position)
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(gameObject.transform.DOScale(1f, 1.5f).From(0f).SetEase(Ease.OutQuint));
        //seq.Append(gameObject.transform.DOBlendableRotateBy(Vector3.forward * 30f, 0.25f).SetLoops(1, LoopType.Yoyo));
        seq.Join(gameObject.transform.DOShakeRotation(1.5f, str, vibrato, randomness, false, ShakeRandomnessMode.Full));
        //seq.Join(gameObject.transform.DOSh)
    }
}
