extends Label


var score = 0

func _on_collected():
    score += 1
    text = "Treats: %s" % score
