const { getUIDByToken } = require("../firebase/FirebaseAuth");

// Authenticate user and add UID to the request
const authMiddleware = async (req, res, next) => {
    try {
        const authHeader = req.headers.authorization;
        if (!authHeader) return res.status(401).send({ message: "Authentication required" });

        const token = authHeader.split(" ")[1];
        const uid = await getUIDByToken(token);
        req.uid = uid;
        next();
    } catch (error) {
        console.log(error.message);
        res.status(400).json({ message: error.message });
    }
};

module.exports = authMiddleware;