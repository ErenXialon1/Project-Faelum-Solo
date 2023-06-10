using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;

public class XPSystem : MonoBehaviour
{

    public float maxHP = 4;
    public float currentHP;
    public CharacterController characterControllerScript;
    public int currentXP = 0;
    public int LVL = 1;
    public int levelUpXP = 400;
    public bool isHurt;
    public bool isHealing;
    public SkillManager skillManagerScript;
    public Slider xpSlider;
    public TextMeshProUGUI timeElapsedText;
    
    
    
    
    void Start()
    {
        characterControllerScript = FindObjectOfType<CharacterController>().GetComponent<CharacterController>();
        currentHP = maxHP;
        skillManagerScript = FindAnyObjectByType<SkillManager>().GetComponent<SkillManager>();
        xpSlider.minValue = 0;
        xpSlider.maxValue = levelUpXP;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("XP1"))
        {
            currentXP += 50;
            CheckLevelUp();
            Destroy(other.gameObject);
            
        }
        if (other.gameObject.CompareTag("XP2"))
        {
            currentXP += 150;
            CheckLevelUp();
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("XP3"))
        {
            currentXP += 1050;
            CheckLevelUp();
            Destroy(other.gameObject);
        }
    }

    public void CheckLevelUp()
    {
        if (currentXP >= levelUpXP)
        {

            
            currentXP = currentXP - levelUpXP;
            levelUpXP =  levelUpXP + 400;
            xpSlider.value = currentXP;
            xpSlider.maxValue = levelUpXP;
            LevelUp();
        }
        else
        {
            xpSlider.value = currentXP;
        }
    }
    public void LevelUp()
    {
        LVL++;
        skillManagerScript.LevelUp();
       
        
    }
  public void PlayerDeath()
    {
        characterControllerScript.animator.SetBool("IsDead", true);
        characterControllerScript.animator.SetTrigger("Death");
        characterControllerScript.moveSpeed = 0;
       
        gameObject.GetComponent<CharacterController>().enabled = false;
       
       
       
        Invoke("TimeStop", 1);
        
    }
    private void TimeStop()
    {
        Time.timeScale = 0;
    }
    public void PlayerDamaged()
    {
        
        currentHP -= 1;
        characterControllerScript.animator.SetTrigger("Hurt");
       
        isHurt = true;
       

    }
    private void RegenHP()
    {
        currentHP += 1;
        if(currentHP >= maxHP)
        {
            currentHP = maxHP;

        }
    }
    IEnumerator HealingCooldown()
    {
        isHealing = true;
        yield return new WaitForSeconds(3);
        isHealing = false;
        if (currentHP > 0)
        {
            RegenHP();
        }
        
       
    }
 
    private void Update()
    {
        if (isHurt == true)
        {
            isHealing = false;
            StopAllCoroutines();
            
        }
        if (currentHP < maxHP && isHealing == false)
        {
            isHurt = false;
            isHealing = true;
           StartCoroutine(HealingCooldown());
            
        }
        TimeSpan timeSpan = TimeSpan.FromSeconds(Time.time);
        string timeText = string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);
        timeElapsedText.text = timeText;
    }

}
