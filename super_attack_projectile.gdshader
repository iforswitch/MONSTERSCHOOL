shader_type canvas_item;

uniform vec4 color : source_color;	
uniform float flashState : hint_range(0,1) = 0;

void fragment() {
	vec4 pixelCOLOR = texture(TEXTURE, UV);
	pixelCOLOR.r = 0.0;
	pixelCOLOR.g = 0.7;
	pixelCOLOR.b = 0.2;
	COLOR = pixelCOLOR;
	COLOR = mix(pixelCOLOR, color, flashState);
	COLOR.a *= pixelCOLOR.a;
}
