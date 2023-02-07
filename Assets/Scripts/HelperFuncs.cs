using UnityEngine;

public static class HelperFuncs
{
    private static  LayerMask _layerMask = LayerMask.GetMask("Default");
    
    // Check if two element hit in such direction.
    public static bool Raycast(this Rigidbody2D rigidbody, Vector2 direction)
    {

        if (rigidbody.isKinematic)
        {
            return false;
        }

        float radius = 0.25f;
        float distance = 0.375f;
        
        RaycastHit2D hit = Physics2D.CircleCast(rigidbody.position, radius, direction.normalized, distance, _layerMask);
        return hit.collider != null && hit.rigidbody != rigidbody;

    }

    public static bool DotTest(this Transform transform, Transform other, Vector2 testDirection)
    {
        Vector2 direction = other.position - transform.position;
        return Vector2.Dot(direction.normalized, testDirection) > 0.2f;
    }
}
