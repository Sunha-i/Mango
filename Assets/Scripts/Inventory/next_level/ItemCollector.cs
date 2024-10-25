using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemCollector : MonoBehaviour
{
    // 주워야 하는 특정 아이템 리스트
    public List<string> requiredItems = new List<string>();

    // 전환할 씬 이름 (Inspector에서 설정 가능)
    public string sceneToLoad;

    // 문 클릭 시 호출되는 메서드
    public void OnDoorClicked()
    {
        // 모든 필요한 아이템을 줍었는지 확인
        bool allItemsCollected = true;

        List<Item> playerItems = InventoryManager.Instance.Items;

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

        if (allItemsCollected)
        {
            if (!string.IsNullOrEmpty(sceneToLoad))
            {
                SceneManager.LoadScene(sceneToLoad);  // Load the specified scene from the Inspector
            }
            else
            {
                Debug.LogError("Scene name to load is not set.");
            }
        }
        else
        {
            Debug.Log("You have not collected all the required items yet.");
        }
    }
}
