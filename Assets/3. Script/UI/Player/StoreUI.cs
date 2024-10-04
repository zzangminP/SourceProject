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
    private void OnEnable()
    {
         Cursor.visible = true;
         Cursor.lockState=CursorLockMode.None;
    }
    private void OnDisable()
    {   
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
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
                { 
                    AK47 newGun = new AK47();
                    BuyGun(newGun);
                }
                break;
            case WeaponSetting.Type.XM1014:
                {
                    XM1014 newGun = new XM1014();
                    BuyGun(newGun);
                }
                break;
            case WeaponSetting.Type.AWP:
                {
                    AWP newGun = new AWP();
                    BuyGun(newGun);
                }
                break;
            case WeaponSetting.Type.Armor:
                {
                    player.armor = 100;
                    player.money -= 650;
                }
                break;
            case WeaponSetting.Type.Glock18:
                {
                    Glock18 newGun = new Glock18();
                    BuyGun(newGun);
                }
                break;
            case WeaponSetting.Type.FL:
                {
                    GrenadeThrowerFL newGun = new GrenadeThrowerFL();
                    
                    BuyGun(newGun);
                }
                break;
            case WeaponSetting.Type.HE:
                {
                    GrenadeThrowerHE newGun = new GrenadeThrowerHE();
                    BuyGun(newGun);

                }
                break;
            case WeaponSetting.Type.SMOKE:
                {
                    GrenadeThrowerSMOKE newGun = new GrenadeThrowerSMOKE();
                    
                    BuyGun(newGun);
                }
                break;
            default:
                break;

        }

    }
    void BuyGun(Weapon tempGun)
    {
        int _type = (int)tempGun.type;

        if ((_type >= 100 && _type <= 199))
        {
            //pri_weapon
            if (player.playerWeapon_List[0] != null || player.money < tempGun.cost)
            {
                return;
            }
            else
            {
                player.playerWeapon_List[0] = player.BuyWeapon(tempGun);
                player.money -= tempGun.cost;

            }

        }
        else if ((_type >= 200 && _type <= 299))
        {
            //sec_weapon

            if (player.playerWeapon_List[1] != null || player.money < tempGun.cost)
            {
                return;
            }
            else
            {
                player.playerWeapon_List[1] = player.BuyWeapon(tempGun);
                player.money -= tempGun.cost;
            }
        }
        else if (_type >= 400 && _type <= 499)
        {
            // GE
            if(player.playerWeapon_List[3] != null || player.money < 300)
            {
                return;
            }
            else
            {
                player.playerWeapon_List[3] = player.BuyWeapon(tempGun);
                player.money -= tempGun.cost;

            }
        }
        else
            return;
    }

    



    void Close()
    {
        gameObject.SetActive(false);

    }
}