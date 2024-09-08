using UnityEngine;

public class DartRotationLocker : MonoBehaviour
{
    [Header("Rotation Constraints (degrees)")]
    [Tooltip("Set the minimum and maximum allowed global rotation in the X axis.")]
    public Vector2 xRotationLimits = new Vector2(-45f, 45f); // Min and Max limits for X rotation

    [Tooltip("Set the minimum and maximum allowed global rotation in the Y axis.")]
    public Vector2 yRotationLimits = new Vector2(-45f, 45f); // Min and Max limits for Y rotation

    [Tooltip("Set the minimum and maximum allowed global rotation in the Z axis.")]
    public Vector2 zRotationLimits = new Vector2(-45f, 45f); // Min and Max limits for Z rotation

    [Tooltip("Toggle to apply the rotation constraints.")]
    public bool applyConstraints = true; // Toggle to apply constraints

    private void Update()
    {
        if (applyConstraints)
        {
            ApplyGlobalConstraints();
        }
    }

    private void ApplyGlobalConstraints()
    {
        // Get the global rotation of the object in Euler angles
        Vector3 globalRotation = transform.rotation.eulerAngles;

        // Wrap angles to the range -180 to 180
        globalRotation.x = WrapAngle(globalRotation.x);
        globalRotation.y = WrapAngle(globalRotation.y);
        globalRotation.z = WrapAngle(globalRotation.z);

        // Apply constraints to each axis
        globalRotation.x = Mathf.Clamp(globalRotation.x, xRotationLimits.x, xRotationLimits.y);
        globalRotation.y = Mathf.Clamp(globalRotation.y, yRotationLimits.x, yRotationLimits.y);
        globalRotation.z = Mathf.Clamp(globalRotation.z, zRotationLimits.x, zRotationLimits.y);

        // Apply the constrained rotation back to the object (as a Quaternion)
        transform.rotation = Quaternion.Euler(globalRotation);
    }

    private float WrapAngle(float angle)
    {
        // Wrap the angle to be between -180 and 180
        if (angle > 180f)
            angle -= 360f;
        return angle;
    }

    public void SetApplyConstrainsToTrue()
    {
        applyConstraints = true;
    }

    public void SetApplyConstrainsToFalse()
    {
        applyConstraints = false;
    }
}