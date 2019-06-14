using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SpellBook : MonoBehaviour
{
    private static SpellBook instance;

    public static SpellBook MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SpellBook>();
            }

            return instance;
        }
    }

    [SerializeField]
    private Spell[] spells = null;

    [SerializeField]
    private Image castingBar = null;

    [SerializeField]
    private Text currentSpell = null;

    [SerializeField]
    private Image icon = null;

    [SerializeField]
    private Text castTime = null;

    [SerializeField]
    private CanvasGroup canvasGroup = null;

    private Coroutine spellRoutine = null;

    private Coroutine fadeRoutine = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Spell CastSpell(string spellName)
    {
        Spell spell = Array.Find(spells, x => x.MyName == spellName);

        castingBar.fillAmount = 0.0f;

        castingBar.color = spell.MyBarColor;

        currentSpell.text = spell.MyName;

        castTime.text = spell.MyCastTime.ToString("F2");

        icon.sprite = spell.MyIcon;

        spellRoutine = StartCoroutine(Progress(spell));

        fadeRoutine = StartCoroutine(FadeBar());

        return spell;
    }

    private IEnumerator Progress(Spell spell)
    {
        float timePassed = Time.deltaTime;

        float rate = 1.0f / spell.MyCastTime;

        float progress = 0.0f;

        while (progress <= 1.0f)
        {
            castingBar.fillAmount = Mathf.Lerp(0, 1, progress);

            progress += rate * Time.deltaTime;

            timePassed += Time.deltaTime;

            castTime.text = (spell.MyCastTime - timePassed).ToString("F2");

            if (spell.MyCastTime - timePassed <= 0)
            {
                castTime.text = "0.00";
            }

            yield return null;
        }

        StopCasting();
    }

    private IEnumerator FadeBar()
    {
        float rate = 1.0f / 0.5f;

        float progress = 0.0f;

        while (progress <= 1.0f)
        {
            canvasGroup.alpha = Mathf.Lerp(0, 1, progress);

            progress += rate * Time.deltaTime;

            yield return null;
        }
    }

    public void StopCasting()
    {
        if (fadeRoutine != null)
        {
            StopCoroutine(fadeRoutine);
            canvasGroup.alpha = 0;
        }

        if (spellRoutine != null)
        {
            StopCoroutine(spellRoutine);
            spellRoutine = null;
        }
    }

    public Spell GetSpell(string spellName)
    {
        return Array.Find(spells, x => x.MyName == spellName);
    }
} 
