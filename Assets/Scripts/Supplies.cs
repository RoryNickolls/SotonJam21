using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Supplies : MonoBehaviour
{
    [SerializeField] private int supplyCount = 10;

    public int SupplyCount
    {
        get { return supplyCount; }
        set
        {
            supplyCount = value;
        }
    }
}
