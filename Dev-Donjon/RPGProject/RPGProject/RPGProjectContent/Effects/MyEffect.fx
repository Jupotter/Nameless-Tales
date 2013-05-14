
//------- Constants --------
float4x4 xView;
float4x4 xProjection;
float4x4 xWorld;

float3 xLightDirection;
float xAmbient;
bool xEnableLighting;

bool xClipping;
float4 xClipPlane0;

float3 xCamPos;


//------- Texture Samplers --------

Texture xTexture;
sampler TextureSampler = sampler_state { texture = <xTexture>; magfilter = LINEAR; minfilter = LINEAR; mipfilter=LINEAR; AddressU = mirror; AddressV = mirror;};

Texture xReflectionMap;
sampler ReflectionSampler = sampler_state { texture = <xReflectionMap> ; magfilter = LINEAR; minfilter = LINEAR; mipfilter=NONE; AddressU = mirror; AddressV = mirror;};

Texture xRefractionMap;
sampler RefractionSampler = sampler_state { texture = <xRefractionMap> ; magfilter = LINEAR; minfilter = LINEAR; mipfilter=NONE; AddressU = mirror; AddressV = mirror;};

Texture xWaterBumpMap;
sampler WaterBumpMapSampler = sampler_state { texture = <xWaterBumpMap> ; magfilter = LINEAR; minfilter = LINEAR; mipfilter=LINEAR; AddressU = mirror; AddressV = mirror;};



//------- Technique: Colored --------
struct ColVertexToPixel
{
    float4 Position   	: POSITION;    
    float4 Color		: COLOR0;
    float LightingFactor: TEXCOORD0;
	float4 clipDistances     : TEXCOORD5;
};

struct ColPixelToFrame
{
    float4 Color : COLOR0;
};

ColVertexToPixel ColoredVS( float4 inPos : POSITION, float4 inColor: COLOR, float3 inNormal: NORMAL)
{	
	ColVertexToPixel Output = (ColVertexToPixel)0;
	float4x4 preViewProjection = mul (xView, xProjection);
	float4x4 preWorldViewProjection = mul (xWorld, preViewProjection);
    
	Output.Position = mul(inPos, preWorldViewProjection);
	Output.Color = inColor;

	Output.clipDistances = dot(inPos, xClipPlane0);
	
	float3 Normal = normalize(mul(normalize(inNormal), xWorld));	
	Output.LightingFactor = 1;
	if (xEnableLighting)
		Output.LightingFactor = saturate(dot(Normal, -xLightDirection));
    
	return Output;    
}

ColPixelToFrame ColoredPS(ColVertexToPixel PSIn) 
{
	ColPixelToFrame Output = (ColPixelToFrame)0;		
    
	Output.Color = PSIn.Color;
	Output.Color.rgb *= saturate(PSIn.LightingFactor + xAmbient);	

	if (xClipping)
		clip(PSIn.clipDistances);
	
	return Output;
}

technique Colored
{
	pass Pass0
    {   
    	VertexShader = compile vs_2_0 ColoredVS();
        PixelShader  = compile ps_2_0 ColoredPS();
    }
}

//------- Technique: Textured --------
struct TexVertexToPixel
{
    float4 Position   	: POSITION;    
    float4 Color		: COLOR0;
    float LightingFactor: TEXCOORD0;
    float2 TextureCoords: TEXCOORD1;
	float4 clipDistances     : TEXCOORD5;
};

struct TexPixelToFrame
{
    float4 Color : COLOR0;
};

TexVertexToPixel TexturedVS( float4 inPos : POSITION, float3 inNormal: NORMAL, float2 inTexCoords: TEXCOORD0)
{	
	TexVertexToPixel Output = (TexVertexToPixel)0;
	float4x4 preViewProjection = mul (xView, xProjection);
	float4x4 preWorldViewProjection = mul (xWorld, preViewProjection);
    
	Output.Position = mul(inPos, preWorldViewProjection);	
	Output.TextureCoords = inTexCoords;

	Output.clipDistances = dot(inPos, xClipPlane0);
	
	float3 Normal = normalize(mul(normalize(inNormal), (float3x3)xWorld));	
	Output.LightingFactor = 1;
	if (xEnableLighting)
		Output.LightingFactor = saturate(dot(Normal, -xLightDirection));
    
	return Output;     
}

TexPixelToFrame TexturedPS(TexVertexToPixel PSIn) 
{
	TexPixelToFrame Output = (TexPixelToFrame)0;		
    
	Output.Color = tex2D(TextureSampler, PSIn.TextureCoords);
	Output.Color.rgb *= saturate(PSIn.LightingFactor + xAmbient);

	if (xClipping)
		clip(PSIn.clipDistances);

	return Output;
}

technique Textured
{
	pass Pass0
    {   
    	VertexShader = compile vs_2_0 TexturedVS();
        PixelShader  = compile ps_2_0 TexturedPS();
    }
}


//----------- Region: MultiTextured ----------

float2 xWorldScale;
float2 xAtlasSize;
float xAtlasTextureSize;

float4 xTextureTypes;

Texture xTexturesMap;
sampler TexturesMapSampler = sampler_state { texture = <xTexturesMap> ; magfilter = POINT; minfilter = POINT; mipfilter=NONE; AddressU = clamp; AddressV = clamp;};

Texture3D xTextureAtlas;
sampler AtlasSampler = sampler_state { texture = <xTextureAtlas> ; magfilter = LINEAR; minfilter = LINEAR; mipfilter=LINEAR; AddressU = wrap; AddressV = wrap; AddressW = clamp;};

//------- Technique: MultiColored --------
struct MColVertexToPixel
{
    float4 Position   	: POSITION;
    float LightingFactor: TEXCOORD0;
	float4 clipDistances: TEXCOORD1;
	float2 WorldPos		: TEXCOORD2;
};

struct MColPixelToFrame
{
    float4 Color : COLOR0;
};

MColVertexToPixel MColoredVS(float4 inPos : POSITION, float3 inNormal: NORMAL)
{	
	MColVertexToPixel Output = (MColVertexToPixel)0;
	float4x4 preViewProjection = mul (xView, xProjection);
	float4x4 preWorldViewProjection = mul (xWorld, preViewProjection);
    
	Output.Position = mul(inPos, preWorldViewProjection);
	//Output.Color = inColor;

	Output.clipDistances = dot(inPos, xClipPlane0);
	Output.WorldPos.xy = inPos.zx / xWorldScale;
	
	float3 Normal = normalize(mul(normalize(inNormal), xWorld));
	Output.LightingFactor = 1;
	if (xEnableLighting)
		Output.LightingFactor = saturate(dot(Normal, -xLightDirection));
    
	return Output;    
}

ColPixelToFrame MColoredPS(MColVertexToPixel PSIn) 
{
	MColPixelToFrame Output = (ColPixelToFrame)0;		
    
	Output.Color = tex2D(TexturesMapSampler, PSIn.WorldPos);
	Output.Color.rgb = Output.Color.bgr;
	Output.Color.rgb *= saturate(PSIn.LightingFactor + xAmbient);	

	if (xClipping)
		clip(PSIn.clipDistances);
	
	return Output;
}

technique MultiColored
{
	pass Pass0
    {   
    	VertexShader = compile vs_2_0 MColoredVS();
        PixelShader  = compile ps_2_0 MColoredPS();
    }
}


//------- Technique: MultiTextured --------
struct MTexVertexToPixel
{
    float4 Position   		: POSITION;
    float LightingFactor	: TEXCOORD0;
	float4 TexWeights		: TEXCOORD1;
	float4 clipDistances    : TEXCOORD5;
	float2 WorldPos			: TEXCOORD2;
};

struct MTexPixelToFrame
{
    float4 Color : COLOR0;
};

MTexVertexToPixel MTexturedVS( float4 inPos : POSITION, float3 inNormal: NORMAL, float4 inTexWeights: TEXCOORD0, float4 inTexTypes : TEXCOORD1)
{	
	MTexVertexToPixel Output = (MTexVertexToPixel)0;
	float4x4 preViewProjection = mul (xView, xProjection);
	float4x4 preWorldViewProjection = mul (xWorld, preViewProjection);
    
	Output.Position = mul(inPos, preWorldViewProjection);	

	Output.clipDistances = dot(inPos, xClipPlane0);
	
	float3 Normal = normalize(mul(normalize(inNormal), (float3x3)xWorld));	
	Output.LightingFactor = 1;
	if (xEnableLighting)
		Output.LightingFactor = saturate(dot(Normal, -xLightDirection));

	Output.TexWeights = inTexWeights;
	Output.WorldPos.xy = inPos.zx / xWorldScale;
    
	return Output;     
}

MTexPixelToFrame MTexturedPS(MTexVertexToPixel PSIn) 
{
	MTexPixelToFrame Output = (MTexPixelToFrame)0;		
    
	//float TexNum = PSIn.TexTypes.x;

	float texNum = (tex2D(TexturesMapSampler, PSIn.WorldPos)).b;
	float3 texCoord;
	texCoord.xy = PSIn.WorldPos.xy*xWorldScale;
	texCoord.z = texNum;

	Output.Color = tex3D(AtlasSampler, texCoord);
	Output.Color.rgb *= saturate(PSIn.LightingFactor + xAmbient);
	if (xClipping)
		clip(PSIn.clipDistances);

	return Output;
}

technique MultiTextured
{
	pass Pass0
    {   
    	VertexShader = compile vs_3_0 MTexturedVS();
        PixelShader  = compile ps_3_0 MTexturedPS();
    }
}

//------- Technique: Water --------

//------- Constants --------
float xTime;
float3 xWindDirection;
float xWindForce;
float4x4 xReflectionView;
float xWaveLength;
float xWaveHeight;

struct WVertexToPixel
{
    float4 Position						: POSITION;
    float4 ReflectionMapSamplingPos		: TEXCOORD1;
	float2 BumpMapSamplingPos			: TEXCOORD2;
	float4 RefractionMapSamplingPos		: TEXCOORD3;
    float4 Position3D					: TEXCOORD4;
};

struct WPixelToFrame
{
    float4 Color : COLOR0;
};

WVertexToPixel WaterVS(float4 inPos : POSITION, float2 inTex: TEXCOORD)
{    
    WVertexToPixel Output = (WVertexToPixel)0;

    float4x4 preViewProjection = mul (xView, xProjection);
    float4x4 preWorldViewProjection = mul (xWorld, preViewProjection);
    float4x4 preReflectionViewProjection = mul (xReflectionView, xProjection);
    float4x4 preWorldReflectionViewProjection = mul (xWorld, preReflectionViewProjection);
	float3 windDir = normalize(xWindDirection);    
	float3 perpDir = cross(windDir, float3(0,1,0));

	float ydot = dot(inTex, windDir.xz);
	float xdot = dot(inTex, perpDir.xz);
	float2 moveVector = float2(xdot, ydot);

    Output.Position = mul(inPos, preWorldViewProjection);
    Output.ReflectionMapSamplingPos = mul(inPos, preWorldReflectionViewProjection);

	moveVector.y += xTime*xWindForce;
	//moveVector = float2(0,xTime*xWindForce);
	Output.BumpMapSamplingPos = (moveVector)/xWaveLength;
	Output.RefractionMapSamplingPos = mul(inPos, preWorldViewProjection);
	Output.Position3D = mul(inPos, xWorld);

    return Output;
}

WPixelToFrame WaterPS(WVertexToPixel PSIn)
{
    WPixelToFrame Output = (WPixelToFrame)0;        
    
    float2 ProjectedTexCoords;
    ProjectedTexCoords.x = PSIn.ReflectionMapSamplingPos.x/PSIn.ReflectionMapSamplingPos.w/2.0f + 0.5f;
    ProjectedTexCoords.y = -PSIn.ReflectionMapSamplingPos.y/PSIn.ReflectionMapSamplingPos.w/2.0f + 0.5f;
	

	float4 dullColor = float4(0.3f, 0.3f, 0.5f, 1.0f);

	float4 bumpColor = tex2D(WaterBumpMapSampler, PSIn.BumpMapSamplingPos);
	
	float2 perturbation = xWaveHeight*(bumpColor.rg - 0.5f)*2.0f;
	float2 perturbatedTexCoords = ProjectedTexCoords + perturbation;

	float4 reflectiveColor = tex2D(ReflectionSampler, perturbatedTexCoords);

	float2 ProjectedRefrTexCoords;
	ProjectedRefrTexCoords.x = PSIn.RefractionMapSamplingPos.x/PSIn.RefractionMapSamplingPos.w/2.0f + 0.5f;
	ProjectedRefrTexCoords.y = -PSIn.RefractionMapSamplingPos.y/PSIn.RefractionMapSamplingPos.w/2.0f + 0.5f;    
	float2 perturbatedRefrTexCoords = ProjectedRefrTexCoords + perturbation;    
	float4 refractiveColor = tex2D(RefractionSampler, perturbatedRefrTexCoords);

	float3 eyeVector = normalize(xCamPos - PSIn.Position3D);
	float3 normalVector = (bumpColor.rbg-0.5f)*2.0f;
	float fresnelTerm = dot(eyeVector, normalVector);

	float4 combinedColor = lerp(reflectiveColor, refractiveColor, fresnelTerm);

	Output.Color = lerp(combinedColor, dullColor, 0.2f);

	float3 reflectionVector = -reflect(xLightDirection.zyx, normalVector);
	float specular = dot(normalize(reflectionVector), normalize(eyeVector));
	specular = pow(specular, 256);        
	Output.Color.rgb += specular;
    
    return Output;
}

technique Water
{
    pass Pass0
    {
        VertexShader = compile vs_1_1 WaterVS();
        PixelShader = compile ps_2_0 WaterPS();
    }
}


//------- Technique: SkyBox --------

Texture xSkyBoxTexture; 
samplerCUBE SkyBoxSampler = sampler_state 
{ 
   texture = <xSkyBoxTexture>; 
   magfilter = LINEAR; 
   minfilter = LINEAR; 
   mipfilter = LINEAR; 
   AddressU = Wrap; 
   AddressV = Wrap; 
};

struct SkyVertexToPixel
{
	float4 Position		: POSITION;
	float3 TexCoord		: TEXCOORD0;
};

SkyVertexToPixel SkyboxVS(float4 inPos : POSITION)
{
    SkyVertexToPixel Output;

	float4x4 preViewProjection = mul (xView, xProjection);
	float4x4 preWorldViewProjection = mul (xWorld, preViewProjection);
    
	Output.Position = mul(inPos, preWorldViewProjection);
 
    float4 VertexPosition = mul(inPos, xWorld);
    Output.TexCoord = VertexPosition - xCamPos;
 
    return Output;
}

float4 SkyboxPS(SkyVertexToPixel input) : COLOR0
{
	float4 color = texCUBE(SkyBoxSampler, normalize(input.TexCoord));
	//color.xyz = normalize(input.TexCoord.xyz);
	return color;
}

technique SkyBox
{
	pass Pass0
	{
		VertexShader = compile vs_2_0 SkyboxVS();
		PixelShader = compile ps_2_0 SkyboxPS();
	}
}
