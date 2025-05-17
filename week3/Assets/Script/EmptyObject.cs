using UnityEngine;

public class EmptyObject : MonoBehaviour
{
    HandController playerHand;

    // Start is called before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.CompareTag("Hand"))
        {
            playerHand = coll.gameObject.GetComponent<HandController>();
            playerHand.SetText("�����ϱ�\n(Trigger)");
        }
    }

    void OnTriggerStay(Collider coll)
    {
        if (coll.CompareTag("Hand"))
        {
            if (playerHand.isTrigger == true)
            {
                playerHand.SetText("�ƹ��͵� ����.");
            }
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
