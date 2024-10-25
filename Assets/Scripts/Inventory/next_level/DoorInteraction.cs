using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteraction : MonoBehaviour
{
    public ItemCollector itemCollector;  // 플레이어의 아이템 수집기 스크립트 참조

    private void OnMouseDown()
    {
        // 플레이어의 아이템 수집기 스크립트에서 문 클릭 메서드 호출
        itemCollector.OnDoorClicked();
    }
}
