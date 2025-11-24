Shader "Custom/RimLighting"
{
    Properties
    {
        // Base Color Set To Black
        _BaseColor ("Base Color", Color) = (0, 0, 0, 0)
        // Rim Color Set To Orange To add a hot temperature to the wood
        _RimColor ("Rim Color", Color) = (1, 0.5, 0, 1)
        // Adjusted Rim Power Values and Range to Adhere to the range wanted to change how hot the wood of the torches would look
        _RimPower ("Rim Power", Range(1, 10.0)) = 5.0
    }

    SubShader
    {
        //Insructions for rendering
        Tags { "RenderPipeline" = "UniversalRenderPipeline" "RenderType" = "Opaque" }

        Pass
        {
            //Allows us to use vertex and fragment shader
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            //Getting Unity HLSL Packages for the shader
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;// Setting object space position
                float3 normalOS : NORMAL; //Setting object space normal
                float4 tangentOS : TANGENT; //Setting Tangent space to perfrom rim light calculations
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION; //Giving object coordinates to fragment shader 
                float3 viewDirWS : TEXCOORD0; //Giving camera viewDir to fragment shader
                float3 normalWS : TEXCOORD1; //Giving the world space normal to fragment shader
            };

            //Storing Constant Variables
            CBUFFER_START(UnityPerMaterial)
                float4 _BaseColor;
                float4 _RimColor;
                float _RimPower; 
            CBUFFER_END

            //Vertex Shader: Setting up variables to give to fragment shader 
            Varyings vert(Attributes IN)
            {
                //Sets up data storage for variables to go to the fragment shader
                Varyings OUT;

                OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz); //Transforms the vertex position from object space to homogenous clip space
                OUT.normalWS = normalize(TransformObjectToWorldNormal(IN.normalOS)); //Transforms the normal vector to the world space and then normalizes it
                float3 worldPosWS = TransformObjectToWorld(IN.positionOS.xyz); //Transforms the world position of the vertex
                OUT.viewDirWS = normalize(GetCameraPositionWS() - worldPosWS);  //Normalizes the cameras positon in the world space minus the vertexs world position 

                //Returns set up variables
                return OUT;
            }

            //Fragment shader: Does math to calculate the color for each pixel
            half4 frag(Varyings IN) : SV_Target
            {
                //Normalizing the surface normal
                half3 normalWS = normalize(IN.normalWS);
                //Normalizing the view Direction vector
                half3 viewDirWS = normalize(IN.viewDirWS);
                //Calculating the rim factor:  One minus the clamped dot product to a value between 0 and 1 of the normalized view direction vector and the normal vector
                half rimFactor = 1.0 - saturate(dot(viewDirWS, normalWS));
                //Calculating the rim lighting: taking the rim factor to the power of the variable _rimpower
                half rimLighting = pow(rimFactor, _RimPower);
                //Doing Final Color Calculation: Taking the base color and adding it to the rim color times the rim lighting variable
                half3 finalColor = _BaseColor.rgb + _RimColor.rgb * rimLighting;
                //Gives the final color to the singular pixel
                return half4(finalColor, _BaseColor.a);
            }

            //Ending of the HLSL code
            ENDHLSL
        }
    }
}
