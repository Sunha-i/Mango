using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonColorChange : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // ��ư �ؽ�Ʈ�� ���� ����
    public Text buttonText; // �Ǵ� TextMeshProUGUI�� ���� ����
    private Color originalColor;

void Start()
    {
        // ���� �ؽ�Ʈ ������ ����
        originalColor = buttonText.color;
    }

    // ���콺�� ��ư ���� �ö�� �� ȣ��Ǵ� �Լ�
    public void OnPointerEnter(PointerEventData eventData)
    {
        // �ؽ�Ʈ ���� ���������� ����
        buttonText.color = Color.red;
    }

    // ���콺�� ��ư���� ��� �� ȣ��Ǵ� �Լ�
    public void OnPointerExit(PointerEventData eventData)
    {
        // �ؽ�Ʈ ������ ���� �������� �ǵ���
        buttonText.color = originalColor;
    }

}