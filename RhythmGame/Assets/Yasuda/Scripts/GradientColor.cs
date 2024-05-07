namespace UnityEngine.UI
{
    [AddComponentMenu("UI/Effects/Custom/Gradient", 100)]
    public class GradientColor : BaseMeshEffect
    {
        public Color32 Color1 = Color.red;
        public Color32 Color2 = Color.white;
        public override void ModifyMesh(VertexHelper vh)
        {
            if (!IsActive()) return;
            UIVertex v = new UIVertex();
            float f = 0.0f;
            int idx = 0;
            for (int i = 0; i < vh.currentVertCount; i++)
            {
                vh.PopulateUIVertex(ref v, i);
                switch (idx)
                {
                    case 0: f = 1.0f; break;
                    case 1: f = 0.5f; break;
                    case 2: f = 0.0f; break;
                    case 3: f = 0.5f; break;
                }
                if (++idx >= 4) { idx = 0; }
                v.color = Color32.Lerp(Color1, Color2, f);
                vh.SetUIVertex(v, i);
            }
        }
    }
}