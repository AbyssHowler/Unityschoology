using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class ChangeSc : MonoBehaviour
{
   
    public VideoClip[] VideoSrc = new VideoClip[2];
    public VideoPlayer player;
    public Toggle videoToggle;
    public Text toggleLabelText; // Toggle > Label
    int videoIndex;

    // Start is called before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GetComponent<VideoPlayer>();
        videoIndex = 0;
        videoToggle.onValueChanged.AddListener(OnToggleChanged);
        UpdateToggleLabel(videoToggle.isOn); // 초기 상태 반영
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ChangeVideo()
    {
        videoIndex++;
        if (videoIndex > 1)
        {
            videoIndex = 0;
        }
        videoToggle.isOn = false;
        player.clip = VideoSrc[videoIndex];
    }
    public void OnToggleChanged(bool isOn)
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

    public void UpdateToggleLabel(bool isOn)
    {
        toggleLabelText.text = isOn ? "pause" : "Play";
    }
}

