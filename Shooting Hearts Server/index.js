const express = require("express");
require("dotenv").config();
const { initFirebase } = require("./firebase/FirebaseAdmin.js");

// Init firebase Admin SDK
initFirebase();

const app = express();
const port = process.env.SERVER_PORT;
app.use(express.json());

const authMiddleware = require("./middleware/authMiddleware.js");

// Use auth routes
const authRoutes = require("./routes/authRoutes.js");
app.use("/api/auth", authRoutes);

// Use player routes
const playerRoutes = require("./routes/playerRoutes.js");
app.use("/api/player", authMiddleware, playerRoutes);

// Start the server
app.listen(port, () => {
    console.log(`Server running on port ${port}`);
});