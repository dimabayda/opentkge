#version 430 core

in vec2 textureUV;
in vec3 pNormal;
in vec4 pVertexPosition;
in vec4 pWorldPosition;

layout(location = 0) out vec4 outColor;
layout(location = 1) out vec4 outNormal;
layout(location = 2) out vec4 outPosition;
layout(location = 3) out vec4 outTexture;

uniform sampler2D texture2D;

void main() {
	outColor = texture(texture2D, textureUV);
	outNormal = vec4(pNormal, 1.0);
	outPosition = normalize(pWorldPosition);
	outTexture = vec4(textureUV, 1.0, 1.0);
}