using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Energy : MonoBehaviour
{
    private static Slider m_energySlider;
    private static float m_value; // [0;1]

    void Start()
    {
        m_energySlider = GameObject.Find("EnergySlider").GetComponent<Slider>();
        Value = 1.0f;
    }

    public static float Value
    {
        get { return m_value; }
        set
        {
            m_value = value;
            UpdateUI();
        }
    }

    public static void AddEnergy(float f)
    {
        if (m_value + f > 1.0f)
            m_value = 1.0f;
        else if (m_value - f < 0.0f)
            m_value = 0.0f;

        m_value += f;

        UpdateUI();
    }

    public static void RemoveEnergy(float f)
    {
        AddEnergy(-f);
    }

    private static void UpdateUI()
    {
        m_energySlider.value = m_value;
    }
}
