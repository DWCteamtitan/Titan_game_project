using UnityEngine;
using System.Collections;

public class Camera_Switching_Script : MonoBehaviour
{


    public Camera[] cameras;
    int cameraIndex = 0;
    bool switchAudioListener = true;
    private AudioListener listener;

    void Start()
    {
        if (cameras.Length < 1)
        {
            Debug.LogError("No cameras set.");
            return;
        }

        foreach (Camera c in cameras)
        {
            ToggleCam(c, false);
        }

        if ((cameraIndex < 0) || (cameraIndex >= cameras.Length))
        {
            Debug.LogError("Invalid camera index.");
            cameraIndex = 0;
        }
        ToggleCam(cameras[cameraIndex], true);
    }

    void ToggleCam(Camera cam, bool enabled)
    {
        cam.enabled = enabled;
        if (switchAudioListener)
        {
            listener = cam.GetComponent<AudioListener>();
            if (listener)
            {
                listener.enabled = enabled;
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown("x"))
        {
            ToggleCam(cameras[cameraIndex], false);
            cameraIndex = (cameraIndex + 1) % cameras.Length;
            ToggleCam(cameras[cameraIndex], true);
        }
    }

}

