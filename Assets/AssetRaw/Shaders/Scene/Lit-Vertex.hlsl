// ---- Created with 3Dmigoto v1.2.45 on Sat Jan  3 12:47:02 2026
cbuffer cb3 : register(b3)
{
  float4 cb3[8];
}

cbuffer cb2 : register(b2)
{
  float4 cb2[22];
}

cbuffer cb1 : register(b1)
{
  float4 cb1[1];
}

cbuffer cb0 : register(b0)
{
  float4 cb0[105];
}




// 3Dmigoto declarations
#define cmp -
Texture1D<float4> IniParams : register(t120);
Texture2D<float4> StereoParams : register(t125);


void main( 
  float4 v0 : POSITION0,
  float3 v1 : NORMAL0,
  float4 v2 : TANGENT0,
  float4 v3 : TEXCOORD0,
  float2 v4 : TEXCOORD1,
  uint v5 : SV_InstanceID0,
  out float4 o0 : SV_POSITION0,
  out float4 o1 : TEXCOORD0,
  out float4 o2 : TEXCOORD1,
  out float4 o3 : TEXCOORD2,
  out float3 o4 : TEXCOORD4,
  out uint o5 : SV_InstanceID0)
{
// Needs manual fix for instruction: 
// unknown dcl_: dcl_input_sgv v5.x, instance_id
  float4 r0,r1,r2;
  uint4 bitmask, uiDest;
  float4 fDest;

  r0.x = (int)v5.x + asint(cb1[0].x);
  r0.x = (int)r0.x * 11;
  r0.yzw = cb2[r0.x+1].xyz * v0.yyy;
  r0.yzw = cb2[r0.x+0].xyz * v0.xxx + r0.yzw;
  r0.yzw = cb2[r0.x+2].xyz * v0.zzz + r0.yzw;
  r0.yzw = cb2[r0.x+3].xyz + r0.yzw;
  r1.xyzw = cb0[102].xyzw * r0.zzzz;
  r1.xyzw = cb0[101].xyzw * r0.yyyy + r1.xyzw;
  r1.xyzw = cb0[103].xyzw * r0.wwww + r1.xyzw;
  o0.xyzw = cb0[104].xyzw + r1.xyzw;
  r1.x = dot(v1.xyz, cb2[r0.x+4].xyz);
  r1.y = dot(v1.xyz, cb2[r0.x+5].xyz);
  r1.z = dot(v1.xyz, cb2[r0.x+6].xyz);
  r1.w = dot(r1.xyz, r1.xyz);
  r1.w = max(1.17549435e-38, r1.w);
  r1.w = rsqrt(r1.w);
  o2.xyz = r1.xyz * r1.www;
  r1.xyz = cb2[r0.x+1].xyz * v2.yyy;
  r1.xyz = cb2[r0.x+0].xyz * v2.xxx + r1.xyz;
  r1.xyz = cb2[r0.x+2].xyz * v2.zzz + r1.xyz;
  r1.w = dot(r1.xyz, r1.xyz);
  r1.w = max(1.17549435e-38, r1.w);
  r1.w = rsqrt(r1.w);
  o3.xyz = r1.xyz * r1.www;
  r1.xw = v3.yx * cb3[4].yx + cb3[4].wz;
  r2.x = cmp(0.5 < cb3[7].y);
  if (r2.x != 0) {
    r2.x = dot(cb2[r0.x+1].xyz, cb2[r0.x+1].xyz);
    r2.x = sqrt(r2.x);
    r1.x = r2.x * r1.x;
  }
  o3.w = cb2[r0.x+9].w * v2.w;
  r1.yz = v4.xy * cb2[r0.x+10].xy + cb2[r0.x+10].zw;
  o1.xyzw = r1.wxyz;
  o4.xyz = r0.yzw;
  o5.x = v5.x;
  return;
}