////////////////////////////////////////////////////////////
/////   BaseStateUI.cs
/////   James McNeil - 2021
////////////////////////////////////////////////////////////

using UnityEngine;
using UnityEngine.UI;

public class BaseStateUI
{
    private Transform m_rootTransform = null;
    private Image m_timerImage = null;

    public BaseStateUI(GameObject prefabToCreate, Vector3 spawnPosition)
    {
        m_rootTransform = GameObject.Instantiate(prefabToCreate, spawnPosition, Quaternion.identity).transform;
        m_timerImage = m_rootTransform.Find("Background/TimerImage").GetComponent<Image>();
    }

    public void UpdatePositionAndRotation(Transform playerTransform, float offsetDistance, float dt)
    {
        VRFriendlyUIUtil.UpdateUIPosition(m_rootTransform, playerTransform, offsetDistance, dt);
    }

    public void SetTimerFill(float currentValue, float maxValue)
    {
        m_timerImage.fillAmount = currentValue / maxValue;
    }

    public void Show(bool shouldShow)
    {
        if(m_rootTransform.gameObject.activeInHierarchy != shouldShow)
        {
            m_rootTransform.gameObject.SetActive(shouldShow);
        }
    }
}
