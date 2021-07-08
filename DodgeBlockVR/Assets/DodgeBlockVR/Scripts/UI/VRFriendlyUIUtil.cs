////////////////////////////////////////////////////////////
/////   VRFriendlyUIUtil.cs
/////   James McNeil - 2021
////////////////////////////////////////////////////////////

using UnityEngine;

public static class VRFriendlyUIUtil
{
    private const float k_uiMoveSpeed = 2.0f;
    private const float k_minDistBeforeMove = 0.5f;

    public static void UpdateUIPosition(Transform uiTransform, Transform playerTransform, float offsetDistance, float dt)
    {
        Vector3 directionOffset = (playerTransform.forward * offsetDistance);
        Vector3 targetPos = playerTransform.position + directionOffset;
        (bool changed, Vector3 newPosition) = LerpPosition(uiTransform.position, targetPos, dt);

        if (changed)
        {
            uiTransform.position = newPosition;
        }

        Vector3 facePoint = 2f * newPosition - playerTransform.position;
        uiTransform.LookAt(facePoint);
    }

    private static (bool changed, Vector3 newPosition) LerpPosition(Vector3 startPos, Vector3 targetPos, float dt)
    {
        Vector3 change = targetPos - startPos;
        float distance = change.magnitude;
        if (distance < k_minDistBeforeMove)
        {
            return (false, startPos);
        }

        float jumpDistance = Mathf.Min(distance, dt * k_uiMoveSpeed);
        return (true, startPos + (change.normalized * jumpDistance));
    }
}
