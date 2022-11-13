/*
Shader Features
    -PSX_VERTEX_LIT
    -PSX_CUTOUT_VAL [float threshold]
	-PSX_CUBEMAP [texCUBE cubemap]
		-PSX_CUBEMAP_COLOR [float intensity]

Shader Customization
    -PSX_TRIANGLE_SORTING_FUNC [float4 v1, float4 v2, float4 v3]

*/

//Globals set by PSXShaderManager.cs
float _PSX_GridSize;
float _PSX_DepthDebug;
float _PSX_LightingNormalFactor;
float _PSX_TextureWarpingFactor;
float _CustomDepthOffset;

//Math Utils
float3 SnapVertexToGrid(float3 vertex)
{
    return _PSX_GridSize < 0.00001f ? vertex : (floor(vertex * _PSX_GridSize) / _PSX_GridSize);
}

float4 CalculateAffineUV(float4 vertex, float2 uv) 
{
	float4x4 matrix_p = UNITY_MATRIX_P;

	float vertDist = length(vertex.xyz);
	float affineFactor = vertex.w / vertDist;
	affineFactor = lerp(1 , affineFactor, _PSX_TextureWarpingFactor);
    return float4(uv * affineFactor, affineFactor, 0);
}

//Triangle sorting functions. Input is 3 object-space verts, output is the custom depth to be used by the entire triangle.
float GetTriangleSortingDepth_CenterDepth(float4 v1, float4 v2, float4 v3)
{
	float4x4 matrix_mv = UNITY_MATRIX_MV;
	float4x4 matrix_p = UNITY_MATRIX_P;

	float4 viewCenter =  mul(matrix_mv, (v1 + v2 + v3) / 3.0f);
	// Move the vertex along its direction from the camera to nudge its depth and affect the sorting priority.
	viewCenter.xyz += normalize(viewCenter.xyz) * _CustomDepthOffset;

	float4 clipCenter = mul(matrix_p, viewCenter);
	//Output clip space vertex z/w to simulate how depth is calculated for a regular depth buffer.
	return clipCenter.z / clipCenter.w;
}

float GetTriangleSortingDepth_ClosestVertexDepth(float4 v1, float4 v2, float4 v3)
{	
	float4x4 matrix_mv = UNITY_MATRIX_MV;
	float4x4 matrix_p = UNITY_MATRIX_P;

	v1 = mul(matrix_mv, v1);
	v2 = mul(matrix_mv, v2);
	v3 = mul(matrix_mv, v3);

	v1.xyz += normalize(v1.xyz) * _CustomDepthOffset;
	v2.xyz += normalize(v2.xyz) * _CustomDepthOffset;
	v3.xyz += normalize(v3.xyz) * _CustomDepthOffset;

	v1 = mul(matrix_p, v1);
	v2 = mul(matrix_p, v2);
	v3 = mul(matrix_p, v3);

	//Clip space w can be negative if the vertex is off-screen and it messes up the calculations.
	//Only consider triangles whose w is positive. lerp and step are a lot cheaper than conditionals.
	float depth = 100;
	depth = lerp(depth, min(depth, v1.z/v1.w), step(0, v1.w));
	depth = lerp(depth, min(depth, v2.z/v2.w), step(0, v2.w));
	depth = lerp(depth, min(depth, v3.z/v3.w), step(0, v3.w));
	return depth;
}

float GetTriangleSortingDepth_LinearClosestVertexDistance(float4 v1, float4 v2, float4 v3)
{
	v1.xyz = UnityObjectToViewPos(v1.xyz);
	v2.xyz = UnityObjectToViewPos(v2.xyz);
	v3.xyz = UnityObjectToViewPos(v3.xyz);

	v1.xyz += normalize(v1.xyz) * _CustomDepthOffset;
	v2.xyz += normalize(v2.xyz) * _CustomDepthOffset;
	v3.xyz += normalize(v3.xyz) * _CustomDepthOffset;

	float depth = 9999;
	depth = min(length(v1.xyz), min(length(v2.xyz), length(v3.xyz)));
	return 1 / depth;
}

//This function doesn't try to mimic the value distribution of a regular depth buffer, but still works
//if only PSX shaders are used in your scene.
float GetTriangleSortingDepth_LinearCenterDistance(float4 v1, float4 v2, float4 v3)
{
	float3 center = UnityObjectToViewPos((v1 + v2 + v3).xyz  / 3.0f);
	center.xyz += normalize(center.xyz) * _CustomDepthOffset;
	return 1 / length(center);
}

//Custom template.
float GetTriangleSortingDepth_Custom(float4 v1, float4 v2, float4 v3)
{
    return 0;
}

//Change this to use the function that's most suitable for your scene, or to GetTriangleSortingDepth_Custom if you want to make your own.
#define PSX_TRIANGLE_SORTING_FUNC(v1, v2, v3) GetTriangleSortingDepth_CenterDepth(v1, v2, v3)