using UnityEngine;
using UnityEngine.UI;

public class EndingSlides2 : MonoBehaviour
{
    public Image slideDisplay;       // 슬라이드를 표시할 UI Image 컴포넌트
    public Sprite[] slides;          // 슬라이드 이미지 배열
    public Button previousButton;    // 이전 슬라이드 버튼
    public Button nextButton;        // 다음 슬라이드 버튼
    public Button quitButton;        // Quit 버튼

    private int currentSlideIndex = 0;  // 현재 슬라이드의 인덱스

    void Start()
    {
        // 엔딩 슬라이드가 시작될 때 마우스 클릭 활성화
        CursorManager.SetPauseState(true);

        ShowSlide(currentSlideIndex);  // 첫 번째 슬라이드를 표시
        UpdateButtonVisibility();      // 버튼 상태 업데이트
    }

    private void OnDisable()
    {
        // 엔딩 슬라이드가 끝났을 때 마우스 클릭 비활성화 (예: 다른 UI나 게임 플레이로 돌아갈 때)
        CursorManager.SetPauseState(false);
    }

    public void ShowNextSlide()
    {
        if (currentSlideIndex < slides.Length - 1)  // 마지막 슬라이드가 아니라면
        {
            currentSlideIndex++;       // 인덱스를 증가시켜 다음 슬라이드로 이동
            ShowSlide(currentSlideIndex);
            UpdateButtonVisibility();  // 버튼 상태 업데이트
        }
    }

    public void ShowPreviousSlide()
    {
        if (currentSlideIndex > 0)  // 첫 번째 슬라이드가 아니라면
        {
            currentSlideIndex--;       // 인덱스를 감소시켜 이전 슬라이드로 이동
            ShowSlide(currentSlideIndex);
            UpdateButtonVisibility();  // 버튼 상태 업데이트
        }
    }

    private void ShowSlide(int index)
    {
        slideDisplay.sprite = slides[index];  // slideDisplay에 현재 슬라이드 이미지를 설정
    }

    private void UpdateButtonVisibility()
    {
        // 첫 번째 슬라이드에서는 Previous 버튼을 숨기고, 그렇지 않으면 표시
        previousButton.gameObject.SetActive(currentSlideIndex > 0);

        // 마지막 슬라이드에서는 Next 버튼을 숨기고, 그렇지 않으면 표시
        nextButton.gameObject.SetActive(currentSlideIndex < slides.Length - 1);

        // 마지막 슬라이드에서만 Quit 버튼 표시
        quitButton.gameObject.SetActive(currentSlideIndex == slides.Length - 1);
    }
}
