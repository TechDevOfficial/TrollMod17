using TrollMod17.Roles;
using MiraAPI.GameEnd;
using MiraAPI.Utilities;
using UnityEngine;

namespace TrollMod17.GameOver;

public class TrollGameOver : CustomGameOver
{
    public override bool VerifyCondition(PlayerControl playerControl, NetworkedPlayerInfo[] winners)
    {
        return winners is [{ Role: TrollRole }];
    }

    public override void AfterEndGameSetup(EndGameManager endGameManager)
    {
        endGameManager.WinText.text = "Troll Wins!";
        endGameManager.WinText.color = new Color32(128, 0, 128, 255);
        endGameManager.BackgroundBar.material.SetColor("_Color", new Color32(128, 0, 128, 255));
    }
}