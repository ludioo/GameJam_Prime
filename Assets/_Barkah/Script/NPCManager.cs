using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCManager : MonoBehaviour
{
    public Image image; // The Image component you want to change the color of
    public Button button;
    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() => {
            GameplayManager.Instance.ChooseSuspect(GameplayManager.Instance.onGameplayNPC.IndexOf(this.gameObject));
        });
    }
}
