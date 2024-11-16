using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingDivideCollector : MonoBehaviour
{
    // 반드시 포함되어야 하는 특정 아이템 (예: 특정 오브젝트의 이름)
    public string requiredSpecificItem;

    // 모든 아이템을 줍기 위한 아이템 리스트
    public List<string> requiredItems = new List<string>();

    // 엔딩 씬 이름 (Inspector에서 설정 가능)
    public string trueEndingScene;
    public string happyEndingScene;
    public string badEndingScene;

    // 문 클릭 시 호출되는 메서드
    public void OnDoorClicked()
    {
        bool allItemsCollected = true;
        bool specificItemCollected = false;

        List<Item> playerItems = InventoryManager.Instance.Items;

        // 특정 아이템이 수집되었는지 확인
        foreach (Item playerItem in playerItems)
        {
            if (playerItem.itemName == requiredSpecificItem)
            {
                specificItemCollected = true;
                break;
            }
        }

        // 모든 필요한 아이템을 줍었는지 확인
        foreach (string requiredItem in requiredItems)
        {
            bool itemFound = false;

            // InventoryManager의 아이템 목록에서 해당 아이템이 있는지 확인
            foreach (Item playerItem in playerItems)
            {
                if (playerItem.itemName == requiredItem)
                {
                    itemFound = true;
                    break;
                }
            }

            // 필요한 아이템이 하나라도 없으면 실패
            if (!itemFound)
            {
                allItemsCollected = false;
                break;
            }
        }

        // 엔딩 분기 처리
        if (specificItemCollected)
        {
            if (allItemsCollected)
            {
                // True Ending
                SceneManager.LoadScene(trueEndingScene);
            }
            else
            {
                // Happy Ending
                SceneManager.LoadScene(happyEndingScene);
            }
        }
        else
        {
            // Bad Ending
            SceneManager.LoadScene(badEndingScene);
        }
    }
}
