extends Area2D

@export var scroll_speed = 1

signal collected


# Called when the node enters the scene tree for the first time.
func _ready():
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(_delta):
	position.x -= scroll_speed



func _on_body_entered(_body:Node2D):
	print("collected")
	collected.emit()
	queue_free()
