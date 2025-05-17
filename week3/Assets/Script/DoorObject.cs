using UnityEngine;
using System.Collections;

public class DoorObject : MonoBehaviour
{
    public bool doorSwitch;
    public Transform targetObject; // �̵��� ��� ��ü

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
            playerHand.SetText("�����ϱ�\n(Trigger)");
        }
    }

    void OnTriggerStay(Collider coll)
    {
        if (coll.CompareTag("Hand") && playerHand.isTrigger)
        {
            if (doorSwitch)
            {
                doorAni.Play();
                StartCoroutine(DelayedMove()); // 0.5�� �� �̵� ����
            }
            else
            {
                playerHand.SetText("���� ��� �ִ�.");
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
        yield return new WaitForSeconds(0.5f); // �ִϸ��̼� �� 0.5�� ���

        if (targetObject != null)
        {
            Vector3 newPosition = targetObject.position + targetObject.forward * 10f; // ������ 10��ŭ �̵�
            yield return StartCoroutine(MoveObject(targetObject, newPosition, 2f)); // 2�� ���� �ε巴�� �̵�
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

        obj.position = targetPos; // ���� ��ġ ����
    }
}
