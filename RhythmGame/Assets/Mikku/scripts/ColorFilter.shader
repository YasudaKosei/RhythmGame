Shader "Hidden/ColorFilter"
{
    Properties
    {
        _Color("Color", Color) = (1,1,1,1)
    }
    SubShader
    {
        Pass
        {
            ZTest Always Cull Off ZWrite Off
            SetTexture[_MainTex] { combine texture }
            SetTexture[_MainTex]
            {
                constantColor[_Color]
                combine texture * constant
            }
        }
    }
}
