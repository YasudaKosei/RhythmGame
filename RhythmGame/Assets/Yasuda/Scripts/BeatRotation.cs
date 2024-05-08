using UnityEngine;

public class BeatRotation : MonoBehaviour
{
    public float beatsPerMinute = 120f; // �f�t�H���g��BPM��120�ł����A�K�v�ɉ����ĕύX���Ă�������
    public float rotationAmount = 10f; // ��]�p�x

    private float timer;
    private Quaternion startRotation;
    private bool isRotating = false; // ��]�̃I���E�I�t�𐧌䂷��t���O

    void Start()
    {
        startRotation = transform.rotation; // �L�����N�^�[�̏�����]��ۑ�
        timer = 0f;
    }

    void Update()
    {
        if (isRotating)
        {
            // ��]��ǉ�
            float rotationAmountThisFrame = Mathf.Sin(timer * Mathf.PI * (beatsPerMinute / 60f)) * rotationAmount;
            transform.rotation = startRotation * Quaternion.Euler(0f, 0f, rotationAmountThisFrame);

            // �^�C�}�[���X�V
            timer += Time.deltaTime;
        }
    }

    // ��]���I���ɂ���֐�
    public void TurnOnRotation()
    {
        isRotating = true;
    }

    // ��]���I�t�ɂ���֐�
    public void TurnOffRotation()
    {
        isRotating = false;
    }
}