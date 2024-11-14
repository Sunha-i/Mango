using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public static bool isPaused = false;
    private static bool isExamining = false;

    public static void SetPauseState(bool paused)
    {
        isPaused = paused;
        UpdateCursorState();
    }

    public static void SetExaminationState(bool examining)
    {
        isExamining = examining;
        UpdateCursorState();
    }

    private static void UpdateCursorState()
    {
        if (isPaused)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else if (isExamining)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
