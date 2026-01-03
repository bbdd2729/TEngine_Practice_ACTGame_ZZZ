// ---- Created with 3Dmigoto v1.2.45 on Sat Jan  3 09:45:45 2026
Texture2D<float4> t8 : register(t8);

Texture2DArray<float4> t7 : register(t7);

Texture2D<float4> t6 : register(t6);

Texture2D<float4> t5 : register(t5);

Texture2D<float4> t4 : register(t4);

Texture2D<float4> t3 : register(t3);

Texture2D<float4> t2 : register(t2);

Texture2DArray<float4> t0 : register(t0);

SamplerState s2_s : register(s2);

SamplerComparisonState s1_s : register(s1);

SamplerState s0_s : register(s0);

cbuffer cb4 : register(b4)
{
  float4 cb4[167];
}

cbuffer cb3 : register(b3)
{
  float4 cb3[41];
}

cbuffer cb2 : register(b2)
{
  float4 cb2[27];
}

cbuffer cb1 : register(b1)
{
  float4 cb1[29];
}

cbuffer cb0 : register(b0)
{
  float4 cb0[201];
}




// 3Dmigoto declarations
#define cmp -
Texture1D<float4> IniParams : register(t120);
Texture2D<float4> StereoParams : register(t125);


void main( 
  float4 v0 : TEXCOORD0,
  float4 v1 : TEXCOORD1,
  float4 v2 : TEXCOORD2,
  float4 v3 : TEXCOORD3,
  float4 v4 : TEXCOORD4,
  float4 v5 : TEXCOORD5,
  float4 v6 : TXCOORDD6,
  float4 v7 : TEXCOORD7,
  float3 v8 : TEXCOORD8,
  float4 v9 : SV_POSITION0,
  uint v10 : SV_IsFrontFace0,
  out float4 o0 : SV_Target0,
  out float4 o1 : SV_Target1,
  out float4 o2 : SV_Target2,
  out float4 o3 : SV_Target3)
{
// Needs manual fix for instruction: 
// unknown dcl_: dcl_resource_structured t1, 128
  float4 r0,r1,r2,r3,r4,r5,r6,r7,r8,r9,r10,r11,r12,r13,r14,r15,r16,r17,r18,r19,r20,r21;
  uint4 bitmask, uiDest;
  float4 fDest;

  r0.x = v10.x ? 1 : -1;
  r0.y = cmp(0.5 < cb1[28].y);
  r0.zw = v5.xy / v5.ww;
  r1.xy = v6.xy / v6.ww;
  r1.xy = -r1.xy + r0.zw;
  r0.zw = float2(0.5,-0.5) * r1.xy;
  r0.zw = sqrt(abs(r0.zw));
  r1.z = -r1.y;
  r1.yw = cmp(float2(0,0) < r1.xz);
  r1.xz = cmp(r1.xz < float2(0,0));
  r1.xy = (int2)-r1.yw + (int2)r1.xz;
  r1.xy = (int2)r1.xy;
  r0.zw = r1.xy * r0.zw;
  r0.zw = r0.zw * float2(0.5,0.5) + float2(0.498039216,0.498039216);
  o2.xy = r0.yy ? r0.zw : float2(0.497999996,0.497999996);
  r0.y = cmp(cb3[40].x == 1.000000);
  r0.zw = cmp(float2(0,0) != cb4[151].wz);
  r1.x = cmp(0 != cb4[152].x);
  bitmask.x = ((~(-1 << 1)) << 3) & 0xffffffff;  r1.x = (((uint)r1.x << 3) & bitmask.x) | ((uint)0 & ~bitmask.x);
  bitmask.y = ((~(-1 << 1)) << 2) & 0xffffffff;  r0.y = (((uint)r0.y << 2) & bitmask.y) | ((uint)0 & ~bitmask.y);
  bitmask.w = ((~(-1 << 1)) << 1) & 0xffffffff;  r0.w = (((uint)r0.w << 1) & bitmask.w) | ((uint)0 & ~bitmask.w);
  r0.y = (int)r1.x + (int)r0.y;
  r0.y = (int)r0.y + (int)r0.w;
  bitmask.y = ((~(-1 << 1)) << 0) & 0xffffffff;  r0.y = (((uint)r0.z << 0) & bitmask.y) | ((uint)r0.y & ~bitmask.y);
  r0.y = (uint)r0.y;
  o2.z = 0.00392156886 * r0.y;
  r0.yz = cmp(float2(0.5,0.5) < cb4[147].xy);
  r0.w = cmp((int)v10.x == 0);
  r0.y = r0.w ? r0.y : 0;
  r0.w = cmp(1 < v0.z);
  r0.z = r0.w ? r0.z : 0;
  r0.z = (int)r0.z | (int)r0.y;
  r0.zw = r0.zz ? v0.zw : v0.xy;
  r1.xyz = t3.SampleBias(s0_s, r0.zw, cb0[191].x).xyz;
  r0.z = cmp(0.5 < cb4[133].x);
  r2.xyz = cb4[57].xyz * r1.xyz;
  r0.yw = r0.yy ? v0.zw : v0.xy;
  r3.xyz = t4.SampleBias(s0_s, r0.yw, cb0[191].x).xyz;
  r3.xyz = saturate(r3.xyz);
  r3.xyz = r3.xyz * float3(2,2,2) + float3(-1.00399995,-1.00399995,-1);
  r3.xy = cb4[137].zz * r3.xy;
  r1.w = dot(r3.xy, r3.xy);
  r1.w = min(1, r1.w);
  r1.w = 1 + -r1.w;
  r1.w = sqrt(r1.w);
  r0.x = r1.w * r0.x;
  r4.xyz = v4.xyz * r3.yyy;
  r3.xyw = r3.xxx * v3.xyz + r4.xyz;
  r3.xyw = r0.xxx * v2.xyz + r3.xyw;
  r0.x = dot(r3.xyw, r3.xyw);
  r0.x = rsqrt(r0.x);
  r3.xyw = r3.xyw * r0.xxx;
  r4.xyz = t5.SampleBias(s0_s, r0.yw, cb0[191].x).zxy;
  r4.xyz = saturate(r4.xyz);
  r0.x = cb4[137].w * r4.z;
  r5.xyz = t6.SampleBias(s0_s, r0.yw, cb0[191].x).xyz;
  r5.xyz = saturate(r5.xyz);
  r0.yw = cmp(float2(0.5,0.5) < cb4[144].yz);
  r0.w = r0.w ? r0.y : 0;
  r1.w = -0.200000003 + r5.z;
  r1.w = 1.25 * r1.w;
  r1.w = max(0, r1.w);
  r1.w = r0.w ? r1.w : r5.z;
  r0.y = r0.y ? r1.w : 0;
  r1.w = 5 * r4.y;
  r1.w = floor(r1.w);
  r1.w = 4 + -r1.w;
  r1.w = max(0, r1.w);
  r1.w = (int)r1.w;
  r2.w = cmp((int)r1.w == asint(cb4[132].y));
  r4.w = r2.w ? 0.000000 : 0;
  r5.w = cmp(0.5 < cb4[137].x);
  r6.xy = cb4[137].yy * v0.xy;
  r6.xyz = t2.SampleBias(s0_s, r6.xy, cb0[191].x).xyz;
  r1.xyz = r1.xyz * cb4[57].xyz + r6.xyz;
  r1.xyz = float3(-0.5,-0.5,-0.5) + r1.xyz;
  r1.xyz = max(float3(0,0,0), r1.xyz);
  r1.xyz = r5.www ? r1.xyz : r2.xyz;
  r6.xyzw = cmp(r4.yyyy < float4(0.200000003,0.400000006,0.600000024,0.800000012));
  r2.x = r6.w ? cb4[134].y : cb4[134].x;
  r2.x = r6.z ? cb4[134].z : r2.x;
  r2.x = r6.y ? cb4[134].w : r2.x;
  r2.x = r6.x ? cb4[135].x : r2.x;
  r7.x = v2.w;
  r7.y = v3.w;
  r7.z = v4.w;
  r8.xyz = cb0[53].xyz + -r7.xyz;
  r2.y = dot(r8.xyz, r8.xyz);
  r2.z = max(1.17549435e-38, r2.y);
  r2.z = rsqrt(r2.z);
  r9.xyz = r8.xyz * r2.zzz;
  r4.y = sqrt(r2.y);
  r5.w = cmp(0 < asint(cb0[188].z));
  if (r5.w != 0) {
    r7.w = (int)cb3[2].z;
    r8.w = asint(cb0[188].z) + -1;
    r7.w = min((int)r8.w, (int)r7.w);
    r7.w = max(0, (int)r7.w);
  // Missing reflection info for shader. No names possible.
  // Known bad code for instruction (needs manual fix):
    ld_structured_indexable(structured_buffer, stride=128)(mixed,mixed,mixed,mixed) r10.xyzw, r7.w, l(0), t1.xyzw
  r10.x = no_StructuredBufferName[no_srcAddressRegister].no_srcByteOffsetName.swiz;
  r10.y = no_StructuredBufferName[no_srcAddressRegister].no_srcByteOffsetName.swiz;
  r10.z = no_StructuredBufferName[no_srcAddressRegister].no_srcByteOffsetName.swiz;
  r10.w = no_StructuredBufferName[no_srcAddressRegister].no_srcByteOffsetName.swiz;
  // Missing reflection info for shader. No names possible.
  // Known bad code for instruction (needs manual fix):
    ld_structured_indexable(structured_buffer, stride=128)(mixed,mixed,mixed,mixed) r11.xyzw, r7.w, l(16), t1.xyzw
  r11.x = no_StructuredBufferName[no_srcAddressRegister].no_srcByteOffsetName.swiz;
  r11.y = no_StructuredBufferName[no_srcAddressRegister].no_srcByteOffsetName.swiz;
  r11.z = no_StructuredBufferName[no_srcAddressRegister].no_srcByteOffsetName.swiz;
  r11.w = no_StructuredBufferName[no_srcAddressRegister].no_srcByteOffsetName.swiz;
  // Missing reflection info for shader. No names possible.
  // Known bad code for instruction (needs manual fix):
    ld_structured_indexable(structured_buffer, stride=128)(mixed,mixed,mixed,mixed) r12.y, r7.w, l(32), t1.xxxx
  r12.y = no_StructuredBufferName[no_srcAddressRegister].no_srcByteOffsetName.swiz;
    r12.x = r11.w;
  } else {
    r12.xy = float2(0,0);
    r10.xyzw = float4(0,0,0,0);
    r11.xyz = float3(0,0,0);
  }
  r11.xyz = r11.xyz + -r7.xyz;
  r7.w = dot(r11.xyz, r11.xyz);
  r7.w = max(1.17549435e-38, r7.w);
  r8.w = rsqrt(r7.w);
  r13.xyz = r11.xyz * r8.www;
  r9.w = r10.w * r10.w;
  r7.w = r7.w / r9.w;
  r7.w = 1 + -r7.w;
  r7.w = max(0, r7.w);
  r10.xyz = -cb0[189].xyz + r10.xyz;
  r10.xyz = r7.www * r10.xyz + cb0[189].xyz;
  r9.w = -1 + r7.w;
  r7.w = r7.w * r9.w + 1;
  r9.w = cmp(0.5 < cb0[22].x);
  if (r9.w != 0) {
    r9.w = r6.w ? cb4[163].x : cb4[162].w;
    r9.w = r6.z ? cb4[163].y : r9.w;
    r9.w = r6.y ? cb4[163].z : r9.w;
    r9.w = r6.x ? cb4[163].w : r9.w;
    r14.xyz = r3.xyw * cb4[135].yyy + r7.xyz;
    r14.xyz = -cb3[39].xyz + r14.xyz;
    r15.xyz = cb3[34].xyz * r14.yyy;
    r14.xyw = cb3[33].xyz * r14.xxx + r15.xyz;
    r14.xyz = cb3[35].xyz * r14.zzz + r14.xyw;
    r14.xyz = cb3[36].xyz + r14.xyz;
    r12.zw = r14.xy * cb3[38].xy + cb3[38].zw;
    r14.xy = -cb0[200].xy + r12.zw;
    r10.w = t8.SampleCmpLevelZero(s1_s, r14.xy, r14.z).x;
    r15.xyzw = cb0[200].xyxy * float4(-1,1,1,-1) + r12.zwzw;
    r11.w = t8.SampleCmpLevelZero(s1_s, r15.xy, r14.z).x;
    r10.w = r11.w + r10.w;
    r11.w = t8.SampleCmpLevelZero(s1_s, r15.zw, r14.z).x;
    r10.w = r11.w + r10.w;
    r14.xy = cb0[200].xy + r12.zw;
    r11.w = t8.SampleCmpLevelZero(s1_s, r14.xy, r14.z).x;
    r10.w = r11.w + r10.w;
    r15.xyzw = cb0[200].xyxy * float4(-1.41421294,0,1.41421294,0) + r12.zwzw;
    r11.w = t8.SampleCmpLevelZero(s1_s, r15.xy, r14.z).x;
    r10.w = r11.w + r10.w;
    r11.w = t8.SampleCmpLevelZero(s1_s, r15.zw, r14.z).x;
    r10.w = r11.w + r10.w;
    r15.xyzw = cb0[200].xyxy * float4(0,-1.41421294,0,1.41421294) + r12.zwzw;
    r11.w = t8.SampleCmpLevelZero(s1_s, r15.xy, r14.z).x;
    r10.w = r11.w + r10.w;
    r11.w = t8.SampleCmpLevelZero(s1_s, r15.zw, r14.z).x;
    r10.w = r11.w + r10.w;
    r11.w = t8.SampleCmpLevelZero(s1_s, r12.zw, r14.z).x;
    r10.w = r11.w + r10.w;
    r10.w = r10.w * 0.111100003 + -1;
    r9.w = r10.w * r9.w;
    r9.w = cb3[37].x * r9.w;
    r14.xyz = -cb2[20].xyz + r7.xyz;
    r15.xyz = -cb2[21].xyz + r7.xyz;
    r16.xyz = -cb2[22].xyz + r7.xyz;
    r17.xyz = -cb2[23].xyz + r7.xyz;
    r14.x = dot(r14.xyz, r14.xyz);
    r14.y = dot(r15.xyz, r15.xyz);
    r14.z = dot(r16.xyz, r16.xyz);
    r14.w = dot(r17.xyz, r17.xyz);
    r14.xyzw = cmp(r14.xyzw < cb2[24].xyzw);
    r15.xyzw = r14.xyzw ? float4(1,1,1,1) : 0;
    r14.xyz = r14.xyz ? float3(-1,-1,-1) : float3(-0,-0,-0);
    r14.xyz = r15.yzw + r14.xyz;
    r15.yzw = max(float3(0,0,0), r14.xyz);
    r10.w = dot(r15.xyzw, float4(4,3,2,1));
    r14.z = 4 + -r10.w;
    r10.w = (uint)r14.z;
    r10.w = (uint)r10.w << 2;
    r15.xyz = cb2[r10.w+1].xyz * v3.www;
    r15.xyz = cb2[r10.w+0].xyz * v2.www + r15.xyz;
    r15.xyz = cb2[r10.w+2].xyz * v4.www + r15.xyz;
    r15.xyz = cb2[r10.w+3].xyz + r15.xyz;
    r12.zw = float2(1024,1024) * r15.xy;
    r12.zw = frac(r12.zw);
    r10.w = dot(r12.zw, float2(12.9898005,78.2330017));
    r10.w = sin(r10.w);
    r10.w = 43758.5469 * r10.w;
    r10.w = frac(r10.w);
    sincos(r10.w, r16.x, r17.x);
    r18.xyzw = float4(1.29999995,1.29999995,1.29999995,1.29999995) * cb2[26].xxyy;
    r16.xz = r16.xx;
    r16.yw = r17.xx;
    r16.xyzw = r16.xyzw * r18.xyzw;
    r17.xyzw = float4(-0.172399998,-0.978299975,-0.978299975,0.172399998) * r16.xyzw;
    r12.zw = r17.xz + r17.yw;
    r14.xy = r12.zw + r15.xy;
    r10.w = t0.SampleCmpLevelZero(s1_s, r14.xyz, r15.z).x;
    r17.xyzw = float4(0.87470001,0.484600008,0.484600008,-0.87470001) * r16.xyzw;
    r12.zw = r17.xz + r17.yw;
    r14.xy = r12.zw + r15.xy;
    r11.w = t0.SampleCmpLevelZero(s1_s, r14.xyz, r15.z).x;
    r10.w = r11.w + r10.w;
    r17.xyzw = float4(-0.968299985,-0.0373999998,-0.0373999998,0.968299985) * r16.xyzw;
    r12.zw = r17.xz + r17.yw;
    r14.xy = r12.zw + r15.xy;
    r11.w = t0.SampleCmpLevelZero(s1_s, r14.xyz, r15.z).x;
    r10.w = r11.w + r10.w;
    r17.xyzw = float4(0.278299987,0.41960001,0.41960001,-0.278299987) * r16.xyzw;
    r12.zw = r17.xz + r17.yw;
    r14.xy = r12.zw + r15.xy;
    r11.w = t0.SampleCmpLevelZero(s1_s, r14.xyz, r15.z).x;
    r10.w = r11.w + r10.w;
    r17.xyzw = float4(-0.150700003,0.839100003,0.839100003,-0.150700003) * r16.xyzw;
    r12.zw = r17.xz + r17.yw;
    r14.xy = r12.zw + r15.xy;
    r11.w = t0.SampleCmpLevelZero(s1_s, r14.xyz, r15.z).x;
    r10.w = r11.w + r10.w;
    r17.xyzw = float4(-0.641700029,0.479299992,0.479299992,-0.641700029) * r16.xyzw;
    r12.zw = r17.xz + r17.yw;
    r14.xy = r12.zw + r15.xy;
    r11.w = t0.SampleCmpLevelZero(s1_s, r14.xyz, r15.z).x;
    r10.w = r11.w + r10.w;
    r17.xyzw = float4(0.577899992,-0.816100001,-0.816100001,0.577899992) * r16.xyzw;
    r12.zw = r17.xz + r17.yw;
    r14.xy = r12.zw + r15.xy;
    r11.w = t0.SampleCmpLevelZero(s1_s, r14.xyz, r15.z).x;
    r10.w = r11.w + r10.w;
    r17.xyzw = float4(-0.540899992,-0.458799988,-0.458799988,0.540899992) * r16.xyzw;
    r12.zw = r17.xz + r17.yw;
    r14.xy = r12.zw + r15.xy;
    r11.w = t0.SampleCmpLevelZero(s1_s, r14.xyz, r15.z).x;
    r10.w = r11.w + r10.w;
    r17.xyzw = float4(0.704400003,-0.1919,-0.1919,0.704400003) * r16.xyzw;
    r12.zw = r17.xz + r17.yw;
    r14.xy = r12.zw + r15.xy;
    r11.w = t0.SampleCmpLevelZero(s1_s, r14.xyz, r15.z).x;
    r10.w = r11.w + r10.w;
    r17.xyzw = float4(0.105300002,-0.446399987,-0.446399987,0.105300002) * r16.xyzw;
    r12.zw = r17.xz + r17.yw;
    r14.xy = r12.zw + r15.xy;
    r11.w = t0.SampleCmpLevelZero(s1_s, r14.xyz, r15.z).x;
    r10.w = r11.w + r10.w;
    r16.xyzw = float4(-0.206599995,0.0661000013,0.0661000013,-0.206599995) * r16.xyzw;
    r12.zw = r16.xz + r16.yw;
    r14.xy = r12.zw + r15.xy;
    r11.w = t0.SampleCmpLevelZero(s1_s, r14.xyz, r15.z).x;
    r10.w = r11.w + r10.w;
    r10.w = cb2[25].x * r10.w;
    r11.w = 1 + -cb2[25].x;
    r10.w = r10.w * 0.0908999965 + r11.w;
    r11.w = cmp(0 >= r15.z);
    r12.z = cmp(r15.z >= 1);
    r11.w = (int)r11.w | (int)r12.z;
    r10.w = r11.w ? 1 : r10.w;
    r11.w = 1 + -r12.x;
    r11.w = cb3[40].y * r11.w + r12.x;
    r10.w = r11.w * r10.w;
    r11.w = saturate(r12.y * 2 + -1);
    r12.x = cb0[189].w * r11.w;
    r12.y = cmp(0.5 < cb3[37].x);
    r9.w = r11.w * r9.w + 1;
    r9.w = min(1, r9.w);
    r9.w = r12.y ? r9.w : 1;
    r9.w = r9.w * r10.w;
    r10.w = 1 + -cb4[133].w;
    r10.w = r9.w * cb4[133].w + r10.w;
    r11.w = cb4[133].w * r12.x;
    r12.x = -r12.x * cb4[133].w + 1;
    r9.w = r9.w * r11.w + r12.x;
  } else {
    r10.w = 1;
    r9.w = 1;
  }
  r11.w = saturate(2.5 * cb3[1].w);
  r12.x = 1 + -r11.w;
  r9.w = r12.x * r9.w + r11.w;
  r12.xyz = float3(-0.800000012,-0.800000012,-0.800000012) + r10.xyz;
  r12.xyz = cb4[57].www * r12.xyz + float3(0.800000012,0.800000012,0.800000012);
  r2.y = rsqrt(r2.y);
  r14.xyz = r8.xyz * r2.yyy;
  r11.xyz = r11.xyz * r8.www + -r14.xyz;
  r11.xyz = cb4[57].www * r11.xyz + r14.xyz;
  r11.xyz = r0.zzz ? r11.xyz : r13.xyz;
  r10.xyz = r0.zzz ? r12.xyz : r10.xyz;
  r2.y = cmp(cb3[3].w != 0.000000);
  r7.x = dot(cb3[3].xyz, r7.xyz);
  r7.x = saturate(-cb3[3].w + r7.x);
  r12.xyz = cb0[15].xyz * r10.xyz;
  r13.xyz = cb0[15].xyz + r10.xyz;
  r13.xyz = -r10.xyz * cb0[15].xyz + r13.xyz;
  r12.xyz = cb0[15].www * r13.xyz + r12.xyz;
  r12.xyz = r12.xyz + -r10.xyz;
  r7.xyz = r7.xxx * r12.xyz + r10.xyz;
  r7.xyz = r2.yyy ? r7.xyz : r10.xyz;
  r2.x = max(9.99999975e-06, r2.x);
  r2.y = rcp(r2.x);
  r8.w = dot(r3.xyw, r11.xyz);
  r10.x = 1 + r8.w;
  r10.y = 3 * r11.y;
  r10.y = min(1, r10.y);
  r10.y = -r10.y * 0.5 + r3.y;
  r10.y = saturate(1.5 + r10.y);
  r10.x = r10.x * r10.y + -1;
  r10.x = r10.x + -r8.w;
  r10.x = v7.y * r10.x + r8.w;
  r3.z = r3.z * 2 + r10.x;
  r10.x = 3 * r3.z;
  r12.xy = float2(1.5,4.5) * r2.xx;
  r10.yz = r3.zz * float2(3,3) + -r12.xy;
  r10.xyz = float3(3,1,-1) + r10.xyz;
  r2.x = -r2.x * 3 + 2;
  r12.xyw = r10.xyz / r2.xxx;
  r10.xyz = float3(1,1,1) + -r12.xyw;
  r13.xyz = float3(0.333299994,-0.333299994,-0.333299994) + r3.zzz;
  r13.xyz = r2.yyy * r13.xyz + float3(0.5,0.5,-0.5);
  r14.xyz = float3(1,1,1) + -r13.xyz;
  r15.xy = min(r14.yx, r12.yx);
  r12.xz = min(r13.xz, r10.yz);
  r15.z = r10.x;
  r15.w = r12.x;
  r10.xyz = saturate(r15.zyw);
  r15.y = saturate(min(r14.z, r13.y));
  r15.x = saturate(r15.x);
  r12.zw = saturate(r12.zw);
  r2.x = r10.w + -r9.w;
  r2.x = cb3[40].y * r2.x + r9.w;
  r13.xyzw = r2.xxxx * float4(2,2,-2,-2) + float4(0,-1,1,2);
  r13.x = saturate(min(r13.x, r13.w));
  r13.yz = saturate(r13.yz);
  r2.x = cmp(0.5 < cb3[40].y);
  if (r2.x != 0) {
    if (r5.w != 0) {
      r2.x = (int)cb3[2].z;
      r2.y = asint(cb0[188].z) + -1;
      r2.x = min((int)r2.x, (int)r2.y);
      r2.x = max(0, (int)r2.x);
    // Missing reflection info for shader. No names possible.
    // Known bad code for instruction (needs manual fix):
        ld_structured_indexable(structured_buffer, stride=128)(mixed,mixed,mixed,mixed) r2.x, r2.x, l(32), t1.xxxx
    r2.x = no_StructuredBufferName[no_srcAddressRegister].no_srcByteOffsetName.swiz;
    } else {
      r2.x = 0;
    }
    r2.x = cb0[189].w * r2.x;
    r14.y = r13.x * r2.x;
    r12.xy = -r13.zx * r2.xx + r13.zx;
    r2.y = r12.x + r12.y;
    r14.z = r13.y + r2.y;
    if (r5.w != 0) {
      r2.y = (int)cb3[2].z;
      r11.w = asint(cb0[188].z) + -1;
      r2.y = min((int)r11.w, (int)r2.y);
      r2.y = max(0, (int)r2.y);
    // Missing reflection info for shader. No names possible.
    // Known bad code for instruction (needs manual fix):
        ld_structured_indexable(structured_buffer, stride=128)(mixed,mixed,mixed,mixed) r2.y, r2.y, l(28), t1.xxxx
    r2.y = no_StructuredBufferName[no_srcAddressRegister].no_srcByteOffsetName.swiz;
    } else {
      r2.y = 0;
    }
    r13.xy = r14.yz * r2.yy;
    r12.xy = -r14.yz * r2.yy + r14.yz;
    r2.y = r12.x + r12.y;
    r13.z = r13.z * r2.x + r2.y;
  } else {
    if (r5.w != 0) {
      r2.x = (int)cb3[2].z;
      r2.y = asint(cb0[188].z) + -1;
      r2.x = min((int)r2.x, (int)r2.y);
      r2.x = max(0, (int)r2.x);
    // Missing reflection info for shader. No names possible.
    // Known bad code for instruction (needs manual fix):
        ld_structured_indexable(structured_buffer, stride=128)(mixed,mixed,mixed,mixed) r2.x, r2.x, l(32), t1.xxxx
    r2.x = no_StructuredBufferName[no_srcAddressRegister].no_srcByteOffsetName.swiz;
    } else {
      r2.x = 0;
    }
    r13.xz = r13.xz * r2.xx;
  }
  r2.x = 1 + -r10.x;
  r2.x = r2.x + -r10.y;
  r2.x = r2.x + -r10.z;
  r2.x = r13.z * r2.x + r10.z;
  r2.y = r13.x + r13.y;
  r12.xy = r15.xy * r2.yy;
  r2.y = r12.z + r12.w;
  r2.y = r2.y * r13.x + r12.y;
  r10.z = r13.y * r12.z;
  r11.w = v7.x * r10.x;
  r10.x = -r10.x * v7.x + r10.x;
  r10.x = r10.y + r10.x;
  r13.xzw = r6.www ? cb4[60].xyz : cb4[59].xyz;
  r13.xzw = r6.zzz ? cb4[61].xyz : r13.xzw;
  r13.xzw = r6.yyy ? cb4[62].xyz : r13.xzw;
  r13.xzw = r6.xxx ? cb4[63].xyz : r13.xzw;
  r14.xyz = r6.www ? cb4[65].xyz : cb4[64].xyz;
  r14.xyz = r6.zzz ? cb4[66].xyz : r14.xyz;
  r14.xyz = r6.yyy ? cb4[67].xyz : r14.xyz;
  r14.xyz = r6.xxx ? cb4[68].xyz : r14.xyz;
  r15.xyz = r2.www ? cb0[10].xyz : cb0[3].xyz;
  r16.xyz = r2.www ? cb0[11].xyz : cb0[4].xyz;
  r17.xyz = r2.www ? cb0[12].xyz : cb0[5].xyz;
  r18.xyz = r2.www ? cb0[9].xyz : cb0[6].xyz;
  r19.xyz = r2.www ? cb0[13].xyz : cb0[7].xyz;
  r20.xyz = r2.www ? cb0[14].xyz : cb0[8].xyz;
  r10.y = 0.437249988 * r4.y;
  r10.y = min(1, r10.y);
  r21.x = r10.y * cb3[1].w + -r10.y;
  r21.y = -r10.y * cb3[1].w + r10.y;
  r12.yz = float2(1,-1) + r21.xy;
  r12.yz = cb4[133].zz * r12.yz + float2(0,1);
  r13.xzw = float3(6.10351562e-05,6.10351562e-05,6.10351562e-05) + r13.xzw;
  r10.y = r13.x + r13.z;
  r10.y = r10.y + r13.w;
  r10.y = 0.333330005 * r10.y;
  r21.xyz = saturate(r13.xzw / r10.yyy);
  r13.xzw = r13.xzw * r12.zzz;
  r13.xzw = r21.xyz * r12.yyy + r13.xzw;
  r14.xyz = float3(6.10351562e-05,6.10351562e-05,6.10351562e-05) + r14.xyz;
  r10.y = r14.x + r14.y;
  r10.y = r10.y + r14.z;
  r10.y = 0.333330005 * r10.y;
  r21.xyz = saturate(r14.xyz / r10.yyy);
  r14.xyz = r14.xyz * r12.zzz;
  r14.xyz = r21.xyz * r12.yyy + r14.xyz;
  r15.xyz = r13.xzw * r15.xyz;
  r13.xzw = r13.xzw * r16.xyz;
  r16.xyz = r14.xyz * r17.xyz;
  r14.xyz = r14.xyz * r20.xyz;
  r17.xyz = float3(1.17549435e-38,1.17549435e-38,1.17549435e-38) + r7.xyz;
  r10.y = max(r17.x, r17.y);
  r10.y = max(r10.y, r17.z);
  r10.y = rcp(r10.y);
  if (r5.w != 0) {
    r12.y = (int)cb3[2].z;
    r12.z = asint(cb0[188].z) + -1;
    r12.y = min((int)r12.y, (int)r12.z);
    r12.y = max(0, (int)r12.y);
  // Missing reflection info for shader. No names possible.
  // Known bad code for instruction (needs manual fix):
    ld_structured_indexable(structured_buffer, stride=128)(mixed,mixed,mixed,mixed) r12.y, r12.y, l(32), t1.xxxx
  r12.y = no_StructuredBufferName[no_srcAddressRegister].no_srcByteOffsetName.swiz;
  } else {
    r12.y = 0;
  }
  r12.z = 1 + -r7.w;
  r7.w = r12.y * r12.z + r7.w;
  r17.xyz = r7.xyz * r7.www;
  r7.w = min(1, r10.y);
  r20.xyz = r17.xyz * r7.www;
  r18.xyz = r18.xyz * r10.zzz;
  r18.xyz = r19.xyz * r2.yyy + r18.xyz;
  r12.yzw = r12.www * r13.yyy + r18.xyz;
  r10.xyz = r16.xyz * r10.xxx;
  r10.xyz = r14.xyz * r11.www + r10.xyz;
  r10.xyz = r13.xzw * r2.xxx + r10.xyz;
  r10.xyz = r15.xyz * r12.xxx + r10.xyz;
  r10.xyz = r10.xyz * r20.xyz;
  r10.xyz = r17.xyz * r12.yzw + r10.xyz;
  r12.xyz = r10.xyz + -r7.xyz;
  r7.xyz = cb4[57].www * r12.xyz + r7.xyz;
  r7.xyz = r0.zzz ? r7.xyz : r10.xyz;
  r2.x = cmp(cb0[22].z >= 0.5);
  if (r2.x != 0) {
    r2.x = cmp(cb4[r1.w+10].x < 50);
    if (r2.x != 0) {
      r2.x = 5.0999999 * r5.z;
      r2.x = min(1, r2.x);
      r2.y = cmp(0.200000003 >= r5.z);
      r2.y = r2.y ? 1.000000 : 0;
      r2.x = r2.x * r2.y;
      r0.w = r0.w ? r2.x : r5.z;
      r2.x = cmp(cb4[144].z >= 0.5);
      r2.x = r2.x ? 1.000000 : 0;
      r0.w = -1 + r0.w;
      r0.w = r2.x * r0.w + 1;
      r2.xy = cb0[117].xy * r3.yy;
      r2.xy = cb0[116].xy * r3.xx + r2.xy;
      r2.xy = cb0[118].xy * r3.ww + r2.xy;
      r2.xy = r2.xy * float2(0.5,0.5) + float2(0.5,0.5);
      r5.z = cmp(0.5 < cb4[r1.w+15].z);
      if (r5.z != 0) {
        r5.x = cb4[r1.w+15].w * r5.x;
        r10.xy = cb4[r1.w+0].xy * v0.xy + cb4[r1.w+0].zw;
        r2.xy = r5.xx * r2.xy + r10.xy;
      }
      r10.x = cb4[r1.w+10].w * cb0[40].y;
      r10.y = cb4[r1.w+15].x * cb0[40].y;
      r10.xy = r10.xy + r2.xy;
      r10.z = cb4[r1.w+10].x;
      r12.xyzw = t7.Sample(s2_s, r10.xyz).xyzw;
      r10.xyz = cb4[r1.w+5].xyz * r12.xyz;
      r0.w = r12.w * r0.w;
      r2.x = cmp(cb4[r1.w+15].y < 0.5);
      if (r2.x != 0) {
        r2.x = saturate(cb4[r1.w+10].z * r0.w);
        r13.xyz = r10.xyz * cb4[r1.w+10].yyy + -r1.xyz;
        r1.xyz = r2.xxx * r13.xyz + r1.xyz;
      } else {
        r2.x = cmp(cb4[r1.w+15].y < 1.5);
        if (r2.x != 0) {
          r2.x = saturate(cb4[r1.w+10].z * r0.w);
          r13.xyz = r10.xyz * r2.xxx;
          r1.xyz = r13.xyz * cb4[r1.w+10].yyy + r1.xyz;
        } else {
          r12.xyz = r12.xyz * cb4[r1.w+5].xyz + float3(-0.5,-0.5,-0.5);
          r10.xyz = saturate(r12.xyz * cb4[r1.w+10].yyy + r10.xyz);
          r0.w = saturate(cb4[r1.w+10].z * r0.w);
          r10.xyz = float3(-0.5,-0.5,-0.5) + r10.xyz;
          r10.xyz = r0.www * r10.xyz + float3(0.5,0.5,0.5);
          r12.xyz = r10.xyz * r1.xyz;
          r13.xyz = r12.xyz + r12.xyz;
          r14.xyz = float3(1,1,1) + -r1.xyz;
          r14.xyz = r14.xyz + r14.xyz;
          r10.xyz = float3(1,1,1) + -r10.xyz;
          r10.xyz = -r14.xyz * r10.xyz + float3(1,1,1);
          r14.xyz = cmp(r1.xyz >= float3(0.5,0.5,0.5));
          r14.xyz = r14.xyz ? float3(1,1,1) : 0;
          r10.xyz = -r12.xyz * float3(2,2,2) + r10.xyz;
          r1.xyz = r14.xyz * r10.xyz + r13.xyz;
        }
      }
    }
  }
  r0.w = cmp(0.5 < v7.z);
  if (r2.w == 0) {
    r2.xyw = r0.www ? r3.xyw : v2.xyz;
    r1.w = dot(r1.xyz, float3(0.289999992,0.600000024,0.109999999));
    r5.x = cmp(v7.z < 0.5);
    r10.xy = r1.ww * float2(0.287499994,0.400000006) + float2(1.4375,1);
    r2.x = dot(r11.xyz, r2.xyw);
    r2.y = r2.x + -r8.w;
    r2.y = saturate(-r2.y * 3 + 1);
    r2.w = r2.y + r2.y;
    r2.y = sqrt(r2.y);
    r2.y = r2.w * r2.y;
    r2.y = min(1, r2.y);
    r2.w = r8.w * 0.5 + 0.5;
    r5.z = saturate(r8.w);
    r2.y = r2.w * r2.y + -r5.z;
    r2.y = r2.y * 0.5 + r5.z;
    r2.x = saturate(r2.x);
    r2.w = max(r1.y, r1.z);
    r2.w = max(r2.w, r1.x);
    r5.z = cmp(1 < r2.w);
    r12.xyz = r1.xyz / r2.www;
    r12.xyz = r5.zzz ? r12.xyz : r1.xyz;
    r2.w = 1 + -r10.x;
    r2.y = r2.y * r2.w + r10.x;
    r12.xyz = log2(r12.xyz);
    r12.xyz = r12.xyz * r2.yyy;
    r12.xyz = exp2(r12.xyz);
    r13.xyz = r12.xyz + -r1.xyz;
    r13.xyz = r13.xyz * float3(0.5,0.5,0.5) + r1.xyz;
    r12.xyz = -r13.xyz + r12.xyz;
    r2.xyw = r2.xxx * r12.xyz + r13.xyz;
    r1.w = -r1.w * 0.0500000007 + 1.04999995;
    r12.xyz = log2(r1.xyz);
    r10.xyz = r12.xyz * r10.yyy;
    r10.xyz = exp2(r10.xyz);
    r10.xyz = r10.xyz * r1.www;
    r1.xyz = r5.xxx ? r2.xyw : r10.xyz;
  }
  r1.w = -r0.x * 0.959999979 + 0.959999979;
  r2.xyw = r1.xyz * r1.www;
  r10.xyz = float3(-0.0399999991,-0.0399999991,-0.0399999991) + r1.xyz;
  r10.xyz = r0.xxx * r10.xyz + float3(0.0399999991,0.0399999991,0.0399999991);
  r5.x = -r5.y * cb4[138].x + 1;
  r5.x = r5.x * r5.x;
  r5.z = r5.x * 4 + 2;
  r7.w = r5.x * r5.x;
  r11.w = r5.x * r5.x + -1;
  r12.xyz = cb0[2].xyz + r7.xyz;
  r12.xyz = v8.xyz + r12.xyz;
  r12.x = dot(r12.xyz, float3(0.212672904,0.715152204,0.0721750036));
  r12.y = cb0[19].y + -cb0[19].x;
  r12.z = 1 / r12.y;
  r12.w = -cb0[19].x * r12.z + 1;
  r12.z = r12.x * r12.z + r12.w;
  r12.z = rcp(r12.z);
  r12.y = -r12.y * r12.z + cb0[19].y;
  r12.z = cmp(r12.x < cb0[19].x);
  r12.y = r12.z ? r12.x : r12.y;
  r12.x = 9.99999975e-05 + r12.x;
  r12.x = r12.y / r12.x;
  r12.yzw = r12.xxx * r7.xyz;
  r13.xyz = r6.www ? cb4[75].xyz : cb4[74].xyz;
  r13.xyz = r6.zzz ? cb4[76].xyz : r13.xyz;
  r13.xyz = r6.yyy ? cb4[77].xyz : r13.xyz;
  r13.xyz = r6.xxx ? cb4[78].xyz : r13.xyz;
  r13.w = r6.w ? cb4[141].w : cb4[141].z;
  r13.w = r6.z ? cb4[142].x : r13.w;
  r13.w = r6.y ? cb4[142].y : r13.w;
  r13.w = r6.x ? cb4[142].z : r13.w;
  r14.x = cmp(0.5 < r13.w);
  if (r14.x != 0) {
    r3.z = saturate(r3.z * 1.5 + -0.5);
    r14.x = r6.w ? cb4[143].x : cb4[142].w;
    r14.x = r6.z ? cb4[143].y : r14.x;
    r14.x = r6.y ? cb4[143].z : r14.x;
    r14.x = r6.x ? cb4[143].w : r14.x;
    r14.yzw = r8.xyz * r2.zzz + r11.xyz;
    r15.x = dot(r14.yzw, r14.yzw);
    r15.x = rsqrt(r15.x);
    r14.yzw = r15.xxx * r14.yzw;
    r14.y = saturate(dot(r3.xyw, r14.yzw));
    r3.z = r3.z * r3.z;
    r3.z = -r3.z * r14.y + 1;
    r3.z = r4.x + -r3.z;
    r14.x = max(9.99999975e-06, r14.x);
    r4.x = saturate(r3.z / r14.x);
  }
  r3.z = cb4[144].x * r4.x;
  r13.xyz = r3.zzz * r13.xyz;
  r13.xyz = r13.xyz * r10.xyz;
  r3.z = cmp(r13.w < 0.5);
  r8.xyz = r8.xyz * r2.zzz + r11.xyz;
  r2.z = dot(r8.xyz, r8.xyz);
  r2.z = rsqrt(r2.z);
  r8.xyz = r8.xyz * r2.zzz;
  r2.z = r6.w ? cb4[140].z : cb4[140].y;
  r2.z = r6.z ? cb4[140].w : r2.z;
  r2.z = r6.y ? cb4[141].x : r2.z;
  r2.z = r6.x ? cb4[141].y : r2.z;
  r4.x = r2.z * r8.w;
  r4.x = saturate(r4.x * 0.75 + 0.25);
  r13.w = dot(r3.xyw, r8.xyz);
  r13.w = r13.w * r2.z;
  r13.w = saturate(r13.w * 0.75 + 0.25);
  r8.x = dot(r11.xyz, r8.xyz);
  r2.z = r8.x * r2.z;
  r2.z = saturate(r2.z * 0.75 + 0.25);
  r8.x = r13.w * r13.w;
  r8.x = r8.x * r11.w + 1.00001001;
  r2.z = r2.z * r2.z;
  r8.x = r8.x * r8.x;
  r2.z = max(0.100000001, r2.z);
  r8.x = r8.x * r2.z;
  r5.z = r8.x * r5.z;
  r5.z = r7.w / r5.z;
  r5.y = saturate(-r5.y * cb4[138].x + r5.z);
  r5.y = r5.y * r4.x;
  r5.x = max(9.99999975e-06, r5.x);
  r5.x = r5.y / r5.x;
  r5.y = r6.w ? cb4[139].y : cb4[139].x;
  r5.y = r6.z ? cb4[139].z : r5.y;
  r5.y = r6.y ? cb4[139].w : r5.y;
  r5.y = r6.x ? cb4[140].x : r5.y;
  r5.z = r6.w ? cb4[166].x : cb4[165].w;
  r5.z = r6.z ? cb4[166].y : r5.z;
  r5.z = r6.y ? cb4[166].z : r5.z;
  r5.z = r6.x ? cb4[166].w : r5.z;
  r5.y = r5.y * r5.z;
  r5.x = r5.y * r5.x;
  r5.x = saturate(10 * r5.x);
  r5.x = 100 * r5.x;
  r2.z = 0.166663334 / r2.z;
  r2.z = min(1, r2.z);
  r2.z = r2.z * r4.x;
  r2.z = 100 * r2.z;
  r2.z = r3.z ? r5.x : r2.z;
  r5.xyz = r2.zzz * r13.xyz;
  r8.xyz = r5.xyz * r12.yzw;
  r5.xyz = r5.xyz * r12.yzw + float3(-1,-1,-1);
  r5.xyz = max(float3(0,0,0), r5.xyz);
  r8.xyz = r2.xyw * r12.yzw + r8.xyz;
  r2.z = cmp(cb4[144].y >= 0.5);
  r13.xyz = r6.www ? cb4[80].xyz : cb4[79].xyz;
  r13.xyz = r6.zzz ? cb4[81].xyz : r13.xyz;
  r13.xyz = r6.yyy ? cb4[82].xyz : r13.xyz;
  r13.xyz = r6.xxx ? cb4[83].xyz : r13.xyz;
  r13.xyz = r13.xyz * r0.yyy;
  r13.xyz = r13.xyz * r1.xyz;
  r13.xyz = r2.zzz ? r13.xyz : 0;
  r0.y = r13.x + r13.y;
  r0.y = r0.y + r13.z;
  r14.xyz = v8.xyz * r12.xxx;
  r8.xyz = r14.xyz * r2.xyw + r8.xyz;
  if (r5.w != 0) {
    r2.z = (int)cb3[2].z;
    r3.z = asint(cb0[188].z) + -1;
    r2.z = min((int)r3.z, (int)r2.z);
    r2.z = max(0, (int)r2.z);
  // Missing reflection info for shader. No names possible.
  // Known bad code for instruction (needs manual fix):
    ld_structured_indexable(structured_buffer, stride=128)(mixed,mixed,mixed,mixed) r15.xyz, r2.z, l(96), t1.xyzx
  r15.x = no_StructuredBufferName[no_srcAddressRegister].no_srcByteOffsetName.swiz;
  r15.y = no_StructuredBufferName[no_srcAddressRegister].no_srcByteOffsetName.swiz;
  r15.z = no_StructuredBufferName[no_srcAddressRegister].no_srcByteOffsetName.swiz;
  // Missing reflection info for shader. No names possible.
  // Known bad code for instruction (needs manual fix):
    ld_structured_indexable(structured_buffer, stride=128)(mixed,mixed,mixed,mixed) r16.xyz, r2.z, l(112), t1.xyzx
  r16.x = no_StructuredBufferName[no_srcAddressRegister].no_srcByteOffsetName.swiz;
  r16.y = no_StructuredBufferName[no_srcAddressRegister].no_srcByteOffsetName.swiz;
  r16.z = no_StructuredBufferName[no_srcAddressRegister].no_srcByteOffsetName.swiz;
  } else {
    r15.xyz = float3(0,0,0);
    r16.xyz = float3(0,0,0);
  }
  r2.z = cmp(0.5 < cb0[23].y);
  r15.xyz = r4.www ? r15.xyz : r16.xyz;
  r15.xyz = r2.zzz ? float3(0.0500000007,0.0500000007,0.0500000007) : r15.xyz;
  r16.xyz = r12.xxx * r1.xyz;
  r8.xyz = r15.xyz * r16.xyz + r8.xyz;
  r5.xyz = r8.xyz + r5.xyz;
  r1.x = cmp(0.5 >= cb0[188].w);
  if (r1.x != 0) {
    r1.x = dot(r9.xyz, r11.xyz);
    r1.x = saturate(-r1.x * 0.5 + 0.5);
    r1.y = r1.x * 0.800000012 + 0.200000003;
    r2.z = r3.y * 0.5 + 0.5;
    r3.z = r2.z * r2.z;
    r2.z = r4.w ? r2.z : r3.z;
    r2.z = -0.200000003 + r2.z;
    r2.z = saturate(1.25 * r2.z);
    r3.z = r2.z * -2 + 3;
    r2.z = r2.z * r2.z;
    r2.z = r3.z * r2.z;
    r3.z = r2.z * r2.z;
    r3.z = r3.z * r3.z;
    r3.z = r3.z * r2.z;
    r8.xyz = r4.www ? float3(1,0.300000012,-1) : float3(0.5,1,-0.5);
    r4.x = r8.y + r8.z;
    r3.z = r3.z * r4.x + r8.x;
    r2.z = r2.z * r3.z + -0.100000001;
    r2.z = cb0[199].x * r2.z + 0.100000001;
    r3.z = r8.w * 0.5 + 0.5;
    r3.z = r3.z * r9.w;
    r3.z = r3.z * 1.39999998 + 0.100000001;
    if (r5.w != 0) {
      r4.x = (int)cb3[2].z;
      r5.w = asint(cb0[188].z) + -1;
      r4.x = min((int)r5.w, (int)r4.x);
      r4.x = max(0, (int)r4.x);
    // Missing reflection info for shader. No names possible.
    // Known bad code for instruction (needs manual fix):
        ld_structured_indexable(structured_buffer, stride=128)(mixed,mixed,mixed,mixed) r4.x, r4.x, l(32), t1.xxxx
    r4.x = no_StructuredBufferName[no_srcAddressRegister].no_srcByteOffsetName.swiz;
    } else {
      r4.x = 0;
    }
    r4.x = r4.x * 0.399999976 + r10.w;
    r4.x = saturate(0.600000024 + r4.x);
    r1.y = r2.z * r1.y;
    r1.y = r1.y * r3.z;
    r1.y = r1.y * r4.x;
    r2.z = cmp(0 != v7.z);
    r3.z = saturate(-r9.y);
    r4.x = r4.z * cb4[137].w + 2.5;
    r2.z = r2.z ? 4.5 : r4.x;
    r2.z = r3.z * r2.z + -0.5;
    r2.z = cb0[199].y * r2.z + 1;
    r1.y = r2.z * r1.y;
    r2.z = 0.0833333358 * r4.y;
    r2.z = min(1, r2.z);
    r4.xz = r2.zz * float2(-0.300000012,-0.300000012) + float2(0.5,0.600000024);
    r2.z = dot(r9.xyz, r3.xyw);
    r2.z = 1 + -r2.z;
    r4.xz = r2.zz + -r4.xz;
    r4.xz = saturate(float2(3.33333325,5.00000048) * r4.xz);
    r8.xy = r4.xz * float2(-2,-2) + float2(3,3);
    r4.xz = r4.xz * r4.xz;
    r4.xz = r8.xy * r4.xz;
    r2.z = r4.w ? r4.x : r4.z;
    r3.z = dot(cb0[189].xyz, float3(0.330000013,0.330000013,0.330000013));
    r8.xyz = cb0[189].xyz * cb0[189].xyz;
    r8.xyz = r8.xyz * r8.xyz;
    r8.xyz = r8.xyz * r8.xyz;
    r4.x = dot(r8.xyz, float3(0.699999988,0.699999988,0.699999988));
    r4.x = max(0.00999999978, r4.x);
    r4.x = rcp(r4.x);
    r3.z = r4.x * r3.z;
    r8.xyz = r3.zzz * r8.xyz + -r12.yzw;
    r8.xyz = r10.www * r8.xyz + r12.yzw;
    r1.x = r1.x * r1.x;
    r1.x = log2(r1.x);
    r1.x = 20 * r1.x;
    r1.x = exp2(r1.x);
    r9.xyz = r7.xyz * r12.xxx + -r8.xyz;
    r8.xyz = r1.xxx * r9.xyz + r8.xyz;
    r7.xyz = -r7.xyz * r12.xxx + r8.xyz;
    r7.xyz = cb0[199].zzz * r7.xyz + r12.yzw;
    r1.x = r2.x + r2.y;
    r1.x = r1.z * r1.w + r1.x;
    r1.x = 0.330000013 * r1.x;
    r1.x = r1.x * r1.x;
    r1.x = cb0[199].w * r1.x;
    r1.x = r1.x * -0.199999988 + 1;
    r2.xyw = saturate(r2.xyw);
    r2.xyw = log2(r2.xyw);
    r2.xyw = float3(0.200000003,0.200000003,0.200000003) * r2.xyw;
    r2.xyw = exp2(r2.xyw);
    r1.z = dot(r2.xyw, r2.xyw);
    r1.z = max(6.10351562e-05, r1.z);
    r1.z = rsqrt(r1.z);
    r2.xyw = r2.xyw * r1.zzz;
    r1.z = 48 * cb0[198].w;
    r1.x = 0.100000001 * r1.x;
    r8.xyz = r1.xxx * r2.xyw;
    r2.xyw = -r1.xxx * r2.xyw + r10.xyz;
    r2.xyw = r0.xxx * r2.xyw + r8.xyz;
    r1.xzw = r2.xyw * r1.zzz;
    r0.x = r2.z * r1.y;
    r2.xyz = r0.xxx * r7.xyz;
    r1.xyz = r2.xyz * r1.xzw;
    r2.xyz = r6.www ? cb4[87].xyz : cb4[86].xyz;
    r2.xyz = r6.zzz ? cb4[88].xyz : r2.xyz;
    r2.xyz = r6.yyy ? cb4[89].xyz : r2.xyz;
    r2.xyz = r6.xxx ? cb4[90].xyz : r2.xyz;
    r1.xyz = r2.xyz * r1.xyz;
    r0.x = saturate(r4.y * 0.200000003 + -1);
    r0.x = r0.x * -0.699999988 + 1;
    r1.xyw = r1.xyz * r0.xxx;
    r2.x = r1.x + r1.y;
    r0.x = r1.z * r0.x + r2.x;
    r0.x = r0.x * r0.x;
    r0.x = r0.x * 0.0500000007 + 1;
    r1.xyz = r1.xyw * r0.xxx;
    r0.x = r0.w ? 0.5 : 1;
    r1.xyz = r1.xyz * r0.xxx;
    r0.x = cmp(0.5 < cb3[1].w);
    r2.xyz = min(float3(0.699999988,0.699999988,0.699999988), r1.xyz);
    r1.xyz = r0.xxx ? r2.xyz : r1.xyz;
    r1.xyz = cb0[198].xyz * r1.xyz;
  } else {
    r1.xyz = float3(0,0,0);
  }
  r2.xyz = cb4[57].www * r1.xyz;
  r0.xzw = r0.zzz ? r2.xyz : r1.xyz;
  r1.xyz = r14.xyz * float3(2,2,2) + float3(1,1,1);
  r0.xzw = r1.xyz * r0.xzw;
  r1.xyz = r5.xyz + r13.xyz;
  r1.w = cb4[155].w * cb4[29].w;
  r2.xyz = cb4[29].xyz + -r1.xyz;
  o0.xyz = r1.www * r2.xyz + r1.xyz;
  r1.x = -cb4[29].w * cb4[155].w + 1;
  r0.xzw = r1.xxx * r0.xzw;
  r0.xzw = max(float3(0,0,0), r0.xzw);
  r0.xzw = sqrt(r0.xzw);
  r0.xzw = float3(0.200000003,0.200000003,0.200000003) * r0.xzw;
  o1.xyz = min(float3(1,1,1), r0.xzw);
  o1.w = 0.333299994 * r0.y;
  o2.w = r4.w ? 0.340000004 : 0;
  o3.xyz = r3.xyw * float3(0.5,0.5,0.5) + float3(0.5,0.5,0.5);
  o0.w = 1;
  o3.w = 1;
  return;
}