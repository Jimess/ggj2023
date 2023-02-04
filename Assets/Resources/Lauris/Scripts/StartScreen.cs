using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StartScreen : MonoBehaviour
{
    [SerializeField] CanvasGroup cg;
    [SerializeField] RectTransform rtf;

    public void StartPressed()
    {
        DisableStartScreen();    }

    void DisableStartScreen()
    {
        cg.blocksRaycasts = false;
        //cg.interactable
        rtf.DORotate(Vector3.forward * 720f, 1f, RotateMode.LocalAxisAdd).SetUpdate(true);
        cg.DOFade(0f, 1f).SetUpdate(true);
        rtf.DOScale(0f, 1f).SetUpdate(true).OnComplete(() => {
            gameObject.SetActive(false);
            LevelController.Instance.StartGame();
        });
    }
}
