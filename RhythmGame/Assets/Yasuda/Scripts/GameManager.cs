using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Playables;
using TMPro;
//using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    [SerializeField] Text countDownText = default;

    [SerializeField] Text scoreText = default;

    [SerializeField] TextMeshProUGUI comboText = default;

    [SerializeField] Text gameScoreText = default;

    [SerializeField] GameObject resultPanal = default;

    [SerializeField] TextScaler textScaler = default;

    [SerializeField] BeatRotation beatRotation;

    [SerializeField] Slider slider;
    [SerializeField] Image sliderImage;

    [SerializeField] public float ExcellentScore;

    [SerializeField] public float GoodScore;

    [SerializeField] public float BadScore;

    [SerializeField]
    private GameObject particle;
    [SerializeField]
    private Transform particlePos;

    private GameObject effect;

    bool OnClear = false;

    float score;

    public int maxScore;

    int combo = 0;

    public string songName;

    public NotesManager notesManager;

    private void Start()
    {
        //e_NotesManager.Load(songName);

        StartCoroutine(GameMain());
    }

    IEnumerator GameMain()
    {
        countDownText.text = "3";
        yield return new WaitForSeconds(1);
        countDownText.text = "2";
        yield return new WaitForSeconds(1);
        countDownText.text = "1";
        yield return new WaitForSeconds(1);
        countDownText.text = "GO";
        yield return new WaitForSeconds(0.3f);
        countDownText.text = "";
        notesManager.GameStart();
    }

    public void AddScore(float point)
    {
        // スコアを加算
        score += point;

        // スコアがマイナスにならないようにチェック
        if (score < 0)
        {
            score = 0;
        }

        // スコア表示の更新
        scoreText.text = score.ToString();
        gameScoreText.text = score.ToString() + " 回視聴";

        // sliderの更新
        UpdateSlider();
    }

    private void UpdateSlider()
    {
        if (maxScore > 0) // maxScoreが0以上の場合のみ処理を実行
        {
            // sliderのvalueを計算 (score / maxScore)
            slider.value = score / maxScore;

            if (slider.value > 0.7f) 
            {
                beatRotation.TurnOnRotation();
                sliderImage.color = Color.yellow;
                if (OnClear) return;
                effect = Instantiate(particle, new Vector3(particlePos.position.x, particlePos.position.y, particlePos.position.z), Quaternion.identity);
                OnClear = true;
            }
            else
            {
                beatRotation.TurnOffRotation();
                sliderImage.color = Color.red;
                Destroy(effect);
                OnClear = false;
            }
        }
    }
    public void AddCombo()
    {
        combo += 1;
        // リッチテキストを使用して特定の文字のサイズを変更
        comboText.text = combo.ToString() + "\n" + "<size=60%>Combo</size>";
        textScaler.ScaleText();
    }

    public void ResetCombo()
    {
        combo = 0;
        comboText.text = "";
    }

    public void OnEndEvent()
    {
        resultPanal.SetActive(true);
    }

    public void OnRetry()
    {
        SceneManager.LoadScene("GameScene");
    }
}
