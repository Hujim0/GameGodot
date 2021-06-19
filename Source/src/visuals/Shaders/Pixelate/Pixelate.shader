shader_type canvas_item;


uniform float size_x;
uniform float size_y;

void fragment(){
	vec2 uv = SCREEN_UV;
	
	uv -= mod(SCREEN_UV, vec2(size_x, size_y));
	
	COLOR.rgb = textureLod(SCREEN_TEXTURE, uv, 0.0).rgb;
}
