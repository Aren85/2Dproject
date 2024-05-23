using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("基本屬性")]
    public float maxHealth;//最大血量
    public float currentHealth;//當前血量

    [Header("受傷無敵")]
    public int invulnerableDuration;//無敵持續時間
    private float invulnerableCounter;
    public bool invulnerable;//是否正在無敵狀態
    private void Start()
    {
        currentHealth = maxHealth;
    }
    private void Update()
    {
        if (invulnerable)
        {
            invulnerableCounter -= Time.deltaTime;
            if (invulnerableCounter <= 0)
            {
                invulnerable = false;
            }
        }
    }
    public void TakeDamage(Attack attacker)
    {
        if (invulnerable) { return; }

        //Debug.Log(attacker.damage);

        if (currentHealth - attacker.damage > 0)
        {
            currentHealth -= attacker.damage;
            TriggerInvulnerable();
        }
        else
        {
            currentHealth = 0;
            //觸發死亡
        }
    }
    /// <summary>
    /// 觸發受傷無敵
    /// </summary>
    private void TriggerInvulnerable()
    {
        if (!invulnerable)
        {
            invulnerable = true;
            invulnerableCounter = invulnerableDuration;
        }
    }
}
