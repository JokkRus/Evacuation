using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveScript : MonoBehaviour
{
    public int location;
    public int Health = 100;
    public float gustota = 0;
    public Dictionary<string, int> Inv;
    public string equippedItem;
    public bool runGame = false;
    public int sup = 500;
    public int gu = 0;
    public int ra = 0;
	
	void Start ()
    {
        
    }
	
	void Update ()
    {
        Inv = new Dictionary<string, int>();
    }
}
