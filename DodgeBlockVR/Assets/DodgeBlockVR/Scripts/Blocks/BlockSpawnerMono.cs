////////////////////////////////////////////////////////////
/////   BlockSpawnerMono.cs
/////   James McNeil - 2021
////////////////////////////////////////////////////////////

using UnityEngine;

public class BlockSpawnerMono : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, 0.3f);
    }
}
