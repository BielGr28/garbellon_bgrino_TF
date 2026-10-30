using System;
using System.Collections;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    
    [Header("INHERITED WEAPON STATS")]

    public abstract string Type {get;}
    public abstract int Damgage { get; }
    public abstract int TotalAmmo {get;}
    public abstract int Ammo { get; }
    public abstract float ReloadSpeed { get; }
    public abstract float FireRate { get; }
    public abstract float Precision { get; }

    protected float Range = 100.0f;

    public abstract void Shoot();
    public abstract void Reload(int ammo);
    public abstract IEnumerator ShootingDelay(float delay);
    public abstract void Especial();
    public abstract void CroshairState(GameObject croshair);

}
/*
public interface IWeaponStats
{
    int Damgage{get;}
    int Ammo {get;}
    float ReloadSpeed{get;}
    float Precision{get;}

}*/
