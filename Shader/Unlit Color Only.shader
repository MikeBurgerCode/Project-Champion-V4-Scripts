Shader "Unlit/Unlit Color Only"
{
	   Properties {
        _Color ("Main Color", Color) = (1,1,1,1)
        _MainTex ("Base (RGB)", 2D) = "white" {}
         _Cutoff ("Alpha cutoff", Range(0.000000,1.000000)) = 0.500000
    }

    Category {
       Lighting Off
       ZWrite On
       Cull Back
       SubShader {
            Pass {
               SetTexture [_MainTex] {
                   
                    Combine texture * constant, texture * constant  constantColor [_Color]
                 }
            }
        }


    }


}