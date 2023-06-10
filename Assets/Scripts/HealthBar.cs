using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private XPSystem xpSystemScript;
    [SerializeField] private Image maxHealthBar;
    [SerializeField] private Image currentHealthBar;
    // Start is called before the first frame update
    void Start()
    {
        xpSystemScript = FindObjectOfType<XPSystem>().GetComponent<XPSystem>();
        maxHealthBar.fillAmount = xpSystemScript.maxHP / 10;
    }

    // Update is called once per frame
    void Update()
    {
        currentHealthBar.fillAmount = xpSystemScript.currentHP / 10;
    }
}
