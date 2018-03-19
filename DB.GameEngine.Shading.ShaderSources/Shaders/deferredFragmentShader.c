#version 430 core

in vec2 textureUV;

out vec4 color;

uniform sampler2D tColor;
uniform sampler2D tNormal;
uniform sampler2D tPosition;
uniform sampler2D tTexture;
uniform float time;
//uniform sampler2D tDepth;

//float LinearizeDepth(in vec2 uv)
//{
 //   float zNear = 0.1;   
 //   float zFar  = 1000.0;
 //   float depth = texture2D(depthm, uv).x;
 //   return (2.0 * zNear) / (zFar + zNear - depth * (zFar - zNear));
//}

void main() {
	vec3 texCoord = texture(tTexture, textureUV).xyz;
    vec3 worldPos = texture(tPosition, textureUV).xyz;
    vec3 dColor = texture(tColor, textureUV).xyz;
    vec3 normal = texture(tNormal, textureUV).xyz;

	color = vec4(dColor, 1.0);


	/*vec3 lightPos = vec3(-cos(time)*1000, sin(time)*1000, 1000);

	vec3 l = normalize(lightPos - worldPos);
    vec3 v = normalize(-worldPos);
    vec3 h  = normalize(l + v);
    float diff = max(0.2, dot (l, normal));
    float spec = pow(max (0.0, dot(h, normal)), 2.0);
    
	if (dColor != vec3(1)) {
		color = vec4(diff * dColor + vec3(spec), 1.0);
	} else {
		color = vec4(dColor, 1.0);
	}*/
}

