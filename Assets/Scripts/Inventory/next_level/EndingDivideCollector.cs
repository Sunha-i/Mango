using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingDivideCollector : MonoBehaviour
{
    // �ݵ�� ���ԵǾ�� �ϴ� Ư�� ������ (��: Ư�� ������Ʈ�� �̸�)
    public string requiredSpecificItem;

    // ��� �������� �ݱ� ���� ������ ����Ʈ
    public List<string> requiredItems = new List<string>();

    // ���� �� �̸� (Inspector���� ���� ����)
    public string trueEndingScene;
    public string happyEndingScene;
    public string badEndingScene;

    // �� Ŭ�� �� ȣ��Ǵ� �޼���
    public void OnDoorClicked()
    {
        bool allItemsCollected = true;
        bool specificItemCollected = false;

        List<Item> playerItems = InventoryManager.Instance.Items;

        // Ư�� �������� �����Ǿ����� Ȯ��
        foreach (Item playerItem in playerItems)
        {
            if (playerItem.itemName == requiredSpecificItem)
            {
                specificItemCollected = true;
                break;
            }
        }

        // ��� �ʿ��� �������� �ݾ����� Ȯ��
        foreach (string requiredItem in requiredItems)
        {
            bool itemFound = false;

            // InventoryManager�� ������ ��Ͽ��� �ش� �������� �ִ��� Ȯ��
            foreach (Item playerItem in playerItems)
            {
                if (playerItem.itemName == requiredItem)
                {
                    itemFound = true;
                    break;
                }
            }

            // �ʿ��� �������� �ϳ��� ������ ����
            if (!itemFound)
            {
                allItemsCollected = false;
                break;
            }
        }

        // ���� �б� ó��
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
