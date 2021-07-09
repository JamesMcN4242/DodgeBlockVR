////////////////////////////////////////////////////////////
/////   BlockData.cs
/////   James McNeil - 2021
////////////////////////////////////////////////////////////

using UnityEngine;

[CreateAssetMenu(fileName = "BlockData", menuName = "Data/Block")]
public class BlockData : ScriptableObject
{
    public const int k_minimumBlocks = 1;
    public const int k_maximumBlocks = 100;
    public const float k_minimumSpeed = 2.0f;
    public const float k_maximumSpeed = 15.0f;

    [Range(k_minimumBlocks, k_maximumBlocks)] public int m_maximumBlocks = 5;
    [Range(0, 10f)] public float m_minimumTimeBeforeNextSpawn = 0.75f;

    [Header("Speed Range")]
    [Range(k_minimumSpeed, k_maximumSpeed)] public float m_minimumSpeed = 2.0f;
    [Range(k_minimumSpeed, k_maximumSpeed)] public float m_maximumSpeed = 10.0f;
}
