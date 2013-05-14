

struct VertexShaderOutput
{
	float4 Position		: POSITION;
    float4 Position2	: TEXCOORD0;
};

VertexShaderOutput VertexShaderFunction(float3 inPos : POSITION)
{
    VertexShaderOutput output;
    output.Position = float4(inPos,1);
	output.Position2 = float4(inPos,1);

	output.Position.xyz = output.Position.xzy;

    // TODO: add your vertex shader code here.

    return output;
}

float4 PixelShaderFunction(VertexShaderOutput PSIn) : COLOR0
{
	float4 output;
	output.rgb = PSIn.Position2.z;
	output.a = 1;
    return output;
}

technique Technique1
{
    pass Pass0
    {
        VertexShader = compile vs_3_0 VertexShaderFunction();
        PixelShader = compile ps_3_0 PixelShaderFunction();
    }
}
