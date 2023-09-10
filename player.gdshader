shader_type canvas_item;

uniform vec4 color : source_color;	
uniform float flashState : hint_range(0,1) = 0;

void fragment() {
	vec4 pixelCOLOR = texture(TEXTURE, UV);
	COLOR = mix(pixelCOLOR, color, flashState);
	COLOR.a *= pixelCOLOR.a;
}

