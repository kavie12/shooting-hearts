const { getAuth } = require("./FirebaseAdmin");
const { initPlayerRecord } = require("./FirebaseDatabase");

const apiKey = process.env.FIREBASE_API_KEY;

// Create new user
// API Doc: https://firebase.google.com/docs/reference/rest/auth#section-create-email-password
async function createUser(name, email, password) {
    try {
        const res = await fetch(`https://identitytoolkit.googleapis.com/v1/accounts:signUp?key=${apiKey}`, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({
                email: email,
                password: password,
                returnSecureToken: true
            })
        });
        const data = await res.json();

        if (!res.ok) {
            throw data.error;
        }

        await updateDisplayName(data.idToken, name);
        
        await initPlayerRecord(data.localId, name);

        return {
            accessToken: data.idToken,
            refreshToken: data.refreshToken
        };
    } catch (error) {
        throw errorConverter(error);
    }
}

// Update user's display name
// API Doc: https://firebase.google.com/docs/reference/rest/auth#section-update-profile
async function updateDisplayName(token, name) {
    try {
        const res = await fetch(`https://identitytoolkit.googleapis.com/v1/accounts:update?key=${apiKey}`, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({
                idToken: token,
                displayName: name,
                deleteAttribute: ["PHOTO_URL"],
                returnSecureToken: false
            })
        });
        const data = await res.json();

        if (!res.ok) {
            throw data.error;
        }
    } catch (error) {
        throw errorConverter(error);
    }
}

// Login user
// API Doc: https://firebase.google.com/docs/reference/rest/auth#section-sign-in-email-password
async function authenticate(email, password) {
    try {
        const res = await fetch(`https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?key=${apiKey}`, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({
                email: email,
                password: password,
                returnSecureToken: true
            })
        });
        data = await res.json();

        if (!res.ok) {
            throw data.error;
        }

        return {
            accessToken: data.idToken,
            refreshToken: data.refreshToken
        };
    } catch (error) {
        throw errorConverter(error);
    }
}

// Get new token pair with refresh token
// API Doc: https://firebase.google.com/docs/reference/rest/auth#section-refresh-token
async function refreshTokens(refreshToken) {
    try {
        const res = await fetch(`https://securetoken.googleapis.com/v1/token?key=${apiKey}`, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({
                grant_type: "refresh_token",
                refresh_token: refreshToken
            })
        });
        const data = await res.json();

        if (!res.ok) {
            throw data.error;
        }

        return {
            accessToken: data.id_token,
            refreshToken: data.refresh_token
        };
    } catch (error) {
        throw errorConverter(error);
    }
}

// Send reset password email
// https://firebase.google.com/docs/reference/rest/auth#section-send-password-reset-email
async function resetPasswordEmail(email) {
    try {
        const res = await fetch(`https://identitytoolkit.googleapis.com/v1/accounts:sendOobCode?key=${apiKey}`, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({
                requestType: "PASSWORD_RESET",
                email: email,
            })
        });
        const data = await res.json();

        if (!res.ok) {
            throw data.error;
        }

        return "Success";
    } catch (error) {
        throw errorConverter(error);
    }
}

// Verify access token and get UID
async function getUIDByToken(token) {
    try {
        const decodedToken = await getAuth().verifyIdToken(token);
        return decodedToken.uid;
    } catch (error) {
        error.message = "INVALID_ID_TOKEN";
        throw errorConverter(error);
    }
}

// Convert Firebase Auth Error Ids into readable errors
function errorConverter(error) {
    switch (error.message) {
        case "TOKEN_EXPIRED":
        case "INVALID_ID_TOKEN":
            return {
                code: error.message,
                message: "Invalid or expired token."
            };
        case "USER_NOT_FOUND":
        case "INVALID_LOGIN_CREDENTIALS":
            return {
                code: error.message,
                message: "No user found with the provided credentials."
            };
        case "INVALID_REFRESH_TOKEN":
            return {
                code: error.message,
                message: "The provided refresh token is invalid or has expired."
            };
        case "MISSING_REFRESH_TOKEN":
            return {
                code: error.message,
                message: "Missing refresh token."
            };
        case "EMAIL_EXISTS":
            return {
                code: error.message,
                message: "This email is already registered."
            };
        case "EMAIL_NOT_FOUND":
            return {
                code: error.message,
                message: "No account found with this email."
            };
        case "INVALID_PASSWORD":
            return {
                code: error.message,
                message: "The password you entered is incorrect."
            };
        case "EXPIRED_OOB_CODE":
            return {
                code: error.message,
                message: "Code is expired."
            };
        case "INVALID_OOB_CODE":
            return {
                code: error.message,
                message: "Code is invalid."
            };
        default:
            return {
                code: "UNKNOWN_ERROR",
                message: "An unexpected error occurred."
            };
    }
}

module.exports = {
    createUser,
    authenticate,
    refreshTokens,
    getUIDByToken,
    resetPasswordEmail
};