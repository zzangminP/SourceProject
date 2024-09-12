using TMPro;
using UnityEngine;

public class WeaponSetting : MonoBehaviour
{

    public IWeaponState currentState;

    public Animator animator        { get; set; }

    /// <summary>
    /// Physics
    /// </summary>

    public int damage               { get; set; }
    public float range              { get; set; }
    public float impactForce        { get; set; }
    public int pellet               { get; set; }
    public bool isAuto              { get; set; }
    public float fireRate           { get; set; }
    public float lastFireTime       { get; set; }


    /// <su
    /// Amm
    /// </s

    public int maxMag               { get; set; }
    public int maxAmmo              { get; set; }
    public int currentAmmo          { get; set; }



    public int cost                 { get; set; }
    public int reward               { get; set; }


    public Camera fpsCam            { get; set; }


    //public TMP_Text Ammo_ui { get; set; }

}
