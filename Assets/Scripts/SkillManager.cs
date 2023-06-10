using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillManager : MonoBehaviour
{

    //BECERÝ 1 HASAR ARTTIRIR
    //BECERÝ 2 CAN ARTTIRIR
    //BECERÝ 3 MERMÝLERÝ 2 YE KATLAR

    public int maxSkillLevel = 4;
    public string[] skillNames; // Becerilerin isimlerini içeren dizi
    public int[] skillLevels; // Becerilerin seviyelerini içeren dizi

    public TextMeshProUGUI skillText; // UI Text elemaný
    public Gun_Script gunScript;
    public CharacterController characterControllerScript;
    public KnifeSkill knifeSkillScript;
    public Garlic garlicScript;
    public string skillName;
    public int skillLevel;
    public GameObject garlicPrefab;
    public float distanceNeeded;
    
    private void Start()
    {
        ClearSkillText();
        
        knifeSkillScript = FindObjectOfType<KnifeSkill>().GetComponent<KnifeSkill>();
        characterControllerScript = FindObjectOfType<CharacterController>().GetComponent<CharacterController>();
        gunScript = FindObjectOfType<Gun_Script>().GetComponent<Gun_Script>();
        skillLevels = new int[skillNames.Length]; // Becerilerin seviyelerini saklamak için bir dizi oluþturuluyor
        
    }

    public void LevelUp()
    {
        
        int randomSkillIndex = Random.Range(0, skillNames.Length); // Rastgele bir beceri seçiliyor

        if (skillLevels[randomSkillIndex] < maxSkillLevel)
        {
            skillLevels[randomSkillIndex]++; // Seçilen becerinin seviyesi artýrýlýyor

             skillName = skillNames[randomSkillIndex];
             skillLevel = skillLevels[randomSkillIndex];

            // Metni ekrana yazdýrma
            skillText.text = skillName + " becerisi geliþti, þu anda " + skillLevel + ". seviyede.";

            Invoke("ClearSkillText", 3f);
            if (skillLevel == 4)
            {
                if (skillName == "DamageUp")
                {
                    gunScript.bulletDamage += 20;
                }

                if (skillName == "SpeedUp")
                {
                    characterControllerScript.moveSpeed += 2;
                }
                if (skillName == "KnifeSkill")
                {
                    knifeSkillScript.knifedamage += 20;
                }
                if (skillName == "AttackSpeedUp")
                {
                    gunScript.throwCooldownTime -= 0.1f;
                }
                if (skillName == "Garlic")
                {
                    garlicScript.garlicDamage += 2;
                }
                if (skillName == "XPDistance")
                {
                    distanceNeeded += 5;
                }



            }
            // Burada, beceriye özel çalýþacak kodu buraya ekleyebilirsiniz.
            // Örneðin:
            //___________________________________________________________________________________________________________
            if (skillLevel == 3)
            {
                if (skillName == "DamageUp")
                {
                    gunScript.bulletDamage += 20;
                }

                if (skillName == "SpeedUp")
                {
                    characterControllerScript.moveSpeed += 1;
                }
                if (skillName == "KnifeSkill")
                {
                    knifeSkillScript.ToggleObject2();
                }
                if (skillName == "AttackSpeedUp")
                {
                    gunScript.throwCooldownTime -= 0.1f;
                }
                if (skillName == "Garlic")
                {
                    garlicScript.ToggleScale();
                }
                if (skillName == "XPDistance")
                {
                    distanceNeeded += 4;
                }



            }
            //___________________________________________________________________________________________________________
            if (skillLevel == 2)
            {

                if (skillName == "DamageUp")
                {
                    gunScript.bulletDamage += 10;
                }

                if (skillName == "SpeedUp")
                {
                    characterControllerScript.moveSpeed += 1;
                }
                if (skillName == "KnifeSkill")
                {
                    knifeSkillScript.ToggleSizeIncrease();
                }
                if (skillName == "AttackSpeedUp")
                {
                    gunScript.throwCooldownTime -= 0.1f;
                }
                if (skillName == "Garlic")
                {
                    garlicScript.ToggleScale();
                }
                if (skillName == "XPDistance")
                {
                    distanceNeeded += 4;
                }



            }
            //___________________________________________________________________________________________________________
            if (skillLevel == 1)
            {

                if (skillName == "DamageUp")
                {
                    gunScript.bulletDamage += 10;
                }

                if (skillName == "SpeedUp")
                {
                    characterControllerScript.moveSpeed += 1;
                }
                if (skillName == "KnifeSkill")
                {
                    knifeSkillScript.knifeSkillActive = true;
                    knifeSkillScript.AddKnives();
                }
                if (skillName == "AttackSpeedUp")
                {
                    gunScript.throwCooldownTime -= 0.1f;
                }
                if (skillName == "Garlic")
                {

                    Instantiate(garlicPrefab, Vector3.zero, Quaternion.identity);
                    garlicScript = FindObjectOfType<Garlic>().GetComponent<Garlic>();
                }
                if(skillName == "XPDistance")
                {
                    distanceNeeded += 3; 
                }

                

            }


        }
    }
    private void ClearSkillText()
    {
        skillText.text = string.Empty; // Metni temizleme
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            LevelUp();
        }
    }
}