using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotemBehavior : MonoBehaviour
{
    public float FieldRadius;
    private GameObject m_field;

    void Start()
    {
        m_field = transform.Find("Field").gameObject;
        m_field.transform.localScale = new Vector3(FieldRadius, FieldRadius, FieldRadius);
    }
    
    void Update()
    {
        
    }
}
