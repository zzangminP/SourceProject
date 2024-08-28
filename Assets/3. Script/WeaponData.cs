using System.Collections;
using System.Collections.Generic;
using UnityEngine;




[CreateAssetMenu(fileName ="NewWeapon",menuName ="SourceProject/Weapon", order =1)]
public class WeaponData : ScriptableObject
{


    /// <summary>
    /// 
    /// 
    ///  TODO : Audio, sync,
    /// 
    /// 
    /// </summary>

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
        Kevlar,
        KevlarAndHelmet,
        Kit,
        C4
    }

    
    public string name;
    public int maxAmmo;
    public int maxMag;

    // to buy weapon
    public int cost;

    // relate with player movement
    public int mass;


    // 
    public GameObject prefab;

    // for animator
    // GameObject ViewModel, WorldModel
    public GameObject _v_model;
    //public GameObject _w_model;
    // public List<AudioClip> audioClips;
    

    
    
    
    
    // public string name[]

}
