using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonColorChange : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // 버튼 텍스트를 위한 변수
    public Text buttonText; // 또는 TextMeshProUGUI로 변경 가능
    private Color originalColor;

void Start()
    {
        // 원래 텍스트 색상을 저장
        originalColor = buttonText.color;
    }

    // 마우스가 버튼 위에 올라올 때 호출되는 함수
    public void OnPointerEnter(PointerEventData eventData)
    {
        // 텍스트 색상 빨간색으로 변경
        buttonText.color = Color.red;
    }

    // 마우스가 버튼에서 벗어날 때 호출되는 함수
    public void OnPointerExit(PointerEventData eventData)
    {
        // 텍스트 색상을 원래 색상으로 되돌림
        buttonText.color = originalColor;
    }

}