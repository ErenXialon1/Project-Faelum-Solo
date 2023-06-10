using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu (fileName = "Upgrade", menuName = "Create Upgrade")]
public class UpgradeOption : ScriptableObject
{
    public string name;
    public string description;
    public string effect;
   
}
