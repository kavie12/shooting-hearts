const { updateHighScore, getLeaderboard, getHighScore } = require("../controllers/playerController");

const router = require("express").Router();

// Handle leaderboard request
router.get("/leaderboard", getLeaderboard);

// Update highscore comparing the new score and the current high score
router.post("/update-high-score", updateHighScore);

// Handle player high score request
router.get("/get-high-score", getHighScore);

module.exports = router;