// ---- Created with 3Dmigoto v1.2.45 on Sat Jan  3 06:31:51 2026
cbuffer cb2 : register(b2)
{
  float4 cb2[64];
}

cbuffer cb1 : register(b1)
{
  float4 cb1[4];
}

cbuffer cb0 : register(b0)
{
  float4 cb0[178];
}




// 3Dmigoto declarations
#define cmp -
Texture1D<float4> IniParams : register(t120);
Texture2D<float4> StereoParams : register(t125);


void main( 
  float3 v0 : POSITION0,
  float3 v1 : NORMAL0,
  float4 v2 : COLOR0,
  float4 v3 : TEXCOORD0,
  float4 v4 : TEXCOORD1,
  float4 v5 : TEXCOORD2,
  float4 v6 : TEXCOORD3,
  float4 v7 : TEXCOORD4,
  float4 v8 : TEXCOORD6,
  out float4 o0 : SV_POSITION0,
  out float4 o1 : TEXCOORD0,
  out float4 o2 : TEXCOORD1,
  out float4 o3 : TEXCOORD2,
  out float4 o4 : TEXCOORD3,
  out float4 o5 : TEXCOORD4,
  out float4 o6 : TEXCOORD5,
  out float4 o7 : TEXCOORD6,
  out float4 o8 : TEXCOORD7,
  out float3 o9 : TEXCOORD8)
{
  float4 r0,r1,r2,r3,r4,r5,r6,r7,r8,r9,r10,r11,r12;
  uint4 bitmask, uiDest;
  float4 fDest;

  float4 x0[2];
  float4 x1[6];
  float4 x2[6];
  float4 x3[6];
  float4 x4[6];
  r0.x = (uint)cb2[42].z;
  r0.y = cmp(0.5 < cb2[42].y);
  r0.x = r0.y ? cb0[14].x : cb0[r0.x+17].y;
  r0.x = -cb2[60].z + r0.x;
  r0.y = cmp(0.5 < cb2[27].y);
  r1.xy = v4.xy;
  r1.zw = v5.xy;
  r2.xy = v6.zw;
  r2.zw = v8.zw;
  r1.xyzw = r0.yyyy ? r1.xyzw : r2.xyzw;
  r0.z = 1 + -v2.w;
  x0[0].x = 0;
  x0[1].x = r0.z;
  x1[0].x = 0;
  x1[1].x = 1;
  r0.zw = (uint2)cb2[63].xy;
  x0[r0.z+0].x = v5.x;
  x1[r0.w+0].x = v5.y;
  r0.z = x0[1].x;
  o4.x = r0.y ? cb2[27].z : r0.z;
  r0.z = x1[1].x;
  o4.z = r0.y ? cb2[63].z : r0.z;
  r0.z = cmp(0 != cb2[12].x);
  r2.xyzw = r0.zzzz ? float4(1,1,1,1) : v2.xyzw;
  r0.z = cmp(0.5 < cb2[53].w);
  if (r0.z != 0) {
    r3.xz = cb0[22].xz;
    r3.y = cb2[53].z + cb0[22].y;
    r3.xyz = -v7.xyz + r3.xyz;
    r0.z = dot(r3.xyz, r3.xyz);
    r0.z = sqrt(r0.z);
    r0.w = dot(-r3.xyz, -r3.xyz);
    r0.w = rsqrt(r0.w);
    r3.xyz = -r3.xyz * r0.www;
    r0.z = -cb2[53].x + r0.z;
    r0.w = cb2[53].y + -cb2[53].x;
    r0.z = saturate(r0.z / r0.w);
    r0.z = 1 + -r0.z;
    r3.xyz = r3.xyz * r0.zzz;
    r3.xyz = r3.xyz * cb2[53].xxx + v0.xyz;
  } else {
    r3.xyz = v0.xyz;
  }
  r4.xyz = cb1[1].xyz * r3.yyy;
  r3.xyw = cb1[0].xyz * r3.xxx + r4.xyz;
  r3.xyz = cb1[2].xyz * r3.zzz + r3.xyw;
  r3.xyz = cb1[3].xyz + r3.xyz;
  r4.xyz = cb0[30].xyz + -r3.xyz;
  r0.z = dot(r4.xyz, r4.xyz);
  o7.w = sqrt(r0.z);
  r4.xyz = cb0[94].xyz * r3.yyy;
  r4.xyz = cb0[93].xyz * r3.xxx + r4.xyz;
  r4.xyz = cb0[95].xyz * r3.zzz + r4.xyz;
  r4.xyz = cb0[96].xyz + r4.xyz;
  r0.z = dot(r4.xyz, r4.xyz);
  r0.z = rsqrt(r0.z);
  r5.xyz = r4.xyz * r0.zzz;
  r4.xyz = r5.xyz * cb2[11].www + r4.xyz;
  r5.x = cb0[89].x;
  r5.y = cb0[91].x;
  r5.z = cb0[92].x;
  r4.w = 1;
  r5.x = dot(r5.xyz, r4.xzw);
  r6.x = cb0[90].y;
  r6.y = cb0[91].y;
  r6.zw = cb0[92].yz;
  r5.y = dot(r6.xyz, r4.yzw);
  r6.x = cb0[89].z;
  r6.y = cb0[90].z;
  r6.z = cb0[91].z;
  r0.z = dot(r6.xyzw, r4.xyzw);
  r4.x = cb0[91].w;
  r4.y = cb0[92].w;
  r0.w = dot(r4.xy, r4.zw);
  r3.w = cmp(0 != cb2[57].z);
  r4.xy = v5.zw;
  r5.zw = v6.xy;
  r6.x = r3.w ? cb0[35].w : r4.x;
  r6.y = r3.w ? cb0[36].w : r4.y;
  r6.z = r3.w ? cb0[37].w : r5.z;
  r6.w = r3.w ? cb0[38].w : r5.w;
  r6.xyzw = r0.yyyy ? cb2[26].xyzw : r6.xyzw;
  r0.y = cmp(cb2[25].y < 0.5);
  r2.xyzw = cb2[13].xyzw * r2.xyzw;
  r2.w = r0.y ? r6.w : r2.w;
  r4.xy = cmp(float2(0.5,0.5) < cb2[57].wy);
  r7.x = cb0[39].y;
  r7.y = cb0[40].y;
  r7.z = cb0[41].y;
  r7.w = cb0[42].y;
  r8.xyzw = r7.xyzw * r6.xyzw;
  r9.xyzw = r7.xyzw * r2.xyzw;
  r8.xyzw = r4.yyyy ? r8.xyzw : r7.xyzw;
  r7.xyzw = r4.yyyy ? r9.xyzw : r7.xyzw;
  o5.xyzw = r4.xxxx ? r8.xyzw : r6.xyzw;
  o6.xyzw = r4.xxxx ? r7.xyzw : r2.xyzw;
  r0.y = max(0, cb2[15].x);
  r0.y = min(5, r0.y);
  x2[0].xy = v3.xy;
  x2[1].xy = r1.xy;
  x2[2].xy = v3.xy;
  x2[3].xy = r1.xy;
  x2[4].xy = v3.xy;
  x2[5].xy = r1.zw;
  r0.y = floor(r0.y);
  r0.y = (uint)r0.y;
  r2.xy = x2[r0.y+0].xy;
  r0.y = max(0, cb2[22].y);
  r0.y = min(5, r0.y);
  x3[0].xy = v3.xy;
  x3[1].xy = r1.xy;
  x3[2].xy = v3.xy;
  x3[3].xy = r1.xy;
  x3[4].xy = v3.xy;
  x3[5].xy = r1.zw;
  r0.y = floor(r0.y);
  r0.y = (uint)r0.y;
  r6.xy = x3[r0.y+0].xy;
  r0.y = max(0, cb2[29].y);
  r0.y = min(5, r0.y);
  x4[0].xy = v3.xy;
  x4[1].xy = r1.xy;
  x4[2].xy = v3.xy;
  x4[3].xy = r1.xy;
  x4[4].xy = v3.xy;
  x4[5].xy = r1.zw;
  r0.y = floor(r0.y);
  r0.y = (uint)r0.y;
  r7.xy = x4[r0.y+0].xy;
  r0.y = cmp(cb2[27].y < 0.5);
  r1.xy = r0.yy ? v3.zw : 0;
  r4.xy = r0.yy ? v4.zw : 0;
  r5.z = x0[0].x;
  r5.w = x1[0].x;
  r5.zw = r0.yy ? r5.zw : 0;
  r2.zw = float2(1,1) + -r2.xy;
  r8.xyzw = r2.yzwx + -r2.xyzw;
  r2.xyzw = cb2[16].xxxx * r8.xyzw + r2.xyzw;
  r8.xyzw = cmp(cb2[15].wwww < float4(0.5,1.5,2.5,65504));
  r9.xyzw = r8.xyzw ? float4(1,1,1,1) : 0;
  r8.xyz = r8.xyz ? float3(-1,-1,-1) : float3(-0,-0,-0);
  r8.xyz = r9.yzw + r8.xyz;
  r8.xyz = max(float3(0,0,0), r8.xyz);
  r10.xy = r2.zw;
  r10.z = 1;
  r10.w = 1 + -cb2[16].x;
  r11.xy = r2.xw;
  r11.z = 0;
  r11.w = 1 + -cb2[16].x;
  r12.xyzw = r11.xyzw * r8.yyyy;
  r12.xyzw = r10.xyzw * r8.zzzz + r12.xyzw;
  r2.xz = r10.xz;
  r2.w = cb2[16].x;
  r8.xyzw = r2.xyzw * r8.xxxx + r12.xyzw;
  r2.xz = r11.xz;
  r2.xyzw = r2.xyzw * r9.xxxx + r8.xyzw;
  r2.xy = r2.xy + -r2.zw;
  r2.xy = r2.xy * cb2[1].xy + cb2[1].zw;
  r2.xy = r2.xy + r2.zw;
  r0.y = cmp(0.5 < cb2[24].z);
  r8.xyzw = cb2[9].xyzw * r0.xxxx;
  r8.xyzw = frac(r8.xyzw);
  r2.zw = r8.xy + r4.xy;
  r4.xy = cmp(cb2[17].xy != float2(0,0));
  r8.xy = r2.zw / cb2[17].xy;
  r8.xy = floor(r8.xy);
  r8.xy = cb2[17].xy * r8.xy;
  r2.zw = r4.xy ? r8.xy : r2.zw;
  r2.zw = r2.xy + r2.zw;
  r2.xy = r0.yy ? r2.zw : r2.xy;
  r0.y = cmp(0.5 < cb2[16].y);
  r2.zw = r1.xy * float2(3.14159274,1.44269502) + r2.xy;
  o2.xy = r0.yy ? r2.zw : r2.xy;
  r6.zw = float2(1,1) + -r6.xy;
  r2.xyzw = r6.yzwx + -r6.xyzw;
  r2.xyzw = cb2[23].yyyy * r2.xyzw + r6.xyzw;
  r6.xyzw = cmp(cb2[23].xxxx < float4(0.5,1.5,2.5,65504));
  r9.xyzw = r6.xyzw ? float4(1,1,1,1) : 0;
  r4.xyw = r6.xyz ? float3(-1,-1,-1) : float3(-0,-0,-0);
  r4.xyw = r9.yzw + r4.xyw;
  r4.xyw = max(float3(0,0,0), r4.xyw);
  r6.xy = r2.zw;
  r6.z = 1;
  r6.w = 1 + -cb2[23].y;
  r10.xy = r2.xw;
  r10.z = 0;
  r10.w = 1 + -cb2[23].y;
  r11.xyzw = r10.xyzw * r4.yyyy;
  r11.xyzw = r6.xyzw * r4.wwww + r11.xyzw;
  r2.xz = r6.xz;
  r2.w = cb2[23].y;
  r6.xyzw = r2.xyzw * r4.xxxx + r11.xyzw;
  r2.xz = r10.xz;
  r2.xyzw = r2.xyzw * r9.xxxx + r6.xyzw;
  r2.xy = r2.xy + -r2.zw;
  r2.xy = r2.xy * cb2[2].xy + cb2[2].zw;
  r2.xy = r2.xy + r2.zw;
  r2.xy = r2.xy + r5.zw;
  o2.zw = r2.xy + r8.zw;
  r7.zw = float2(1,1) + -r7.xy;
  r2.xyzw = r7.yzwx + -r7.xyzw;
  r2.xyzw = cb2[30].yyyy * r2.xyzw + r7.xyzw;
  r6.xyzw = cmp(cb2[30].xxxx < float4(0.5,1.5,2.5,65504));
  r7.xyzw = r6.xyzw ? float4(1,1,1,1) : 0;
  r4.xyw = r6.xyz ? float3(-1,-1,-1) : float3(-0,-0,-0);
  r4.xyw = r7.yzw + r4.xyw;
  r4.xyw = max(float3(0,0,0), r4.xyw);
  r6.xy = r2.zw;
  r6.z = 1;
  r6.w = 1 + -cb2[30].y;
  r8.xy = r2.xw;
  r8.z = 0;
  r8.w = 1 + -cb2[30].y;
  r9.xyzw = r8.xyzw * r4.yyyy;
  r9.xyzw = r6.xyzw * r4.wwww + r9.xyzw;
  r2.xz = r6.xz;
  r2.w = cb2[30].y;
  r6.xyzw = r2.xyzw * r4.xxxx + r9.xyzw;
  r2.xz = r8.xz;
  r2.xyzw = r2.xyzw * r7.xxxx + r6.xyzw;
  r2.xy = r2.xy + -r2.zw;
  r2.xy = r2.xy * cb2[3].xy + cb2[3].zw;
  r2.xy = r2.xy + r2.zw;
  r0.xy = cb2[10].xy * r0.xx;
  r0.xy = frac(r0.xy);
  r0.xy = r2.xy + r0.xy;
  r2.x = cmp(0.5 < cb2[30].z);
  r1.xy = r0.xy + r1.xy;
  o3.xy = r2.xx ? r1.xy : r0.xy;
  r0.x = cmp(0.5 < cb2[0].x);
  r0.y = max(0, r0.z);
  o0.z = r0.x ? r0.y : r0.z;
  o0.xy = cb0[177].xy * r0.ww + r5.xy;
  o0.w = r0.w;
  o1.xyzw = v3.xyzw;
  o3.zw = float2(0,0);
  o4.y = 0;
  o4.w = r4.z;
  o7.xyz = r3.xyz;
  o8.xy = v5.xy;
  o8.zw = float2(0,0);
  o9.xy = r1.zw;
  o9.z = v7.w;
  return;
}