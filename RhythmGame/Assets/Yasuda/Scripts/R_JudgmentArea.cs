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
    [SerializeField] GameObject RENDA_Prefab;

    [SerializeField]
    [Tooltip("パーティクル")]
    private ParticleSystem particle;

    [SerializeField]
    [Tooltip("連打パーティクル")]
    private ParticleSystem rendaparticle;

    public bool DethArea;

    // Specify the tags you want to interact with
    [SerializeField]
    private string[] interactableTags;

    private bool isRendaActive = false;

    public GameObject rendaobj;

    public ParticleSystem rendParticle;

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
        else if (Input.GetKey(keyCode))
        {
            if (!isRendaActive)
            {
                RaycastHit[] hits = Physics.SphereCastAll(transform.position, radius, Vector3.forward, 0);

                List<RaycastHit> raycastHits = hits.ToList();

                if (raycastHits.Count == 0) return;

                raycastHits.Sort((a, b) => (int)(a.transform.position.z - b.transform.position.z));

                RaycastHit hit = raycastHits[0];

                if (hit.transform.tag == "LongNots")
                {
                    if(rendaobj == null) rendaobj = Instantiate(RENDA_Prefab);
                    rendParticle = Instantiate(rendaparticle);
                    rendParticle.transform.position = this.transform.position;
                    rendParticle.Play();
                    isRendaActive = true;
                    Debug.Log("連打開始");
                }
            }
        }
        else if (Input.GetKeyUp(keyCode) && isRendaActive)
        {
            isRendaActive = false;
            Destroy(rendaobj);
            rendaobj = null;
            rendParticle.Stop();
            Debug.Log("連打終了");
        }

        if (isRendaActive)
        {
            // Check if there are still LongNots objects in the area
            RaycastHit[] hits = Physics.SphereCastAll(transform.position, radius, Vector3.forward, 0);

            bool longNotsPresent = hits.Any(hit => hit.transform.tag == "LongNots");

            if (!longNotsPresent)
            {
                isRendaActive = false;
                Destroy(rendaobj);
                rendaobj = null;
                rendParticle.Stop();
                Debug.Log("連打終了 (LongNots消失)");
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
