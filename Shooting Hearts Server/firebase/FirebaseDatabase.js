const { getFirestore } = require("./FirebaseAdmin");

// Player collection reference
const collectionRef = getFirestore().collection("players");

// Initialize leaderboard record for a player. (score: 0)
async function initPlayerRecord(uid, name) {
    try {
        await collectionRef.doc(uid).set({ name: name, highScore: 0 });
    } catch (error) {
        throw new Error("Failed to initialize high score.");
    }
}

// Get player high score by UID
async function getHighScoreByUID(uid) {
    try {
        const docSnapshot = await collectionRef.doc(uid).get();
        return {
            playerName: docSnapshot.get("name"),
            highScore: docSnapshot.get("highScore")
        };
    } catch (error) {
        throw new Error("Failed to retrieve high score.");
    }
}

// Update player high score
async function updateHighScoreByUID(uid, highScore) {
    try {
        await collectionRef.doc(uid).update({ highScore: highScore });
    } catch (error) {
        throw new Error("Failed to update high score.");
    }
}

// Retrieve leaderboard records
async function getLeaderboardRecords() {
    try {
        const snapshot = await collectionRef.orderBy("highScore", "desc").get();
        const records = [];
        snapshot.forEach(doc => {
            records.push({
                playerName: doc.data().name,
                playerScore: doc.data().highScore
            });
        });
        return records;
    } catch (error) {
        throw new Error("Failed to retrieve leaderboard.");
    }
}

module.exports = {
    initPlayerRecord,
    getHighScoreByUID,
    updateHighScoreByUID,
    getLeaderboardRecords
};