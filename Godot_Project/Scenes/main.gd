extends Node2D

@export var obstacle_scene: PackedScene
@export var treat_scene: PackedScene



func _on_obstacle_timer_timeout():
	var obstacle = obstacle_scene.instantiate()
	obstacle_scene.instantiate()

	var obstacle_spawn_location = $SpawnBounds/SpawnPath
	obstacle_spawn_location.progress_ratio = randf()

	obstacle.position = obstacle_spawn_location.position

	add_child(obstacle)


func _on_treat_timer_timeout():
	var treat = treat_scene.instantiate()
	treat_scene.instantiate()

	var treat_spawn_location = $SpawnBounds/SpawnPath
	treat_spawn_location.progress_ratio = randf()

	treat.position = treat_spawn_location.position

	add_child(treat)

	treat.collected.connect($UserInterface/ScoreLabel._on_collected.bind())

func _on_pause_button_pressed():
	if get_tree().paused == false:
		get_tree().paused = true
		$PauseMenu.show()
	else:
		get_tree().paused = false
		$PauseMenu.hide()