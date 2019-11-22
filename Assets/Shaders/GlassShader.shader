Shader "Glass (unlit)" {
Properties {
    _Color ("Glass Color", Color) = (1,1,1,0.5)
    _Cube ("Reflection cubemap", Cube) = "" { TexGen CubeReflect }
}
Category {
    Tags {"Queue" = "Transparent" }
    Blend SrcAlpha OneMinusSrcAlpha
    ZWrite Off
    BindChannels {
        Bind "Vertex", vertex
        Bind "Normal", normal
    }
    // cards with cube maps
    SubShader {
        Pass {
            SetTexture [_Cube] {
                constantColor [_Color]
                combine texture * constant, constant
            }
        }
    }
    // cards without cube maps - just simple transparency
    SubShader {
        Pass {
            SetTexture [_Dummy] {
                constantColor [_Color]
                combine constant
            }
        }
    }
}
}
