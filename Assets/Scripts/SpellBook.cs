using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SpellBook : MonoBehaviour
{
    [SerializeField]
    private Spell[] spells;

    [SerializeField]
    private Image castingBar;

    [SerializeField]
    private Text spellName;

    [SerializeField]
    private Image icon;

    private Coroutine spellRoutine;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Spell CastSpell(int index)
    {
        castingBar.color = spells[index].MyBarColor;
        castingBar.fillAmount = 0.5f;

        spellName.text = spells[index].MyName;

        icon.sprite = spells[index].MyIcon;

        spellRoutine = StartCoroutine(Progress(index));

        return spells[index];
    }

    private IEnumerator Progress(int index)
    {
        float rate = 1.0f / spells[index].MyCastTime;

        float progress = 0.0f;

        while (progress <= 1.0f)
        {
            castingBar.fillAmount = Mathf.Lerp(0, 1, progress);

            progress += rate * Time.deltaTime;

            yield return null;
        }

        StopCasting();
    }

    public void StopCasting()
    {
        if (spellRoutine != null)
        {
            StopCoroutine(spellRoutine);
            spellRoutine = null;
        }
    }
} 
