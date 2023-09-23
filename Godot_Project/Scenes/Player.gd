extends CharacterBody2D


const SPEED = 300.0

func _ready():
	pass

func get_input():
	var input_direction = Input.get_vector("left", "right", "up", "down")
	velocity = input_direction * SPEED

func _physics_process(_delta):
	
	get_input()
	move_and_slide()
