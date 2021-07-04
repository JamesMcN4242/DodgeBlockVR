////////////////////////////////////////////////////////////
/////   BlockData.cs
/////   James McNeil - 2021
////////////////////////////////////////////////////////////

using UnityEngine;

[CreateAssetMenu(fileName = "BlockData", menuName = "Data/Block")]
public class BlockData : ScriptableObject
{
    public float m_minimumSpeed = 2.0f;
    public float m_maximumSpeed = 10.0f;

    public float GetNextBlockSpeed => Random.Range(m_minimumSpeed, m_maximumSpeed);
}
