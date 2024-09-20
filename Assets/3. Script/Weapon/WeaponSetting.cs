using TMPro;
using UnityEngine;

public class WeaponSetting : MonoBehaviour
{

    public IWeaponState currentState;

    public enum Type
    {
        None    =  0,
        AWP     = 100,
        AK47    = 101,
        XM1014  = 102,

        Glock18 = 200,

        Knife   = 300, 


        HE      = 400,
        FL      = 401,
        SMOKE   = 402,


        C4      = 500,
    } 
    public Type type { get; set; }
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
