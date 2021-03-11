using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[CreateAssetMenu(fileName ="Item", menuName ="Items")]
public class Item : ScriptableObject
{
    public string Name;
    public string FileName;
    public Sprite Sprite;
    public bool IsStackable;
    public int MaxCount;
    public string Description;
    public bool IsUseble;
    public bool IsWeapon;
    public bool IsEquipment;
    //public void LoadPrefab()
    //{


    //    GameObject prefab = Resources.Load(FileName + ".prefab") as GameObject;
    //    GameObject copy = Object.Instantiate(prefab);
    
    //}
}
