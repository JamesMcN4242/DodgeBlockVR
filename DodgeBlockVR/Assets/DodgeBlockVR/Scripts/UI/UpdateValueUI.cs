////////////////////////////////////////////////////////////
/////   UpdateValueUI.cs
/////   James McNeil - 2021
////////////////////////////////////////////////////////////

using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpdateValueUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_valueChangeText = null;
    [SerializeField] private TextMeshProUGUI m_currentValueText = null;
    [SerializeField] private TextMeshProUGUI m_minValueText = null;
    [SerializeField] private TextMeshProUGUI m_maxValueText = null;
    [SerializeField] private Image m_fillImage = null;

    public void SetStaticValues(string valueDescription, int minVal, int maxVal)
    {
        m_valueChangeText.text = valueDescription;
        m_minValueText.text = minVal.ToString();
        m_maxValueText.text = maxVal.ToString();
    }

    public void SetCurrentValue(int current, int min, int max)
    {
        m_currentValueText.text = current.ToString();
        m_fillImage.fillAmount = (float)(current - min) / (float)max;
    }
}
