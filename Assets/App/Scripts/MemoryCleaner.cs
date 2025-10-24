using UnityEditor;
using UnityEngine;

public class MemoryCleaner : MonoBehaviour
{
#if UNITY_EDITOR
    private float timer = 11f;
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 10f)
        {
            timer = 0f;
            EditorUtility.UnloadUnusedAssetsImmediate();
            System.GC.Collect();
        }
    }
#endif
}
