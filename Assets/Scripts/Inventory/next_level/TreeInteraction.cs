using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeInteraction : MonoBehaviour
{
    public EndingDivideCollector itemCollector;  // �÷��̾��� EndingDivideCollector ��ũ��Ʈ ����

    private void OnMouseDown()
    {
        // �÷��̾��� EndingDivideCollector ��ũ��Ʈ���� �� Ŭ�� �޼��� ȣ��
        itemCollector.OnDoorClicked();
    }
}
