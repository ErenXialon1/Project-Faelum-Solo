using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public GameObject xpPrefab;
    public Gun_Script gunScript;
    public XPSystem xpSystem;
    public float enemyMaxHP;
    public float enemyCurrentHP;
    public KnifeSkill knifeSkillScript;
    public Slider hpBar;
   

    private void Start()
    {
        hpBar = transform.GetChild(0).GetChild(0).gameObject.GetComponent<Slider>();
        hpBar.maxValue = enemyMaxHP;
        hpBar.gameObject.SetActive(false);
        knifeSkillScript = FindObjectOfType<KnifeSkill>().GetComponent<KnifeSkill>();
        xpSystem = FindObjectOfType<XPSystem>().GetComponent<XPSystem>();
        gunScript = FindObjectOfType<Gun_Script>().GetComponent<Gun_Script>();
        enemyCurrentHP = enemyMaxHP;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
      
        if (collision.gameObject.CompareTag("Player"))
        {
            Instantiate(xpPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
            xpSystem.PlayerDamaged();
            if(xpSystem.currentHP == 0)
            {
                xpSystem.PlayerDeath();
            }
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject); // Çarpan mermiyi yok et
            enemyCurrentHP -= gunScript.bulletDamage;
            if(enemyCurrentHP <= 0)
            {
                EnemyDeath();
            }
            
        }
        if (collision.gameObject.CompareTag("Knife"))
        {
            
            enemyCurrentHP -= knifeSkillScript.knifedamage;

            if (enemyCurrentHP <= 0)
            {
                EnemyDeath();
            }
        }

    }
    private void Update()
    {
        if(enemyCurrentHP < enemyMaxHP)
        {
            hpBar.gameObject.SetActive(true);
            hpBar.value = enemyCurrentHP;
        }
    }

    public void EnemyDeath()
    {
        // XP game object'inin spawnlanmasý
        Instantiate(xpPrefab, transform.position, Quaternion.identity);

        Destroy(gameObject); // Düþmaný yok et
    }
}
