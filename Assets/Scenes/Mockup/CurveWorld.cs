using UnityEngine;

public class CurveWorld : MonoBehaviour
{
    private static string CURVATURE_KEYWORD = "WORLD_CURVATURE";

    void OnEnable()
    {
        Shader.EnableKeyword(CURVATURE_KEYWORD);
    }

    private void OnDisable()
    {
        Shader.DisableKeyword(CURVATURE_KEYWORD);
    }
}
