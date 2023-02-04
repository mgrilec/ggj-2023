using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Transform fill;
    
    public void Set(float percentage)
    {
        fill.transform.localScale = new Vector3(percentage, fill.transform.localScale.y, fill.transform.localScale.z);
    }
}
