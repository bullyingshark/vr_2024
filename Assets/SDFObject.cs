using UnityEngine;

[System.Serializable]
public class SDFObject
{
    public enum SDFType
    {
        Sphere,
        Box,
        Torus
    }

    public SDFType type;
    public Vector3 position;
    public Vector3 size; // For Box and Torus
    public float radius; // For Sphere and Torus

    public float Evaluate(Vector3 point)
    {
        switch (type)
        {
            case SDFType.Sphere:
                return Vector3.Distance(point, position) - radius;

            case SDFType.Box:
                Vector3 boxDistance = new Vector3(
                    Mathf.Abs(point.x - position.x) - size.x,
                    Mathf.Abs(point.y - position.y) - size.y,
                    Mathf.Abs(point.z - position.z) - size.z
                );
                return Mathf.Max(boxDistance.x, Mathf.Max(boxDistance.y, boxDistance.z));

            case SDFType.Torus:
                Vector3 torusOffset = point - position;
                Vector2 torusDistance = new Vector2(
                    new Vector2(torusOffset.x, torusOffset.z).magnitude - size.x,
                    torusOffset.y
                );
                return torusDistance.magnitude - radius;

            default:
                return float.MaxValue;
        }
    }
}
