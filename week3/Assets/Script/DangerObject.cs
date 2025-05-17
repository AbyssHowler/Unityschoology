using UnityEngine;
using UnityEngine.UI;

public class DangerObject : MonoBehaviour
{
    HandController playerHand;
   // UI 이미지 참조
    public AudioClip dangerSound; // 사운드 클립 참조
    private AudioSource audioSource;

    void Start()
    {
      

        audioSource = gameObject.AddComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.CompareTag("Hand"))
        {
            playerHand = coll.gameObject.GetComponent<HandController>();
            playerHand.SetText("왠지 위험할것 같다...\n조사하기\n(Trigger)");




        }
    }

    void OnTriggerStay(Collider coll)
    {
        if (coll.CompareTag("Hand") && playerHand.isTrigger)
        {
           
            audioSource.PlayOneShot(dangerSound);
            //2초후 이미지 지우기
        }

    }


    void OnTriggerExit(Collider coll)
    {
        if (coll.CompareTag("Hand"))
        {
            playerHand.SetText("");
            playerHand = null;

           
        }
    }
}
