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
}
