using UnityEngine;
using UnityEngine.UI;

public class DangerObject : MonoBehaviour
{
    HandController playerHand;
   // UI �̹��� ����
    public AudioClip dangerSound; // ���� Ŭ�� ����
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
            playerHand.SetText("���� �����Ұ� ����...\n�����ϱ�\n(Trigger)");




        }
    }

    void OnTriggerStay(Collider coll)
    {
        if (coll.CompareTag("Hand") && playerHand.isTrigger)
        {
           
            audioSource.PlayOneShot(dangerSound);
            //2���� �̹��� �����
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
