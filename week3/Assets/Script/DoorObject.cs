using UnityEngine;
using System.Collections;

public class DoorObject : MonoBehaviour
{
    public bool doorSwitch;
    public Transform targetObject; // 이동할 대상 물체

    HandController playerHand;
    Animation doorAni;

    void Start()
    {
        doorSwitch = false;
        doorAni = GetComponent<Animation>();
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
        if (coll.CompareTag("Hand") && playerHand.isTrigger)
        {
            if (doorSwitch)
            {
                doorAni.Play();
                StartCoroutine(DelayedMove()); // 0.5초 후 이동 실행
            }
            else
            {
                playerHand.SetText("문이 잠겨 있다.");
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

    IEnumerator DelayedMove()
    {
        yield return new WaitForSeconds(0.5f); // 애니메이션 후 0.5초 대기

        if (targetObject != null)
        {
            Vector3 newPosition = targetObject.position + targetObject.forward * 10f; // 앞으로 10만큼 이동
            yield return StartCoroutine(MoveObject(targetObject, newPosition, 2f)); // 2초 동안 부드럽게 이동
        }
    }

    IEnumerator MoveObject(Transform obj, Vector3 targetPos, float duration)
    {
        float time = 0f;
        Vector3 startPos = obj.position;

        while (time < duration)
        {
            obj.position = Vector3.Lerp(startPos, targetPos, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        obj.position = targetPos; // 최종 위치 보정
    }
}
