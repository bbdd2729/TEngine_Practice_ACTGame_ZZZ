// ---- Created with 3Dmigoto v1.2.45 on Sat Jan  3 09:43:43 2026
cbuffer cb2 : register(b2)
{
  float4 cb2[25];
}

cbuffer cb1 : register(b1)
{
  float4 cb1[29];
}

cbuffer cb0 : register(b0)
{
  float4 cb0[192];
}




// 3Dmigoto declarations
#define cmp -
Texture1D<float4> IniParams : register(t120);
Texture2D<float4> StereoParams : register(t125);


void main( 
  float3 v0 : POSITION0,
  float3 v1 : NORMAL0,
  float4 v2 : TANGENT0,
  float4 v3 : COLOR0,
  float4 v4 : TEXCOORD0,
  float4 v5 : TEXCOORD1,
  float4 v6 : TEXCOORD2,
  float4 v7 : TEXCOORD3,
  float3 v8 : TEXCOORD4,
  out float4 o0 : TEXCOORD0,
  out float4 o1 : TEXCOORD1,
  out float4 o2 : TEXCOORD2,
  out float4 o3 : TEXCOORD3,
  out float4 o4 : TEXCOORD4,
  out float4 o5 : TEXCOORD5,
  out float4 o6 : TXCOORDD6,
  out float4 o7 : TEXCOORD7,
  out float3 o8 : TEXCOORD8,
  out float4 o9 : SV_POSITION0)
{
// Needs manual fix for instruction: 
// unknown dcl_: dcl_resource_structured t0, 128
  float4 r0,r1,r2,r3,r4,r5,r6,r7,r8;
  uint4 bitmask, uiDest;
  float4 fDest;

  r0.xyz = cb1[1].xyz * v0.yyy;
  r0.xyz = cb1[0].xyz * v0.xxx + r0.xyz;
  r0.xyz = cb1[2].xyz * v0.zzz + r0.xyz;
  r0.xyz = cb1[3].xyz + r0.xyz;
  r1.xyzw = cb0[125].xyzw * r0.yyyy;
  r1.xyzw = cb0[124].xyzw * r0.xxxx + r1.xyzw;
  r1.xyzw = cb0[126].xyzw * r0.zzzz + r1.xyzw;
  o9.xyzw = cb0[127].xyzw + r1.xyzw;
  r0.w = cb1[9].w * v2.w;
  r1.x = dot(v1.xyz, cb1[4].xyz);
  r1.y = dot(v1.xyz, cb1[5].xyz);
  r1.z = dot(v1.xyz, cb1[6].xyz);
  r1.w = dot(r1.xyz, r1.xyz);
  r1.w = max(1.17549435e-38, r1.w);
  r1.w = rsqrt(r1.w);
  r1.xyz = r1.xyz * r1.www;
  r2.xyz = cb1[1].xyz * v2.yyy;
  r2.xyz = cb1[0].xyz * v2.xxx + r2.xyz;
  r2.xyz = cb1[2].xyz * v2.zzz + r2.xyz;
  r1.w = dot(r2.xyz, r2.xyz);
  r1.w = max(1.17549435e-38, r1.w);
  r1.w = rsqrt(r1.w);
  r2.xyz = r2.xyz * r1.www;
  r3.xyz = r2.yzx * r1.zxy;
  r3.xyz = r1.yzx * r2.zxy + -r3.xyz;
  o4.xyz = r3.xyz * r0.www;
  r0.w = v3.z * 255 + 0.5;
  r0.w = (int)r0.w;
  r0.w = (uint)r0.w >> 5;
  r0.w = (int)r0.w & 1;
  r0.w = (int)r0.w;
  r0.w = cmp(0 != r0.w);
  o7.y = r0.w ? 0 : v3.w;
  o7.z = r0.w ? 1.000000 : 0;
  r3.xyzw = cb1[1].xyzw * v0.yyyy;
  r3.xyzw = cb1[0].xyzw * v0.xxxx + r3.xyzw;
  r3.xyzw = cb1[2].xyzw * v0.zzzz + r3.xyzw;
  r3.xyzw = cb1[3].xyzw + r3.xyzw;
  r4.xyzw = cb0[141].xyzw * r3.yyyy;
  r4.xyzw = cb0[140].xyzw * r3.xxxx + r4.xyzw;
  r4.xyzw = cb0[142].xyzw * r3.zzzz + r4.xyzw;
  o5.xyzw = cb0[143].xyzw * r3.wwww + r4.xyzw;
  r0.w = cmp(0 < cb1[28].x);
  r3.xyz = r0.www ? v8.xyz : v0.xyz;
  r4.xyzw = cb1[21].xyzw * r3.yyyy;
  r4.xyzw = cb1[20].xyzw * r3.xxxx + r4.xyzw;
  r3.xyzw = cb1[22].xyzw * r3.zzzz + r4.xyzw;
  r3.xyzw = cb1[23].xyzw + r3.xyzw;
  r4.xyzw = cb0[133].xyzw * r3.yyyy;
  r4.xyzw = cb0[132].xyzw * r3.xxxx + r4.xyzw;
  r4.xyzw = cb0[134].xyzw * r3.zzzz + r4.xyzw;
  o6.xyzw = cb0[135].xyzw * r3.wwww + r4.xyzw;
  r0.w = cmp(0 < asint(cb0[188].z));
  if (r0.w != 0) {
    r0.w = (int)cb2[2].z;
    r1.w = asint(cb0[188].z) + -1;
    r0.w = min((int)r1.w, (int)r0.w);
    r0.w = max(0, (int)r0.w);
  // Missing reflection info for shader. No names possible.
  // Known bad code for instruction (needs manual fix):
    ld_structured_indexable(structured_buffer, stride=128)(mixed,mixed,mixed,mixed) r0.w, r0.w, l(52), t0.xxxx
  r0.w = no_StructuredBufferName[no_srcAddressRegister].no_srcByteOffsetName.swiz;
  } else {
    r0.w = 0;
  }
  o7.x = r0.w * -v3.w + 1;
  r0.w = min(4, cb2[2].x);
  r0.w = (int)r0.w;
  r1.w = cmp(0 < (int)r0.w);
  if (r1.w != 0) {
    r3.xyz = cb2[9].xyz + -r0.xyz;
    r1.w = dot(r3.xyz, r3.xyz);
    r1.w = max(1.17549435e-38, r1.w);
    r2.w = rsqrt(r1.w);
    r3.xyz = r3.xyz * r2.www;
    r4.y = cb2[11].x * r1.w;
    r2.w = cmp(-0.5 < cb2[11].y);
    r5.x = 1 / r1.w;
    r5.y = r4.y * r4.y;
    r4.x = 1;
    r4.xy = r2.ww ? r5.xy : r4.xy;
    r1.w = cmp(cb2[11].y < -1.5);
    r2.w = cmp(r4.y >= 1);
    r2.w = r2.w ? 1.000000 : 0;
    r1.w = r1.w ? r2.w : r4.y;
    r1.w = saturate(1 + -r1.w);
    r1.w = r1.w * r1.w;
    r1.w = r4.x * r1.w;
    r2.w = dot(cb2[10].xyz, r3.xyz);
    r2.w = saturate(r2.w * cb2[11].z + cb2[11].w);
    r2.w = r2.w * r2.w;
    r1.w = r2.w * r1.w;
    r1.w = cb0[191].y * r1.w;
    r3.xyz = cb2[12].xyz * r1.www;
    r1.w = cmp(1 < (int)r0.w);
    if (r1.w != 0) {
      r4.xyz = cb2[13].xyz + -r0.xyz;
      r1.w = dot(r4.xyz, r4.xyz);
      r1.w = max(1.17549435e-38, r1.w);
      r2.w = rsqrt(r1.w);
      r4.xyz = r4.xyz * r2.www;
      r5.y = cb2[15].x * r1.w;
      r2.w = cmp(-0.5 < cb2[15].y);
      r6.x = 1 / r1.w;
      r6.y = r5.y * r5.y;
      r5.x = 1;
      r5.xy = r2.ww ? r6.xy : r5.xy;
      r1.w = cmp(cb2[15].y < -1.5);
      r2.w = cmp(r5.y >= 1);
      r2.w = r2.w ? 1.000000 : 0;
      r1.w = r1.w ? r2.w : r5.y;
      r1.w = saturate(1 + -r1.w);
      r1.w = r1.w * r1.w;
      r1.w = r5.x * r1.w;
      r2.w = dot(cb2[14].xyz, r4.xyz);
      r2.w = saturate(r2.w * cb2[15].z + cb2[15].w);
      r2.w = r2.w * r2.w;
      r1.w = r2.w * r1.w;
      r1.w = cb0[191].y * r1.w;
      r4.xyz = cb2[16].xyz * r1.www + r3.xyz;
      r1.w = cmp(2 < (int)r0.w);
      if (r1.w != 0) {
        r5.xyz = cb2[17].xyz + -r0.xyz;
        r1.w = dot(r5.xyz, r5.xyz);
        r1.w = max(1.17549435e-38, r1.w);
        r2.w = rsqrt(r1.w);
        r5.xyz = r5.xyz * r2.www;
        r6.y = cb2[19].x * r1.w;
        r2.w = cmp(-0.5 < cb2[19].y);
        r7.x = 1 / r1.w;
        r7.y = r6.y * r6.y;
        r6.x = 1;
        r6.xy = r2.ww ? r7.xy : r6.xy;
        r1.w = cmp(cb2[19].y < -1.5);
        r2.w = cmp(r6.y >= 1);
        r2.w = r2.w ? 1.000000 : 0;
        r1.w = r1.w ? r2.w : r6.y;
        r1.w = saturate(1 + -r1.w);
        r1.w = r1.w * r1.w;
        r1.w = r6.x * r1.w;
        r2.w = dot(cb2[18].xyz, r5.xyz);
        r2.w = saturate(r2.w * cb2[19].z + cb2[19].w);
        r2.w = r2.w * r2.w;
        r1.w = r2.w * r1.w;
        r1.w = cb0[191].y * r1.w;
        r5.xyz = cb2[20].xyz * r1.www + r4.xyz;
        r0.w = cmp(3 < (int)r0.w);
        if (r0.w != 0) {
          r6.xyz = cb2[21].xyz + -r0.xyz;
          r0.w = dot(r6.xyz, r6.xyz);
          r0.w = max(1.17549435e-38, r0.w);
          r1.w = rsqrt(r0.w);
          r6.xyz = r6.xyz * r1.www;
          r7.y = cb2[23].x * r0.w;
          r1.w = cmp(-0.5 < cb2[23].y);
          r8.x = 1 / r0.w;
          r8.y = r7.y * r7.y;
          r7.x = 1;
          r7.xy = r1.ww ? r8.xy : r7.xy;
          r0.w = cmp(cb2[23].y < -1.5);
          r1.w = cmp(r7.y >= 1);
          r1.w = r1.w ? 1.000000 : 0;
          r0.w = r0.w ? r1.w : r7.y;
          r0.w = saturate(1 + -r0.w);
          r0.w = r0.w * r0.w;
          r0.w = r7.x * r0.w;
          r1.w = dot(cb2[22].xyz, r6.xyz);
          r1.w = saturate(r1.w * cb2[23].z + cb2[23].w);
          r1.w = r1.w * r1.w;
          r0.w = r1.w * r0.w;
          r0.w = cb0[191].y * r0.w;
          o8.xyz = cb2[24].xyz * r0.www + r5.xyz;
        } else {
          o8.xyz = r5.xyz;
        }
      } else {
        o8.xyz = r4.xyz;
      }
    } else {
      o8.xyz = r3.xyz;
    }
  } else {
    o8.xyz = float3(0,0,0);
  }
  o0.xy = v4.xy;
  o0.zw = v7.xy;
  o1.xy = v6.xy;
  o1.zw = float2(0,0);
  o2.w = r0.x;
  o2.xyz = r1.xyz;
  o3.w = r0.y;
  o3.xyz = r2.xyz;
  o4.w = r0.z;
  o7.w = 0;
  return;
}