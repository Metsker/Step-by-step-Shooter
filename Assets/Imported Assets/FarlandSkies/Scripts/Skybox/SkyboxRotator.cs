using UnityEngine;

public class SkyboxRotator : MonoBehaviour
{
    [SerializeField] private float rotationPerSecond = 1;
    private static readonly int Rotation = Shader.PropertyToID("_Rotation");

    protected void Update()
    {
        RenderSettings.skybox.SetFloat(Rotation, Time.time * rotationPerSecond);
    }
}