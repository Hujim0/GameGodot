shader_type canvas_item;

uniform highp vec2 size;

void fragment(){
	vec2 uv = SCREEN_UV;
	
	uv -= mod(SCREEN_UV, vec2(size.x, size.y));
	
	COLOR= textureLod(SCREEN_TEXTURE, uv,0);
	//COLOR.rgb = vec3(uv, 0.0);
}
