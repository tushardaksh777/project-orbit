using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CardView : MonoBehaviour
{
    public GameObject cardObject;
    public int cardId = 0;
    public Image realImage;

    public Animator animator;
    public Button cardButton;

    private void Awake()
    {
        cardButton.onClick.AddListener(()=> OnCardClicked());
    }

    public void FlipToReal()
    {
        animator.SetTrigger("FlipToReal");
        cardButton.interactable = false;
    }
    public void FlipToFake()
    {
        animator.SetTrigger("FlipToFake");
        cardButton.interactable = true;
    }

    public void WaitForFake(float duration)
    {
        FlipToReal();
        AudioManager.Instance.PlayCardFlipFx();
        StartCoroutine(FlipToFakeWithDuration(duration));
    }

    IEnumerator FlipToFakeWithDuration(float duration)
    {
        yield return new WaitForSeconds(duration);
        FlipToFake();

    }
    protected void OnCardClicked()
    {
        AudioManager.Instance.PlayCardFlipFx();
        GameManager.Instance.onCardSelected(this);
    }
    public void ResetObject()
    {
        Color white = Color.white;
        white.a = 1f;
        realImage.color = white;
        cardButton.interactable = true;
    }
    public void DisableObject()
    {
        Color white = Color.white;
        white.a = 0.5f;
        realImage.color = white;
        cardButton.interactable = false;
    }
}
