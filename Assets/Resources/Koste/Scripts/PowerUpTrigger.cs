using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerUp
{
    Heal,
    Invurnability,
    Bounce
}

public class PowerUpTrigger : MonoBehaviour
{
    [SerializeField]
    private PowerUp powerUp;
    private bool used = false;
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
            CollisionManager.Instance.OnPowerUp(powerUp);
            Destroy(gameObject);
        }
    }

    public void ActivateHeal()
    {
        LevelController.Instance.IncreaseLife();
    }

    public void ActivateInvurnability()
    {
        LevelController.Instance.MakeInvurnable(4.0f);
    }
    public void ActivateBounce(AcornJumper acornJumper)
    {
        acornJumper?.Bounce(5.0f, null);
    }
}
