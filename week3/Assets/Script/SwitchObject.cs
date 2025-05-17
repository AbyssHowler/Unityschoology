using UnityEngine;

public class SwitchObject : MonoBehaviour
{
    public DoorObject door;
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
            playerHand.SetText("조사하기\n(Trigger)");
        }
    }

    void OnTriggerStay(Collider coll)
    {
        if (coll.CompareTag("Hand"))
        {
            if (playerHand.isTrigger == true)
            {
                playerHand.SetText("스위치가 작동했다.");
                door.doorSwitch = true;
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
