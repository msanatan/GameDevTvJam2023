using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAttack : Skill
{
    private bool attackFinished;
    private bool animationFinished;

    public ShootAttack(ActionHandler actionHandler, Action onFinished, CharacterStats characterStats) : base(actionHandler, onFinished, characterStats)
    {
    }

    public override Sprite SkillSprite => actionHandler.shootConfig.skillSprite;

    public override void ExecuteAction(Action onFinished)
    {
        actionHandler.GetComponent<SimpleAnimator>().SetAnimation(actionHandler.shootConfig.aniation, () => Shoot(), () => AnimationFinished());
        this.onFinished = onFinished;
        attackFinished = false;
        animationFinished = false;
    }

    private void AnimationFinished()
    {
        Debug.Log("animation finished");
        animationFinished = true;
        if(attackFinished) onFinished?.Invoke();
    }
    private void AttackFinished()
    {
        Debug.Log("attack finished");
        attackFinished = true;
        if(animationFinished) onFinished?.Invoke();
    }

    private void Shoot()
    {
        Vector3 spawnedPosition = new Vector3(actionHandler.transform.position.x + actionHandler.Direction * 0.5f, actionHandler.transform.position.y + 0.5f, 0);
        var projectile = GameObject.Instantiate(actionHandler.shootConfig.projectilePrefab, spawnedPosition, Quaternion.identity);
        projectile.GetComponent<Projectile>().Init(actionHandler.Direction, actionHandler.shootConfig.projectileSpeed, actionHandler.shootConfig.damage, actionHandler.GetComponent<Collider2D>(), () => AttackFinished());

    }

}
