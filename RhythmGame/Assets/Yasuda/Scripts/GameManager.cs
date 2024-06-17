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
        // �X�R�A�����Z
        score += point;

        // �X�R�A���}�C�i�X�ɂȂ�Ȃ��悤�Ƀ`�F�b�N
        if (score < 0)
        {
            score = 0;
        }

        // �X�R�A�\���̍X�V
        scoreText.text = score.ToString();
        gameScoreText.text = score.ToString() + " �񎋒�";

        // slider�̍X�V
        UpdateSlider();
    }

    private void UpdateSlider()
    {
        if (maxScore > 0) // maxScore��0�ȏ�̏ꍇ�̂ݏ��������s
        {
            // slider��value���v�Z (score / maxScore)
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
        // ���b�`�e�L�X�g���g�p���ē���̕����̃T�C�Y��ύX
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
