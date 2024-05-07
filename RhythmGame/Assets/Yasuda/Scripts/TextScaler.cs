using UnityEngine;
using TMPro; // TextMeshPro�̖��O��Ԃ�ǉ�
using System.Collections;

public class TextScaler : MonoBehaviour
{
    public TextMeshProUGUI textComponent; // TextMeshProUGUI�R���|�[�l���g�ւ̎Q��

    [SerializeField]
    private float startSize = 20f; // �J�n���̃e�L�X�g�̑傫��

    [SerializeField]
    private float endSize = 50f; // �I�����̃e�L�X�g�̑傫��

    [SerializeField]
    private float duration = 2f; // �A�j���[�V�����̌p�����ԁi�b�j

    // �e�L�X�g���g�傷��֐�
    public void ScaleText()
    {
        StartCoroutine(ScaleTextCoroutine());
    }

    // �R���[�`�����g�p���ăe�L�X�g���g�傷�鏈��
    private IEnumerator ScaleTextCoroutine()
    {
        float elapsedTime = 0f;
        float newSize = startSize;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            // ���݂̃e�L�X�g�̑傫�����v�Z
            newSize = Mathf.Lerp(startSize, endSize, elapsedTime / duration);

            // �e�L�X�g�̑傫����ݒ�
            textComponent.fontSize = newSize;

            yield return null;
        }

        // �A�j���[�V�������I��������A�I���T�C�Y�Ƀe�L�X�g�̑傫����ݒ肷��
        textComponent.fontSize = endSize;
    }
}
