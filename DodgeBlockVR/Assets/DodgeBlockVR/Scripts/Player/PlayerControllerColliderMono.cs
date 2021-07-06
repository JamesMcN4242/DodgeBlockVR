////////////////////////////////////////////////////////////
/////   PlayerControllerColliderMono.cs
/////   James McNeil - 2021
////////////////////////////////////////////////////////////

using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class PlayerControllerColliderMono : MonoBehaviour
{
    private BoxCollider m_collider = null;
    private List<Transform> m_collisionObjects = null;
    public bool HasCollisionsToProcess => m_collisionObjects.Count > 0;

    private void Start()
    {
        m_collider = GetComponent<BoxCollider>();
        Debug.Assert(m_collider != null, $"No collider was on the player controller with parent name {transform.parent.name}");

        const int startingCapacity = 5;
        m_collisionObjects = new List<Transform>(startingCapacity);
    }

    private void OnTriggerEnter(Collider other)
    {
        m_collisionObjects.Add(other.transform);
    }

    public Transform[] ConsumeCollisions()
    {
        var collisions = m_collisionObjects.ToArray();
        m_collisionObjects.Clear();
        return collisions;
    }
}
