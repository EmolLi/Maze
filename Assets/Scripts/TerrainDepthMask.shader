Shader "TerrainDepthMask" {
  SubShader {
    Tags {"Queue" = "Geometry+10" } // earlier = hides stuff later in queue
    Lighting Off
    ZTest LEqual
    ZWrite On
    ColorMask 0
    Pass {}
  }
}

