using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoToggleController : MonoBehaviour
{
    public VideoPlayer player;
    public Toggle videoToggle;
    public Text toggleLabelText; // Toggle > Label

    void Start()
    {
        videoToggle.onValueChanged.AddListener(OnToggleChanged);
        UpdateToggleLabel(videoToggle.isOn); // 초기 상태 반영
    }

    void OnToggleChanged(bool isOn)
    {
        if (isOn)
        {
            player.Pause();
        }
        else
        {
            player.Play();
        }

        UpdateToggleLabel(isOn);
    }

    void UpdateToggleLabel(bool isOn)
    {
        toggleLabelText.text = isOn ? "재생" : "일시정지";
    }
}