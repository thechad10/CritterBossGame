using System.Collections.Generic;
using FirstGearGames.SmoothCameraShaker;
using UnityEngine;

public class Slam_EA : EnemyAttack
{
    [SerializeField]
    private ShakeData worldShake;
    public bool bShake;
    public bool bShaking;

    public override void Attack()
    {
        if (IsAttacking)
        {
            Debug.LogError($"{this} is already attacking! Don't call this.");
            return;
        }
        IsAttacking = true;
        
    }
    private void Update()
    {
        if (bShake)
        {
            bShake = false;
            bShaking = true;
            Shake();
        }
        if (bShaking)
        {

        }
        else
        {
            stopShake();
        }
    }

    private void Shake()
    {
        CameraShakerHandler.Shake(worldShake);
    }

    private void stopShake()
    {
        CameraShakerHandler.Stop();
    }

}
