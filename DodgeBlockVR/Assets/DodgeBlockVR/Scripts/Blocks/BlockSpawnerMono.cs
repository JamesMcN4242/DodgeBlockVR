////////////////////////////////////////////////////////////
/////   BlockSpawnerMono.cs
/////   James McNeil - 2021
////////////////////////////////////////////////////////////

using UnityEngine;

public class BlockSpawnerMono : MonoBehaviour
{
    [SerializeField] private float m_maxSpawnedSpeed = 10.0f;
    [SerializeField] private float m_minSpawnedSpeed = 2.0f;

    public float GetNextBlockSpeed => Random.Range(m_minSpawnedSpeed, m_maxSpawnedSpeed);

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, 0.3f);
    }
}
