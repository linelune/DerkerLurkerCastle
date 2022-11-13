struct appdata
{
	float4 vertex : POSITION;
	float2 uv : TEXCOORD0;
#if defined(PSX_VERTEX_LIT)||defined(PSX_CUBEMAP)
	float3 normal : NORMAL;
#endif
};

struct v2g
{
	float4 vertex : SV_POSITION;
	float2 uv : TEXCOORD0;
#ifdef PSX_VERTEX_LIT
	float4 color : COLOR0;
#endif
#ifdef PSX_CUBEMAP
	float3 reflectionDir : TEXCOORD1;
#endif
};

struct g2f
{
	float4 affineUV1 : TEXCOORD0;
	float4 vertex : SV_POSITION;
	float customDepth : TEXCOORD1;
#ifdef PSX_VERTEX_LIT
	float4 color : COLOR0;
#endif
	UNITY_FOG_COORDS(2)
#ifdef PSX_VERTEX_LIT
	float4 affineUV2 : TEXCOORD3;
#endif
#ifdef PSX_CUBEMAP
	float3 reflectionDir : TEXCOORD4;
#endif
};

struct fragOut
{
	half4 color : COLOR;
	float depth : DEPTH;
};

fixed4 _Color;
sampler2D _MainTex;
float4 _MainTex_ST;

#ifdef PSX_VERTEX_LIT
fixed4 _EmissionColor;
sampler2D _EmissiveTex;
float4 _EmissiveTex_ST;
#endif

float3 ShadePSXVertexLightsFull (float4 vertex, float3 normal, int lightCount, bool spotLight)
{
    float3 viewpos = UnityObjectToViewPos (vertex.xyz);
    float3 viewN = normalize (mul ((float3x3)UNITY_MATRIX_IT_MV, normal));
    float3 lightColor = UNITY_LIGHTMODEL_AMBIENT.xyz;

    for (int i = 0; i < lightCount; i++) {
		float3 toLight = unity_LightPosition[i].xyz - viewpos.xyz * unity_LightPosition[i].w;
        float lightDist = length(toLight);
		toLight = normalize(toLight);

        // don't produce NaNs if some vertex position overlaps with the light
        lightDist = max(lightDist, 0.000001);

        float atten = saturate(1 - lightDist / sqrt(unity_LightAtten[i].w));
		float diff = max (0, dot (viewN, toLight));
        lightColor += unity_LightColor[i].rgb * atten * 0.25 * lerp(1, diff, _PSX_LightingNormalFactor);
    }
    return lightColor;
}

float3 ShadePSXVertexLights (float4 vertex, float3 normal)
{
    return ShadePSXVertexLightsFull (vertex, normal, 4, false);
}

v2g vert(appdata v)
{
	v2g o;
	o.vertex = v.vertex;
	o.uv = v.uv;

#ifdef PSX_VERTEX_LIT

#ifdef PSX_ENABLE_CUSTOM_VERTEX_LIGHTING
	o.color.rgb = ShadePSXVertexLights(v.vertex, v.normal);
#else
	o.color.rgb = ShadeVertexLights(v.vertex, v.normal);
#endif

	o.color.a = 1;
#endif

#ifdef PSX_CUBEMAP
		float3 viewDir = mul(unity_ObjectToWorld, v.vertex).xyz - _WorldSpaceCameraPos;
		float3 normalDir = normalize(mul(float4(v.normal, 0.0), unity_WorldToObject).xyz);
		o.reflectionDir = reflect(viewDir, normalDir);
#endif

	return o;
}


[maxvertexcount(3)]
void geom(triangle v2g IN[3], inout TriangleStream<g2f> triStream)
{
	g2f o;
	
	float4x4 matrix_mv = UNITY_MATRIX_MV;
	float4x4 matrix_p = UNITY_MATRIX_P;
	
#ifdef PSX_ENABLE_TRIANGLE_SORTING
	float triSortDepth = PSX_TRIANGLE_SORTING_FUNC(IN[0].vertex, IN[1].vertex, IN[2].vertex);
#else
	float triSortDepth = 0;
#endif

	for (int i = 0; i < 3; i++)
	{
		o.vertex = mul(matrix_mv, IN[i].vertex);
		o.affineUV1 = CalculateAffineUV(o.vertex, TRANSFORM_TEX(IN[i].uv, _MainTex));
#ifdef PSX_VERTEX_LIT
		o.affineUV2 = CalculateAffineUV(o.vertex, TRANSFORM_TEX(IN[i].uv, _EmissiveTex));
#endif

		o.vertex.xyz = SnapVertexToGrid(o.vertex.xyz);
		o.vertex = mul(matrix_p, o.vertex);
#ifdef PSX_ENABLE_TRIANGLE_SORTING
		o.customDepth = triSortDepth;
#else
		o.customDepth = 0;
#endif

#ifdef PSX_CUBEMAP
		o.reflectionDir = IN[i].reflectionDir;
#endif

#ifdef PSX_VERTEX_LIT
		o.color = IN[i].color;
#endif

		UNITY_TRANSFER_FOG(o, o.vertex);
		triStream.Append(o);
	}

	triStream.RestartStrip();
}

#ifdef PSX_ENABLE_TRIANGLE_SORTING
fragOut frag(g2f i)
#else
fixed4 frag(g2f i) : COLOR
#endif
{
	fragOut o;
	o.color = tex2D(_MainTex, i.affineUV1.xy / i.affineUV1.z) * _Color;
	
#ifdef PSX_VERTEX_LIT
	o.color *= i.color;
#endif

#ifdef PSX_CUTOUT_VAL
	clip(o.color.a - PSX_CUTOUT_VAL);
#endif

#ifdef PSX_VERTEX_LIT
	o.color.rgb += tex2D(_EmissiveTex, i.affineUV2.xy / i.affineUV2.z) * _EmissionColor.rgb * _EmissionColor.a;
#endif

#ifdef PSX_CUBEMAP
	o.color.rgb += texCUBE(PSX_CUBEMAP, i.reflectionDir) * PSX_CUBEMAP_COLOR.rgb * PSX_CUBEMAP_COLOR.a;
#endif

#ifdef PSX_ENABLE_TRIANGLE_SORTING
	o.depth = i.customDepth;
#else
	o.depth = 0;
#endif

	UNITY_APPLY_FOG(i.fogCoord, o.color);
	o.color.rgb = lerp(o.color.rgb, o.depth, _PSX_DepthDebug);

#ifdef PSX_ENABLE_TRIANGLE_SORTING
	return o;
#else
	return o.color;
#endif
}
