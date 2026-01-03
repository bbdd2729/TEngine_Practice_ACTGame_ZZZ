// ---- Created with 3Dmigoto v1.2.45 on Sat Jan  3 12:47:50 2026
Texture2D<float4> t7 : register(t7);

Texture2D<float4> t6 : register(t6);

Texture2D<float4> t5 : register(t5);

Texture2D<float4> t4 : register(t4);

Texture2D<float4> t3 : register(t3);

Texture2D<float4> t2 : register(t2);

Texture2D<float4> t1 : register(t1);

Texture2D<float4> t0 : register(t0);

SamplerState s7_s : register(s7);

SamplerState s6_s : register(s6);

SamplerState s5_s : register(s5);

SamplerState s4_s : register(s4);

SamplerState s3_s : register(s3);

SamplerComparisonState s2_s : register(s2);

SamplerState s1_s : register(s1);

SamplerState s0_s : register(s0);

cbuffer cb3 : register(b3)
{
  float4 cb3[75];
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
  float4 cb0[164];
}




// 3Dmigoto declarations
#define cmp -
Texture1D<float4> IniParams : register(t120);
Texture2D<float4> StereoParams : register(t125);


void main( 
  float4 v0 : SV_POSITION0,
  float4 v1 : TEXCOORD0,
  float4 v2 : TEXCOORD1,
  float4 v3 : TEXCOORD2,
  float3 v4 : TEXCOORD4,
  nointerpolation uint v5 : SV_InstanceID0,
  out float4 o0 : SV_Target0,
  out float4 o1 : SV_Target1,
  out float4 o2 : SV_Target2,
  out float4 o3 : SV_Target3)
{
  float4 r0,r1,r2,r3,r4,r5,r6,r7,r8,r9,r10;
  uint4 bitmask, uiDest;
  float4 fDest;

  r0.xyz = t6.SampleBias(s5_s, v1.xy, cb0[161].z).xyw;
  r0.x = r0.z * r0.x;
  r0.xy = r0.xy * float2(2,2) + float2(-1,-1);
  r0.xy = cb3[8].xx * r0.xy;
  r0.w = dot(r0.xy, r0.xy);
  r0.w = min(1, r0.w);
  r0.w = 1 + -r0.w;
  r0.z = sqrt(r0.w);
  r1.xyz = t5.SampleBias(s4_s, v1.xy, cb0[161].y).xyz;
  r2.xyz = t7.SampleBias(s6_s, v1.xy, cb0[161].w).xyz;
  r3.xyz = cb3[5].xyz * r1.xyz;
  o2.z = cb3[7].z * r2.x;
  r0.w = saturate(1 + -r2.y);
  r0.w = min(0.99000001, r0.w);
  r1.w = cb3[7].w * r0.w;
  r2.xyw = cb3[6].xyz * r3.xyz;
  r4.xyz = v3.yzx * v2.zxy;
  r4.xyz = v2.yzx * v3.zxy + -r4.xyz;
  r4.xyz = v3.www * r4.xyz;
  r5.xyz = r4.xyz * r0.yyy;
  r5.xyz = r0.xxx * v3.xyz + r5.xyz;
  r5.xyz = r0.zzz * v2.xyz + r5.xyz;
  r3.w = dot(r5.xyz, r5.xyz);
  r3.w = rsqrt(r3.w);
  r5.xyz = r5.xyz * r3.www;
  r6.xy = cmp(float2(0.5,0.5) < cb0[163].zw);
  if (r6.y != 0) {
    r7.xyzw = t2.SampleBias(s3_s, v1.zw, cb0[161].x).xyzw;
    r7.xyzw = min(float4(0.996100008,0.996100008,0.996100008,0.996100008), r7.xyzw);
    r6.yzw = float3(0.5,0.5,0.5) * r7.xyz;
    r8.xyz = float3(1,1,1) + -r7.xyz;
    r7.xyz = r6.yzw / r8.xyz;
    r3.w = dot(v2.xyz, v2.xyz);
    r3.w = rsqrt(r3.w);
    r6.yzw = v2.xyz * r3.www;
    r3.w = saturate(dot(r5.xyz, r6.yzw));
    r3.w = r3.w * 0.699999988 + 0.300000012;
    r7.xyzw = r7.xyzw * r3.wwww;
    r6.yzw = r7.www * float3(0.600000024,0.600000024,0.600000024) + r7.xyz;
    r6.yzw = r6.xxx ? r7.xyz : r6.yzw;
    r6.yzw = cb3[56].xxx * r6.yzw;
  } else {
    if (r6.x != 0) {
      r7.xyz = t4.SampleBias(s7_s, v1.zw, cb0[161].x).xyz;
    } else {
      r8.xyz = t2.SampleBias(s3_s, v1.zw, cb0[161].x).xyz;
      r3.w = dot(v2.xyz, v2.xyz);
      r3.w = rsqrt(r3.w);
      r9.xyz = v2.xyz * r3.www;
      r3.w = saturate(dot(r5.xyz, r9.xyz));
      r3.w = r3.w * 0.699999988 + 0.300000012;
      r7.xyz = r8.xyz * r3.www;
    }
    r6.yzw = cb3[56].xxx * r7.xyz;
  }
  r3.w = max(r2.x, r2.y);
  r3.w = max(r3.w, r2.w);
  r2.z = r3.w * r2.z;
  r4.w = cmp(0 < r3.w);
  r3.w = max(9.99999975e-05, r3.w);
  r2.xyw = r2.xyw / r3.www;
  r2.xyw = r4.www ? r2.xyw : 0;
  r3.w = saturate(r2.z);
  r1.xyz = -r1.xyz * cb3[5].xyz + r2.xyw;
  r1.xyz = r3.www * r1.xyz + r3.xyz;
  r2.x = cmp(0 != cb3[67].x);
  if (r2.x != 0) {
    r2.x = (int)v5.x + asint(cb1[0].x);
    r2.x = (int)r2.x * 11;
    r3.xyz = cb2[r2.x+5].xyz * v4.yyy;
    r3.xyz = cb2[r2.x+4].xyz * v4.xxx + r3.xyz;
    r3.xyz = cb2[r2.x+6].xyz * v4.zzz + r3.xyz;
    r2.xyw = cb2[r2.x+7].xyz + r3.xyz;
  } else {
    r2.xyw = v4.xyz;
  }
  r3.x = cmp(cb0[53].y < 0.5);
  if (r3.x != 0) {
    r3.yzw = v2.xyz * float3(0.449999988,0.449999988,0.449999988) + r2.xyw;
    r3.y = r3.y * cb0[31].w + cb0[33].w;
    r3.w = r3.w * cb0[32].w + cb0[34].w;
    r3.z = 100.050003 + r3.z;
    r3.z = 0.00333333341 * r3.z;
    r5.x = cb0[33].x * r3.y;
    r5.y = cb0[34].x * r3.w;
    r3.yw = float2(0.5,0.5) + r5.xy;
    r3.yw = floor(r3.yw);
    r5.xy = r5.xy + -r3.yw;
    r7.xyzw = float4(0.5,1,0.5,1) + r5.xxyy;
    r8.xw = r7.xz * r7.xz;
    r5.zw = r8.xw * float2(0.5,0.5) + -r5.xy;
    r7.xz = float2(1,1) + -r5.xy;
    r9.xy = min(float2(0,0), r5.xy);
    r7.xz = -r9.xy * r9.xy + r7.xz;
    r5.xy = max(float2(0,0), r5.xy);
    r5.xy = -r5.xy * r5.xy + r7.yw;
    r9.y = r7.x;
    r9.xz = r5.zx;
    r9.w = r8.x;
    r9.xyzw = float4(0.444440007,0.444440007,0.444440007,0.222220004) * r9.xyzw;
    r8.y = r7.z;
    r8.xz = r5.wy;
    r5.xyzw = float4(0.444440007,0.444440007,0.444440007,0.222220004) * r8.xyzw;
    r7.xyzw = r9.xzxz + r9.ywyw;
    r8.xyzw = r5.xxzz + r5.yyww;
    r5.xz = r9.yw / r7.zw;
    r5.yw = r5.yw / r8.yw;
    r5.xyzw = float4(-1.5,-1.5,0.5,0.5) + r5.xyzw;
    r9.xy = cb0[31].xx * r5.xz;
    r9.zw = cb0[32].xx * r5.yw;
    r5.x = cb0[31].x;
    r5.y = cb0[32].x;
    r10.xyzw = r3.ywyw * r5.xyxy + r9.xzyz;
    r5.xyzw = r3.ywyw * r5.xyxy + r9.xwyw;
    r7.xyzw = r8.xyzw * r7.xyzw;
    r3.y = t1.SampleCmpLevelZero(s2_s, r10.xy, r3.z).x;
    r3.w = t1.SampleCmpLevelZero(s2_s, r10.zw, r3.z).x;
    r3.w = r7.y * r3.w;
    r3.y = r7.x * r3.y + r3.w;
    r3.w = t1.SampleCmpLevelZero(s2_s, r5.xy, r3.z).x;
    r3.y = r7.z * r3.w + r3.y;
    r3.z = t1.SampleCmpLevelZero(s2_s, r5.zw, r3.z).x;
    r3.y = r7.w * r3.z + r3.y;
    r3.z = saturate(v2.y);
    r3.w = 1 + -r3.z;
    r3.w = sqrt(r3.w);
    r4.w = r3.z * -0.0187292993 + 0.0742610022;
    r4.w = r4.w * r3.z + -0.212114394;
    r3.z = r4.w * r3.z + 1.57072878;
    r3.z = -r3.z * r3.w + 1.57079637;
    r3.z = r3.z * 2 + -1.57079637;
    r3.z = sin(r3.z);
    r3.z = 1 + r3.z;
    r3.y = r3.z * r3.y;
    r3.y = 0.5 * r3.y;
  } else {
    r3.y = 1;
  }
  r3.zw = cb0[35].yy * r2.xw;
  r3.z = t0.Sample(s1_s, r3.zw).x;
  r3.z = r3.z * cb0[36].y + cb0[37].y;
  r3.w = cmp(cb0[31].z < r2.y);
  r2.y = cmp(r2.y < cb0[32].z);
  r2.y = r2.y ? r3.w : 0;
  if (r2.y != 0) {
    r2.x = r2.x * cb0[31].y + cb0[33].y;
    r2.y = r2.w * cb0[32].y + cb0[34].y;
    r5.xyzw = t3.Sample(s0_s, r2.xy).xyzw;
    r2.x = dot(r5.xyzw, float4(0.25,0.25,0.25,0.25));
  } else {
    r2.x = 0;
  }
  r2.x = saturate(r2.x * 1.01010096 + -0.0101010101);
  r2.x = max(r3.z, r2.x);
  r2.x = min(1, r2.x);
  r2.x = cb0[37].x * r2.x;
  r2.x = saturate(r2.x * r3.y);
  r2.y = r1.w * cb0[36].x + cb0[35].x;
  r2.y = r2.x * r2.y;
  r3.yzw = r1.xyz * r1.xyz + -r1.xyz;
  o1.xyz = r2.yyy * r3.yzw + r1.xyz;
  r1.x = cb3[8].y * r2.x;
  r1.x = r3.x ? r1.x : r2.x;
  r1.y = -cb0[51].y + r1.x;
  r1.z = 1 + -cb0[51].y;
  r1.y = saturate(r1.y / r1.z);
  r0.w = -r0.w * cb3[7].w + cb0[52].y;
  o2.y = r1.y * r0.w + r1.w;
  r0.w = -0.449999988 + r1.x;
  r0.w = saturate(2 * r0.w);
  r1.xyz = float3(0,0,1) + -r0.xyz;
  r1.xyz = r0.www * r1.xyz + r0.xyz;
  r0.xyz = r3.xxx ? r1.xyz : r0.xyz;
  r1.xyz = r0.yyy * r4.xyz;
  r0.xyw = r0.xxx * v3.xyz + r1.xyz;
  r0.xyz = r0.zzz * v2.xyz + r0.xyw;
  r0.w = dot(r0.xyz, r0.xyz);
  r0.w = rsqrt(r0.w);
  r0.xyz = r0.xyz * r0.www;
  r0.w = cmp(0.5 < cb3[7].x);
  r1.x = cmp(0.5 < cb3[74].z);
  r1.y = cmp(0.5 < cb3[8].w);
  r0.w = r0.w ? 0.670000 : 0;
  r1.x = r1.x ? 0.340000 : 0;
  o1.w = r1.x + r0.w;
  r0.w = cmp(0.5 >= cb3[9].x);
  r1.x = cb0[41].z * r2.z;
  r0.w = r0.w ? r1.x : r2.z;
  r0.w = sqrt(r0.w);
  r0.w = 0.0500000007 * r0.w;
  o2.x = min(1, r0.w);
  o2.w = r1.y ? 1.00999999 : 0.670000017;
  o3.xyz = r0.xyz * float3(0.5,0.5,0.5) + float3(0.5,0.5,0.5);
  o0.xyz = r6.yzw;
  o0.w = 1;
  o3.w = 0;
  return;
}