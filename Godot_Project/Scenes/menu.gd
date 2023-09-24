extends Control



func _on_start_button_button_up():
	get_tree().change_scene_to_file("res://Scenes/Main_Scene_Phil.tscn")


func _on_exit_button_button_up():
	get_tree().quit()
