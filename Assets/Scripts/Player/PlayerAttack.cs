using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public WeaponBase currentweapon;
    public void Attack(bool isAttack, bool constant)
    {
        if (currentweapon == null)
            return;
        currentweapon.Shoot(isAttack);
    }
    public void realoding()
    {
        currentweapon.Realoding();
    }
}
