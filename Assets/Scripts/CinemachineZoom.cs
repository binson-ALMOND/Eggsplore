using UnityEngine;
using Unity.Cinemachine;
using UnityEngine.InputSystem;

public class CinemachineZoom : MonoBehaviour
{

    private CinemachineCamera vcam;

    [Header("Ustawienia Zoomu")] 
    public float zoomSpeed = 0.005f;
    public float minZoom = 2f;
    public float maxZoom = 12f;
    void Start()
    {
        vcam = GetComponent<CinemachineCamera>();
        if (vcam == null)
        {
            Debug.LogError("❌ BŁĄD: Skrypt 'CinemachineZoom' jest przypięty do złego obiektu! Musisz przypiąć go do obiektu z komponentem CinemachineCamera (a nie do Main Camera).");
        }
    }
    void Update()
    {
        if (Mouse.current == null) return;
        
        float scrollInput = Mouse.current.scroll.ReadValue().y;

        if (scrollInput != 0f)
        {
            LensSettings lens = vcam.Lens;
            
            lens.OrthographicSize -= scrollInput * zoomSpeed;
            lens.OrthographicSize = Mathf.Clamp(lens.OrthographicSize, minZoom, maxZoom);
            
            vcam.Lens = lens;
            Debug.Log($"🔍 Zoom działa! Nowy Size: {lens.OrthographicSize}");
        }
    }
}
