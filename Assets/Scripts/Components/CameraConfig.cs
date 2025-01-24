using UnityEngine;

[CreateAssetMenu(menuName = "Camera Config", fileName = "Camera Config")]
public class CameraConfig : ScriptableObject
{
    public float Sensitivity => sensitivity;
    public float MaxAngleY => maxAngleY;
    [SerializeField] private float sensitivity;
    [SerializeField] private float maxAngleY;
}