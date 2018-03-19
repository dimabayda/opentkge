#version 430 core

layout(location = 0) in vec3 vertexPosition;

out vec2 textureUV;

void main() {
	gl_Position = vec4(vertexPosition, 1.0);
	textureUV = (vertexPosition.xy + vec2(1.0)) / 2.0;
}