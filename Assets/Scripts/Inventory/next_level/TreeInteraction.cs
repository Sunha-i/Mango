using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeInteraction : MonoBehaviour
{
    public EndingDivideCollector itemCollector;  // 플레이어의 EndingDivideCollector 스크립트 참조

    private void OnMouseDown()
    {
        // 플레이어의 EndingDivideCollector 스크립트에서 문 클릭 메서드 호출
        itemCollector.OnDoorClicked();
    }
}
