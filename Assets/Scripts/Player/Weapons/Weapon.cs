using UnityEngine;
using System.Collections;

public abstract class Weapon : MonoBehaviour
{
    public abstract int Damgage { get; }
    public abstract int Ammo { get; }
    public abstract float ReloadSpeed { get; }
    public abstract float Precision { get; }
    public abstract void Shoot(Input input, int ammo);

    public abstract void Reload(Input input, int ammo);
    public abstract void Especial(Input input);

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
