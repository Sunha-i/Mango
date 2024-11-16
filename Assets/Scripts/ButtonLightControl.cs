using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonLightControl : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Light candleLight; // �Һ� ������Ʈ ������ ���� ����

// ���콺�� ��ư ���� �ö�� �� ȣ��
public void OnPointerEnter(PointerEventData eventData)
    {
        // �Һ� ��
        candleLight.enabled = true;
    }

    // ���콺�� ��ư���� ��� �� ȣ��
    public void OnPointerExit(PointerEventData eventData)
    {
        // �Һ� ��
        candleLight.enabled = false;
    }

}