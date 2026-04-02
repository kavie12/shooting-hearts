const { createUser, authenticate, refreshTokens, getUIDByToken, resetPasswordEmail } = require("../firebase/FirebaseAuth");

// Handle signup
const signup = async (req, res) => {
    try {
        const { name, email, password } = req.body;

        const authRes = await createUser(name, email, password);
        
        res.status(201).json(authRes);
    } catch (error) {
        console.log(error.message);
        res.status(400).json(error);
    }
};

// Handle login
const login = async (req, res) => {
    try {
        const { email, password } = req.body;

        const authRes = await authenticate(email, password);

        res.status(200).json(authRes);
    } catch (error) {
        console.log(error.message);
        res.status(400).json(error);
    }
};

// Hanlde access token verification
const verify = async (req, res) => {
    try {
        const authHeader = req.headers.authorization;
        if (!authHeader) return res.status(401).send("Authentication required");

        const token = authHeader.split(" ")[1];
        await getUIDByToken(token);

        res.status(200).send();
    } catch (error) {
        console.log(error.message);
        res.status(400).json(error);
    }
};

// Get new token pairs from refresh token
const refresh = async (req, res) => {
    try {
        const authHeader = req.headers.authorization;
        if (!authHeader) return res.status(401).send("Authentication required");

        const refreshToken = authHeader.split(" ")[1];
        
        const authRes = await refreshTokens(refreshToken);

        res.status(200).json(authRes);
    } catch (error) {
        console.log(error.message);
        res.status(400).json(error);
    }
};

// Send reset password email
const sendResetPasswordEmail = async (req, res) => {
    try {
        const { email } = req.body;
        const result = await resetPasswordEmail(email);
        res.status(200).json({ message: "Password reset email sent." });
    }
    catch (error) {
        console.log(error.message);
        res.status(400).json(error);
    }
};

module.exports = { signup, login, verify, refresh, sendResetPasswordEmail };