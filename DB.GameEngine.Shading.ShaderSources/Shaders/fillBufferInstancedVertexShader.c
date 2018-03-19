#version 430 core

layout(location = 0) in vec3 vertexPosition;
layout(location = 1) in vec3 normal;
layout(location = 2) in vec2 textureCoordinate;
layout(location = 3) in mat4 modelMatrixInstanced;

uniform mat4 modelMatrix;
uniform mat4 viewMatrix;
uniform mat4 projectionMatrix;

out vec2 textureUV;
out vec3 pNormal;
out vec4 pVertexPosition;
out vec4 pWorldPosition;

void main() {
	vec4 worldPosition = modelMatrixInstanced * vec4(vertexPosition, 1.0);
	gl_Position = projectionMatrix * viewMatrix * worldPosition;
	textureUV = textureCoordinate;
	pNormal = normal;
	pVertexPosition = vec4(vertexPosition, 1.0);
	pWorldPosition = worldPosition;
}