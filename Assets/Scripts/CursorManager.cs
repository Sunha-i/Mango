using System.Runtime.InteropServices;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [DllImport("user32.dll")]
    private static extern bool SetCursorPos(int x, int y);

    public static bool isPaused = false;
    private static bool isExamining = false;

    private void Start()
    {
        CenterCursor();
    }

    public static void CenterCursor()
    {
        int centerX = Screen.width / 2;
        int centerY = Screen.height / 2;

        SetCursorPos(centerX, centerY);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

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
