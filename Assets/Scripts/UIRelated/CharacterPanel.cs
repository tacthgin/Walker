using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPanel : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup canvasGroup = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenClose()
    {
        canvasGroup.alpha = canvasGroup.alpha > 0 ? 0 : 1;

        canvasGroup.blocksRaycasts = !canvasGroup.blocksRaycasts;
    }
}
