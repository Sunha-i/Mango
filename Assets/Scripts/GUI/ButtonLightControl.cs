using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonLightControl : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Light candleLight; // 불빛 오브젝트 참조를 위한 변수

// 마우스가 버튼 위에 올라올 때 호출
public void OnPointerEnter(PointerEventData eventData)
    {
        // 불빛 켬
        candleLight.enabled = true;
    }

    // 마우스가 버튼에서 벗어날 때 호출
    public void OnPointerExit(PointerEventData eventData)
    {
        // 불빛 끔
        candleLight.enabled = false;
    }

}