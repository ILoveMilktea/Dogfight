using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterUI : MonoBehaviour
{
    protected Text characterName;
    protected Slider characterHp;

    [SerializeField]
    protected Transform target;
    protected RectTransform rectTransform;

    protected virtual void Awake()
    {
        characterName = GetComponentInChildren<Text>();
        characterHp = GetComponentInChildren<Slider>();

        rectTransform = GetComponent<RectTransform>();
    }

    // Start is called before the first frame update
    protected void Start()
    {
        //target = FindObjectOfType<Player>().transform;
        characterName.text = target.gameObject.name; // 임시
        ResizeUI();
    }

    // Update is called once per frame
    // Late 안쓰면 ui가 먼저 가버려서 떨림 현상 생김
    protected void LateUpdate()
    {
        ChaseTarget();
        
    }

    protected virtual void ResizeUI()
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

    }
    private void ChaseTarget()
    {
        Vector3 UIWorldPos = target.transform.position + new Vector3(0, target.transform.lossyScale.y, 0);
        Vector3 UIScreenPos = Camera.main.WorldToScreenPoint(UIWorldPos);

        rectTransform.anchoredPosition = new Vector2(UIScreenPos.x, UIScreenPos.y);
    }

    public void SetName(string name)
    {
        characterName.text = name;
    }

    public void SetTarget(GameObject targetObj)
    {
        target = targetObj.transform;
    }

    public void SetMaxHp(int maxHp)
    {
        characterHp.maxValue = maxHp;
    }
    public int GetRemainHp()
    {
        return (int)characterHp.value;
    }

    public void HpUp(int value)
    {
        characterHp.value += value;
    }

    public void HpDown(int value)
    {
        characterHp.value -= value;
    }
}
