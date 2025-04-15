using System;
using Script.Player;
using TMPro;
using UnityEngine;

public class UICntManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreTxt;
    [SerializeField] private Player player;

    private void Update()
    {
        scoreTxt.text = $"Count = {player.cnt.ToString()}";
    }
}
