using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Issitaskymas : MonoBehaviour
{
    [SerializeField] private GameObject bodyColliderToDisable;
    [SerializeField] private GameObject bodyToDisable;
    [SerializeField] private GameObject eyeToDisable;
    [SerializeField] private GameObject mouthToDisable;

    [SerializeField] List<GameObject> goreParts;

    // Start is called before the first frame update
    private void OnEnable()
    {
        LevelController.OnEnd += Explosion;
    }

    private void OnDisable()
    {
        LevelController.OnEnd -= Explosion;
    }

    public void Explosion(bool win)
    {
        if (win) return;

        //bodyToDisable.transform.DOScale(0f, 0.5f).SetEase(Ease.OutExpo);
        //mouthToDisable.transform.DOScale(0f, 0.5f).SetEase(Ease.OutExpo);
        //eyeToDisable.transform.DOScale(0f, 0.5f).SetEase(Ease.OutExpo).OnComplete(() => {
        //    bodyToDisable.SetActive(false);
        //    eyeToDisable.SetActive(false);
        //    bodyToDisable.SetActive(false);
        //    mouthToDisable.SetActive(false);
        //});

        bodyToDisable.SetActive(false);
        eyeToDisable.SetActive(false);
        bodyToDisable.SetActive(false);
        mouthToDisable.SetActive(false);
        SpawnGoreParts(transform.position, 3f);
    }

    public void SpawnGoreParts(Vector2 position, float power)
    {
        foreach (GameObject gorept in goreParts)
        {
            GameObject pt = Instantiate(gorept, new Vector3(position.x, position.y, 0f), Quaternion.identity);
            Vector2 randomRot = Random.rotation * pt.transform.forward;
            pt.GetComponent<Rigidbody2D>().AddForce(randomRot * power, ForceMode2D.Impulse);
            pt.GetComponent<SpriteRenderer>().DOFade(0f, 5f).OnComplete(() => Destroy(pt, 5.1f));
        }
    }
}
