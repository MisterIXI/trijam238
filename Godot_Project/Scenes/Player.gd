extends CharacterBody2D


@export var speed = 400.0

func _ready():
	pass

func get_input():
	var input_direction = Input.get_vector("left", "right", "up", "down")
	velocity = input_direction * speed

func _physics_process(_delta):
	
	get_input()
	move_and_slide()

func _process(_delta):
	if velocity.x > 0:
		$DogRun.flip_h = false
		$AnimationPlayer.play("run", -1, 2.0, false)
	elif velocity.x < 0:
		$DogRun.flip_h = true
		$AnimationPlayer.play("run", -1, 2.0, false)
	else:
		$DogRun.flip_h = false
		$AnimationPlayer.play("run", -1, 1.0, false)
