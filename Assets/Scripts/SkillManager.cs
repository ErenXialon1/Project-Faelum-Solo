using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillManager : MonoBehaviour
{

    //BECER� 1 HASAR ARTTIRIR
    //BECER� 2 CAN ARTTIRIR
    //BECER� 3 MERM�LER� 2 YE KATLAR

    public int maxSkillLevel = 4;
    public string[] skillNames; // Becerilerin isimlerini i�eren dizi
    public int[] skillLevels; // Becerilerin seviyelerini i�eren dizi

    public TextMeshProUGUI skillText; // UI Text eleman�
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
        skillLevels = new int[skillNames.Length]; // Becerilerin seviyelerini saklamak i�in bir dizi olu�turuluyor
        
    }

    public void LevelUp()
    {
        
        int randomSkillIndex = Random.Range(0, skillNames.Length); // Rastgele bir beceri se�iliyor

        if (skillLevels[randomSkillIndex] < maxSkillLevel)
        {
            skillLevels[randomSkillIndex]++; // Se�ilen becerinin seviyesi art�r�l�yor

             skillName = skillNames[randomSkillIndex];
             skillLevel = skillLevels[randomSkillIndex];

            // Metni ekrana yazd�rma
            skillText.text = skillName + " becerisi geli�ti, �u anda " + skillLevel + ". seviyede.";

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
            // Burada, beceriye �zel �al��acak kodu buraya ekleyebilirsiniz.
            // �rne�in:
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