using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[System.Serializable]
public class KeyValuePair
{
    public WeaponSetting.Type key;
    public GameObject value;

}


public class WeaponHolder : MonoBehaviour
{

    [SerializeField] private PlayerControl player;
    [SerializeField] public GameObject primary;   
    [SerializeField] public GameObject second;
    [SerializeField] public GameObject knife;
    [SerializeField] public GameObject GE;
    [SerializeField] public GameObject c4;

    //public Dictionary<WeaponSetting.Type, GameObject> selectedWeapon;
    [SerializeField] public List<KeyValuePair> selectedWeapon; //= new List<KeyValuePair>(); 

    private void Start()
    {
        player = GetComponentInParent<PlayerControl>();
    }


    public void WeaponChange()
    {


    }

    public void WeaponDrop(Vector3 position, Weapon playerWeapon)
    {
        if (playerWeapon.type == WeaponSetting.Type.Knife)
            return;
        GameObject dropGunPrefab = null;
        for(int i = 0; i < selectedWeapon.Count; i++)
        {
            if (selectedWeapon[i].key == playerWeapon.type)
            {
                dropGunPrefab = selectedWeapon[i].value;
                //dropGunPrefab.AddComponent<WeaponWorldDrop>();
                dropGunPrefab.TryGetComponent<WeaponWorldDrop>(out WeaponWorldDrop tempGun);
                
                tempGun.maxAmmo = playerWeapon.maxAmmo;
                tempGun.currentAmmo = playerWeapon.currentAmmo;
                tempGun.type = playerWeapon.type;


                break;
            }
        }
                                  
        GameObject dropGun = Instantiate(dropGunPrefab, position, Quaternion.identity,GameObject.Find("SpawnedObject").transform);
        //dropGun.transform.SetParent(GameObject.Find("SpawnedObject").transform);

        //tempGun.maxAmmo = 0;
        //tempGun.currentAmmo = 0;
        //tempGun.type = type;
        dropGun.GetComponent<Rigidbody>().AddForce(player.fpsCam.transform.forward * 10f, ForceMode.Impulse);
    }





}