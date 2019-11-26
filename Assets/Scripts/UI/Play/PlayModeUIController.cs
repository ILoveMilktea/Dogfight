using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayModeUIController : MonoBehaviour, IPlayModeUIController
{
    private Dictionary<GameObject, CharacterUI> characterUIs = new Dictionary<GameObject, CharacterUI>();
    private OnOffSwitch onOffSwitch;

    // UI onoff
    public void HidePlayUI()
    { }
    public void DisplayPlayUI()
    { }
    // pause
    public void PauseGame()
    { }
    public void ReleaseGame()
    { }
    // hp
    public void DamageToCharacter(GameObject character, int value)
    { }
    public void HealCharacter(GameObject character, int value)
    { }
    public void OffCharacterUI(GameObject character) // 죽었을 때
    { }
}
