using TMPro;
using UnityEngine;

public class WeaponSetting : MonoBehaviour
{

    public IWeaponState currentState;

    public enum Type
    {
        AWP     = 10,
        AK47    = 11,
        XM1014  = 12,

        Glock18 = 20,

        Knife   = 30, 


        HE      = 40,
        FL      = 41,
        SMOKE   = 42,


        C4      = 50,
    }

    //public enum Type_Sec
    //{
    //    Glock18 = 0,
    //}
    //
    //public enum Type_GE
    //{
    //    HE = 0,
    //    FL,
    //    SMOKE,
    //}

    public Animator animator        { get; set; }

    /// <summary>
    /// Physics
    /// </summary>

    public int damage               { get; set; }
    public float range              { get; set; }
    public float impactForce        { get; set; }
    public int pellet               { get; set; }
    public bool isAuto              { get; set; }
    public bool isBurst             { get; set; }

    /// <summary>
    /// RPM / 60
    /// </summary>
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
