using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum PowerUp
{
    NONE,
    Heal,
    Invurnability,
    Bounce
}

public class PowerUpTrigger : MonoBehaviour
{
    [SerializeField]
    private PowerUp powerUp;
    private bool used = false;

    [SerializeField] private bool killAfter = false;
    [SerializeField] private float killAfterSeconds;

    public UnityEvent onComplete;

    public void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (used) return;

        if (collider2D.gameObject.CompareTag(CollisionManager.ACORN_TAG))
        {
            used = true;
            switch (powerUp)
            {
                case PowerUp.Heal:
                    ActivateHeal();
                    break;

                case PowerUp.Invurnability:
                    ActivateInvurnability();
                    break;

                case PowerUp.Bounce:
                    ActivateBounce(collider2D.gameObject.GetComponentInParent<AcornJumper>());
                    break;

                default:
                    return;
            }
            CollisionManager.Instance.OnPowerUp(powerUp, transform.position);

            onComplete.Invoke();
            if (killAfter)
            {
                Destroy(gameObject, killAfterSeconds);
            }
        }
    }

    public void ActivateHeal()
    {
        AudioManager.Instance.PlaySFXDetached("powerup");
        LevelController.Instance.IncreaseLife();
    }

    public void ActivateInvurnability()
    {
        AudioManager.Instance.PlaySFXDetached("powerup");
        LevelController.Instance.MakeInvurnable(4.0f, true);
    }
    public void ActivateBounce(AcornJumper acornJumper)
    {
        AudioManager.Instance.PlaySFXDetached("bounce");
        acornJumper.Bounce(3.0f, null);
    }
}
