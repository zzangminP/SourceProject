using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class KeyValuePair
{
    public WeaponSetting.Type key;
    public GameObject value;

}


public class WeaponHolder : MonoBehaviour
{

    
    [SerializeField] public GameObject primary;   
    [SerializeField] public GameObject second;
    [SerializeField] public GameObject knife;
    [SerializeField] public GameObject GE;
    [SerializeField] public GameObject c4;

    //public Dictionary<WeaponSetting.Type, GameObject> selectedWeapon;
    [SerializeField] public List<KeyValuePair> selectedWeapon; //= new List<KeyValuePair>(); 







}