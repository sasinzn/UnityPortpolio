using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dying : MonoBehaviour
{
    public int poolSize = 10;
    private GameObject explosionPrefab;

    private List<GameObject> effects = new List<GameObject>();

    private void Awake()
    {
        explosionPrefab = Resources.Load<GameObject>("Prefabs/Explosion");

        for (int i = 0; i < poolSize; i++)
        {
            GameObject explosion = Instantiate(explosionPrefab, Vector3.zero, Quaternion.identity);
            explosion.SetActive(false);
            effects.Add(explosion);
        }
    }

    public void Die(Vector3 pos)
    {
        foreach (GameObject explosion in effects)
        {
            if (!explosion.activeSelf)
            {
                explosion.transform.position = pos;
                explosion.SetActive(true);
                SoundManager.instance.PlayFX(SoundKey.EXPLOSION);
                return;
            }
        }
    }
}
