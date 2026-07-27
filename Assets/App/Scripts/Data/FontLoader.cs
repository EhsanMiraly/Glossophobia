using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FontLoader : MonoBehaviour
{
    public List<Font> fonts;

    [NonSerialized] public List<Language> languages;


    private void Awake()
    {
        fonts = new List<Font>()
        {
            Resources.Load<Font>("Fonts/English/Roboto-Medium"),
            Resources.Load<Font>("Fonts/Farsi/Parastoo-Bold")
        };
        LanguageTextsData.fonts = fonts;

        languages = new List<Language>
        {
            new Language("Language: English", LanguageDirection.LTR, fonts[0]),
            new Language("زبان: فارسی", LanguageDirection.RTL,fonts[1])
        };
        LanguageTextsData.languages = languages;
    }

    private void OnDisable()
    {
        fonts = null;

        languages = null;
    }


}
