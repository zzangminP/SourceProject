using System.Collections;
using System.Collections.Generic;
using UnityEngine;




[CreateAssetMenu(fileName ="NewWeapon",menuName ="SourceProject/Weapon", order =1)]
public class WeaponData : ScriptableObject
{

    //private const int maxWeaponCount = 34;

    public enum Type
    {
        knife,
        pistol,
        SMG,
        MG,
        SG,
        AR,
        SR,
        DMR,
        GN,
        Miscellaneous,
        C4
    }

    
    public string name;
    public GameObject prefab;
    
    //public List<AudioClip> audioClips;
    

    
    
    
    
    //public string name[]

}
