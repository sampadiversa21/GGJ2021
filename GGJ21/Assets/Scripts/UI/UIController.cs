using DG.Tweening;
using Platformer.Mechanics;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public CanvasGroup creditsCG;
    public CanvasGroup buttonsCG;

    public void OnCreditsClicked()
    {
        creditsCG.DOFade(1, 0.5f).OnComplete(() =>
        {
            creditsCG.blocksRaycasts = true;
        });

        buttonsCG.blocksRaycasts = false;
        buttonsCG.DOFade(0, 0.5f);
    }

    public void OnCloseCredits()
    {
        creditsCG.blocksRaycasts = false;
        creditsCG.DOFade(0, 0.5f);

        buttonsCG.DOFade(1, 0.5f).OnComplete(() => 
        {
            buttonsCG.blocksRaycasts = true;
        });
    }

    public void OnPlayClicked()
    {
        GameController.Instance.StopCinematic1();
        
        buttonsCG.DOFade(0, 0.5f).OnComplete(() =>
        {
            buttonsCG.blocksRaycasts = false;
        });
    }
}
