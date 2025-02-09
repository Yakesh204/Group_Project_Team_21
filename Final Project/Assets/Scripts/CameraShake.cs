using Cinemachine;
using UnityEngine;

public class CameraShake : MonoBehaviour {
    private CinemachineVirtualCamera _cinemachineVirtualCamera;
    private float _shakeTime;
    private CinemachineBasicMultiChannelPerlin _multiChannelPerlin;

    private void Awake() {
        _cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    private void Start() {
        _multiChannelPerlin = _cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        _multiChannelPerlin.m_AmplitudeGain = 0f;
    }

    private void Update() {
        if (_cinemachineVirtualCamera.Follow == null) {
            _cinemachineVirtualCamera.Follow = GameObject.Find("PlayerCameraTarget").transform;
        }

        if (_cinemachineVirtualCamera.LookAt == null) {
            _cinemachineVirtualCamera.LookAt = GameObject.Find("PlayerCameraTarget").transform;
        }

        if (_shakeTime > 0) {
            _shakeTime -= Time.deltaTime;

            if (_shakeTime <= 0) {
                _multiChannelPerlin.m_AmplitudeGain = 0f;
            }
        }
    }

    public void ShakeCamera(float shakeIntensity, float shakeTime) {
        _multiChannelPerlin = _cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        _shakeTime = shakeTime;
        _multiChannelPerlin.m_AmplitudeGain = shakeIntensity;
    }
}

