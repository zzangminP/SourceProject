using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreUI : MonoBehaviour
{
    public Button buyAKButton;
    public Button buyXMButton;
    public Button buyAWPButton;
    public Button buyArmorButton;
    public Button buyGlockButton;
    public Button buyFlashButton;
    public Button buyHEButton;
    public Button buySmokeButton;
    public Button closeButton;

    public PlayerControl player;


    void Start()
    {
        player = GetComponentInParent<PlayerControl>();

        buyAKButton.onClick.AddListener(() => BuySomething(WeaponSetting.Type.AK47));
        buyXMButton.onClick.AddListener(() => BuySomething(WeaponSetting.Type.XM1014));
        buyAWPButton.onClick.AddListener(() => BuySomething(WeaponSetting.Type.AWP));
        buyArmorButton.onClick.AddListener(() => BuySomething(WeaponSetting.Type.Armor));
        buyGlockButton.onClick.AddListener(() => BuySomething(WeaponSetting.Type.Glock18));
        buyFlashButton.onClick.AddListener(() => BuySomething(WeaponSetting.Type.FL));
        buyHEButton.onClick.AddListener(() => BuySomething(WeaponSetting.Type.HE));
        buySmokeButton.onClick.AddListener(() => BuySomething(WeaponSetting.Type.SMOKE));
        closeButton.onClick.AddListener(Close);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameObject.SetActive(false);
        }

        switch (true)
        {
            case bool _ when Input.GetKeyDown(KeyCode.Alpha1):
                BuySomething(WeaponSetting.Type.AK47);
                break;
            case bool _ when Input.GetKeyDown(KeyCode.Alpha2):
                BuySomething(WeaponSetting.Type.XM1014);
                break;
            case bool _ when Input.GetKeyDown(KeyCode.Alpha3):
                BuySomething(WeaponSetting.Type.AWP);
                break;
            case bool _ when Input.GetKeyDown(KeyCode.Alpha4):
                BuySomething(WeaponSetting.Type.Armor);
                break;
            case bool _ when Input.GetKeyDown(KeyCode.Alpha5):
                BuySomething(WeaponSetting.Type.Glock18);
                break;
            case bool _ when Input.GetKeyDown(KeyCode.Alpha6):
                BuySomething(WeaponSetting.Type.FL);
                break;
            case bool _ when Input.GetKeyDown(KeyCode.Alpha7):
                BuySomething(WeaponSetting.Type.HE);
                break;
            case bool _ when Input.GetKeyDown(KeyCode.Alpha8):
                BuySomething(WeaponSetting.Type.SMOKE);
                break;
            default:
                break;
        }





    }

    void BuySomething(WeaponSetting.Type type)
    {
        switch (type)
        {
            case WeaponSetting.Type.AK47:
                break;
            case WeaponSetting.Type.XM1014:
                break;
            case WeaponSetting.Type.AWP:
                break;
            case WeaponSetting.Type.Armor:
                break;
            case WeaponSetting.Type.Glock18:
                break;
            case WeaponSetting.Type.FL:
                break;
            case WeaponSetting.Type.HE:
                break;
            case WeaponSetting.Type.SMOKE:
                break;
            default:
                break;

        }

    }
    void BuyGun(Weapon w)
    {
        int type = (int)w.type;
        
        if ((type >= 100 && type <= 199))
        {
            //pri_weapon
            player.playerWeapon_List[0] = w;

        }
        else if ((type >= 200 && type <= 299))
        {
            //sec_weapon
            player.playerWeapon_List[1] = w;

        }
        else if (type >= 400 && type <= 499)
        {
            // GE
        }
        else if (type == 600)
        {
            // c4
            player.armor = 100;
        }
    }

    



    void Close()
    {
        gameObject.SetActive(false);

    }
}