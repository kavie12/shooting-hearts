const admin = require("firebase-admin");
const serviceAccount = require("../firebase_service_account_keys.json");

let firebaseDB;
let firebaseAuth;

// Init firebase Admin SDK
function initFirebase() {
    admin.initializeApp({
        credential: admin.credential.cert(serviceAccount)
    });
    console.log("Firebase admin initialized");

    firebaseDB = admin.firestore();
    firebaseAuth = admin.auth();
}

function getFirestore() {
    return firebaseDB;
}

function getAuth() {
    return firebaseAuth;
}

module.exports = { initFirebase, getFirestore, getAuth };