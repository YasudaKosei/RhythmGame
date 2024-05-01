using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class JudgmentArea : MonoBehaviour
{
    [SerializeField] float radius;
    [SerializeField] GameManager gameManager = default;
    [SerializeField] KeyCode keyCode;

    [SerializeField] GameObject textEffectPrefab;

    [SerializeField] GameObject TAMBOURINE_Prefab;
    [SerializeField] GameObject KARAUTI_Prefab;

    [SerializeField] SpriteRenderer sriteRenderer;

    [SerializeField]
    [Tooltip("パーティクル")]
    private ParticleSystem particle;

    public bool DethArea;

    private void Update()
    {
        if (Input.GetKeyDown(keyCode) || DethArea)
        {
            if (!DethArea) StartCoroutine(OnAreaColor());

            //円形のレイヤー
            RaycastHit2D[] hits2D = Physics2D.CircleCastAll(transform.position, radius, Vector3.zero);

            if(hits2D.Length == 0 && !DethArea)
            {
                GameObject obj = Instantiate(KARAUTI_Prefab);
                Destroy(obj, 0.5f);
                return;
            }

            List<RaycastHit2D> raycastHit2Ds = hits2D.ToList();

            if (raycastHit2Ds.Count == 0) return;

            raycastHit2Ds.Sort((a,b) => (int)(a.transform.position.y - b.transform.position.y));

            RaycastHit2D hit2D = raycastHit2Ds[0];

            if (hit2D)
            {
                if(DethArea)
                {
                    gameManager.AddScore(gameManager.BadScore);
                    gameManager.ResetCombo();
                    hit2D.collider.gameObject.SetActive(false);
                    return;
                }

                //判定
                float distance = Mathf.Abs(hit2D.transform.position.y - transform.position.y);

                if(distance < 30)
                {
                    GameObject obj = Instantiate(TAMBOURINE_Prefab);
                    Destroy(obj, 0.5f);

                    SpawnParticle();
                    gameManager.AddScore(gameManager.ExcellentScore);
                    gameManager.AddCombo();
                    SpawnTextEffect("Excellent", hit2D.transform.position);
                }
                else if (distance < 35)
                {
                    GameObject obj = Instantiate(TAMBOURINE_Prefab);
                    Destroy(obj, 0.5f);

                    SpawnParticle();
                    gameManager.AddScore(gameManager.GoodScore);
                    gameManager.AddCombo();
                    SpawnTextEffect("Good", hit2D.transform.position);
                }
                else
                {
                    GameObject obj = Instantiate(KARAUTI_Prefab);
                    Destroy(obj, 0.5f);

                    gameManager.AddScore(gameManager.BadScore);
                    gameManager.ResetCombo();
                    SpawnTextEffect("引退しろ", hit2D.transform.position);
                }

                //ぶつかったらけす
                hit2D.collider.gameObject.SetActive(false);
            }
        }
    }

    IEnumerator OnAreaColor()
    {
        sriteRenderer.color = new Color(20, 20,20, 0.1f);

        yield return new WaitForSeconds(0.1f);

        sriteRenderer.color = new Color(0, 0, 0, 0.8f);
    }

    void SpawnTextEffect(string message, Vector3 position)
    {
        GameObject effect = Instantiate(textEffectPrefab, position + Vector3.up*1.5f, Quaternion.identity);
        JudgmentEffect judgmentEffect = effect.GetComponent<JudgmentEffect>();
        judgmentEffect.SetText(message);
    }

    private void SpawnParticle()
    {
        // パーティクルシステムのインスタンスを生成する。
        ParticleSystem newParticle = Instantiate(particle);
        // パーティクルの発生場所をこのスクリプトをアタッチしているGameObjectの場所にする。
        newParticle.transform.position = this.transform.position;
        // パーティクルを発生させる。
        newParticle.Play();
        // インスタンス化したパーティクルシステムのGameObjectを5秒後に削除する。(任意)
        // ※第一引数をnewParticleだけにするとコンポーネントしか削除されない。
        Destroy(newParticle.gameObject, 5.0f);
    }

    //Ray可視化
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, radius);
    }

}
