using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningSkill : MonoBehaviour
{
    private List<Lightning> lightnings = new List<Lightning>();
    public int level = 0;

    private WaitForSeconds skillCoolTime;
    [SerializeField]
    private int targetNum = 0;

    private void Awake()
    {
        skillCoolTime = new WaitForSeconds(5f);

        GameObject prefab = Resources.Load<GameObject>("Prefabs/SkillEffect/Lightning");

        for (int i = 0; i < 5; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false);
            lightnings.Add(obj.GetComponent<Lightning>());
        }
    }

    private void Start()
    {
        StartCoroutine(Lightning());
    }

    private IEnumerator Lightning()
    {
        while (true)
        {
            if (level > 0)
            {
                StartCoroutine(LightningChain());
                SoundManager.instance.PlayFX(SoundKey.LIGHTNING);
            }  
            yield return skillCoolTime;
        }
    }

    private IEnumerator LightningChain()
    {
        targetNum = level;
        List<Transform> targets = EnemyManager.instance.GetClosestEnemys(
            transform.position, targetNum);

        Transform start = transform;
        Transform end;

        for (int i = 0; i < targets.Count; i++)
        {
            end = targets[i];

            lightnings[i].StartLightining(start, end);
            end.gameObject.GetComponent<EnemyHit>().Damaged(5);

            yield return new WaitForSeconds(0.01f);

            start = targets[i];
        }
    }

    public void GetPower()
    {
        if (level > 5) return;

        level++;
    }
}
