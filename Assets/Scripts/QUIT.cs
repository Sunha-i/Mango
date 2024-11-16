using UnityEngine;

public class QuitGame : MonoBehaviour
{
    public void ExitGame()
    {
        // 유니티 에디터에서 실행 중일 때는 로그 메시지만 표시
        #if UNITY_EDITOR
        Debug.Log("게임이 종료됩니다.");
        UnityEditor.EditorApplication.isPlaying = false; // 유니티 에디터 종료
        #else
        Application.Quit(); // 빌드된 게임 종료
        #endif
    }
}
