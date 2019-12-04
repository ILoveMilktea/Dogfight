using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TrajectoryLine : MonoBehaviour
{
    public Color enemyColor = Color.red;
    public Color playerColor = Color.green;

    public LineRenderer line;

    public float lineWidth = 0.5f;

    public IEnumerator DrawTrajectoryWhileTime(GameObject source, GameObject target, float displayTime)
    {
        SetColor(source.tag);

        float timer = 0;
        while(timer < displayTime)
        {
            DrawLine(source.transform.position, target.transform.position);
            yield return new WaitForEndOfFrame();
            timer += Time.deltaTime;
        }

        RemoveLine();
    }

    public IEnumerator DrawTrajectoryWhileInterrupt(GameObject source, GameObject target)
    {
        SetColor(source.tag);

        while (FightSceneController.Instance.GetCurrentFightState() != FightState.Dead
            || FightSceneController.Instance.GetCurrentFightState() != FightState.End)
        {
            DrawLine(source.transform.position, target.transform.position);
            yield return new WaitForEndOfFrame();
        }

        RemoveLine();
    }

    private void DrawLine(Vector3 sourcePos, Vector3 targetPos)
    {
        sourcePos.y = 0.05f;
        targetPos.y = 0.05f;
        
        line.SetPosition(0, sourcePos);
        line.SetPosition(1, targetPos);

        line.startWidth = lineWidth;
        line.endWidth = lineWidth;
    }

    public void DrawLineUntilRange(Vector3 sourcePos, Vector3 direction, float attackRange)
    {
        sourcePos.y = 0.05f;

        direction *= attackRange;
        direction.y = 0.05f;
        
        line.SetPosition(0, sourcePos);
        line.SetPosition(1, sourcePos + direction);

        line.startWidth = lineWidth;
        line.endWidth = lineWidth;
    }

    public void RemoveLine()
    {
        StopAllCoroutines();
        ObjectPoolManager.Instance.Free(gameObject);
    }

    public void SetColor(string sourceTag)
    {
        if(sourceTag == "Player")
        {
            line.startColor = playerColor;
            line.endColor = playerColor;
            //line.material.color = playerColor;
        }
        else if(sourceTag == "Enemy")
        {
            line.startColor = enemyColor;
            line.endColor = enemyColor;
            //line.material.color = enemyColor;
        }
    }
}
