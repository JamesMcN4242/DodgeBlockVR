////////////////////////////////////////////////////////////
/////   BlockData.cs
/////   James McNeil - 2021
////////////////////////////////////////////////////////////

using UnityEngine;

[CreateAssetMenu(fileName = "BlockData", menuName = "Data/Block")]
public class BlockData : ScriptableObject
{
    [Range(0, 100)] public int m_maximumBlocks = 5;
    [Range(0, 10f)] public float m_minimumTimeBeforeNextSpawn = 0.75f;

    [Header("Speed Range")]
    [Range(0.5f, 15f)] public float m_minimumSpeed = 2.0f;
    [Range(0.5f, 15f)] public float m_maximumSpeed = 10.0f;

    public float GetNextBlockSpeed => Random.Range(m_minimumSpeed, m_maximumSpeed);
}
