using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Constant
{
    public static readonly string GAME_CODE = "ab001";
    public enum SceneNumber {
        INIT = 0,
        MAIN_MENU,
        GAME,
        PROLOGUE,
        PROFILE,
        ENDING,
        RANK_BOARD
    }

    public static string[] LEVEL = { "EASY", "NORMAL", "HARD", "CRAZY" };
    public static Color[] COLOR = { Color.yellow, Color.green, new Color(0.84f, 0.3f, 0.9f), new Color(0.88f, 0.03f, 0.35f) };
}
