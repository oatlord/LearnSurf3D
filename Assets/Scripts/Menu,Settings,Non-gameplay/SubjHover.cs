using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class HoverHighlight : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Sprite normalSprite;
    public Sprite highlightedSprite;

    public string sceneToLoad;

    public Animator textAnimator;

    private Image image;

    void Awake()
    {
        image = GetComponent<Image>();
        image.sprite = normalSprite;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Hovered!");
        image.sprite = highlightedSprite;
        if (textAnimator != null)
            textAnimator.ResetTrigger("Hover");
            textAnimator.SetTrigger("Hover");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        image.sprite = normalSprite;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            Debug.LogWarning("⚠️ No scene name set in " + gameObject.name);
        }
    }
    

}
