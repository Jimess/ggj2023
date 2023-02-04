using System.Collections.Generic;
using UnityEngine;

namespace Parallax2D.Modules.Background.Code.Behaviours
{
  public class BackgroundMovement : MonoBehaviour
  {
    public Vector2 Speed;
    public List<CheckPosition> CheckPosition = new List<CheckPosition>();
    private Transform player;

    public void Construct(Transform player, bool nineImage)
    {
      this.player = player;
      transform.position = player.position;

      foreach (var checkInBound in CheckPosition)
        checkInBound.Construct(player, nineImage);
    }

    public void UpdateMovement()
    {
      var position = new Vector3(player.position.x, player.position.y, 0);
            transform.position = new Vector3(position.x * Speed.x, position.y * Speed.y, position.z);
      
      foreach (var checkPosition in CheckPosition)
        checkPosition.UpdateStatus();
    }
  }
}