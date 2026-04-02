const router = require("express").Router();
const { signup, login, verify, refresh, sendResetPasswordEmail } = require("../controllers/authController.js");

// Handle signup
router.post("/signup", signup);

// Handle login
router.post("/login", login);

// Hanlde access token verification
router.post("/verify", verify);

// Get new token pairs from refresh token
router.post("/refresh", refresh);

// Send reset password email
router.post("/reset-password-email", sendResetPasswordEmail);

module.exports = router;