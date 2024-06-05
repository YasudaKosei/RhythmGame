using System.Collections;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class LoadingAnimation : MonoBehaviour
{
    [SerializeField] private TMP_Text tmpText;
    [SerializeField] private float bounceHeight = 10f;
    [SerializeField] private float bounceDuration = 0.5f;
    [SerializeField] private float delayBetweenCharacters = 0.2f;

    private Coroutine animationCoroutine; // �R���[�`����ێ�����ϐ�

    void Start()
    {
        StartCoroutine(AnimateTextLoop());
    }

    private IEnumerator AnimateTextLoop()
    {
        while (true) // �������[�v�ŏ����𑱂���
        {
            tmpText.ForceMeshUpdate(); // ���b�V�����X�V���čŐV�̏����擾

            Vector3[] originalVertices = tmpText.mesh.vertices; // �I���W�i���̒��_�ʒu��ێ�
            int vertexIndex;
            TMP_CharacterInfo charInfo;

            tmpText.maxVisibleCharacters = 0;

            for (int i = 0; i < tmpText.text.Length; i++)
            {
                tmpText.maxVisibleCharacters = i + 1;
                tmpText.ForceMeshUpdate();

                charInfo = tmpText.textInfo.characterInfo[i];

                if (charInfo.isVisible)
                {
                    vertexIndex = charInfo.vertexIndex;
                    Vector3 offset = Vector3.up * bounceHeight;

                    // DOTween���g�p���Ē��_�𓮂���
                    Sequence sequence = DOTween.Sequence();
                    sequence.Append(DOTween.To(() => originalVertices[vertexIndex], x => UpdateVertex(tmpText, vertexIndex, x), originalVertices[vertexIndex] + offset, bounceDuration));
                    sequence.Append(DOTween.To(() => originalVertices[vertexIndex] + offset, x => UpdateVertex(tmpText, vertexIndex, x), originalVertices[vertexIndex], bounceDuration));

                    yield return new WaitForSeconds(delayBetweenCharacters);
                }
            }
            yield return new WaitForSeconds(delayBetweenCharacters * 5); // ���[�v�̊Ԋu��ݒ肷�邽�߂̑ҋ@����
        }
    }

    void UpdateVertex(TMP_Text text, int vertexIndex, Vector3 position)
    {
        Vector3[] vertices = text.mesh.vertices;
        vertices[vertexIndex] = position;
        text.mesh.vertices = vertices;
        text.canvasRenderer.SetMesh(text.mesh);
    }
    void OnDisable()
    {
        if (animationCoroutine != null)
        {
            StopCoroutine(animationCoroutine); // �R���[�`�����~
        }
        DOTween.KillAll(); // ���ׂĂ�DOTween�A�j���[�V�������~
    }
}