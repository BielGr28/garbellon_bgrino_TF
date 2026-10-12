using UnityEngine;

public class Pistol_Script : Weapon
{
    public override int Damgage => 5;

    public override int Ammo => 20;

    public override float ReloadSpeed => 1.0f;

    public override float Precision => 10f;

    public override void CroshairState(GameObject croshair)
    {
        throw new System.NotImplementedException();
    }

   

    public override void Especial(Input input)
    {
        throw new System.NotImplementedException();
    }



    public override void Reload(Input input, int ammo)
    {
        throw new System.NotImplementedException();
    }



    public override void Shoot(Input input, int ammo)
    {
        throw new System.NotImplementedException();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
