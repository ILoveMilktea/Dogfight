using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterUI : MonoBehaviour
{
    protected GameObject floatingText;
    private int xTextStack;
    private int yTextStack;

    protected Text characterName;
    protected Slider characterHp;

    [SerializeField]
    protected Transform target;
    protected RectTransform rectTransform;

    protected virtual void Awake()
    {
        floatingText = Resources.Load("Prefab/UI/FloatingText") as GameObject;

        characterName = GetComponentInChildren<Text>();
        characterHp = GetComponentInChildren<Slider>();

        rectTransform = GetComponent<RectTransform>();
    }

    // Start is called before the first frame update
    protected void Start()
    {
        xTextStack = 0;
        yTextStack = 0;
    }

    // Update is called once per frame
    // Late 안쓰면 ui가 먼저 가버려서 떨림 현상 생김
    protected void LateUpdate()
    {
        ChaseTarget();
        
    }

    public virtual void ResizeUI()
    {
        Vector3 targetPos = target.transform.position;
        Vector3 targetRightHandPos = target.transform.position + new Vector3(target.transform.lossyScale.x, 0, 0); ;

        targetPos = Camera.main.WorldToScreenPoint(targetPos);
        targetRightHandPos = Camera.main.WorldToScreenPoint(targetRightHandPos);

        float characterWidth = (targetRightHandPos.x - targetPos.x);

        // resize name
        //RectTransform nameRT = characterName.rectTransform;
        //float nameWidth = nameRT.sizeDelta.x;
        //float sizeRatio = characterWidth / nameWidth;
        //nameRT.sizeDelta = new Vector2(nameRT.sizeDelta.x * sizeRatio, nameRT.sizeDelta.y);
        // resize Hp
        RectTransform hpRT = characterHp.GetComponent<RectTransform>();
        float HpWidth = hpRT.sizeDelta.x;
        float sizeRatio = characterWidth / HpWidth;
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
        characterHp.value = maxHp;
    }
    public int GetRemainHp()
    {
        return (int)characterHp.value;
    }

    public void HpUp(int value)
    {
        characterHp.value += value;
        DisplayDamage(value);
    }

    public void HpDown(int value)
    {
        if(characterHp.value < value)
        {
            characterHp.value = 0;
        }
        else
        {
            Debug.Log(gameObject.name + "hit");
            characterHp.value -= value;
        }
        
        DisplayDamage(value);
    }

    private void DisplayDamage(int value)
    {
        GameObject damageText = Instantiate(floatingText, transform) as GameObject;
        //yTextStack++;
        //if (yTextStack > 3)
        //{
        //    xTextStack++;
        //    yTextStack = 1;
        //}

        // stack으로 약간 뎀지 입으면 순서대로 뜨게 좀 하고싶은데
        StartCoroutine(damageText.GetComponent<FloatingText>().DisplayDamage(value, rectTransform, RemoveFloatingText));
    }

    public void RemoveFloatingText()
    {
        //yTextStack--;
        //if(yTextStack < 1)
        //{
        //    xTextStack--;
        //    yTextStack = 3;
        //}
    }
}
