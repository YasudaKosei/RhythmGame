using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class R_JudgmentArea : MonoBehaviour
{
    [SerializeField] float radius;
    [SerializeField] KeyCode keyCode;

    [SerializeField] private GameObject[] MessageObj; // プレイヤーに判定を伝えるゲームオブジェクト


    public int LaneNum;

    [SerializeField] GameObject TAMBOURINE_Prefab;
    [SerializeField] GameObject KARAUTI_Prefab;

    [SerializeField]
    [Tooltip("パーティクル")]
    private ParticleSystem particle;

    public bool DethArea;

    // Specify the tags you want to interact with
    [SerializeField]
    private string[] interactableTags;

    private void Update()
    {
        if (Input.GetKeyDown(keyCode) || DethArea)
        {
            // 3D sphere cast
            RaycastHit[] hits = Physics.SphereCastAll(transform.position, radius, Vector3.forward, 0);

            List<RaycastHit> raycastHits = hits.ToList();

            if (raycastHits.Count == 0) return;

            raycastHits.Sort((a, b) => (int)(a.transform.position.z - b.transform.position.z));

            RaycastHit hit = raycastHits[0];

            if (interactableTags.Contains(hit.collider.tag))
            {
                if (DethArea)
                {
                    hit.collider.gameObject.SetActive(false);
                    return;
                }

                // 判定
                float distance = Mathf.Abs(hit.transform.position.z - transform.position.z);

                Debug.Log(distance);

                if (distance < 0.3f)
                {
                    GameObject obj = Instantiate(TAMBOURINE_Prefab);
                    Destroy(obj, 0.5f);

                    SpawnParticle();
                    message(0);
                }
                else if (distance < 0.5f)
                {
                    GameObject obj = Instantiate(TAMBOURINE_Prefab);
                    Destroy(obj, 0.5f);

                    SpawnParticle();
                    message(1);
                }
                else
                {
                    GameObject obj = Instantiate(KARAUTI_Prefab);
                    Destroy(obj, 0.5f);
                    message(2);
                }

                hit.collider.gameObject.SetActive(false);
            }
        }
    }

    void message(int judge) // 判定を表示する
    {
        GameObject mo = Instantiate(MessageObj[judge], new Vector3(LaneNum - 1.5f, 0.76f, 0.15f), Quaternion.Euler(45, 0, 0));
        mo.gameObject.transform.localScale = Vector3.one;
        Destroy(mo, 1.0f);
    }

    private void SpawnParticle()
    {
        ParticleSystem newParticle = Instantiate(particle);
        newParticle.transform.position = this.transform.position;
        newParticle.Play();
        Destroy(newParticle.gameObject, 5.0f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, radius);
    }
}
