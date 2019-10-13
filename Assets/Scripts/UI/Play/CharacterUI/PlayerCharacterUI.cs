using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCharacterUI : CharacterUI
{
    private Slider reloadGauge;
    void Awake()
    {
        characterName = GetComponentInChildren<Text>();
        Slider[] sliders = GetComponentsInChildren<Slider>();
        characterHp = sliders[0];
        reloadGauge = sliders[1];

        rectTransform = GetComponent<RectTransform>();
    }

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    // Late 안쓰면 ui가 먼저 가버려서 떨림 현상 생김
    void LateUpdate()
    {
        base.LateUpdate();
    }

    protected override void ResizeUI()
    {
        Vector3 targetPos = target.transform.position;
        Vector3 targetRightHandPos = target.transform.position + new Vector3(target.transform.lossyScale.x, 0, 0); ;

        targetPos = Camera.main.WorldToScreenPoint(targetPos);
        targetRightHandPos = Camera.main.WorldToScreenPoint(targetRightHandPos);

        float characterWidth = (targetRightHandPos.x - targetPos.x);

        // resize name
        RectTransform nameRT = characterName.rectTransform;
        float nameWidth = nameRT.sizeDelta.x;
        float sizeRatio = characterWidth / nameWidth;
        nameRT.sizeDelta = new Vector2(nameRT.sizeDelta.x * sizeRatio, nameRT.sizeDelta.y);
        // resize Hp
        RectTransform hpRT = characterHp.GetComponent<RectTransform>();
        float HpWidth = hpRT.sizeDelta.x;
        sizeRatio = characterWidth / HpWidth;
        hpRT.sizeDelta = new Vector2(hpRT.sizeDelta.x * sizeRatio, hpRT.sizeDelta.y);
        // resize reloadGauge
        RectTransform reloadRT = reloadGauge.GetComponent<RectTransform>();
        float reloadWidth = reloadRT.sizeDelta.x;
        sizeRatio = characterWidth / reloadWidth;
        reloadRT.sizeDelta = new Vector2(reloadRT.sizeDelta.x * sizeRatio, reloadRT.sizeDelta.y);
    }
}
