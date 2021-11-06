using System.Collections;
using UnityEngine;
using Platformer.Mechanics;

public class PlatformerSpeedPad : MonoBehaviour
{
    public float maxSpeed;

    [Range (0, 5)]
    public float duration = 1f;

    private void OnTriggerEnter2D(Collider2D other){
        var rb = other.attachedRigidbody;
        if (rb == null) return;
        var player = rb.GetComponent<PlayerController>();
        if (player == null) return;
        player.StartCoroutine(PlayerModifier(player, duration));
    }

    /// <summary>
    /// Haven't used yet but a bug may happen here which would be the same as UseSpeedPowerup's bug
    /// due to it being a coroutine (namespace Platformer.JobFair.Gameplay -> UseSpeedPowerup)
    /// </summary>
    /// <param name="player"></param>
    /// <param name="lifetime"></param>
    /// <returns></returns>
    private IEnumerator PlayerModifier(PlayerController player, float lifetime){
        var initialSpeed = player.MaxSpeed;
        player.curMaxSpeed = maxSpeed;
        yield return new WaitForSeconds(lifetime);
        player.curMaxSpeed = initialSpeed;
    }

}
